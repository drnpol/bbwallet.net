namespace Wallet.Net.Models.Budgets;

/// <summary>
/// Wallet budget.
/// </summary>
public sealed record Budget
{
    public IReadOnlyList<string> AccountIds { get; init; } = [];
    public IReadOnlyList<string> CategoryIds { get; init; } = [];
    public bool? Closed { get; init; }
    public string? ClosedDate { get; init; }
    public DateTimeOffset? CreatedAt { get; init; }
    public string CurrencyCode { get; init; } = string.Empty;
    public string? EndDate { get; init; }
    public string Id { get; init; } = string.Empty;
    public IReadOnlyList<string> LabelIds { get; init; } = [];
    public double? Limit { get; init; }
    public IReadOnlyList<BudgetChangeEntry> LimitOverrides { get; init; } = [];
    public string Name { get; init; } = string.Empty;
    public IReadOnlyList<BudgetChangeEntry> PastLimitOverrides { get; init; } = [];
    public BudgetSpending? Spending { get; init; }
    public string? StartDate { get; init; }
    public string? Type { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
}
