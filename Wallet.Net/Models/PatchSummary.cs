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

/// <summary>Summary of patch/update operation results</summary>
public sealed record PatchSummary
{
    /// <summary>Items that failed due to invalid input (client errors)</summary>
    public int ClientErrors { get; init; }

    /// <summary>Documents actually written, counted from confirmed write outcomes and deduplicated — independent of item success: an item that failed AFTER its document committed still counts its document, and the error text says what landed. A no-op item (fields already hold the sent values) succeeds with 0 here. Can EXCEED `total` on records: a created mirror, severing, orphaning, converting a target, propagating to a mirror, or rewriting a CRM order document for an orderId bind each write another document, and every one of them is counted here.</summary>
    public int DocumentsWritten { get; init; }

    /// <summary>Items that failed due to internal errors (server errors)</summary>
    public int ServerErrors { get; init; }

    /// <summary>Successfully updated items</summary>
    public int Succeeded { get; init; }

    /// <summary>Total items in request</summary>
    public int Total { get; init; }

}
