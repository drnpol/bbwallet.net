using Wallet.Net.Models.Accounts;
using Wallet.Net.Tests.Models;

namespace Wallet.Net.Tests.Api.Accounts
{
    public class ListAccountsTestModel : ResultBasedTestModel
    {
        public required AccountListOptions AccountListOptions { get; set; }
    }
}
