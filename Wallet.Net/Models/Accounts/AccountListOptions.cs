using Wallet.Net.Models;

namespace Wallet.Net.Models.Accounts;

/// <summary>
/// Account list query options.
/// </summary>
public sealed record AccountListOptions : WalletListOptions
{
    public string? AccountType { get; init; }
    public string? CurrencyCode { get; init; }
    public bool? Archived { get; init; }
    public string? BudgetId { get; init; }
}
