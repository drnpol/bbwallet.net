using Wallet.Net.Models;
using Wallet.Net.Models.Labels;

namespace Wallet.Net.Services
{
    public interface ILabelService
    {
        /// <summary>Creates a label. No automatic retries are performed.</summary>
        /// <param name="request">Creation request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Creation response, including hints and write summary.</returns>
        Task<CreateLabelResponse> CreateLabelAsync(CreateLabelRequest request, CancellationToken cancellationToken = default);

        /// <summary>Updates between 1 and 10 labels. Omitted fields remain unchanged.</summary>
        /// <param name="requests">Partial updates.</param>
        /// <param name="options">Write options.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Per-item results; HTTP 207 is returned normally. Batch 400/500 throws a typed WalletBatchException retaining the results.</returns>
        Task<PatchLabelsResponse> PatchLabelsAsync(IReadOnlyList<PatchLabelItem> requests, WriteOptions? options = null, CancellationToken cancellationToken = default);
        Task<WalletPage<Label>> GetLabelsAsync(LabelListOptions? options = null, CancellationToken cancellationToken = default);
    }
}

