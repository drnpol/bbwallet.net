using Newtonsoft.Json;

namespace Wallet.Net.Models;

/// <summary>Base for response bodies carrying HTTP metadata.</summary>
public abstract record WalletResponse
{
    /// <summary>Headers and status of this response; never serialized into the API body.</summary>
    [JsonIgnore]
    public WalletResponseMetadata Metadata { get; internal set; } = new();
}

/// <summary>Marks a response containing per-input batch outcomes.</summary>
public interface IWalletBatchResponse;

/// <summary>A response body together with metadata, including for dictionary responses.</summary>
/// <typeparam name="T">Response body type.</typeparam>
public sealed record WalletResult<T>(T Data, WalletResponseMetadata Metadata);
