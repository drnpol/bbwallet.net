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

/// <summary>APIUsageStatsEntry API contract.</summary>
public sealed record APIUsageStatsEntry
{
    /// <summary>Start of period, inclusive (ISO 8601 UTC timestamp, e.g., '2025-01-07T00:00:00Z')</summary>
    public DateTimeOffset From { get; init; }

    /// <summary>End of period, exclusive (ISO 8601 UTC timestamp). This is the start of the next period, so the range is [from, to). For daily granularity, 'to' is 'from' + 1 day.</summary>
    public DateTimeOffset To { get; init; }

    /// <summary>Total API requests in this period</summary>
    public int Total { get; init; }

}
