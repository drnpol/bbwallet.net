namespace Wallet.Net.Models;

/// <summary>
/// Common list options accepted by Wallet API read endpoints.
/// </summary>
public record WalletListOptions
{
    /// <summary>
    /// Maximum number of items to return.
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Number of items to skip.
    /// </summary>
    public int? Offset { get; init; }

    /// <summary>
    /// Enables agent hints in the API response.
    /// </summary>
    public bool? AgentHints { get; init; }

    /// <summary>
    /// Requests total count from the API.
    /// </summary>
    public bool? WithTotal { get; init; }

    /// <summary>
    /// Filter by one or more identifiers.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Filter by name using an API filter prefix.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Filter by created timestamp using an API filter prefix.
    /// </summary>
    public string? CreatedAt { get; init; }

    /// <summary>
    /// Filter by updated timestamp using an API filter prefix.
    /// </summary>
    public string? UpdatedAt { get; init; }

    /// <summary>
    /// Sort field with optional direction prefix.
    /// </summary>
    public string? SortBy { get; init; }
}
