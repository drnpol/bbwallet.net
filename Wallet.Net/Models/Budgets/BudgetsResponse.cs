using Wallet.Net.Models;

namespace Wallet.Net.Models.Budgets;

/// <summary>
/// Budget list response.
/// </summary>
public sealed record BudgetsResponse : PaginatedResponse
{
    public IReadOnlyList<AgentHint> AgentHints { get; init; } = [];
    public IReadOnlyList<Budget> Budgets { get; init; } = [];
}
