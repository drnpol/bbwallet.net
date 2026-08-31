using System.ComponentModel.DataAnnotations;

namespace Wallet.Net.Configuration;

/// <summary>
/// Configuration options for the Budget Bakers Wallet API client.
/// </summary>
public sealed record WalletClientOptions
{

    /// <summary>
    /// Base API URL for Budget Bakers Wallet.
    /// </summary>
    [Required]
    public Uri BaseUrl { get; init; } = new("https://rest.budgetbakers.com/wallet");

    /// <summary>
    /// Bearer access token used for Wallet API requests.
    /// </summary>
    [Required]
    public string AccessToken { get; init; } = string.Empty;

    /// <summary>
    /// Default list page size.
    /// </summary>
    [Range(1, 200)]
    public int DefaultPageSize { get; init; } = 200;
}
