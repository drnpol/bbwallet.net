namespace Wallet.Net.Models.Labels;

/// <summary>
/// Wallet label.
/// </summary>
public sealed record Label
{
    public bool Archived { get; init; }
    public string? Color { get; init; }
    public DateTimeOffset? CreatedAt { get; init; }
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public DateTimeOffset? UpdatedAt { get; init; }
}
