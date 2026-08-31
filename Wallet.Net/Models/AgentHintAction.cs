namespace Wallet.Net.Models;

/// <summary>
/// Action data attached to an API agent hint.
/// </summary>
public sealed record AgentHintAction
{
    /// <summary>
    /// URL for the recommended follow-up action.
    /// </summary>
    public string? Url { get; init; }
}
