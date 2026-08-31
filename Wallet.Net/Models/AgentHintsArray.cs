namespace Wallet.Net.Models;

/// <summary>
/// Wrapper for API agent hints.
/// </summary>
public sealed record AgentHintsArray
{
    /// <summary>
    /// Hint values.
    /// </summary>
    public IReadOnlyList<AgentHint> Values { get; init; } = [];
}
