using Wallet.Net.Models;

namespace Wallet.Net.Models.RecordRules;

/// <summary>
/// Record rule list query options.
/// </summary>
public sealed record RecordRuleListOptions : WalletListOptions
{
    public string? LabelId { get; init; }
}
