// DTOs generated from budgetbakers-wallet-api-v2.1.0.json; see scripts/Generate-WriteModels.cjs.
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Wallet.Net.Models;
using Wallet.Net.Models.Accounts;
using Wallet.Net.Models.Budgets;
using Wallet.Net.Models.Categories;
using Wallet.Net.Models.Labels;
using Record = Wallet.Net.Models.Records.Record;

namespace Wallet.Net.Models.Categories;

/// <summary>PatchCategoryRequest API contract.</summary>
public sealed record PatchCategoryRequest
{
    /// <summary>New cardinality ('none' to clear)</summary>
    [WalletAllowedValues("none", "must", "need", "want")]
    public string? Cardinality { get; init; }

    /// <summary>Universal color name.</summary>
    [WalletAllowedValues("Blue", "Charcoal", "CoffeeBrown", "DarkPurple", "Green", "LightBlue", "Lime", "Orange", "PineGreen", "Pink", "Purple", "Red", "Stone", "Turquoise", "Wine", "Yellow")]
    public string? Color { get; init; }

    /// <summary>Category ID (system or custom)</summary>
    [Required]
    public required string Id { get; init; }

    /// <summary>New name (sets customName=true). Cannot be used together with reset:["name"].</summary>
    [StringLength(80)]
    public string? Name { get; init; }

    /// <summary>Restore fields to the base (system) category default. Base categories only — custom subcategories have no defaults. A field cannot be both set and reset in the same call. reset:["color"] restores the colour of the category's ENVELOPE GROUP, shared by every category in that group — not the colour this category had before you changed it. A colour that arrived from one of the mobile or web apps outside the 16 universal colours is therefore not recoverable: reset writes the group colour, and the original cannot be written back because color only accepts the 16. Read the current colour first if you may want it again.</summary>
    public IReadOnlyList<string>? Reset { get; init; }

}
