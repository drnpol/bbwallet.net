namespace Wallet.Net.Models.Records;

/// <summary>
/// Transfer state returned for a record.
/// </summary>
public sealed record TransferOutput
{
    public MirrorRecordEmbed? MirrorRecord { get; init; }
    public string? TransferId { get; init; }
    public string? Type { get; init; }
}
