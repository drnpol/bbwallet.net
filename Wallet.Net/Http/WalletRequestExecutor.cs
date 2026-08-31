using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using Wallet.Net.Exceptions;
using Wallet.Net.Models;

namespace Wallet.Net.Http;

/// <summary>
/// Executes Wallet API requests and maps API failures to typed exceptions.
/// </summary>
public sealed class WalletRequestExecutor(IRestClient restClient)
{
    private static readonly JsonSerializerSettings ErrorSerializerSettings = CreateSerializerSettings();

    /// <summary>
    /// Creates the Newtonsoft serializer settings used by the Wallet client.
    /// </summary>
    public static JsonSerializerSettings CreateSerializerSettings() => new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        NullValueHandling = NullValueHandling.Ignore
    };

    /// <summary>
    /// Executes a GET request and deserializes the response.
    /// </summary>
    public async Task<TResponse> GetAsync<TResponse>(
        string resource,
        object? query = null,
        CancellationToken cancellationToken = default)
    {
        var request = new RestRequest(resource, Method.Get);
        AddQueryParameters(request, query);

        var response = await restClient.ExecuteAsync<TResponse>(request, cancellationToken).ConfigureAwait(false);

        if (response.IsSuccessful && response.Data is not null)
        {
            return response.Data;
        }

        throw CreateException(response);
    }

    private static void AddQueryParameters(RestRequest request, object? query)
    {
        if (query is null)
        {
            return;
        }

        foreach (var property in query.GetType().GetProperties())
        {
            var value = property.GetValue(query);
            if (value is null)
            {
                continue;
            }

            request.AddQueryParameter(ToCamelCase(property.Name), FormatValue(value));
        }
    }

    private static string FormatValue(object value) =>
        value switch
        {
            bool boolean => boolean ? "true" : "false",
            DateTimeOffset dateTime => dateTime.ToString("O"),
            _ => Convert.ToString(value, System.Globalization.CultureInfo.InvariantCulture) ?? string.Empty
        };

    private static string ToCamelCase(string value) =>
        string.IsNullOrWhiteSpace(value) || char.IsLower(value[0])
            ? value
            : char.ToLowerInvariant(value[0]) + value[1..];

    private static WalletApiException CreateException(RestResponse response)
    {
        var statusCode = response.StatusCode;
        var responseContent = response.Content;
        var message = TryReadErrorMessage(responseContent) ?? response.ErrorMessage ?? $"Wallet API request failed with status code {(int)statusCode}.";

        return statusCode switch
        {
            HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden => new WalletAuthenticationException(message, statusCode, responseContent),
            HttpStatusCode.TooManyRequests => new WalletRateLimitException(message, statusCode, responseContent),
            HttpStatusCode.BadRequest or HttpStatusCode.UnprocessableEntity => new WalletValidationException(message, statusCode, responseContent),
            _ => new WalletApiException(message, statusCode, responseContent, response.ErrorException)
        };
    }

    private static string? TryReadErrorMessage(string? responseContent)
    {
        if (string.IsNullOrWhiteSpace(responseContent))
        {
            return null;
        }

        try
        {
            return JsonConvert.DeserializeObject<Error>(responseContent, ErrorSerializerSettings)?.Message;
        }
        catch (JsonException)
        {
            return null;
        }
    }
}
