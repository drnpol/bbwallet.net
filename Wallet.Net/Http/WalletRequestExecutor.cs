using System.Globalization;
using System.Net;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestSharp;
using Wallet.Net.Configuration;
using Wallet.Net.Exceptions;
using Wallet.Net.Models;

namespace Wallet.Net.Http;

/// <summary>Executes Wallet requests, preserving response metadata and typed batch failures.</summary>
public sealed class WalletRequestExecutor
{
    private readonly IRestClient _restClient;
    private readonly int _defaultPageSize;
    private static readonly JsonSerializerSettings SerializerSettings = CreateSerializerSettings();

    /// <summary>Creates an executor for an injected REST client.</summary>
    /// <param name="restClient">Configured REST client.</param>
    /// <param name="options">Optional defaults; omitted options use the standard client defaults.</param>
    public WalletRequestExecutor(IRestClient restClient, IOptions<WalletClientOptions>? options = null)
    {
        ArgumentNullException.ThrowIfNull(restClient);
        _restClient = restClient;
        _defaultPageSize = options?.Value.DefaultPageSize ?? new WalletClientOptions().DefaultPageSize;
        if (_defaultPageSize is < 1 or > 200) throw new ArgumentOutOfRangeException(nameof(options), "Default page size must be between 1 and 200.");
    }

    /// <summary>Creates the JSON settings shared by requests and responses.</summary>
    /// <returns>Settings using camel case and omitting null input fields.</returns>
    public static JsonSerializerSettings CreateSerializerSettings() => new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        NullValueHandling = NullValueHandling.Ignore,
        DateParseHandling = DateParseHandling.DateTimeOffset
    };

    /// <summary>Executes a GET request.</summary>
    /// <typeparam name="TResponse">Response body type.</typeparam>
    /// <param name="resource">Relative API resource.</param>
    /// <param name="query">Query options.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Deserialized response.</returns>
    public async Task<TResponse> GetAsync<TResponse>(string resource, object? query = null, CancellationToken cancellationToken = default)
        => (await SendAsync<TResponse>(resource, Method.Get, null, query, cancellationToken).ConfigureAwait(false)).Data;

    /// <summary>Executes a request with a JSON body, preserving typed batch failures.</summary>
    /// <typeparam name="TResponse">Response body type.</typeparam>
    /// <param name="resource">Relative API resource.</param>
    /// <param name="method">HTTP method.</param>
    /// <param name="body">Optional JSON body.</param>
    /// <param name="query">Optional query options.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Body and response metadata.</returns>
    public async Task<WalletResult<TResponse>> SendAsync<TResponse>(string resource, Method method, object? body = null,
        object? query = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (query is not null) RequestValidation.Validate(query);
        if (body is not null) RequestValidation.Validate(body);
        var request = new RestRequest(resource, method);
        AddQueryParameters(request, query);
        if (method == Method.Get && typeof(PaginatedResponse).IsAssignableFrom(typeof(TResponse)) &&
            (query as WalletListOptions)?.Limit is null)
            request.AddQueryParameter("limit", _defaultPageSize);
        if (body is not null) request.AddJsonBody(body);

        var response = await _restClient.ExecuteAsync(request, cancellationToken).ConfigureAwait(false);
        cancellationToken.ThrowIfCancellationRequested();
        if (response.ErrorException is OperationCanceledException canceled) throw canceled;
        var metadata = ReadMetadata(response);
        var success = (int)response.StatusCode is >= 200 and < 300;
        var batchFailure = typeof(IWalletBatchResponse).IsAssignableFrom(typeof(TResponse)) &&
            response.StatusCode is HttpStatusCode.BadRequest or HttpStatusCode.InternalServerError;
        if (success || batchFailure)
        {
            try
            {
                var token = JToken.Parse(response.Content ?? "");
                var isBatchBody = token is JObject obj && obj["results"] is JArray && obj["summary"] is JObject;
                if (success || isBatchBody)
                {
                    var data = token.ToObject<TResponse>(JsonSerializer.Create(SerializerSettings));
                    if (data is null) throw new JsonSerializationException("The API returned a null response body.");
                    if (data is WalletResponse walletResponse) walletResponse.Metadata = metadata;
                    if (!success) throw new WalletBatchException<TResponse>(data, response.StatusCode, response.Content) { Metadata = metadata };
                    return new(data, metadata);
                }
            }
            catch (JsonException exception)
            {
                if (success) throw new WalletApiException("The API returned an invalid or empty JSON response.", response.StatusCode, response.Content, exception) { Metadata = metadata };
            }
        }
        throw CreateException(response, metadata);
    }

    private static void AddQueryParameters(RestRequest request, object? query)
    {
        if (query is null) return;
        foreach (var property in query.GetType().GetProperties())
        {
            var value = property.GetValue(query);
            if (value is null) continue;
            var name = char.ToLowerInvariant(property.Name[0]) + property.Name[1..];
            request.AddQueryParameter(name, value switch
            {
                bool boolean => boolean ? "true" : "false",
                DateTimeOffset date => date.ToString("O", CultureInfo.InvariantCulture),
                _ => Convert.ToString(value, CultureInfo.InvariantCulture) ?? ""
            });
        }
    }

    private static WalletResponseMetadata ReadMetadata(RestResponse response)
    {
        string? Header(string name) => response.Headers?.FirstOrDefault(h => string.Equals(h.Name, name, StringComparison.OrdinalIgnoreCase))?.Value?.ToString();
        int? Integer(string name) => int.TryParse(Header(name), NumberStyles.Integer, CultureInfo.InvariantCulture, out var n) ? n : null;
        return new()
        {
            StatusCode = response.StatusCode,
            RateLimit = Integer("X-RateLimit-Limit"),
            RateLimitRemaining = Integer("X-RateLimit-Remaining"),
            RetryAfterSeconds = Integer("Retry-After"),
            LastDataChangeRevision = Header("X-Last-Data-Change-Rev"),
            LastDataChangeAt = DateTimeOffset.TryParse(Header("X-Last-Data-Change-At"), CultureInfo.InvariantCulture, DateTimeStyles.None, out var at) ? at : null,
            SyncInProgress = bool.TryParse(Header("X-Sync-In-Progress"), out var sync) ? sync : null
        };
    }

    private static WalletApiException CreateException(RestResponse response, WalletResponseMetadata metadata)
    {
        string? message = null;
        string? code = null;
        try
        {
            var error = JObject.Parse(response.Content ?? "");
            message = error.Value<string>("error");
            code = error.Value<string>("code");
        }
        catch (JsonException)
        {
            if (response.StatusCode == HttpStatusCode.Forbidden) message = response.Content;
        }
        message ??= response.ErrorMessage ?? $"Wallet API request failed with status code {(int)response.StatusCode}.";
        WalletApiException exception = response.StatusCode switch
        {
            HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden => new WalletAuthenticationException(message, response.StatusCode, response.Content) { Code = code },
            HttpStatusCode.TooManyRequests => new WalletRateLimitException(message, response.StatusCode, response.Content),
            HttpStatusCode.BadRequest or HttpStatusCode.UnprocessableEntity => new WalletValidationException(message, response.StatusCode, response.Content),
            _ => new WalletApiException(message, response.StatusCode, response.Content, response.ErrorException)
        };
        exception.Metadata = metadata;
        return exception;
    }
}
