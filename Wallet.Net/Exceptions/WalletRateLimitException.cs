using System.Net;

namespace Wallet.Net.Exceptions;

/// <summary>
/// Exception for Wallet API rate-limit failures.
/// </summary>
public sealed class WalletRateLimitException : WalletApiException
{
    public WalletRateLimitException(string message, HttpStatusCode? statusCode = null, string? responseContent = null)
        : base(message, statusCode, responseContent)
    {
    }
}
