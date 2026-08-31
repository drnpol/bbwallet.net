namespace Wallet.Net.Models;

/// <summary>
/// Minimum and maximum date metadata.
/// </summary>
public sealed record StatDateRange
{
    /// <summary>
    /// Maximum date value.
    /// </summary>
    public string? Max { get; init; }

    /// <summary>
    /// Minimum date value.
    /// </summary>
    public string? Min { get; init; }
}
