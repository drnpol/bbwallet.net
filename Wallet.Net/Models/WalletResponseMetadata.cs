using System.Net;

namespace Wallet.Net.Models;

/// <summary>HTTP metadata reported by the Wallet API. Missing headers remain null.</summary>
public sealed record WalletResponseMetadata
{
    /// <summary>HTTP response status.</summary>
    public HttpStatusCode StatusCode { get; init; }
    /// <summary>Hourly request capacity.</summary>
    public int? RateLimit { get; init; }
    /// <summary>Remaining request capacity.</summary>
    public int? RateLimitRemaining { get; init; }
    /// <summary>Last reported data modification time.</summary>
    public DateTimeOffset? LastDataChangeAt { get; init; }
    /// <summary>Opaque revision used for change detection.</summary>
    public string? LastDataChangeRevision { get; init; }
    /// <summary>Whether background sync is still running.</summary>
    public bool? SyncInProgress { get; init; }
    /// <summary>Retry delay in seconds, as reported by the API.</summary>
    public int? RetryAfterSeconds { get; init; }
}
