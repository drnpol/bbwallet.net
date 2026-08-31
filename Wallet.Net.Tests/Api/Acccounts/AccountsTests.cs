using NSubstitute.Core;
using Wallet.Net.Models.Accounts;
using Xunit.Abstractions;

namespace Wallet.Net.Tests.Api.Accounts
{
    [Collection("WalletAPITestCollection")]
    public class AccountTests
    {
        protected readonly WalletTestFixture _fixture;
        protected readonly ITestOutputHelper _outputHelper;
        public AccountTests(
            WalletTestFixture fixture,
            ITestOutputHelper outputHelper
        )
        {
            _fixture = fixture;
            _outputHelper = outputHelper;
        }
        [Fact]
        public void ExampleTest_Should_Pass()
        {
            // Arrange

            // Act

            // Assert
            Assert.True(true);
        }
        
        [Theory]
        [MemberData(nameof(AccountTestData.ListAccountTestData), MemberType = typeof(AccountTestData))]
        public async Task GetAccounts_Should_Return_Lists_Of_Accounts(ListAccountsTestModel model)
        { 

            this._outputHelper.WriteLine(model.Title);
            this._outputHelper.WriteLine(model.Description);

            var result = false;

            var response = await this._fixture.Client.GetAccountsAsync(model.AccountListOptions);

            response.Items.Should().NotBeEmpty();

            for(var i = 0; i < response.Items.Count; i++)
            {
                var item = response.Items[i];
                this._outputHelper.WriteLine($"Account={item.Name} found.");
            }

            result = true;
            
            result.Should().Be(model.ExpectedResult, model.TestComment);
        }
        [Fact]
        public async Task GetAccounts_Should_Return_List_Of_ArchivedAccounts()
        {
            // Arrange
            AccountListOptions options = new AccountListOptions()
            {
                Archived = true
            };

            var result = await this._fixture.Client.GetAccountsAsync(options);
            
            result.Items.Should().NotBeEmpty();

            for(var i = 0; i <result.Items.Count; i++)
            {
                var item = result.Items[i];
                item.Archived.Should().BeTrue();
                this._outputHelper.WriteLine($"Account={item.Name} found.");
            }
        }
    }
}
