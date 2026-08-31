using Wallet.Net.Models;

namespace Wallet.Net.Models.StandingOrders;

/// <summary>
/// Standing order list query options.
/// </summary>
public sealed record StandingOrderListOptions : WalletListOptions
{
    public string? CurrencyCode { get; init; }
    public string? LabelId { get; init; }
}
