namespace Wallet.Net.Models;

/// <summary>
/// Represents a converted amount and the conversion metadata returned by the API.
/// </summary>
public sealed record ConvertedAmount
{
    /// <summary>
    /// Currency pair direction.
    /// </summary>
    public string? ConversionPair { get; init; }

    /// <summary>
    /// Target currency code.
    /// </summary>
    public string CurrencyCode { get; init; } = string.Empty;

    /// <summary>
    /// Error message when a rate is unavailable.
    /// </summary>
    public string? Error { get; init; }

    /// <summary>
    /// Exchange rate used.
    /// </summary>
    public double? Ratio { get; init; }

    /// <summary>
    /// Converted amount value.
    /// </summary>
    public double? Value { get; init; }
}
