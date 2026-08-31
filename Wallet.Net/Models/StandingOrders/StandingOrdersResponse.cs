using Wallet.Net.Models;

namespace Wallet.Net.Models.StandingOrders;

/// <summary>
/// Standing order list response.
/// </summary>
public sealed record StandingOrdersResponse : PaginatedResponse
{
    public IReadOnlyList<AgentHint> AgentHints { get; init; } = [];
    public IReadOnlyList<StandingOrder> StandingOrders { get; init; } = [];
}
