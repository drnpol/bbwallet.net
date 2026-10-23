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

/// <summary>Amount on the counter account (cross-currency 'new' only). Opposite sign to record amount. Omit to auto-convert via FX.</summary>
public sealed record TransferAmountInput
{
    /// <summary>Must match counter account's currency code.</summary>
    [Required]
    public required string CurrencyCode { get; init; }

    /// <summary>Numeric amount (opposite sign to record amount).</summary>
    public required decimal Value { get; init; }

}
