namespace Wallet.Net.Models;

/// <summary>Options for write endpoints supporting omission of returned entity bodies.</summary>
public record WriteOptions
{
    /// <summary>False omits entity bodies; IDs and per-item outcomes are still returned. Null uses the server default (true).</summary>
    public bool? ReturnData { get; init; }
}
