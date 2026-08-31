using Wallet.Net.Models;

namespace Wallet.Net.Models.Labels;

/// <summary>
/// Label list query options.
/// </summary>
public sealed record LabelListOptions : WalletListOptions
{
    public bool? Archived { get; init; }
    public string? RecordId { get; init; }
    public string? BudgetId { get; init; }
    public string? StandingOrderId { get; init; }
    public string? RecordRuleId { get; init; }
}
