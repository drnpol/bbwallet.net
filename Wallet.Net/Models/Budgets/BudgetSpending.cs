namespace Wallet.Net.Models.Budgets;

/// <summary>
/// Budget spending summary.
/// </summary>
public sealed record BudgetSpending
{
    public string? ComputedAt { get; init; }
    public BudgetPeriodSpending? Current { get; init; }
    public IReadOnlyList<BudgetPeriodSpending> Past { get; init; } = [];
}
