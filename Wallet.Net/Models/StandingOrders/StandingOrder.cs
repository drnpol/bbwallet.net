using Wallet.Net.Models.Labels;

namespace Wallet.Net.Models.StandingOrders;

/// <summary>
/// Wallet standing order.
/// </summary>
public sealed record StandingOrder
{
    public string? AccountId { get; init; }
    public double? Amount { get; init; }
    public string? CategoryId { get; init; }
    public string? CounterParty { get; init; }
    public DateTimeOffset? CreatedAt { get; init; }
    public string CurrencyCode { get; init; } = string.Empty;
    public DateTimeOffset? DueDate { get; init; }
    public bool DueDateNotificationEnabled { get; init; }
    public string? GenerateFromDate { get; init; }
    public string Id { get; init; } = string.Empty;
    public IReadOnlyList<LabelEmbed> Labels { get; init; } = [];
    public bool ManualPayment { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Note { get; init; }
    public int? PaymentCount { get; init; }
    public string? RecurrenceRule { get; init; }
    public string? Reminder { get; init; }
    public bool ThreeDaysBeforeNotificationEnabled { get; init; }
    public string? Type { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
}
