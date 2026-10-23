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

/// <summary>PatchBudgetRequest API contract.</summary>
public sealed record PatchBudgetRequest
{
    /// <summary>Account IDs to track. Array = replace all. Object {add, remove} = partial update.</summary>
    public IdCollectionUpdate? AccountIds { get; init; }

    /// <summary>Category IDs to track (system or custom). Array = replace all. Object {add, remove} = partial update.</summary>
    public IdCollectionUpdate? CategoryIds { get; init; }

    /// <summary>true to close (auto-sets closedDate to today UTC); false to reopen (clears closedDate)</summary>
    public bool? Closed { get; init; }

    /// <summary>End date. Only patchable on BUDGET_CUSTOM.</summary>
    public DateOnly? EndDate { get; init; }

    /// <summary>Budget ID (UUID)</summary>
    [Required]
    public required string Id { get; init; }

    /// <summary>Label IDs to filter transactions. Array = replace all. Object {add, remove} = partial update.</summary>
    public IdCollectionUpdate? LabelIds { get; init; }

    /// <summary>Array of limit overrides (max 50), EFFECTIVE-FROM: each entry sets the limit for its period and every period after it, until the next entry takes over. To bound an override to N periods, set periodCount on the entry (periodCount 1 = that period only). Replaces all current+future overrides — one you do not re-send is deleted, not merged, including any part beyond your own entries (past overrides are preserved). Not allowed on BUDGET_CUSTOM.</summary>
    [MaxLength(50)]
    public IReadOnlyList<LimitOverrideInput>? LimitOverrides { get; init; }

    /// <summary>Budget display name</summary>
    [StringLength(80)]
    public string? Name { get; init; }

    /// <summary>Clears ALL period overrides (past + future), sets uniform baseline limit. Works on all budget types. Can be combined with limitOverrides (resetLimit applies first).</summary>
    public decimal? ResetLimit { get; init; }

    /// <summary>Start date. Only patchable on BUDGET_CUSTOM.</summary>
    public DateOnly? StartDate { get; init; }

}
