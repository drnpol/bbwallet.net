namespace Wallet.Net.Tests.Fixtures;

public static class QueryParameterReader
{
    public static IReadOnlyDictionary<string, string> Read(Uri requestUri)
    {
        var query = requestUri.Query;

        if (string.IsNullOrEmpty(query))
        {
            return new Dictionary<string, string>();
        }

        return query[1..]
            .Split('&', StringSplitOptions.RemoveEmptyEntries)
            .Select(parameter => parameter.Split('=', 2))
            .ToDictionary(
                parts => Uri.UnescapeDataString(parts[0]),
                parts => parts.Length == 2 ? Uri.UnescapeDataString(parts[1]) : string.Empty,
                StringComparer.Ordinal);
    }

}
