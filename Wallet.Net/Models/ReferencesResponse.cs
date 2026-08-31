using Newtonsoft.Json;

namespace Wallet.Net.Models;

/// <summary>
/// Reference information for a requested entity.
/// </summary>
public sealed record ReferencesResponse
{
    /// <summary>
    /// Actual entity type when the requested type did not match.
    /// </summary>
    public string? ActualType { get; init; }

    /// <summary>
    /// Budget references.
    /// </summary>
    public ReferenceResult? Budgets { get; init; }

    /// <summary>
    /// Error message when the entity cannot be resolved.
    /// </summary>
    [JsonProperty("error")]
    public string? Error { get; init; }

    /// <summary>
    /// Human-readable error detail.
    /// </summary>
    public string? Message { get; init; }

    /// <summary>
    /// Record rule references.
    /// </summary>
    public ReferenceResult? RecordRules { get; init; }

    /// <summary>
    /// Record references.
    /// </summary>
    public ReferenceResult? Records { get; init; }

    /// <summary>
    /// Standing order references.
    /// </summary>
    public ReferenceResult? StandingOrders { get; init; }
}
