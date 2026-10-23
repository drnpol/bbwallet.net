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

/// <summary>DeleteRequest API contract.</summary>
public sealed record DeleteRequest
{
    /// <summary>ids value.</summary>
    [Required]
    [MaxLength(10)]
    [MinLength(1)]
    public required IReadOnlyList<string> Ids { get; init; }

}
