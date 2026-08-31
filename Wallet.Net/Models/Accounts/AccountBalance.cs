namespace Wallet.Net.Models.Accounts;

/// <summary>
/// Computed account balance.
/// </summary>
public sealed record AccountBalance
{
    public double? AvailableCredit { get; init; }
    public string? BalanceDisplayOption { get; init; }
    public string? BalanceMode { get; init; }
    public string? BalanceModeFormula { get; init; }
    public double? CreditBalance { get; init; }
    public double? CreditLimit { get; init; }
    public string CurrencyCode { get; init; } = string.Empty;
    public double? CurrentBalance { get; init; }
    public string? Error { get; init; }
    public string? Formula { get; init; }
    public double? Initial { get; init; }
    public double? RawCurrentBalance { get; init; }
}
