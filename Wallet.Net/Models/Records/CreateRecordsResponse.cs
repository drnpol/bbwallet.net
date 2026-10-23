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

/// <summary>CreateRecordsResponse API contract.</summary>
public sealed record CreateRecordsResponse : WalletResponse, IWalletBatchResponse
{
    /// <summary>Per-item results, one entry per input record</summary>
    public IReadOnlyList<CreateRecordsResponseResultsItem>? Results { get; init; }

    /// <summary>summary value.</summary>
    public BatchSummary? Summary { get; init; }

    /// <summary>Optional server hints, including best-effort transfer mirror failures.</summary>
    public IReadOnlyList<AgentHint>? AgentHints { get; init; }
}
