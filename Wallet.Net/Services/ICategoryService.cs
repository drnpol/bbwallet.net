using Wallet.Net.Models;
using Wallet.Net.Models.Categories;

namespace Wallet.Net.Services
{
    public interface ICategoryService
    {
        Task<WalletPage<Category>> GetCategoriesAsync(CategoryListOptions? options = null, CancellationToken cancellationToken = default);
    }
}

