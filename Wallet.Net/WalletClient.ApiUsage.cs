using System.Globalization;
using System.Text.RegularExpressions;
using Wallet.Net.Models.ApiUsage;

namespace Wallet.Net;

public partial class WalletClient
{
    /// <inheritdoc />
    public Task<APIUsageStats> GetApiUsageStatsAsync(string period, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(period);
        var match = Regex.Match(period, @"\A([1-9][0-9]{0,2})(days|weeks|months)\z", RegexOptions.CultureInvariant);
        if (!match.Success || int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture) >
            (match.Groups[2].Value switch { "days" => 366, "weeks" => 53, _ => 13 }))
            throw new ArgumentException("Use 1–366 days, 1–53 weeks, or 1–13 months, e.g. 30days.", nameof(period));
        return _executor.GetAsync<APIUsageStats>("/v1/api/api-usage/stats", new { period }, cancellationToken);
    }
}
