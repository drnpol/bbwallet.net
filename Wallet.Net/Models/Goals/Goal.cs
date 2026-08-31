using Wallet.Net.Models;

namespace Wallet.Net.Models.Goals;

/// <summary>
/// Wallet goal.
/// </summary>
public sealed record Goal
{
    public string? Color { get; init; }
    public DateTimeOffset? CreatedAt { get; init; }
    public DateTimeOffset? DesiredDate { get; init; }
    public string Id { get; init; } = string.Empty;
    public AmountWithCurrency? InitialAmount { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Note { get; init; }
    public string? State { get; init; }
    public DateTimeOffset? StateUpdatedAt { get; init; }
    public AmountWithCurrency? TargetAmount { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
}
