// DTOs generated from budgetbakers-wallet-api-v2.1.0.json; see scripts/Generate-WriteModels.cjs.
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Wallet.Net.Models;
using Wallet.Net.Models.Accounts;
using Wallet.Net.Models.Budgets;
using Wallet.Net.Models.Categories;
using Wallet.Net.Models.Labels;
using Record = Wallet.Net.Models.Records.Record;

namespace Wallet.Net.Models.ApiUsage;

/// <summary>APIUsageStats API contract.</summary>
public sealed record APIUsageStats : WalletResponse
{
    /// <summary>Start of the queried date range, inclusive (ISO 8601 UTC timestamp)</summary>
    public DateTimeOffset From { get; init; }

    /// <summary>Time granularity of the usage entries</summary>
    public string? Granularity { get; init; }

    /// <summary>The requested period (e.g., '30days', '4weeks')</summary>
    public string? Period { get; init; }

    /// <summary>End of the queried date range, exclusive (ISO 8601 UTC timestamp)</summary>
    public DateTimeOffset To { get; init; }

    /// <summary>Total API requests in the period</summary>
    public int Total { get; init; }

    /// <summary>Per-period request totals. Periods with zero usage are omitted.</summary>
    public IReadOnlyList<APIUsageStatsEntry>? Usage { get; init; }

}
