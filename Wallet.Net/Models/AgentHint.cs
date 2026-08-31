using Newtonsoft.Json.Linq;

namespace Wallet.Net.Models;

/// <summary>
/// Structured hint returned by the API for pagination, warnings, or partial matches.
/// </summary>
public sealed record AgentHint
{
    /// <summary>
    /// Hint category.
    /// </summary>
    public string Type { get; init; } = string.Empty;

    /// <summary>
    /// Hint severity.
    /// </summary>
    public string Severity { get; init; } = string.Empty;

    /// <summary>
    /// Human-readable hint text.
    /// </summary>
    public string Text { get; init; } = string.Empty;

    /// <summary>
    /// Optional action data.
    /// </summary>
    public AgentHintAction? Action { get; init; }

    /// <summary>
    /// Optional structured hint data.
    /// </summary>
    public JObject? Data { get; init; }
}
