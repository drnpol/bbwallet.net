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

/// <summary>CreateSubcategoryRequest API contract.</summary>
public sealed record CreateSubcategoryRequest
{
    /// <summary>Defaults to parent's cardinality</summary>
    [WalletAllowedValues("none", "must", "need", "want")]
    public string? Cardinality { get; init; }

    /// <summary>Universal color name. Defaults to parent's color if omitted.</summary>
    [WalletAllowedValues("Blue", "Charcoal", "CoffeeBrown", "DarkPurple", "Green", "LightBlue", "Lime", "Orange", "PineGreen", "Pink", "Purple", "Red", "Stone", "Turquoise", "Wine", "Yellow")]
    public string? Color { get; init; }

    /// <summary>Custom subcategory name</summary>
    [Required]
    [StringLength(80)]
    public required string Name { get; init; }

    /// <summary>Parent base category ID. Use GET /categories to find available parent IDs.</summary>
    [Required]
    public required string ParentId { get; init; }

}
