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

/// <summary>CreateBudgetRequest API contract.</summary>
public sealed record CreateBudgetRequest
{
    /// <summary>Account IDs to track</summary>
    public IReadOnlyList<string>? AccountIds { get; init; }

    /// <summary>Category IDs to track (system or custom)</summary>
    public IReadOnlyList<string>? CategoryIds { get; init; }

    /// <summary>3-letter currency code (e.g. USD, EUR)</summary>
    [Required]
    public required string CurrencyCode { get; init; }

    /// <summary>End date (YYYY-MM-DD). Required for BUDGET_CUSTOM.</summary>
    public DateOnly? EndDate { get; init; }

    /// <summary>Label IDs to filter transactions</summary>
    public IReadOnlyList<string>? LabelIds { get; init; }

    /// <summary>Spending limit (must be &gt; 0)</summary>
    public required decimal Limit { get; init; }

    /// <summary>Budget display name</summary>
    [Required]
    public required string Name { get; init; }

    /// <summary>Start date (YYYY-MM-DD). Required for BUDGET_CUSTOM.</summary>
    public DateOnly? StartDate { get; init; }

    /// <summary>Budget interval type. BUDGET_CUSTOM requires startDate and endDate. Type is immutable after creation.</summary>
    [Required]
    [WalletAllowedValues("BUDGET_INTERVAL_WEEK", "BUDGET_INTERVAL_MONTH", "BUDGET_INTERVAL_YEAR", "BUDGET_CUSTOM")]
    public required string Type { get; init; }

}
