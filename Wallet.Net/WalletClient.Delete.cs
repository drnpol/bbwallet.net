using RestSharp;
using Wallet.Net.Http;
using Wallet.Net.Models;

namespace Wallet.Net;

public partial class WalletClient
{
    /// <inheritdoc />
    public async Task<DeleteResponse> DeleteAsync(DeleteEntityType type, IReadOnlyList<string> ids, CancellationToken cancellationToken = default)
    {
        var path = type switch
        {
            DeleteEntityType.Records => "records",
            DeleteEntityType.Budgets => "budgets",
            DeleteEntityType.StandingOrders => "standing-orders",
            DeleteEntityType.RecordRules => "record-rules",
            DeleteEntityType.Categories => "categories",
            DeleteEntityType.Accounts => "accounts",
            DeleteEntityType.Labels => "labels",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unsupported deletion type.")
        };
        var snapshot = RequestValidation.Batch(ids, 10);
        if (snapshot.Any(string.IsNullOrWhiteSpace)) throw new ArgumentException("IDs cannot be blank.", nameof(ids));
        return (await _executor.SendAsync<DeleteResponse>($"/v1/api/{path}", Method.Delete,
            new DeleteRequest { Ids = snapshot }, cancellationToken: cancellationToken).ConfigureAwait(false)).Data;
    }
}
