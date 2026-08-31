using Wallet.Net.Models;
using Wallet.Net.Models.Categories;

using Wallet.Net.Services;

namespace Wallet.Net
{
    public partial class WalletClient : ICategoryService
    {
        public async Task<WalletPage<Category>> GetCategoriesAsync(CategoryListOptions? options = null, CancellationToken cancellationToken = default)
        {
            var response = await _executor.GetAsync<CategoriesResponse>("/v1/api/categories", options, cancellationToken).ConfigureAwait(false);
            return ToPage(response, response.Categories);
        }
    }
}