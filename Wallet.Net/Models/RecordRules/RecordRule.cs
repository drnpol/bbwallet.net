using Wallet.Net.Models.Categories;
using Wallet.Net.Models.Labels;

namespace Wallet.Net.Models.RecordRules;

/// <summary>
/// Wallet record rule.
/// </summary>
public sealed record RecordRule
{
    public CategoryEmbed? Category { get; init; }
    public DateTimeOffset? CreatedAt { get; init; }
    public string? FromAccountId { get; init; }
    public string Id { get; init; } = string.Empty;
    public IReadOnlyList<string> Keywords { get; init; } = [];
    public IReadOnlyList<LabelEmbed> Labels { get; init; } = [];
    public string Name { get; init; } = string.Empty;
    public string? ToAccountId { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
}
