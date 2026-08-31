using Wallet.Net.Models;
using Wallet.Net.Models.Accounts;

namespace Wallet.Net.Services
{
    public interface IAccountService
    {
        Task<WalletPage<Account>> GetAccountsAsync(AccountListOptions? options = null, CancellationToken cancellationToken = default);
    }
}

