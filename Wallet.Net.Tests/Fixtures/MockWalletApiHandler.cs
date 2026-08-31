using System.Net;
using System.Text;

namespace Wallet.Net.Tests.Fixtures;

public sealed class MockWalletApiHandler : HttpMessageHandler
{
    private readonly Dictionary<string, MockWalletApiResponse> _responses = new(StringComparer.Ordinal);

    public IReadOnlyList<HttpRequestMessage> Requests => _requests;

    private readonly List<HttpRequestMessage> _requests = [];

    public MockWalletApiHandler WhenGet(
        string pathAndQuery,
        string json,
        HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        _responses[pathAndQuery] = new MockWalletApiResponse(statusCode, json);
        return this;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        _requests.Add(request);

        if (request.Method != HttpMethod.Get)
        {
            return Task.FromResult(CreateJsonResponse(
                HttpStatusCode.MethodNotAllowed,
                $$"""{"message":"Mock Wallet API only supports GET requests. Received {{request.Method}}."}"""));
        }

        var pathAndQuery = request.RequestUri?.PathAndQuery ?? string.Empty;

        if (_responses.TryGetValue(pathAndQuery, out var response))
        {
            return Task.FromResult(CreateJsonResponse(response.StatusCode, response.Json));
        }

        var path = request.RequestUri?.AbsolutePath ?? string.Empty;

        if (_responses.TryGetValue(path, out response))
        {
            return Task.FromResult(CreateJsonResponse(response.StatusCode, response.Json));
        }

        return Task.FromResult(CreateJsonResponse(
            HttpStatusCode.NotFound,
            $$"""{"message":"No mock Wallet API response registered for {{pathAndQuery}}."}"""));
    }

    private static HttpResponseMessage CreateJsonResponse(HttpStatusCode statusCode, string json) =>
        new(statusCode)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
}
