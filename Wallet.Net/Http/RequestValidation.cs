using Wallet.Net.Models.Categories;
using Wallet.Net.Models.Records;
using Wallet.Net.Models.Budgets;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Wallet.Net.Models;

namespace Wallet.Net.Http;

internal static class RequestValidation
{
    internal static void Validate(object value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value is string || value.GetType().IsValueType) return;
        if (value is IEnumerable items)
        {
            foreach (var item in items)
            {
                if (item is null) throw new ArgumentException("Collections cannot contain null items.");
                Validate(item);
            }
            return;
        }
        Validator.ValidateObject(value, new ValidationContext(value), validateAllProperties: true);
        foreach (var property in value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var child = property.GetValue(value);
            if (child is not null) Validate(child);
        }
        switch (value)
        {
            case WalletListOptions options:
                if (options.Limit is < 1 or > 200) Invalid("Limit must be between 1 and 200.");
                if (options.Offset is < 0) Invalid("Offset cannot be negative.");
                if (options.Id is { } ids && ids.Split(',').Length > 30) Invalid("At most 30 IDs are allowed.");
                break;
            case RecordAmountInput amount when amount.Value == 0:
                Invalid("Record amount must not be zero."); break;
            case CreateRecordRequest record:
                if (record.RecordDate > DateTimeOffset.UtcNow.AddHours(24)) Invalid("Record date cannot be more than 24 hours in the future.");
                if (record.Transfer?.CounterAmount is { } counter && Math.Sign(counter.Value) != -Math.Sign(record.Amount.Value))
                    Invalid("Counter amount must have the opposite sign to the record amount.");
                break;
            case TransferInput transfer:
                if (string.Equals(transfer.PairingMode, "new", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(transfer.AccountId))
                    Invalid("New transfers require a target account ID.");
                if (string.Equals(transfer.PairingMode, "existing", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(transfer.RecordId))
                    Invalid("Existing transfers require a target record ID.");
                break;
            case CreateBudgetRequest budget:
                if (budget.Limit <= 0) Invalid("Budget limit must be positive.");
                if (string.Equals(budget.Type, "BUDGET_CUSTOM", StringComparison.OrdinalIgnoreCase) && (budget.StartDate is null || budget.EndDate is null))
                    Invalid("Custom budgets require start and end dates.");
                if (budget.StartDate > budget.EndDate) Invalid("Budget start date cannot follow its end date.");
                break;
            case LimitOverrideInput entry:
                if (entry.Limit is <= 0) Invalid("Override limit must be positive.");
                if (entry.SetBaseline == true && (entry.Limit is not null || entry.PeriodCount is not null))
                    Invalid("setBaseline cannot be combined with limit or periodCount.");
                if (entry.Limit is null && entry.SetBaseline != true) Invalid("An override requires limit or setBaseline=true.");
                break;
            case PatchBudgetRequest budget:
                if (budget.ResetLimit is <= 0) Invalid("Reset limit must be positive.");
                if (budget.StartDate > budget.EndDate) Invalid("Budget start date cannot follow its end date.");
                break;
            case PatchRecordItem record:
                ValidateClears(record, record.Clear, ["categoryId", "labelIds", "note", "counterParty", "place", "transfer"]);
                break;
            case PatchCategoryRequest category:
                ValidateClears(category, category.Reset, ["name", "color", "cardinality"]);
                break;
        }
    }

    private static void ValidateClears(object value, IReadOnlyList<string>? fields, string[] allowed)
    {
        foreach (var field in fields ?? [])
        {
            if (!allowed.Contains(field, StringComparer.Ordinal)) Invalid($"Unsupported clear/reset field: {field}.");
            var property = value.GetType().GetProperty(char.ToUpperInvariant(field[0]) + field[1..]);
            if (property?.GetValue(value) is not null) Invalid($"Cannot both set and clear/reset {field}.");
        }
    }

    internal static T[] Batch<T>(IReadOnlyList<T> items, int maximum)
    {
        ArgumentNullException.ThrowIfNull(items);
        var snapshot = items.ToArray();
        if (snapshot.Length < 1 || snapshot.Length > maximum)
            throw new ArgumentException($"Supply between 1 and {maximum} items.", nameof(items));
        Validate(snapshot);
        return snapshot;
    }

    private static void Invalid(string message) => throw new ValidationException(message);
}
