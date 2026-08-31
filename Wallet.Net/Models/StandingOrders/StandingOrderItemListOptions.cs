using Wallet.Net.Models;

namespace Wallet.Net.Models.StandingOrders;

/// <summary>
/// Standing order item list query options.
/// </summary>
public sealed record StandingOrderItemListOptions : WalletListOptions
{
    public string? StandingOrderId { get; init; }
    public string? OriginalDate { get; init; }
    public bool? Dismissed { get; init; }
    public string? RecordId { get; init; }
    public string? PaidDate { get; init; }
}
