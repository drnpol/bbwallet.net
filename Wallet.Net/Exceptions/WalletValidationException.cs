using System.Net;

namespace Wallet.Net.Exceptions;

/// <summary>
/// Exception for Wallet API validation failures.
/// </summary>
public sealed class WalletValidationException : WalletApiException
{
    public WalletValidationException(string message, HttpStatusCode? statusCode = null, string? responseContent = null)
        : base(message, statusCode, responseContent)
    {
    }
}
