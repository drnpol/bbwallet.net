using Wallet.Net.Models;

namespace Wallet.Net.Models.Accounts;

/// <summary>
/// Wallet account.
/// </summary>
public sealed record Account
{
    public string? AccountType { get; init; }
    public bool Archived { get; init; }
    public AccountBalance? Balance { get; init; }
    public string? BankAccountNumber { get; init; }
    public string? Color { get; init; }
    public DateTimeOffset? CreatedAt { get; init; }
    public string CurrencyCode { get; init; } = string.Empty;
    public bool ExcludeFromStats { get; init; }
    public string Id { get; init; } = string.Empty;
    public bool IsBankSync { get; init; }
    public bool IsInvestmentAccount { get; init; }
    public string Name { get; init; } = string.Empty;
    public AccountStats? RecordStats { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
}
