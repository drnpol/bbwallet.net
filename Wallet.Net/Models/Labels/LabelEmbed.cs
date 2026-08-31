namespace Wallet.Net.Models.Labels;

/// <summary>
/// Embedded label information.
/// </summary>
public sealed record LabelEmbed
{
    public bool Archived { get; init; }
    public string? Color { get; init; }
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
}
