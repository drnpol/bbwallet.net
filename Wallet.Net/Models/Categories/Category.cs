namespace Wallet.Net.Models.Categories;

/// <summary>
/// Wallet category.
/// </summary>
public sealed record Category
{
    public bool Archived { get; init; }
    public string? Cardinality { get; init; }
    public string? Color { get; init; }
    public DateTimeOffset? CreatedAt { get; init; }
    public bool CustomCategory { get; init; }
    public bool CustomName { get; init; }
    public bool Enabled { get; init; }
    public CategoryGroup? Group { get; init; }
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? ParentId { get; init; }
    public string? ParentName { get; init; }
    public string? SystemId { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
}
