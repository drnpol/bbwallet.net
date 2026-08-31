using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Wallet.Net.Configuration;
using Wallet.Net.Http;
using Wallet.Net.Services;

namespace Wallet.Net;

/// <summary>
/// Dependency injection helpers for the Wallet API client.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the Wallet API read client using configuration from the default Wallet section.
    /// </summary>
    public static IServiceCollection AddWalletClient(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services
            .AddOptions<WalletClientOptions>()
            .Bind(configuration.GetRequiredSection("BudgetBakers").GetRequiredSection("Wallet"))
            .Validate(options => options.BaseUrl.IsAbsoluteUri, "Wallet base URL must be absolute.")
            .Validate(options => !string.IsNullOrWhiteSpace(options.AccessToken), "Wallet access token is required.");

        return services.AddWalletClient();
    }

    private static IServiceCollection AddWalletClient(this IServiceCollection services)
    {
        services.AddHttpClient("Wallet.Net");

        services.AddTransient<IRestClient>(serviceProvider =>
        {
            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            var options = serviceProvider.GetRequiredService<IOptions<WalletClientOptions>>().Value;
            var httpClient = httpClientFactory.CreateClient("Wallet.Net");
            httpClient.BaseAddress = options.BaseUrl;
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", options.AccessToken);

            var restOptions = new RestClientOptions
            {
                BaseUrl = options.BaseUrl,
                FailOnDeserializationError = true
            };

            return new RestClient(
                httpClient,
                restOptions,
                disposeHttpClient: false,
                configureSerialization: serializer => serializer.UseNewtonsoftJson(WalletRequestExecutor.CreateSerializerSettings()));
        });

        services.AddTransient<WalletRequestExecutor>();
        services.AddTransient<IWalletClient, WalletClient>();

        return services;
    }
}
