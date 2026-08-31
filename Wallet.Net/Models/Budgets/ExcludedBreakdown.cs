namespace Wallet.Net.Models.Budgets;

/// <summary>
/// Breakdown of records excluded from budget spending.
/// </summary>
public sealed record ExcludedBreakdown
{
    public int Debts { get; init; }
    public int IncomeCategories { get; init; }
    public int Total { get; init; }
    public double? TotalAmountSum { get; init; }
    public int Transfers { get; init; }
    public int UnknownCategories { get; init; }
}
