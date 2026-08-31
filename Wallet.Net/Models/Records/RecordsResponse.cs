using Wallet.Net.Models;

namespace Wallet.Net.Models.Records;

/// <summary>
/// Record list response.
/// </summary>
public sealed record RecordsResponse : PaginatedResponse
{
    public IReadOnlyList<AgentHint> AgentHints { get; init; } = [];
    public IReadOnlyList<string> AppliedRecordDateFilters { get; init; } = [];
    public IReadOnlyList<Record> Records { get; init; } = [];
}
