using Wallet.Net.Models;

namespace Wallet.Net.Models.Records;

/// <summary>Record PATCH options.</summary>
public sealed record RecordPatchOptions : WriteOptions
{
    /// <summary>Set to strict to validate all items before writing; null uses lenient processing.</summary>
    [WalletAllowedValues("strict")]
    public string? Validation { get; init; }
}
