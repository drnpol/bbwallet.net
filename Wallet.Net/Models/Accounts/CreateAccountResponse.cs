// DTOs generated from budgetbakers-wallet-api-v2.1.0.json; see scripts/Generate-WriteModels.cjs.
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Wallet.Net.Models;
using Wallet.Net.Models.Accounts;
using Wallet.Net.Models.Budgets;
using Wallet.Net.Models.Categories;
using Wallet.Net.Models.Labels;
using Record = Wallet.Net.Models.Records.Record;

namespace Wallet.Net.Models.Accounts;

/// <summary>CreateAccountResponse API contract.</summary>
public sealed record CreateAccountResponse : WalletResponse
{
    /// <summary>account value.</summary>
    public Account? Account { get; init; }

    /// <summary>agentHints value.</summary>
    public IReadOnlyList<AgentHint>? AgentHints { get; init; }

    /// <summary>Standard write summary (A7): total 1, succeeded 1, documentsWritten 1 on the success this envelope reports.</summary>
    public BatchSummary? Summary { get; init; }

}
