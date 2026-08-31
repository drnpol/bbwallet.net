using Wallet.Net.Models;

namespace Wallet.Net.Models.Categories;

/// <summary>
/// Category list response.
/// </summary>
public sealed record CategoriesResponse : PaginatedResponse
{
    public IReadOnlyList<Category> Categories { get; init; } = [];
    public IReadOnlyList<AgentHint> AgentHints { get; init; } = [];
}
