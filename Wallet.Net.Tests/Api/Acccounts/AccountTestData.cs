using Wallet.Net.Models.Accounts;
using Wallet.Net.Tests.Api.Accounts;

namespace Wallet.Net.Tests
{
    public partial class AccountTestData
    {
        public static IEnumerable<object[]> ListAccountTestData()
        {
            yield return new object[]
            {
                new ListAccountsTestModel()
                {
                    AccountListOptions = new AccountListOptions()
                    {
                        Archived = false
                    },
                    Title = "List active accounts",
                    Description = "active accounts should be fetched",
                    TestComment = "active accounts should be fetched",
                    ExpectedResult = true
                }
            };
        }
    }
}