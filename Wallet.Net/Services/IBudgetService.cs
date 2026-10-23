using Wallet.Net.Models;
using Wallet.Net.Models.Budgets;

namespace Wallet.Net.Services
{
    public interface IBudgetService
    {
        /// <summary>Creates a budget. No automatic retries are performed.</summary>
        /// <param name="request">Creation request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Creation response, including hints and write summary.</returns>
        Task<CreateBudgetResponse> CreateBudgetAsync(CreateBudgetRequest request, CancellationToken cancellationToken = default);

        /// <summary>Updates between 1 and 10 budgets. Omitted fields remain unchanged.</summary>
        /// <param name="requests">Partial updates.</param>
        /// <param name="options">Write options.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Per-item results; HTTP 207 is returned normally. Batch 400/500 throws a typed WalletBatchException retaining the results.</returns>
        Task<PatchBudgetsResponse> PatchBudgetsAsync(IReadOnlyList<PatchBudgetRequest> requests, WriteOptions? options = null, CancellationToken cancellationToken = default);
        Task<WalletPage<Budget>> GetBudgetsAsync(BudgetListOptions? options = null, CancellationToken cancellationToken = default);
    }
}

