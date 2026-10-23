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

/// <summary>Transaction amount. Omit = no change. Negative=expense, positive=income. Auto-updates recordType. Patching amount ALWAYS sets the record's currency to the account's currency, regardless of the record's original currency. If amount is omitted from the patch, the original currency is preserved.</summary>
public sealed record RecordAmountInput
{
    /// <summary>Reserved for future use. Currently must match the account's currency if provided.</summary>
    public string? CurrencyCode { get; init; }

    /// <summary>Amount value (negative=expense, positive=income). Must not be zero.</summary>
    public required decimal Value { get; init; }

}
