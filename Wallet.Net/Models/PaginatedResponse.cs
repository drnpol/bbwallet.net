namespace Wallet.Net.Models;

/// <summary>
/// Pagination metadata returned by list endpoints.
/// </summary>
public abstract record PaginatedResponse
{
    /// <summary>
    /// Number of items per page.
    /// </summary>
    public int Limit { get; init; }

    /// <summary>
    /// Offset for the next page when more items are available.
    /// </summary>
    public int? NextOffset { get; init; }

    /// <summary>
    /// Starting item offset.
    /// </summary>
    public int Offset { get; init; }

    /// <summary>
    /// Total number of matching items when requested.
    /// </summary>
    public int? Total { get; init; }
}
