// DTOs generated from budgetbakers-wallet-api-v2.1.0.json; see scripts/Generate-WriteModels.cjs.
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Wallet.Net.Models;
using Wallet.Net.Models.Accounts;
using Wallet.Net.Models.Budgets;
using Wallet.Net.Models.Categories;
using Wallet.Net.Models.Labels;
using Record = Wallet.Net.Models.Records.Record;

namespace Wallet.Net.Models.Labels;

/// <summary>CreateLabelRequest API contract.</summary>
public sealed record CreateLabelRequest
{
    /// <summary>Universal color name. Random color assigned if omitted.</summary>
    [WalletAllowedValues("Blue", "Charcoal", "CoffeeBrown", "DarkPurple", "Green", "LightBlue", "Lime", "Orange", "PineGreen", "Pink", "Purple", "Red", "Stone", "Turquoise", "Wine", "Yellow")]
    public string? Color { get; init; }

    /// <summary>Label display name</summary>
    [Required]
    [StringLength(80)]
    public required string Name { get; init; }

}
