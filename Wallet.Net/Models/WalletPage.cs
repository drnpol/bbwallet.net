namespace Wallet.Net.Models;

/// <summary>
/// Normalized page returned by read methods.
/// </summary>
public sealed record WalletPage<T>
{
    /// <summary>
    /// Page items.
    /// </summary>
    public IReadOnlyList<T> Items { get; init; } = [];

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

    /// <summary>
    /// Agent hints returned by the API.
    /// </summary>
    public IReadOnlyList<AgentHint> AgentHints { get; init; } = [];
}
