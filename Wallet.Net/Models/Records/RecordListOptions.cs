using Wallet.Net.Models;

namespace Wallet.Net.Models.Records;

/// <summary>
/// Record list query options.
/// </summary>
public sealed record RecordListOptions : WalletListOptions
{
    public string? AccountId { get; init; }
    public string? RecordDate { get; init; }
    public string? CategoryId { get; init; }
    public string? LabelId { get; init; }
    public string? Note { get; init; }
    public string? CounterParty { get; init; }
    public string? Amount { get; init; }
    public string? RecordType { get; init; }
    public bool? IsTransfer { get; init; }
    public string? TransferId { get; init; }
    public string? RecordState { get; init; }
    public string? Source { get; init; }
    public string? ConvertTo { get; init; }
}
