namespace Wallet.Net.Models.Categories;

/// <summary>
/// Category group metadata.
/// </summary>
public sealed record CategoryGroup
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
}
