using Wallet.Net.Models;
using Wallet.Net.Models.Budgets;
using Wallet.Net.Models.Goals;
using Wallet.Net.Services;

namespace Wallet.Net
{
    public partial class WalletClient : IBudgetService
    {
        public async Task<WalletPage<Budget>> GetBudgetsAsync(BudgetListOptions? options = null, CancellationToken cancellationToken = default)
        {
            var response = await _executor.GetAsync<BudgetsResponse>("/v1/api/budgets", options, cancellationToken).ConfigureAwait(false);
            return ToPage(response, response.Budgets);
        }
    }
}