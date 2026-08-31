using Wallet.Net.Models;
using Wallet.Net.Models.Categories;
using Wallet.Net.Models.Labels;

namespace Wallet.Net.Models.Records;

/// <summary>
/// Wallet transaction record.
/// </summary>
public sealed record Record
{
    public string? AccountId { get; init; }
    public bool? AccountIsBankSync { get; init; }
    public string? AccountName { get; init; }
    public AmountWithCurrency? Amount { get; init; }
    public CategoryEmbed? Category { get; init; }
    public ConvertedAmount? ConvertedAmount { get; init; }
    public string? CounterParty { get; init; }
    public DateTimeOffset? CreatedAt { get; init; }
    public string Id { get; init; } = string.Empty;
    public IReadOnlyList<LabelEmbed> Labels { get; init; } = [];
    public string? Note { get; init; }
    public IReadOnlyList<RecordPhoto> Photos { get; init; } = [];
    public RecordPlace? Place { get; init; }
    public DateTimeOffset? RecordDate { get; init; }
    public string? RecordState { get; init; }
    public string? RecordType { get; init; }
    public string? Source { get; init; }
    public TransferOutput? Transfer { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
}
