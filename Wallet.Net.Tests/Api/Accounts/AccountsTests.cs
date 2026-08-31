using System.Net;
using Xunit.Abstractions;
using Wallet.Net.Models.Accounts;
using Wallet.Net.Exceptions;
using Wallet.Net.Tests.Fixtures;

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

        // [Theory]
        // [MemberData(nameof(AccountTestData.ListAccountTestData), MemberType = typeof(AccountTestData))]
        public async Task GetAccounts_Should_Return_Lists_Of_Accounts(ListAccountsTestModel model)
        {

            this._outputHelper.WriteLine(model.Title);
            this._outputHelper.WriteLine(model.Description);

            var result = false;

            var response = await this._fixture.Client.GetAccountsAsync(model.AccountListOptions);

            response.Items.Should().NotBeEmpty();

            for (var i = 0; i < response.Items.Count; i++)
            {
                var item = response.Items[i];
                this._outputHelper.WriteLine($"Account={item.Name} found.");
            }

            result = true;

            result.Should().Be(model.ExpectedResult, model.TestComment);
        }
        // [Fact]
        public async Task GetAccounts_Should_Return_List_Of_ArchivedAccounts()
        {
            // Arrange
            AccountListOptions options = new AccountListOptions()
            {
                Archived = true
            };

            var result = await this._fixture.Client.GetAccountsAsync(options);

            result.Items.Should().NotBeEmpty();

            for (var i = 0; i < result.Items.Count; i++)
            {
                var item = result.Items[i];
                item.Archived.Should().BeTrue();
                this._outputHelper.WriteLine($"Account={item.Name} found.");
            }
        }

        // Mocked Results

        [Fact]
        public async Task GetAccountsAsync_Should_Call_Accounts_Endpoint_And_Map_Response()
        {
            // Arrange
            var apiHandler = new MockWalletApiHandler()
                .WhenGet("/v1/api/accounts", MockWalletClientFactory.LoadProviderResponse("accounts.json"));
            var client = MockWalletClientFactory.Create(apiHandler);

            // Act
            var response = await client.GetAccountsAsync();

            // Assert
            apiHandler.Requests.Should().ContainSingle();
            apiHandler.Requests[0].RequestUri!.PathAndQuery.Should().Be("/v1/api/accounts");

            response.Items.Should().HaveCount(3);
            response.Limit.Should().Be(3);
            response.Offset.Should().Be(0);
            response.NextOffset.Should().BeNull();
            response.Total.Should().Be(3);
            response.AgentHints.Should().ContainSingle();

            response.Items[0].Id.Should().Be("11111111-1111-4111-8111-111111111111");
            response.Items[0].Name.Should().Be("Everyday Checking");
            response.Items[0].AccountType.Should().Be("CurrentAccount");
            response.Items[0].Balance!.CurrentBalance.Should().Be(3343.75);

            response.Items.Should().Contain(account => account.Archived);
            response.Items.Should().Contain(account => account.AccountType == "CreditCard");
        }

        [Fact]
        public async Task GetAccountsAsync_Should_Send_List_Options_As_CamelCase_Query_Parameters()
        {
            // Arrange
            var apiHandler = new MockWalletApiHandler()
                .WhenGet("/v1/api/accounts", MockWalletClientFactory.LoadProviderResponse("accounts.json"));
            var client = MockWalletClientFactory.Create(apiHandler);
            var options = new AccountListOptions
            {
                Limit = 2,
                Offset = 1,
                AgentHints = true,
                WithTotal = true,
                AccountType = "CreditCard",
                CurrencyCode = "USD",
                Archived = false,
                BudgetId = "budget-123"
            };

            // Act
            var response = await client.GetAccountsAsync(options);

            // Assert
            response.Items.Should().NotBeEmpty();
            apiHandler.Requests.Should().ContainSingle();
            apiHandler.Requests[0].RequestUri!.AbsolutePath.Should().Be("/v1/api/accounts");

            var query = QueryParameterReader.Read(apiHandler.Requests[0].RequestUri!);
            query.Should().Contain("limit", "2");
            query.Should().Contain("offset", "1");
            query.Should().Contain("agentHints", "true");
            query.Should().Contain("withTotal", "true");
            query.Should().Contain("accountType", "CreditCard");
            query.Should().Contain("currencyCode", "USD");
            query.Should().Contain("archived", "false");
            query.Should().Contain("budgetId", "budget-123");
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized, typeof(WalletAuthenticationException))]
        [InlineData(HttpStatusCode.Forbidden, typeof(WalletAuthenticationException))]
        [InlineData(HttpStatusCode.TooManyRequests, typeof(WalletRateLimitException))]
        [InlineData(HttpStatusCode.BadRequest, typeof(WalletValidationException))]
        [InlineData(HttpStatusCode.UnprocessableEntity, typeof(WalletValidationException))]
        public async Task GetAccountsAsync_Should_Map_Provider_Errors_To_Wallet_Exceptions(
            HttpStatusCode statusCode,
            Type expectedExceptionType)
        {
            // Arrange
            var apiHandler = new MockWalletApiHandler()
                .WhenGet("/v1/api/accounts", """{"message":"Mock provider failure."}""", statusCode);
            var client = MockWalletClientFactory.Create(apiHandler);

            // Act
            var action = () => client.GetAccountsAsync();

            // Assert
            var exception = await action.Should().ThrowAsync<WalletApiException>();
            exception.Which.Should().BeOfType(expectedExceptionType);
            exception.Which.StatusCode.Should().Be(statusCode);
        }
    }
}
