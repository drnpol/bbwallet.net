using Xunit.Abstractions;

namespace Wallet.Net.Tests.Api
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
    }
}
