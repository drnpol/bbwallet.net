using Wallet.Net.Models;
using Wallet.Net.Models.Accounts;
using Wallet.Net.Models.Budgets;
using Wallet.Net.Models.Categories;
using Wallet.Net.Models.Goals;
using Wallet.Net.Models.Labels;
using Wallet.Net.Models.RecordRules;
using Wallet.Net.Models.Records;
using Wallet.Net.Models.StandingOrders;
using Wallet.Net.Services;

namespace Wallet.Net
{
    public partial class WalletClient : IAccountService
    {
        public async Task<WalletPage<Account>> GetAccountsAsync(AccountListOptions? options = null, CancellationToken cancellationToken = default)
        {
            var response = await _executor.GetAsync<AccountsResponse>("/v1/api/accounts", options, cancellationToken).ConfigureAwait(false);
            return ToPage(response, response.Accounts);
        }
    }
}