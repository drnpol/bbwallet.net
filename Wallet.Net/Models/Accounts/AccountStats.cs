using Wallet.Net.Models;

namespace Wallet.Net.Models.Accounts;

/// <summary>
/// Account record statistics.
/// </summary>
public sealed record AccountStats
{
    public StatDateRange? CreatedAt { get; init; }
    public string? Error { get; init; }
    public string? ErrorAt { get; init; }
    public string? LastUpdatedAt { get; init; }
    public int? RecordCount { get; init; }
    public StatDateRange? RecordDate { get; init; }
    public double? TotalExpenses { get; init; }
    public double? TotalIncomes { get; init; }
}
