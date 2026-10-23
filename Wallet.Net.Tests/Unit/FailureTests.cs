using Wallet.Net.Models.Labels;
using System.Net;
using Wallet.Net.Exceptions;

namespace Wallet.Net.Tests.Unit;

public class FailureTests
{
    [Theory]
    [InlineData("PREMIUM_REQUIRED")]
    [InlineData("API_CONSENT_REQUIRED")]
    [InlineData("API_CONSENT_AND_PREMIUM_REQUIRED")]
    public async Task Entitlement_codes_are_distinguished_from_invalid_credentials(string code)
    {
        using var wallet = new OfflineWallet($"{{\"error\":\"Access disabled\",\"code\":\"{code}\"}}",HttpStatusCode.Forbidden);
        var error=await Assert.ThrowsAsync<WalletAuthenticationException>(()=>wallet.Client.GetAccountsAsync());
        Assert.Equal(code,error.Code);
        Assert.Equal("Access disabled",error.Message);
        Assert.Equal(HttpStatusCode.Forbidden,error.StatusCode);
        Assert.Equal(1,wallet.Handler.Calls);
    }

    [Fact]
    public async Task Plain_text_forbidden_preserves_the_server_explanation()
    {
        using var wallet = new OfflineWallet("Forbidden: this token belongs to the Board app.",HttpStatusCode.Forbidden);
        wallet.Handler.ContentType="text/plain";
        var error=await Assert.ThrowsAsync<WalletAuthenticationException>(()=>wallet.Client.GetAccountsAsync());
        Assert.Null(error.Code);
        Assert.Contains("Board app",error.Message);
        Assert.Equal(wallet.Handler.Json,error.ResponseContent);
    }

    [Theory]
    [InlineData(401,typeof(WalletAuthenticationException))]
    [InlineData(400,typeof(WalletValidationException))]
    [InlineData(422,typeof(WalletValidationException))]
    [InlineData(429,typeof(WalletRateLimitException))]
    [InlineData(500,typeof(WalletApiException))]
    public async Task Ordinary_errors_keep_typed_exceptions_and_metadata(int status,Type type)
    {
        using var wallet = new OfflineWallet("{\"error\":\"Server explanation\"}",(HttpStatusCode)status);
        wallet.Handler.ResponseHeaders["Retry-After"]="60";
        wallet.Handler.ResponseHeaders["X-RateLimit-Limit"]="500";
        wallet.Handler.ResponseHeaders["X-RateLimit-Remaining"]="0";
        var error=await Assert.ThrowsAsync(type,()=>wallet.Client.GetAccountsAsync());
        var api=Assert.IsAssignableFrom<WalletApiException>(error);
        Assert.Equal("Server explanation",api.Message);
        Assert.Equal(60,api.Metadata.RetryAfterSeconds);
        Assert.Equal(0,api.Metadata.RateLimitRemaining);
        Assert.Equal((HttpStatusCode)status,api.Metadata.StatusCode);
        Assert.Equal(1,wallet.Handler.Calls);
    }

    public static IEnumerable<object[]> Batches() => OperationCases.All()
        .Where(x=>((OperationCase)x[0]).Id.StartsWith("patch",StringComparison.Ordinal) || ((OperationCase)x[0]).Id is "createRecords" or "deleteByType");

    [Theory]
    [MemberData(nameof(Batches))]
    public async Task Every_batch_operation_returns_partial_success_and_preserves_failed_batches(OperationCase operation)
    {
        const string body="{\"results\":[{\"id\":\"a\",\"inputIndex\":0,\"success\":true},{\"id\":\"b\",\"inputIndex\":1,\"success\":false,\"error\":\"Invalid\",\"errorType\":\"client_error\",\"fields\":[\"name\"]}],\"summary\":{\"total\":2,\"succeeded\":1,\"clientErrors\":1,\"serverErrors\":0,\"documentsWritten\":3}}";
        using var wallet = new OfflineWallet(body,HttpStatusCode.MultiStatus);
        var result=await operation.Call(wallet.Client,CancellationToken.None);
        var json=Newtonsoft.Json.Linq.JObject.FromObject(result);
        Assert.Equal(2,((Newtonsoft.Json.Linq.JArray)json["Results"]!).Count);
        Assert.Equal(3,(int)json["Summary"]!["DocumentsWritten"]!);
        foreach(var status in new[]{HttpStatusCode.BadRequest,HttpStatusCode.InternalServerError})
        {
            wallet.Handler.Status=status;
            var error=await Assert.ThrowsAnyAsync<WalletApiException>(()=>operation.Call(wallet.Client,CancellationToken.None));
            Assert.Equal(typeof(WalletBatchException<>),error.GetType().GetGenericTypeDefinition());
            Assert.Equal(status,error.StatusCode);
            var retained=error.GetType().GetProperty("Result")!.GetValue(error)!;
            Assert.Equal(result.GetType(),retained.GetType());
            Assert.Equal(body,error.ResponseContent);
            var results=Newtonsoft.Json.Linq.JObject.FromObject(retained);
            Assert.Equal("client_error",(string?)results["Results"]![1]!["ErrorType"]);
            Assert.Equal(1,(int)results["Results"]![1]!["InputIndex"]!);
        }
        Assert.Equal(3,wallet.Handler.Calls);
    }

    [Fact]
    public async Task Normal_error_envelope_is_not_mistaken_for_a_batch_result()
    {
        using var wallet = new OfflineWallet("{\"error\":\"Malformed batch\"}",HttpStatusCode.BadRequest);
        var error=await Assert.ThrowsAsync<WalletValidationException>(()=>wallet.Client.PatchLabelsAsync([new PatchLabelItem { Id="a" }]));
        Assert.Equal("Malformed batch",error.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("null")]
    [InlineData("invalid-json")]
    [InlineData("{\"accounts\":42}")]
    public async Task Invalid_success_body_is_a_protocol_error(string body)
    {
        using var wallet = new OfflineWallet(body);
        var error=await Assert.ThrowsAsync<WalletApiException>(()=>wallet.Client.GetAccountsAsync());
        Assert.NotNull(error.InnerException);
        Assert.Equal(body,error.ResponseContent);
    }

    [Theory]
    [MemberData(nameof(OperationCases.All), MemberType=typeof(OperationCases))]
    public async Task Cancellation_before_send_never_reaches_HTTP(OperationCase operation)
    {
        using var wallet = new OfflineWallet(operation.Response);
        using var cancellation=new CancellationTokenSource();
        cancellation.Cancel();
        await Assert.ThrowsAnyAsync<OperationCanceledException>(()=>operation.Call(wallet.Client,cancellation.Token));
        Assert.Equal(0,wallet.Handler.Calls);
    }

    [Fact]
    public async Task Cancellation_during_HTTP_is_not_wrapped_in_an_API_exception()
    {
        using var wallet = new OfflineWallet();
        using var cancellation=new CancellationTokenSource();
        var entered=new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        wallet.Handler.BeforeResponse=async token=> { entered.SetResult(); await Task.Delay(Timeout.Infinite,token); };
        var task=wallet.Client.GetAccountsAsync(cancellationToken:cancellation.Token);
        await entered.Task.WaitAsync(TimeSpan.FromSeconds(10));
        cancellation.Cancel();
        await Assert.ThrowsAnyAsync<OperationCanceledException>(()=>task);
        Assert.Equal(1,wallet.Handler.Calls);
    }

    [Fact]
    public async Task Transport_failures_retain_their_cause_and_are_not_retried()
    {
        using var wallet = new OfflineWallet();
        wallet.Handler.BeforeResponse=_=>throw new HttpRequestException("Synthetic connection failure");
        var error=await Assert.ThrowsAsync<WalletApiException>(()=>wallet.Client.CreateLabelAsync(new CreateLabelRequest { Name="label" }));
        Assert.NotNull(error.InnerException);
        Assert.Equal(1,wallet.Handler.Calls);
    }
}
