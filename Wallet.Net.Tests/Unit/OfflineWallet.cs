using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace Wallet.Net.Tests.Unit;

/// <summary>Runs the production DI registration and HTTP serialization entirely in memory.</summary>
internal sealed class OfflineWallet : IDisposable
{
    private readonly ServiceProvider _provider;
    public CapturingHandler Handler { get; } = new();
    public IWalletClient Client { get; }

    public OfflineWallet(string json = "{}", HttpStatusCode status = HttpStatusCode.OK, int pageSize = 200)
    {
        Handler.Json = json;
        Handler.Status = status;
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["BudgetBakers:Wallet:BaseUrl"] = "https://example.invalid/wallet",
            ["BudgetBakers:Wallet:AccessToken"] = "synthetic-unit-test-token",
            ["BudgetBakers:Wallet:DefaultPageSize"] = pageSize.ToString(System.Globalization.CultureInfo.InvariantCulture)
        }).Build();
        var services = new ServiceCollection();
        services.AddWalletClient(configuration);
        services.AddHttpClient("Wallet.Net").ConfigurePrimaryHttpMessageHandler(() => Handler);
        _provider = services.BuildServiceProvider();
        Client = _provider.GetRequiredService<IWalletClient>();
    }

    public void Dispose() => _provider.Dispose();
}

internal sealed class CapturingHandler : HttpMessageHandler
{
    public string Json { get; set; } = "{}";
    public string ContentType { get; set; } = "application/json";
    public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
    public Dictionary<string, string> ResponseHeaders { get; } = new(StringComparer.OrdinalIgnoreCase);
    public int Calls { get; private set; }
    public Uri? Uri { get; private set; }
    public HttpMethod? Method { get; private set; }
    public string? Authorization { get; private set; }
    public string? Body { get; private set; }
    public string? RequestContentType { get; private set; }
    public bool WasCancelable { get; private set; }
    public Func<CancellationToken, Task>? BeforeResponse { get; set; }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Calls++;
        Uri = request.RequestUri;
        Method = request.Method;
        Authorization = request.Headers.Authorization?.ToString();
        Body = request.Content is null ? null : await request.Content.ReadAsStringAsync(cancellationToken);
        RequestContentType = request.Content?.Headers.ContentType?.MediaType;
        WasCancelable = cancellationToken.CanBeCanceled;
        if (BeforeResponse is not null) await BeforeResponse(cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
        var response = new HttpResponseMessage(Status) { Content = new StringContent(Json, Encoding.UTF8, ContentType) };
        foreach (var header in ResponseHeaders) response.Headers.TryAddWithoutValidation(header.Key, header.Value);
        return response;
    }

    public IReadOnlyList<string> Query(string name) => (Uri?.Query.TrimStart('?') ?? "")
        .Split('&', StringSplitOptions.RemoveEmptyEntries)
        .Select(pair => pair.Split('=', 2))
        .Where(pair => System.Uri.UnescapeDataString(pair[0]) == name)
        .Select(pair => pair.Length == 2 ? System.Uri.UnescapeDataString(pair[1]) : "").ToArray();

    public JToken BodyJson => JToken.Parse(Body ?? throw new InvalidOperationException("No request body captured."));
}
