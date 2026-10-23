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

/// <summary>Location data. Full replace on PATCH (no merge). Send empty object to erase, or use $clear with place.</summary>
public sealed record PlaceInput
{
    /// <summary>Street/full address</summary>
    [StringLength(255)]
    public string? Address { get; init; }

    /// <summary>GPS latitude</summary>
    [Range(typeof(decimal), "-90", "90")]
    public decimal? Latitude { get; init; }

    /// <summary>GPS longitude</summary>
    [Range(typeof(decimal), "-180", "180")]
    public decimal? Longitude { get; init; }

    /// <summary>Place name</summary>
    [StringLength(255)]
    public string? Name { get; init; }

}
