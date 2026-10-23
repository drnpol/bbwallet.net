using Wallet.Net.Models.Records;
using Wallet.Net.Models.Categories;
using Wallet.Net.Models.Labels;
using Wallet.Net.Models.Budgets;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;
using Wallet.Net.Models;
using Wallet.Net.Models.Accounts;

namespace Wallet.Net.Tests.Unit;

public class ValidationTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(201)]
    public async Task Invalid_page_sizes_are_rejected_before_HTTP(int limit)
    {
        using var wallet=new OfflineWallet();
        await Assert.ThrowsAsync<ValidationException>(()=>wallet.Client.GetAccountsAsync(new AccountListOptions { Limit=limit }));
        Assert.Equal(0,wallet.Handler.Calls);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(201)]
    public void Invalid_configured_page_sizes_fail_during_resolution(int limit)
        => Assert.Throws<OptionsValidationException>(()=>new OfflineWallet(pageSize:limit));

    [Theory]
    [InlineData("")]
    [InlineData("0days")]
    [InlineData("367days")]
    [InlineData("54weeks")]
    [InlineData("14months")]
    [InlineData("30days\n")]
    [InlineData("999999999999days")]
    public async Task Invalid_usage_periods_are_rejected_before_HTTP(string period)
    {
        using var wallet=new OfflineWallet();
        await Assert.ThrowsAnyAsync<ArgumentException>(()=>wallet.Client.GetApiUsageStatsAsync(period));
        Assert.Equal(0,wallet.Handler.Calls);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(21)]
    public async Task Reference_limits_are_enforced(int count)
    {
        using var wallet=new OfflineWallet();
        await Assert.ThrowsAsync<ArgumentException>(()=>wallet.Client.GetReferencesAsync(ReferenceType.Accounts,Enumerable.Repeat("id",count).ToArray()));
        Assert.Equal(0,wallet.Handler.Calls);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(11)]
    public async Task Patch_and_delete_batch_limits_are_enforced(int count)
    {
        using var wallet=new OfflineWallet();
        await Assert.ThrowsAsync<ArgumentException>(()=>wallet.Client.PatchAccountsAsync(Enumerable.Repeat(new PatchAccountItem { Id="a" },count).ToArray()));
        await Assert.ThrowsAsync<ArgumentException>(()=>wallet.Client.PatchBudgetsAsync(Enumerable.Repeat(new PatchBudgetRequest { Id="a" },count).ToArray()));
        await Assert.ThrowsAsync<ArgumentException>(()=>wallet.Client.PatchCategoriesAsync(Enumerable.Repeat(new PatchCategoryRequest { Id="a" },count).ToArray()));
        await Assert.ThrowsAsync<ArgumentException>(()=>wallet.Client.PatchLabelsAsync(Enumerable.Repeat(new PatchLabelItem { Id="a" },count).ToArray()));
        await Assert.ThrowsAsync<ArgumentException>(()=>wallet.Client.PatchRecordsAsync(Enumerable.Repeat(new PatchRecordItem { Id="a" },count).ToArray()));
        await Assert.ThrowsAsync<ArgumentException>(()=>wallet.Client.DeleteAsync(DeleteEntityType.Records,Enumerable.Repeat("a",count).ToArray()));
        Assert.Equal(0,wallet.Handler.Calls);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(51)]
    public async Task Record_creation_batch_limits_are_enforced(int count)
    {
        using var wallet=new OfflineWallet();
        await Assert.ThrowsAsync<ArgumentException>(()=>wallet.Client.CreateRecordsAsync(Enumerable.Repeat(OperationCases.Record(),count).ToArray()));
        Assert.Equal(0,wallet.Handler.Calls);
    }

    [Fact]
    public async Task Maximum_batch_sizes_are_accepted()
    {
        using var wallet=new OfflineWallet(OperationCases.Batch);
        await wallet.Client.CreateRecordsAsync(Enumerable.Repeat(OperationCases.Record(),50).ToArray());
        Assert.Equal(50,((Newtonsoft.Json.Linq.JArray)wallet.Handler.BodyJson).Count);
        await wallet.Client.PatchRecordsAsync(Enumerable.Repeat(new PatchRecordItem { Id="a" },10).ToArray());
        await wallet.Client.DeleteAsync(DeleteEntityType.Records,Enumerable.Repeat("a",10).ToArray());
        Assert.Equal(3,wallet.Handler.Calls);
    }

    [Fact]
    public async Task Null_requests_and_invalid_identifiers_are_rejected()
    {
        using var wallet=new OfflineWallet();
        await Assert.ThrowsAsync<ArgumentNullException>(()=>wallet.Client.CreateAccountAsync(null!));
        await Assert.ThrowsAsync<ArgumentNullException>(()=>wallet.Client.CreateRecordsAsync(null!));
        await Assert.ThrowsAsync<ArgumentNullException>(()=>wallet.Client.PatchAccountsAsync(null!));
        await Assert.ThrowsAsync<ArgumentException>(()=>wallet.Client.PatchAccountsAsync([null!]));
        await Assert.ThrowsAsync<ValidationException>(()=>wallet.Client.PatchAccountsAsync([new PatchAccountItem { Id=" " }]));
        await Assert.ThrowsAsync<ArgumentException>(()=>wallet.Client.DeleteAsync(DeleteEntityType.Accounts,[""]));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(()=>wallet.Client.DeleteAsync((DeleteEntityType)999,["a"]));
        Assert.Equal(0,wallet.Handler.Calls);
    }

    [Fact]
    public async Task Local_write_constraints_are_validated()
    {
        using var wallet=new OfflineWallet();
        await Assert.ThrowsAsync<ValidationException>(()=>wallet.Client.CreateLabelAsync(new CreateLabelRequest { Name=new string('x',81) }));
        await Assert.ThrowsAsync<ValidationException>(()=>wallet.Client.CreateLabelAsync(new CreateLabelRequest { Name="x", Color="invalid" }));
        await Assert.ThrowsAsync<ValidationException>(()=>wallet.Client.CreateRecordsAsync([OperationCases.Record() with { Amount=new() { Value=0 } }]));
        await Assert.ThrowsAsync<ValidationException>(()=>wallet.Client.CreateRecordsAsync([OperationCases.Record() with { RecordDate=DateTimeOffset.UtcNow.AddDays(3) }]));
        await Assert.ThrowsAsync<ValidationException>(()=>wallet.Client.CreateRecordsAsync([OperationCases.Record() with { Place=new() { Latitude=91 } }]));
        await Assert.ThrowsAsync<ValidationException>(()=>wallet.Client.CreateBudgetAsync(new CreateBudgetRequest { Name="x", CurrencyCode="USD", Type="BUDGET_CUSTOM", Limit=10 }));
        await Assert.ThrowsAsync<ValidationException>(()=>wallet.Client.PatchRecordsAsync([new PatchRecordItem { Id="a", Clear=["note"], Note="conflict" }]));
        await Assert.ThrowsAsync<ValidationException>(()=>wallet.Client.PatchCategoriesAsync([new PatchCategoryRequest { Id="a", Reset=["name"], Name="conflict" }]));
        await Assert.ThrowsAsync<ValidationException>(()=>wallet.Client.PatchRecordsAsync([new PatchRecordItem { Id="a", Clear=["invalid"] }]));
        await Assert.ThrowsAsync<ValidationException>(()=>wallet.Client.PatchRecordsAsync([new PatchRecordItem { Id="a" }],new RecordPatchOptions { Validation="invalid" }));
        await Assert.ThrowsAsync<ValidationException>(()=>wallet.Client.PatchBudgetsAsync([new PatchBudgetRequest { Id="a", LimitOverrides=[new() { Period="2026-10", Limit=1, SetBaseline=true }] }]));
        Assert.Equal(0,wallet.Handler.Calls);
    }

    [Fact]
    public async Task Transfers_require_their_conditional_fields_and_opposite_amount_sign()
    {
        using var wallet=new OfflineWallet();
        await Assert.ThrowsAsync<ValidationException>(()=>wallet.Client.CreateRecordsAsync([OperationCases.Record() with { Transfer=new() { PairingMode="new" } }]));
        await Assert.ThrowsAsync<ValidationException>(()=>wallet.Client.CreateRecordsAsync([OperationCases.Record() with { Transfer=new() { PairingMode="existing" } }]));
        await Assert.ThrowsAsync<ValidationException>(()=>wallet.Client.CreateRecordsAsync([OperationCases.Record() with { Transfer=new() { PairingMode="new", AccountId="b", CounterAmount=new() { Value=-1, CurrencyCode="EUR" } } }]));
        Assert.Equal(0,wallet.Handler.Calls);
    }

    [Theory]
    [InlineData("new")]
    [InlineData("existing")]
    [InlineData("unpaired")]
    public async Task Transfer_modes_and_mirror_IDs_round_trip(string mode)
    {
        using var wallet=new OfflineWallet("{\"results\":[{\"inputIndex\":0,\"id\":\"a\",\"success\":true,\"createdMirrorRecordId\":\"b\"}],\"agentHints\":[{\"type\":\"transfer.mirror_failed\",\"text\":\"Mirror warning\"}],"+OperationCases.Summary+"}");
        var result=await wallet.Client.CreateRecordsAsync([OperationCases.Record() with { Transfer=new() { PairingMode=mode, AccountId=mode=="new"?"b":null, RecordId=mode=="existing"?"b":null } }],new WriteOptions { ReturnData=false });
        Assert.Equal(mode,(string?)wallet.Handler.BodyJson[0]!["transfer"]!["pairingMode"]);
        Assert.Equal("b",Assert.Single(result.Results!).CreatedMirrorRecordId);
        Assert.Null(result.Results![0].Record);
        Assert.Equal("transfer.mirror_failed",Assert.Single(result.AgentHints!).Type);
    }
}
