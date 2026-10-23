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

/// <summary>DeleteResponseResultsItem API contract.</summary>
public sealed record DeleteResponseResultsItem
{
    /// <summary>error value.</summary>
    public string? Error { get; init; }

    /// <summary>Error classification: client_error (bad input) or server_error (internal failure)</summary>
    public string? ErrorType { get; init; }

    /// <summary>Input field names the error is attributable to; absent for item-level errors</summary>
    public IReadOnlyList<string>? Fields { get; init; }

    /// <summary>id value.</summary>
    public string? Id { get; init; }

    /// <summary>0-based position of the input item this result answers — the uniform correlation key across every write endpoint.</summary>
    public int? InputIndex { get; init; }

    /// <summary>success value.</summary>
    public bool? Success { get; init; }

}
