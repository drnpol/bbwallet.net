using Wallet.Net.Models.ApiUsage;

namespace Wallet.Net.Services;

/// <summary>API usage statistics.</summary>
public interface IApiUsageService
{
    /// <summary>Gets usage over a documented number of days, weeks, or months.</summary>
    /// <param name="period">1–366 days, 1–53 weeks, or 1–13 months, for example 30days.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Usage totals and time buckets.</returns>
    Task<APIUsageStats> GetApiUsageStatsAsync(string period, CancellationToken cancellationToken = default);
}
