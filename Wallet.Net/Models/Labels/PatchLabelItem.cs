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

/// <summary>PatchLabelItem API contract.</summary>
public sealed record PatchLabelItem
{
    /// <summary>Archive status (true/false)</summary>
    public bool? Archived { get; init; }

    /// <summary>Universal color name.</summary>
    [WalletAllowedValues("Blue", "Charcoal", "CoffeeBrown", "DarkPurple", "Green", "LightBlue", "Lime", "Orange", "PineGreen", "Pink", "Purple", "Red", "Stone", "Turquoise", "Wine", "Yellow")]
    public string? Color { get; init; }

    /// <summary>Label ID (UUID)</summary>
    [Required]
    public required string Id { get; init; }

    /// <summary>Label display name</summary>
    [StringLength(80)]
    public string? Name { get; init; }

}
