namespace Wallet.Net.Models.Records;

/// <summary>
/// Place metadata attached to a record.
/// </summary>
public sealed record RecordPlace
{
    public string? Address { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
    public string? Name { get; init; }
}
