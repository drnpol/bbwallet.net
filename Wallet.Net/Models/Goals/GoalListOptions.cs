using Wallet.Net.Models;

namespace Wallet.Net.Models.Goals;

/// <summary>
/// Goal list query options.
/// </summary>
public sealed record GoalListOptions : WalletListOptions
{
    public string? Note { get; init; }
}
