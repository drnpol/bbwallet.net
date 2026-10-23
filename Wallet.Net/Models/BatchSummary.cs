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

/// <summary>Summary of batch operation results</summary>
public sealed record BatchSummary
{
    /// <summary>Items that failed due to invalid input (client errors)</summary>
    public int ClientErrors { get; init; }

    /// <summary>Documents actually written or removed, counted from confirmed write outcomes and deduplicated. NOT a copy of `succeeded`: on delete, a repeated id is idempotent, so `succeeded` counts input rows and this counts DISTINCT documents removed — and a retried delete of an already-deleted document reports success with 0 here, because that call wrote nothing. On create it can EXCEED `total`: an auto-created transfer mirror, a rewritten pairing target, and a CRM order document rewritten by an orderId bind are all counted here without being items.</summary>
    public int DocumentsWritten { get; init; }

    /// <summary>Items that failed due to internal errors (server errors)</summary>
    public int ServerErrors { get; init; }

    /// <summary>Successfully processed items</summary>
    public int Succeeded { get; init; }

    /// <summary>Total items in request</summary>
    public int Total { get; init; }

}
