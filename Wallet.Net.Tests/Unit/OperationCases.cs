using System.Net;
using Wallet.Net.Models;
using Wallet.Net.Models.Accounts;
using Wallet.Net.Models.Budgets;
using Wallet.Net.Models.Categories;
using Wallet.Net.Models.Goals;
using Wallet.Net.Models.Labels;
using Wallet.Net.Models.RecordRules;
using Wallet.Net.Models.Records;
using Wallet.Net.Models.StandingOrders;

namespace Wallet.Net.Tests.Unit;

public sealed record OperationCase(string Id, string Method, string Path, string Response,
    Func<IWalletClient, CancellationToken, Task<object>> Call, string? ExpectedBody = null,
    HttpStatusCode Status = HttpStatusCode.OK)
{
    public override string ToString() => Id;
}

public static class OperationCases
{
    public const string Summary = "\"summary\":{\"total\":1,\"succeeded\":1,\"clientErrors\":0,\"serverErrors\":0,\"documentsWritten\":1}";
    public const string Batch = "{\"results\":[{\"id\":\"entity-1\",\"inputIndex\":0,\"success\":true}]," + Summary + "}";

    private static async Task<object> Box<T>(Task<T> task) => (await task)!;
    public static CreateRecordRequest Record() => new()
    {
        AccountId = "account-1", Amount = new() { Value = -42.50m }, RecordDate = DateTimeOffset.Parse("2025-03-15T14:30:00Z")
    };

    public static IEnumerable<object[]> All()
    {
        yield return [new OperationCase("getAccounts", "GET", "/v1/api/accounts", "{\"accounts\":[{\"id\":\"entity-1\",\"name\":\"Cash\"}],\"limit\":20,\"offset\":0,\"nextOffset\":20,\"total\":25}", (c,t)=>Box(c.GetAccountsAsync(new AccountListOptions { Name = "eq-i.Cash", Limit = 20, WithTotal = true },t)))];
        yield return [new OperationCase("getBudgets", "GET", "/v1/api/budgets", "{\"budgets\":[{\"id\":\"entity-1\"}]}", (c,t)=>Box(c.GetBudgetsAsync(new BudgetListOptions { Spending = "current" },t)))];
        yield return [new OperationCase("getCategories", "GET", "/v1/api/categories", "{\"categories\":[{\"id\":\"entity-1\"}]}", (c,t)=>Box(c.GetCategoriesAsync(new CategoryListOptions { CustomCategory = true },t)))];
        yield return [new OperationCase("getGoals", "GET", "/v1/api/goals", "{\"goals\":[{\"id\":\"entity-1\"}]}", (c,t)=>Box(c.GetGoalsAsync(new GoalListOptions { Note = "contains-i.trip" },t)))];
        yield return [new OperationCase("getLabels", "GET", "/v1/api/labels", "{\"labels\":[{\"id\":\"entity-1\"}]}", (c,t)=>Box(c.GetLabelsAsync(new LabelListOptions { RecordId = "record-1" },t)))];
        yield return [new OperationCase("getRecordRules", "GET", "/v1/api/record-rules", "{\"recordRules\":[{\"id\":\"entity-1\"}]}", (c,t)=>Box(c.GetRecordRulesAsync(new RecordRuleListOptions { LabelId = "label-1" },t)))];
        yield return [new OperationCase("getRecords", "GET", "/v1/api/records", "{\"records\":[{\"id\":\"entity-1\",\"category\":{\"id\":\"child\",\"parentId\":\"parent\"}}],\"appliedRecordDateFilters\":[\"gte.2025-01-01\"]}", (c,t)=>Box(c.GetRecordsAsync(new RecordListOptions { RecordDate = "gte.2025-01-01", SortBy = "+recordDate" },t)))];
        yield return [new OperationCase("getStandingOrders", "GET", "/v1/api/standing-orders", "{\"standingOrders\":[{\"id\":\"entity-1\"}]}", (c,t)=>Box(c.GetStandingOrdersAsync(new StandingOrderListOptions { CurrencyCode = "USD" },t)))];
        yield return [new OperationCase("getStandingOrderItems", "GET", "/v1/api/standing-orders/items", "{\"standingOrderItems\":[{\"id\":\"entity-1\"}]}", (c,t)=>Box(c.GetStandingOrderItemsAsync(new StandingOrderItemListOptions { Dismissed = false },t)))];
        yield return [new OperationCase("getReferences", "GET", "/v1/api/categories/references", "{\"entity-1\":{\"records\":{\"ids\":[\"record-1\"],\"total\":1,\"limit\":50,\"hasMore\":false}}}", (c,t)=>Box(c.GetReferencesAsync(ReferenceType.Categories,["entity-1"],t)))];
        yield return [new OperationCase("getApiUsageStats", "GET", "/v1/api/api-usage/stats", "{\"period\":\"30days\",\"granularity\":\"daily\",\"from\":\"2025-01-01T00:00:00Z\",\"to\":\"2025-01-31T00:00:00Z\",\"total\":7,\"usage\":[{\"from\":\"2025-01-01T00:00:00Z\",\"to\":\"2025-01-02T00:00:00Z\",\"total\":7}]}", (c,t)=>Box(c.GetApiUsageStatsAsync("30days",t)))];
        yield return [new OperationCase("createAccount", "POST", "/v1/api/accounts", "{\"account\":{\"id\":\"entity-1\"},"+Summary+"}", (c,t)=>Box(c.CreateAccountAsync(new CreateAccountRequest { Name="Cash", AccountType="Cash", CurrencyCode="USD", InitialBalance=0 },t)), "{\"name\":\"Cash\",\"accountType\":\"Cash\",\"currencyCode\":\"USD\",\"initialBalance\":0}", HttpStatusCode.Created)];
        yield return [new OperationCase("createBudget", "POST", "/v1/api/budgets", "{\"budget\":{\"id\":\"entity-1\"},"+Summary+"}", (c,t)=>Box(c.CreateBudgetAsync(new CreateBudgetRequest { Name="Food", Type="BUDGET_CUSTOM", CurrencyCode="USD", Limit=300m, StartDate=new(2025,1,1), EndDate=new(2025,1,31) },t)), "{\"name\":\"Food\",\"type\":\"BUDGET_CUSTOM\",\"currencyCode\":\"USD\",\"limit\":300,\"startDate\":\"2025-01-01\",\"endDate\":\"2025-01-31\"}", HttpStatusCode.Created)];
        yield return [new OperationCase("createCategory", "POST", "/v1/api/categories/custom", "{\"category\":{\"id\":\"entity-1\"},"+Summary+"}", (c,t)=>Box(c.CreateCategoryAsync(new CreateSubcategoryRequest { Name="Dining", ParentId="parent" },t)), "{\"name\":\"Dining\",\"parentId\":\"parent\"}", HttpStatusCode.Created)];
        yield return [new OperationCase("createLabel", "POST", "/v1/api/labels", "{\"label\":{\"id\":\"entity-1\"},"+Summary+"}", (c,t)=>Box(c.CreateLabelAsync(new CreateLabelRequest { Name="Vacation", Color="Blue" },t)), "{\"name\":\"Vacation\",\"color\":\"Blue\"}", HttpStatusCode.Created)];
        yield return [new OperationCase("createRecords", "POST", "/v1/api/records", Batch, (c,t)=>Box(c.CreateRecordsAsync([Record()], new WriteOptions { ReturnData=false },t)), "[{\"accountId\":\"account-1\",\"amount\":{\"value\":-42.50},\"recordDate\":\"2025-03-15T14:30:00+00:00\"}]")];
        yield return [new OperationCase("patchAccounts", "PATCH", "/v1/api/accounts", Batch, (c,t)=>Box(c.PatchAccountsAsync([new PatchAccountItem { Id="entity-1", Archived=false, InitialBalance=0, BankAccountNumber="" }], new WriteOptions { ReturnData=false },t)), "[{\"id\":\"entity-1\",\"archived\":false,\"initialBalance\":0,\"bankAccountNumber\":\"\"}]")];
        yield return [new OperationCase("patchBudgets", "PATCH", "/v1/api/budgets", Batch, (c,t)=>Box(c.PatchBudgetsAsync([new PatchBudgetRequest { Id="entity-1", Closed=false, LabelIds=IdCollectionUpdate.Replace([]), AccountIds=IdCollectionUpdate.Modify(add:["account-1"]) }], cancellationToken:t)), "[{\"id\":\"entity-1\",\"closed\":false,\"labelIds\":[],\"accountIds\":{\"add\":[\"account-1\"]}}]")];
        yield return [new OperationCase("patchCategories", "PATCH", "/v1/api/categories", Batch, (c,t)=>Box(c.PatchCategoriesAsync([new PatchCategoryRequest { Id="entity-1", Reset=["name","color"] }], cancellationToken:t)), "[{\"id\":\"entity-1\",\"reset\":[\"name\",\"color\"]}]")];
        yield return [new OperationCase("patchLabels", "PATCH", "/v1/api/labels", Batch, (c,t)=>Box(c.PatchLabelsAsync([new PatchLabelItem { Id="entity-1", Archived=false }], cancellationToken:t)), "[{\"id\":\"entity-1\",\"archived\":false}]")];
        yield return [new OperationCase("patchRecords", "PATCH", "/v1/api/records", Batch, (c,t)=>Box(c.PatchRecordsAsync([new PatchRecordItem { Id="entity-1", Clear=["note"], Place=new(), LabelIds=IdCollectionUpdate.Modify(remove:["label-1"]) }],new RecordPatchOptions { Validation="strict", ReturnData=false },t)), "[{\"id\":\"entity-1\",\"$clear\":[\"note\"],\"place\":{},\"labelIds\":{\"remove\":[\"label-1\"]}}]")];
        yield return [new OperationCase("deleteByType", "DELETE", "/v1/api/records", Batch, (c,t)=>Box(c.DeleteAsync(DeleteEntityType.Records,["entity-1"],t)), "{\"ids\":[\"entity-1\"]}")];
    }
}
