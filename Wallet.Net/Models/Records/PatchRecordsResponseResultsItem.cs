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

/// <summary>PatchRecordsResponseResultsItem API contract.</summary>
public sealed record PatchRecordsResponseResultsItem
{
    /// <summary>Error message (on failure only)</summary>
    public string? Error { get; init; }

    /// <summary>Error classification: client_error (bad input) or server_error (internal failure)</summary>
    [WalletAllowedValues("client_error", "server_error")]
    public string? ErrorType { get; init; }

    /// <summary>Input field names the error is attributable to; absent when the error belongs to the item as a whole</summary>
    public IReadOnlyList<string>? Fields { get; init; }

    /// <summary>id value.</summary>
    public string? Id { get; init; }

    /// <summary>0-based position of the input item this result answers — the uniform correlation key across every write endpoint.</summary>
    public int? InputIndex { get; init; }

    /// <summary>Full record object on success (same shape as GET response)</summary>
    public Record? Record { get; init; }

    /// <summary>success value.</summary>
    public bool? Success { get; init; }

}
