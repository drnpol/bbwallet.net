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

/// <summary>PatchRecordItem API contract.</summary>
public sealed record PatchRecordItem
{
    /// <summary>Fields to clear/reset. Example: ["note", "place"]. 'transfer' cancels transfer state (sets category to Unknown). Cannot be combined with an explicit value for the same field.</summary>
    [JsonProperty("$clear")]
    public IReadOnlyList<string>? Clear { get; init; }

    /// <summary>Move record to a different account (UUID). Both accounts must not be bank-synced and must have the same currency.</summary>
    public string? AccountId { get; init; }

    /// <summary>Transaction amount. Omit = no change. Negative=expense, positive=income. Auto-updates recordType. Patching amount ALWAYS sets the record's currency to the account's currency, regardless of the record's original currency. If amount is omitted from the patch, the original currency is preserved.</summary>
    public RecordAmountInput? Amount { get; init; }

    /// <summary>Category ID (system or custom)</summary>
    public string? CategoryId { get; init; }

    /// <summary>counterParty value.</summary>
    [StringLength(255)]
    public string? CounterParty { get; init; }

    /// <summary>id value.</summary>
    [Required]
    public required string Id { get; init; }

    /// <summary>Label IDs. Array = replace all (null=no change, []=clear all). Object {add, remove} = partial update (add to / remove from the existing set).</summary>
    public IdCollectionUpdate? LabelIds { get; init; }

    /// <summary>note value.</summary>
    [StringLength(255)]
    public string? Note { get; init; }

    /// <summary>Location data. Omit=no change. Full replace with {address, latitude, longitude, name}. Use $clear to erase.</summary>
    public PlaceInput? Place { get; init; }

    /// <summary>recordDate value.</summary>
    public DateTimeOffset? RecordDate { get; init; }

    /// <summary>Transaction state: reconciled (verified), cleared (processed), uncleared (pending)</summary>
    [WalletAllowedValues("reconciled", "cleared", "uncleared")]
    public string? RecordState { get; init; }

}
