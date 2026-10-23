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

/// <summary>CreateRecordRequest API contract.</summary>
public sealed record CreateRecordRequest
{
    /// <summary>Account ID to associate the record with</summary>
    [Required]
    public required string AccountId { get; init; }

    /// <summary>Transaction amount. currencyCode is optional (defaults to account's currency). If provided, must match account's currency.</summary>
    [Required]
    public required RecordAmountInput Amount { get; init; }

    /// <summary>Category ID. System categories use deterministic IDs, custom categories use standard IDs.</summary>
    public string? CategoryId { get; init; }

    /// <summary>Payee/merchant name</summary>
    [StringLength(255)]
    public string? CounterParty { get; init; }

    /// <summary>labelIds value.</summary>
    public IReadOnlyList<string>? LabelIds { get; init; }

    /// <summary>note value.</summary>
    [StringLength(255)]
    public string? Note { get; init; }

    /// <summary>Location data (optional)</summary>
    public PlaceInput? Place { get; init; }

    /// <summary>Record date in ISO 8601 format. Must not be more than 24 hours in the future.</summary>
    public required DateTimeOffset RecordDate { get; init; }

    /// <summary>Record state (default: cleared).</summary>
    [WalletAllowedValues("reconciled", "cleared", "uncleared")]
    public string? RecordState { get; init; }

    /// <summary>Account movement settings. Omit for regular records. Requires pairingMode: 'new' (paired, two linked records), 'existing' (pair to existing record), or 'unpaired'.</summary>
    public TransferInput? Transfer { get; init; }

}
