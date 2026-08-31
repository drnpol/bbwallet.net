using Wallet.Net.Models;

namespace Wallet.Net.Models.Goals;

/// <summary>
/// Goal list response.
/// </summary>
public sealed record GoalsResponse : PaginatedResponse
{
    public IReadOnlyList<AgentHint> AgentHints { get; init; } = [];
    public IReadOnlyList<Goal> Goals { get; init; } = [];
}
