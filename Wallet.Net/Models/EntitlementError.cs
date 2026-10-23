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

/// <summary>Returned when the token is valid but the account may not use the API: no active Premium subscription (`PREMIUM_REQUIRED`), API access not enabled in the web app (`API_CONSENT_REQUIRED`), or both.</summary>
public sealed record EntitlementError
{
    /// <summary>code value.</summary>
    public string? Code { get; init; }

    /// <summary>error value.</summary>
    public string? Error { get; init; }

}
