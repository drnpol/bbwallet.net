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

/// <summary>PatchAccountItem API contract.</summary>
public sealed record PatchAccountItem
{
    /// <summary>archived value.</summary>
    public bool? Archived { get; init; }

    /// <summary>Bank account number or IBAN (max 80 chars). Empty string clears the value; omit the field to leave the stored value unchanged. A partial or masked value such as &lt;code&gt;****5399&lt;/code&gt; is rejected — omit it instead.</summary>
    [StringLength(80)]
    public string? BankAccountNumber { get; init; }

    /// <summary>Universal color name.</summary>
    [WalletAllowedValues("Blue", "Charcoal", "CoffeeBrown", "DarkPurple", "Green", "LightBlue", "Lime", "Orange", "PineGreen", "Pink", "Purple", "Red", "Stone", "Turquoise", "Wine", "Yellow")]
    public string? Color { get; init; }

    /// <summary>excludeFromStats value.</summary>
    public bool? ExcludeFromStats { get; init; }

    /// <summary>id value.</summary>
    [Required]
    public required string Id { get; init; }

    /// <summary>Initial balance amount (e.g. 1000.00). Omit = no change. INPUT ONLY — it is not returned on the account object; read it back as balance.initial.</summary>
    public decimal? InitialBalance { get; init; }

    /// <summary>name value.</summary>
    [StringLength(80)]
    public string? Name { get; init; }

}
