using System.Net;

namespace Wallet.Net.Exceptions;

/// <summary>
/// Base exception for Wallet API failures.
/// </summary>
public class WalletApiException : Exception
{
    public WalletApiException(string message, HttpStatusCode? statusCode = null, string? responseContent = null, Exception? innerException = null)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        ResponseContent = responseContent;
    }

    /// <summary>
    /// HTTP status code returned by the API.
    /// </summary>
    public HttpStatusCode? StatusCode { get; }

    /// <summary>
    /// Raw API response content.
    /// </summary>
    public string? ResponseContent { get; }
}
