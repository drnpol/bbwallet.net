namespace Wallet.Net.Models.Categories;

/// <summary>
/// Embedded category information.
/// </summary>
public sealed record CategoryEmbed
{
    public string? Color { get; init; }
    public CategoryGroup? Group { get; init; }
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
}
