using Wallet.Net.Models;
using Wallet.Net.Models.Categories;

namespace Wallet.Net.Services
{
    public interface ICategoryService
    {
        /// <summary>Creates a custom subcategory under a base category. No automatic retries are performed.</summary>
        /// <param name="request">Creation request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Creation response, including hints and write summary.</returns>
        Task<CreateCategoryResponse> CreateCategoryAsync(CreateSubcategoryRequest request, CancellationToken cancellationToken = default);

        /// <summary>Updates between 1 and 10 categories. Omitted fields remain unchanged.</summary>
        /// <param name="requests">Partial updates.</param>
        /// <param name="options">Write options.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Per-item results; HTTP 207 is returned normally. Batch 400/500 throws a typed WalletBatchException retaining the results.</returns>
        Task<PatchCategoriesResponse> PatchCategoriesAsync(IReadOnlyList<PatchCategoryRequest> requests, WriteOptions? options = null, CancellationToken cancellationToken = default);
        Task<WalletPage<Category>> GetCategoriesAsync(CategoryListOptions? options = null, CancellationToken cancellationToken = default);
    }
}

