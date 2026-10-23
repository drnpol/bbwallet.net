using System.Net;
using Wallet.Net.Models;

namespace Wallet.Net.Exceptions;

/// <summary>A failed batch request retaining the server's complete per-item result body.</summary>
/// <typeparam name="TResponse">Typed batch response.</typeparam>
public sealed class WalletBatchException<TResponse> : WalletApiException
{
    /// <summary>Creates a failed batch exception.</summary>
    /// <param name="result">Complete batch response.</param>
    /// <param name="statusCode">HTTP status.</param>
    /// <param name="responseContent">Raw JSON body.</param>
    public WalletBatchException(TResponse result, HttpStatusCode statusCode, string? responseContent)
        : base($"Wallet batch request failed with status code {(int)statusCode}; inspect Result for per-item outcomes.", statusCode, responseContent)
        => Result = result;

    /// <summary>Per-item results and summary, including any confirmed writes.</summary>
    public TResponse Result { get; }
}
