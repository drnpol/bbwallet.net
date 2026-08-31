using Wallet.Net.Models;
using Wallet.Net.Models.Budgets;

namespace Wallet.Net.Services
{
    public interface IBudgetService
    {
        Task<WalletPage<Budget>> GetBudgetsAsync(BudgetListOptions? options = null, CancellationToken cancellationToken = default);
    }
}

