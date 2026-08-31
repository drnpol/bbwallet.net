namespace Wallet.Net.Models.Budgets;

/// <summary>
/// Budget limit override entry.
/// </summary>
public sealed record BudgetChangeEntry
{
    public string? CreatedAt { get; init; }
    public double? Limit { get; init; }
    public string? Period { get; init; }
    public int? PeriodCount { get; init; }
    public string? PeriodStart { get; init; }
}
