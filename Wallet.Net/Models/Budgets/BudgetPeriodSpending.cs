namespace Wallet.Net.Models.Budgets;

/// <summary>
/// Budget spending for one period.
/// </summary>
public sealed record BudgetPeriodSpending
{
    public IReadOnlyList<string> ConvertedCurrencies { get; init; } = [];
    public double? EffectiveLimit { get; init; }
    public string? Error { get; init; }
    public ExcludedBreakdown? Excluded { get; init; }
    public bool Incomplete { get; init; }
    public double? Overspent { get; init; }
    public string? Period { get; init; }
    public string? PeriodEnd { get; init; }
    public string? PeriodStart { get; init; }
    public double? Progress { get; init; }
    public int? RecordCount { get; init; }
    public double? Remaining { get; init; }
    public double? Spent { get; init; }
    public double? TotalExpenses { get; init; }
    public double? TotalIncomes { get; init; }
}
