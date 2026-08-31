using Wallet.Net.Models;

namespace Wallet.Net.Models.Categories;

/// <summary>
/// Category list query options.
/// </summary>
public sealed record CategoryListOptions : WalletListOptions
{
    public bool? CustomCategory { get; init; }
    public string? Cardinality { get; init; }
    public bool? Archived { get; init; }
    public string? BudgetId { get; init; }
}
