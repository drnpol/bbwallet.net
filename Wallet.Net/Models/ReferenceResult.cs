namespace Wallet.Net.Models;

/// <summary>
/// References to an entity from one API collection.
/// </summary>
public sealed record ReferenceResult
{
    /// <summary>
    /// Field name used for the reference.
    /// </summary>
    public string? Field { get; init; }

    /// <summary>
    /// Whether more references exist than were returned.
    /// </summary>
    public bool HasMore { get; init; }

    /// <summary>
    /// Referencing entity identifiers.
    /// </summary>
    public IReadOnlyList<string> Ids { get; init; } = [];

    /// <summary>
    /// Maximum identifiers returned.
    /// </summary>
    public int Limit { get; init; }

    /// <summary>
    /// Total reference count.
    /// </summary>
    public int Total { get; init; }
}
