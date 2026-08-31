using System.Net;

namespace Wallet.Net.Exceptions;

/// <summary>
/// Exception for Wallet authentication and authorization failures.
/// </summary>
public sealed class WalletAuthenticationException : WalletApiException
{
    public WalletAuthenticationException(string message, HttpStatusCode? statusCode = null, string? responseContent = null)
        : base(message, statusCode, responseContent)
    {
    }
}
