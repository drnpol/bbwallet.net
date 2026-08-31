namespace Wallet.Net.Models.Records;

/// <summary>
/// Photo metadata attached to a record.
/// </summary>
public sealed record RecordPhoto
{
    public DateTimeOffset? CreatedAt { get; init; }
    public string? TemporaryUrl { get; init; }
}
