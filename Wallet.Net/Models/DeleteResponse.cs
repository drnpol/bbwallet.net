// DTOs generated from budgetbakers-wallet-api-v2.1.0.json; see scripts/Generate-WriteModels.cjs.
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Wallet.Net.Models;
using Wallet.Net.Models.Accounts;
using Wallet.Net.Models.Budgets;
using Wallet.Net.Models.Categories;
using Wallet.Net.Models.Labels;
using Record = Wallet.Net.Models.Records.Record;

namespace Wallet.Net.Models;

/// <summary>DeleteResponse API contract.</summary>
public sealed record DeleteResponse : WalletResponse, IWalletBatchResponse
{
    /// <summary>agentHints value.</summary>
    public IReadOnlyList<AgentHint>? AgentHints { get; init; }

    /// <summary>results value.</summary>
    public IReadOnlyList<DeleteResponseResultsItem>? Results { get; init; }

    /// <summary>summary value.</summary>
    public BatchSummary? Summary { get; init; }

}
