namespace Wallet.Net.Models.StandingOrders;

/// <summary>
/// Scheduled or tracked item for a standing order.
/// </summary>
public sealed record StandingOrderItem
{
    public DateTimeOffset? AlignedDate { get; init; }
    public bool Dismissed { get; init; }
    public string Id { get; init; } = string.Empty;
    public DateTimeOffset? OriginalDate { get; init; }
    public DateTimeOffset? PaidDate { get; init; }
    public DateTimeOffset? PaidFromAppDate { get; init; }
    public IReadOnlyList<string> RecordIds { get; init; } = [];
    public string? StandingOrderId { get; init; }
}
