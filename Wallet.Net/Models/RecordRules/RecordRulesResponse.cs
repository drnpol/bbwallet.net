using Wallet.Net.Models;

namespace Wallet.Net.Models.RecordRules;

/// <summary>
/// Record rule list response.
/// </summary>
public sealed record RecordRulesResponse : PaginatedResponse
{
    public IReadOnlyList<AgentHint> AgentHints { get; init; } = [];
    public IReadOnlyList<RecordRule> RecordRules { get; init; } = [];
}
