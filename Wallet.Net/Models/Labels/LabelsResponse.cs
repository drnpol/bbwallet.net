using Wallet.Net.Models;

namespace Wallet.Net.Models.Labels;

/// <summary>
/// Label list response.
/// </summary>
public sealed record LabelsResponse : PaginatedResponse
{
    public IReadOnlyList<Label> Labels { get; init; } = [];
    public IReadOnlyList<AgentHint> AgentHints { get; init; } = [];
}
