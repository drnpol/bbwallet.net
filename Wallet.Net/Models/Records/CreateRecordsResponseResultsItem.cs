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

/// <summary>CreateRecordsResponseResultsItem API contract.</summary>
public sealed record CreateRecordsResponseResultsItem
{
    /// <summary>ID of the auto-created B-side of a paired transfer — present only when THIS call wrote that document (pairingMode "new"). Absent when the item is not a paired transfer, or when mirror creation failed (best-effort — a transfer.mirror_failed agent hint reports that case and the item still counts as succeeded). Survives returnData=false; the mirror's body is not returned — read transfer.mirrorRecord on the source, or fetch by this id.</summary>
    public string? CreatedMirrorRecordId { get; init; }

    /// <summary>Error message (on failure)</summary>
    public string? Error { get; init; }

    /// <summary>Error classification: client_error (bad input) or server_error (internal failure)</summary>
    public string? ErrorType { get; init; }

    /// <summary>Input field names the error is attributable to; absent when the error belongs to the item as a whole</summary>
    public IReadOnlyList<string>? Fields { get; init; }

    /// <summary>Record ID (present only when created)</summary>
    public string? Id { get; init; }

    /// <summary>0-based index in the input records array</summary>
    public int? InputIndex { get; init; }

    /// <summary>Full record object (on success)</summary>
    public Record? Record { get; init; }

    /// <summary>success value.</summary>
    public bool? Success { get; init; }

}
