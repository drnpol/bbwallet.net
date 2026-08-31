using Wallet.Net.Models;

namespace Wallet.Net.Models.Budgets;

/// <summary>
/// Budget list query options.
/// </summary>
public sealed record BudgetListOptions : WalletListOptions
{
    public string? CurrencyCode { get; init; }
    public bool? Closed { get; init; }
    public string? Type { get; init; }
    public string? LabelId { get; init; }
    public string? AccountId { get; init; }
    public string? CategoryId { get; init; }
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public string? Spending { get; init; }
}
