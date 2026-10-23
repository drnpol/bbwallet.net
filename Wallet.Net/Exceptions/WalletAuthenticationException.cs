using System.Net;

namespace Wallet.Net.Exceptions;

/// <summary>
/// Exception for Wallet authentication and authorization failures.
/// </summary>
public sealed class WalletAuthenticationException : WalletApiException
{
    /// <summary>Structured entitlement code on HTTP 403, or null for other authentication/authorization failures.</summary>
    public string? Code { get; internal set; }
    public WalletAuthenticationException(string message, HttpStatusCode? statusCode = null, string? responseContent = null)
        : base(message, statusCode, responseContent)
    {
    }
}
