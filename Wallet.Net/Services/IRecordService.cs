using Wallet.Net.Models;
using Wallet.Net.Models.Records;

namespace Wallet.Net.Services
{
    public interface IRecordService
    {
        /// <summary>Creates up to 50 transaction records. No automatic retries are performed.</summary>
        /// <param name="requests">Creation requests.</param>
        /// <param name="options">Controls whether full records are returned.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Creation response, including hints and write summary.</returns>
        Task<CreateRecordsResponse> CreateRecordsAsync(IReadOnlyList<CreateRecordRequest> requests, WriteOptions? options = null, CancellationToken cancellationToken = default);

        /// <summary>Updates between 1 and 10 records. Omitted fields remain unchanged.</summary>
        /// <param name="requests">Partial updates.</param>
        /// <param name="options">Write options.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Per-item results; HTTP 207 is returned normally. Batch 400/500 throws a typed WalletBatchException retaining the results.</returns>
        Task<PatchRecordsResponse> PatchRecordsAsync(IReadOnlyList<PatchRecordItem> requests, RecordPatchOptions? options = null, CancellationToken cancellationToken = default);
        Task<WalletPage<Record>> GetRecordsAsync(RecordListOptions? options = null, CancellationToken cancellationToken = default);
    }
}

