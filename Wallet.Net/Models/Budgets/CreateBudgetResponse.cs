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

/// <summary>CreateBudgetResponse API contract.</summary>
public sealed record CreateBudgetResponse : WalletResponse
{
    /// <summary>agentHints value.</summary>
    public IReadOnlyList<AgentHint>? AgentHints { get; init; }

    /// <summary>budget value.</summary>
    public Budget? Budget { get; init; }

    /// <summary>Standard write summary (A7): total 1, succeeded 1, documentsWritten 1 on the success this envelope reports.</summary>
    public BatchSummary? Summary { get; init; }

}
