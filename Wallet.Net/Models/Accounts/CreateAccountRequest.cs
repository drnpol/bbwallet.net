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

/// <summary>CreateAccountRequest API contract.</summary>
public sealed record CreateAccountRequest
{
    /// <summary>accountType value.</summary>
    [Required]
    [WalletAllowedValues("General", "Cash", "CurrentAccount", "SavingAccount", "Insurance")]
    public required string AccountType { get; init; }

    /// <summary>Bank account number or IBAN (max 80 chars). Optional — omit it entirely if the full number is unavailable or cannot be shared; the account is created without one. A partial or masked value such as &lt;code&gt;****5399&lt;/code&gt; is rejected.</summary>
    [StringLength(80)]
    public string? BankAccountNumber { get; init; }

    /// <summary>Universal color name. Randomly assigned if omitted.</summary>
    [WalletAllowedValues("Blue", "Charcoal", "CoffeeBrown", "DarkPurple", "Green", "LightBlue", "Lime", "Orange", "PineGreen", "Pink", "Purple", "Red", "Stone", "Turquoise", "Wine", "Yellow")]
    public string? Color { get; init; }

    /// <summary>3-letter fiat currency code</summary>
    [Required]
    public required string CurrencyCode { get; init; }

    /// <summary>excludeFromStats value.</summary>
    public bool? ExcludeFromStats { get; init; }

    /// <summary>Initial balance amount (e.g. 1500.50). INPUT ONLY — it is not returned on the account object; read it back as balance.initial (with balance.currencyCode alongside).</summary>
    public required decimal InitialBalance { get; init; }

    /// <summary>name value.</summary>
    [Required]
    [StringLength(80)]
    public required string Name { get; init; }

}
