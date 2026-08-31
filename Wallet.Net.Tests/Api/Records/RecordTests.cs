using Xunit.Abstractions;
using Wallet.Net.Models.Records;
using Wallet.Net.Tests.Fixtures;

namespace Wallet.Net.Tests.Api.Records
{
    [Collection("WalletAPITestCollection")]
    public class RecordTests
    {
        protected readonly WalletTestFixture _fixture;
        protected readonly ITestOutputHelper _outputHelper;
        public RecordTests(
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

        // Mocked Results
        
        [Fact]
        public async Task GetRecordsAsync_Should_Call_Records_Endpoint_And_Map_Response()
        {
            // Arrange
            var apiHandler = new MockWalletApiHandler()
                .WhenGet("/v1/api/records", MockWalletClientFactory.LoadProviderResponse("records.json"));
            var client = MockWalletClientFactory.Create(apiHandler);

            // Act
            var response = await client.GetRecordsAsync();

            // Assert
            apiHandler.Requests.Should().ContainSingle();
            apiHandler.Requests[0].RequestUri!.PathAndQuery.Should().Be("/v1/api/records");

            response.Items.Should().HaveCount(3);
            response.Limit.Should().Be(3);
            response.Offset.Should().Be(0);
            response.NextOffset.Should().BeNull();
            response.Total.Should().Be(3);
            response.AgentHints.Should().ContainSingle();

            response.Items[0].Id.Should().Be("aaaaaaaa-aaaa-4aaa-8aaa-aaaaaaaaaaaa");
            response.Items[0].RecordType.Should().Be("expense");
            response.Items[0].Amount!.CurrencyCode.Should().Be("USD");
            response.Items[0].Amount!.Value.Should().Be(-42.5);
            response.Items[0].Category!.Name.Should().Be("Restaurants");
            response.Items[0].Labels.Should().ContainSingle(label => label.Name == "Business");
            response.Items[0].Photos.Should().ContainSingle();
            response.Items[0].Place!.Name.Should().Be("Sample Bistro");

            response.Items.Should().Contain(record => record.RecordType == "income");
            response.Items.Should().Contain(record => record.Transfer != null);
        }

        [Fact]
        public async Task GetRecordsAsync_Should_Send_List_Options_As_CamelCase_Query_Parameters()
        {
            // Arrange
            var apiHandler = new MockWalletApiHandler()
                .WhenGet("/v1/api/records", MockWalletClientFactory.LoadProviderResponse("records.json"));
            var client = MockWalletClientFactory.Create(apiHandler);
            var options = new RecordListOptions
            {
                Limit = 2,
                Offset = 1,
                AgentHints = true,
                WithTotal = true,
                AccountId = "11111111-1111-4111-8111-111111111111",
                RecordDate = "gte.2026-08-01T00:00:00Z",
                CategoryId = "44444444-4444-4444-8444-444444444444",
                LabelId = "55555555-5555-4555-8555-555555555555",
                Note = "contains-i.lunch",
                CounterParty = "contains-i.Sample",
                Amount = "lt.0",
                RecordType = "expense",
                IsTransfer = false,
                RecordState = "cleared",
                Source = "rest",
                ConvertTo = "USD"
            };

            // Act
            var response = await client.GetRecordsAsync(options);

            // Assert
            response.Items.Should().NotBeEmpty();
            apiHandler.Requests.Should().ContainSingle();
            apiHandler.Requests[0].RequestUri!.AbsolutePath.Should().Be("/v1/api/records");

            var query = QueryParameterReader.Read(apiHandler.Requests[0].RequestUri!);
            query.Should().Contain("limit", "2");
            query.Should().Contain("offset", "1");
            query.Should().Contain("agentHints", "true");
            query.Should().Contain("withTotal", "true");
            query.Should().Contain("accountId", "11111111-1111-4111-8111-111111111111");
            query.Should().Contain("recordDate", "gte.2026-08-01T00:00:00Z");
            query.Should().Contain("categoryId", "44444444-4444-4444-8444-444444444444");
            query.Should().Contain("labelId", "55555555-5555-4555-8555-555555555555");
            query.Should().Contain("note", "contains-i.lunch");
            query.Should().Contain("counterParty", "contains-i.Sample");
            query.Should().Contain("amount", "lt.0");
            query.Should().Contain("recordType", "expense");
            query.Should().Contain("isTransfer", "false");
            query.Should().Contain("recordState", "cleared");
            query.Should().Contain("source", "rest");
            query.Should().Contain("convertTo", "USD");
        }

        [Fact]
        public async Task GetRecordsAsync_Should_Map_Paired_Transfer_Response()
        {
            // Arrange
            var apiHandler = new MockWalletApiHandler()
                .WhenGet("/v1/api/records", MockWalletClientFactory.LoadProviderResponse("records.json"));
            var client = MockWalletClientFactory.Create(apiHandler);

            // Act
            var response = await client.GetRecordsAsync();

            // Assert
            var transferRecord = response.Items.Single(record => record.Transfer is not null);
            transferRecord.Id.Should().Be("cccccccc-cccc-4ccc-8ccc-cccccccccccc");
            transferRecord.Transfer!.Type.Should().Be("paired");
            transferRecord.Transfer.TransferId.Should().Be("eeeeeeee-eeee-4eee-8eee-eeeeeeeeeeee");
            transferRecord.Transfer.MirrorRecord!.Id.Should().Be("dddddddd-dddd-4ddd-8ddd-dddddddddddd");
            transferRecord.Transfer.MirrorRecord.Amount!.CurrencyCode.Should().Be("EUR");
            transferRecord.Transfer.MirrorRecord.Amount.Value.Should().Be(460.0);
        }
    }
}
