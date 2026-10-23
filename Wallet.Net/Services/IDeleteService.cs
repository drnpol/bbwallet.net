using Wallet.Net.Models;

namespace Wallet.Net.Services;

/// <summary>Batch deletion of Wallet entities.</summary>
public interface IDeleteService
{
    /// <summary>Deletes up to 10 IDs. Check references first where applicable. Paired transfer legs must be sent together.</summary>
    /// <param name="type">Entity collection.</param>
    /// <param name="ids">IDs to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Per-input results and confirmed documents removed. Batch 400/500 throws a typed WalletBatchException retaining the results.</returns>
    Task<DeleteResponse> DeleteAsync(DeleteEntityType type, IReadOnlyList<string> ids, CancellationToken cancellationToken = default);
}
