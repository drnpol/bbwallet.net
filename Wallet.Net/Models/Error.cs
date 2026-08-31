using Newtonsoft.Json;

namespace Wallet.Net.Models;

/// <summary>
/// Error payload returned by the API.
/// </summary>
public record Error
{
    /// <summary>
    /// Error message.
    /// </summary>
    [JsonProperty("error")]
    public string? Message { get; init; }
}
