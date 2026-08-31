namespace Wallet.Net.Models;

/// <summary>
/// Represents an amount paired with an ISO currency code.
/// </summary>
public sealed record AmountWithCurrency
{
    /// <summary>
    /// Currency code.
    /// </summary>
    public string CurrencyCode { get; init; } = string.Empty;

    /// <summary>
    /// Amount value in decimal format.
    /// </summary>
    public double Value { get; init; }
}
