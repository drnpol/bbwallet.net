using Wallet.Net.Models;

namespace Wallet.Net.Models.StandingOrders;

/// <summary>
/// Standing order item list response.
/// </summary>
public sealed record StandingOrderItemsResponse : PaginatedResponse
{
    public IReadOnlyList<AgentHint> AgentHints { get; init; } = [];
    public IReadOnlyList<StandingOrderItem> StandingOrderItems { get; init; } = [];
}
