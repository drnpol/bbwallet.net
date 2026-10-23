// DTOs generated from budgetbakers-wallet-api-v2.1.0.json; see scripts/Generate-WriteModels.cjs.
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Wallet.Net.Models;
using Wallet.Net.Models.Accounts;
using Wallet.Net.Models.Budgets;
using Wallet.Net.Models.Categories;
using Wallet.Net.Models.Labels;
using Record = Wallet.Net.Models.Records.Record;

namespace Wallet.Net.Models.Records;

/// <summary>Account movement settings. Omit for regular records. Requires pairingMode: 'new' (paired, two linked records), 'existing' (pair to existing record), or 'unpaired'.</summary>
public sealed record TransferInput
{
    /// <summary>Target account (pairingMode 'new').</summary>
    public string? AccountId { get; init; }

    /// <summary>Amount on the counter account (cross-currency 'new' only). Opposite sign to record amount. Omit to auto-convert via FX.</summary>
    public TransferAmountInput? CounterAmount { get; init; }

    /// <summary>Required. 'new' = create mirror on target account, 'existing' = bind to existing record, 'unpaired' = no mirror.</summary>
    [Required]
    [WalletAllowedValues("new", "existing", "unpaired")]
    public required string PairingMode { get; init; }

    /// <summary>Existing record to bind to (pairingMode 'existing'). Target must be on a different account, have opposite amount sign, and not already be paired.</summary>
    public string? RecordId { get; init; }

}
