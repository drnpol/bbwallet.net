using Wallet.Net.Models;

namespace Wallet.Net.Models.Accounts;

/// <summary>
/// Account list response.
/// </summary>
public sealed record AccountsResponse : PaginatedResponse
{
    public IReadOnlyList<Account> Accounts { get; init; } = [];
    public IReadOnlyList<AgentHint> AgentHints { get; init; } = [];
}
