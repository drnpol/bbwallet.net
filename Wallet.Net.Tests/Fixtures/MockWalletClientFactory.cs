using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Wallet.Net.Http;

namespace Wallet.Net.Tests.Fixtures;

public static class MockWalletClientFactory
{
    private static readonly Uri BaseUrl = new("https://mock.wallet.local");

    public static IWalletClient Create(MockWalletApiHandler apiHandler)
    {
        var httpClient = new HttpClient(apiHandler)
        {
            BaseAddress = BaseUrl
        };

        var restClient = new RestClient(
            httpClient,
            new RestClientOptions
            {
                BaseUrl = BaseUrl,
                FailOnDeserializationError = true
            },
            disposeHttpClient: true,
            configureSerialization: serializer =>
                serializer.UseNewtonsoftJson(WalletRequestExecutor.CreateSerializerSettings()));

        return new WalletClient(new WalletRequestExecutor(restClient));
    }

    public static string LoadProviderResponse(string fileName)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Data", "v2.0.0", fileName);
        return File.ReadAllText(path);
    }
}
