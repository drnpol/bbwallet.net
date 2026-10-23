// DTOs generated from budgetbakers-wallet-api-v2.1.0.json; see scripts/Generate-WriteModels.cjs.
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Wallet.Net.Models;
using Wallet.Net.Models.Accounts;
using Wallet.Net.Models.Budgets;
using Wallet.Net.Models.Categories;
using Wallet.Net.Models.Labels;
using Record = Wallet.Net.Models.Records.Record;

namespace Wallet.Net.Models.Budgets;

/// <summary>LimitOverrideInput API contract.</summary>
public sealed record LimitOverrideInput
{
    /// <summary>New spending limit (must be &gt; 0). Mutually exclusive with setBaseline.</summary>
    public decimal? Limit { get; init; }

    /// <summary>Starting period. Format: YYYY-MM (monthly), YYYY-Www (weekly), YYYY (yearly). Must be &gt;= current period.</summary>
    [Required]
    public required string Period { get; init; }

    /// <summary>How many periods this override lasts, COUNTING ITS OWN: 1 = this period only. Omit for an open-ended override. Requires limit; not allowed with setBaseline. The server writes the entry that ends the override, restoring the limit actually in force at that period. Rejected if another entry in the same call starts inside the span.</summary>
    [Range(typeof(decimal), "1", "120")]
    public int? PeriodCount { get; init; }

    /// <summary>If true, writes an entry holding the budget's current limit, ending any earlier override from this period onward. Entries carry a fixed value, so a later change to the budget's limit does not move it — use resetLimit to clear all overrides instead. Mutually exclusive with limit and with periodCount.</summary>
    public bool? SetBaseline { get; init; }

}
