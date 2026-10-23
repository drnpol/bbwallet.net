using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Wallet.Net.Exceptions;
using Wallet.Net.Http;
using Wallet.Net.Models;
using Wallet.Net.Models.Accounts;
using Wallet.Net.Models.ApiUsage;
using Wallet.Net.Models.Records;

namespace Wallet.Net.Tests.Unit;

public class OperationTests
{
    [Theory]
    [MemberData(nameof(OperationCases.All), MemberType = typeof(OperationCases))]
    public async Task Every_operation_sends_the_documented_request(OperationCase operation)
    {
        using var wallet = new OfflineWallet(operation.Response, operation.Status);
        wallet.Handler.ResponseHeaders["X-RateLimit-Remaining"] = "499";

        var result = await operation.Call(wallet.Client, CancellationToken.None);

        Assert.Equal(operation.Method, wallet.Handler.Method!.Method);
        Assert.Equal("https://example.invalid/wallet" + operation.Path, wallet.Handler.Uri!.GetLeftPart(UriPartial.Path));
        Assert.Equal("Bearer synthetic-unit-test-token", wallet.Handler.Authorization);
        Assert.Equal(1, wallet.Handler.Calls);
        if (operation.ExpectedBody is not null)
        {
            Assert.Equal("application/json", wallet.Handler.RequestContentType);
            AssertJsonEquivalent(JToken.Parse(operation.ExpectedBody), wallet.Handler.BodyJson);
            AssertRequestMatchesSchema(operation, wallet.Handler.BodyJson);
        }
        else Assert.Null(wallet.Handler.Body);

        if (result is WalletResponse response)
        {
            Assert.Equal(operation.Status, response.Metadata.StatusCode);
            Assert.Equal(499, response.Metadata.RateLimitRemaining);
        }
        if (result.GetType().GetProperty("Items")?.GetValue(result) is System.Collections.IEnumerable items)
            Assert.Single(items.Cast<object>());
        else
        {
            var actual = JToken.FromObject(result, JsonSerializer.Create(WalletRequestExecutor.CreateSerializerSettings()));
            AssertJsonSubset(JToken.Parse(operation.Response), actual);
        }
        AssertQueryMatchesSchema(operation, wallet.Handler);
        if (operation.Id is "createRecords" or "patchAccounts" or "patchRecords")
            Assert.Equal("false", Assert.Single(wallet.Handler.Query("returnData")));
        if (operation.Id == "patchRecords") Assert.Equal("strict", Assert.Single(wallet.Handler.Query("validation")));
    }

    [Fact]
    public void Operation_cases_cover_the_entire_checked_in_contract()
    {
        var schema = Schema();
        var ids = ((JObject)schema["paths"]!).Properties().SelectMany(p => ((JObject)p.Value).Properties())
            .Where(p => p.Name is "get" or "post" or "patch" or "delete" or "put")
            .Select(p => (string)p.Value["operationId"]!).Order().ToArray();
        Assert.Equal(22, ids.Length);
        Assert.Equal(ids, OperationCases.All().Select(x => ((OperationCase)x[0]).Id).Order().ToArray());
        var methods = typeof(IWalletClient).GetInterfaces().Append(typeof(IWalletClient)).SelectMany(t => t.GetMethods()).Select(m => m.Name).ToHashSet();
        foreach (var id in ids) Assert.Contains(id == "deleteByType" ? "DeleteAsync" : char.ToUpperInvariant(id[0]) + id[1..] + "Async", methods);
    }

    [Fact]
    public async Task Pagination_and_record_context_survive_normalization()
    {
        using var wallet = new OfflineWallet("{\"limit\":20,\"offset\":10,\"nextOffset\":30,\"total\":40,\"records\":[],\"agentHints\":[{\"type\":\"pagination\",\"text\":\"More\"}],\"appliedRecordDateFilters\":[\"gte.2025-01-01\"]}");
        var page = await wallet.Client.GetRecordsAsync(new RecordListOptions { Limit=20, Offset=10, WithTotal=true });
        Assert.Equal(20,page.Limit);
        Assert.Equal(10,page.Offset);
        Assert.Equal(30,page.NextOffset);
        Assert.Equal(40,page.Total);
        Assert.Equal("More",Assert.Single(page.AgentHints).Text);
        Assert.Equal("gte.2025-01-01",Assert.Single(page.AppliedRecordDateFilters));
    }

    [Fact]
    public async Task Embedded_custom_category_parent_is_preserved()
    {
        using var wallet = new OfflineWallet("{\"records\":[{\"id\":\"record\",\"category\":{\"id\":\"child\",\"parentId\":\"parent\"}}]}");
        Assert.Equal("parent",Assert.Single((await wallet.Client.GetRecordsAsync()).Items).Category!.ParentId);
    }

    [Fact]
    public async Task Missing_totals_and_metadata_remain_null_and_page_default_is_used()
    {
        using var wallet = new OfflineWallet("{\"accounts\":[]}", pageSize:47);
        var page = await wallet.Client.GetAccountsAsync();
        Assert.Null(page.Total);
        Assert.Null(page.NextOffset);
        Assert.Null(page.Metadata.RateLimit);
        Assert.Equal("47",Assert.Single(wallet.Handler.Query("limit")));
        await wallet.Client.GetAccountsAsync(new AccountListOptions { Limit=3, Name="eq-i.Cash & Bank", Archived=false });
        Assert.Equal("3",Assert.Single(wallet.Handler.Query("limit")));
        Assert.Equal("eq-i.Cash & Bank",Assert.Single(wallet.Handler.Query("name")));
        Assert.Equal("false",Assert.Single(wallet.Handler.Query("archived")));
        Assert.DoesNotContain("accountType",wallet.Handler.Uri!.Query);
    }

    [Theory]
    [InlineData(ReferenceType.Accounts,"accounts")]
    [InlineData(ReferenceType.Categories,"categories")]
    [InlineData(ReferenceType.Labels,"labels")]
    [InlineData(ReferenceType.Records,"records")]
    public async Task References_support_each_type_and_preserve_metadata(ReferenceType type, string path)
    {
        using var wallet = new OfflineWallet("{\"a\":{\"error\":\"not_found\",\"message\":\"Missing\"}}");
        wallet.Handler.ResponseHeaders["X-RateLimit-Limit"]="500";
        var result = await wallet.Client.GetReferencesWithMetadataAsync(type,["a","b"]);
        Assert.Equal("/wallet/v1/api/"+path+"/references",wallet.Handler.Uri!.AbsolutePath);
        Assert.Equal("a,b",Assert.Single(wallet.Handler.Query("id")));
        Assert.Equal("not_found",result.Data["a"].Error);
        Assert.Equal(500,result.Metadata.RateLimit);
        Assert.Empty(wallet.Handler.Query("limit"));
    }

    [Theory]
    [InlineData(DeleteEntityType.Records,"records")]
    [InlineData(DeleteEntityType.Budgets,"budgets")]
    [InlineData(DeleteEntityType.StandingOrders,"standing-orders")]
    [InlineData(DeleteEntityType.RecordRules,"record-rules")]
    [InlineData(DeleteEntityType.Categories,"categories")]
    [InlineData(DeleteEntityType.Accounts,"accounts")]
    [InlineData(DeleteEntityType.Labels,"labels")]
    public async Task Deletion_supports_only_Wallet_types(DeleteEntityType type, string path)
    {
        using var wallet = new OfflineWallet(OperationCases.Batch);
        await wallet.Client.DeleteAsync(type,["a","b"]);
        Assert.Equal("DELETE",wallet.Handler.Method!.Method);
        Assert.Equal("/wallet/v1/api/"+path,wallet.Handler.Uri!.AbsolutePath);
        AssertJsonEquivalent(JToken.Parse("{\"ids\":[\"a\",\"b\"]}"),wallet.Handler.BodyJson);
    }

    [Theory]
    [InlineData("366days")]
    [InlineData("53weeks")]
    [InlineData("13months")]
    [InlineData("1days")]
    public async Task Usage_accepts_documented_period_boundaries(string period)
    {
        using var wallet = new OfflineWallet("{\"total\":0,\"usage\":[]}");
        await wallet.Client.GetApiUsageStatsAsync(period);
        Assert.Equal(period,Assert.Single(wallet.Handler.Query("period")));
        Assert.Empty(wallet.Handler.Query("limit"));
    }

    internal static JObject Schema() => JObject.Parse(File.ReadAllText(Path.Combine(AppContext.BaseDirectory,"Fixtures","openapi.json")));

    internal static void AssertJsonEquivalent(JToken expected, JToken actual)
    {
        AssertJsonSubset(expected,actual);
        AssertJsonSubset(actual,expected);
    }

    private static void AssertJsonSubset(JToken expected, JToken actual)
    {
        if (expected is JObject obj)
        {
            Assert.IsType<JObject>(actual);
            foreach(var p in obj.Properties())
            {
                Assert.NotNull(actual[p.Name]);
                AssertJsonSubset(p.Value,actual[p.Name]!);
            }
        }
        else if(expected is JArray array)
        {
            var other=Assert.IsType<JArray>(actual);
            Assert.Equal(array.Count,other.Count);
            for(var i=0;i<array.Count;i++) AssertJsonSubset(array[i],other[i]);
        }
        else if(expected.Type is JTokenType.Integer or JTokenType.Float)
            Assert.Equal(expected.Value<decimal>(),actual.Value<decimal>());
        else Assert.True(JToken.DeepEquals(expected,actual),$"Expected {expected}, got {actual}");
    }

    private static JToken OperationSchema(OperationCase operation)
    {
        var schema=Schema();
        var path=operation.Id switch { "getReferences"=>"/v1/api/{type}/references", "deleteByType"=>"/v1/api/{type}", _=>operation.Path };
        return schema["paths"]![path]![operation.Method.ToLowerInvariant()]!;
    }

    private static void AssertQueryMatchesSchema(OperationCase operation,CapturingHandler handler)
    {
        var parameters=OperationSchema(operation)["parameters"] as JArray ?? [];
        var names=parameters.Where(p=>(string?)p["in"]=="query").Select(p=>(string)p["name"]!).ToHashSet();
        foreach(var pair in handler.Uri!.Query.TrimStart('?').Split('&',StringSplitOptions.RemoveEmptyEntries))
            Assert.Contains(Uri.UnescapeDataString(pair.Split('=')[0]),names);
        foreach(var parameter in parameters.Where(p=>(string?)p["in"]=="query" && (bool?)p["required"]==true))
            Assert.NotEmpty(handler.Query((string)parameter["name"]!));
    }

    private static void AssertRequestMatchesSchema(OperationCase operation,JToken body)
    {
        var schema=Schema();
        var requestSchema=OperationSchema(operation)["requestBody"]!["content"]!["application/json"]!["schema"]!;
        CheckShape(requestSchema,body,schema);
    }

    // Targeted request-contract assertions, not a general JSON Schema implementation.
    private static void CheckShape(JToken shape,JToken body,JObject root)
    {
        if(shape["$ref"] is { } reference)
        { CheckShape(root.SelectToken(((string)reference)![2..].Replace('/','.'))!,body,root); return; }
        if(shape["anyOf"] is JArray alternatives)
        {
            var branch=alternatives.Single(s=>(string?)s["type"]==(body is JArray?"array":"object"));
            CheckShape(branch,body,root); return;
        }
        if(body is JObject obj)
        {
            Assert.Equal("object",(string?)shape["type"]);
            foreach(var required in shape["required"] as JArray ?? []) Assert.NotNull(obj[(string)required!]);
            foreach(var property in obj.Properties())
            {
                Assert.NotNull(shape["properties"]?[property.Name]);
                CheckShape(shape["properties"]![property.Name]!,property.Value,root);
            }
        }
        else if(body is JArray array)
        {
            Assert.Equal("array",(string?)shape["type"]);
            Assert.InRange(array.Count,(int?)shape["minItems"]??0,(int?)shape["maxItems"]??int.MaxValue);
            foreach(var item in array) CheckShape(shape["items"]!,item,root);
        }
        else
        {
            Assert.True(((string?)shape["type"]) switch
            {
                "string"=>body.Type is JTokenType.String or JTokenType.Date,
                "number"=>body.Type is JTokenType.Float or JTokenType.Integer,
                "integer"=>body.Type==JTokenType.Integer,
                "boolean"=>body.Type==JTokenType.Boolean,
                _=>false
            });
            if(shape["enum"] is JArray values) Assert.Contains(values,v=>JToken.DeepEquals(v,body));
        }
    }
}
