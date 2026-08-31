using Wallet.Net.Models;

namespace Wallet.Net.Models.Records;

/// <summary>
/// Embedded linked transfer record.
/// </summary>
public sealed record MirrorRecordEmbed
{
    public string? AccountId { get; init; }
    public AmountWithCurrency? Amount { get; init; }
    public string? CounterParty { get; init; }
    public string? Id { get; init; }
    public string? Note { get; init; }
}
