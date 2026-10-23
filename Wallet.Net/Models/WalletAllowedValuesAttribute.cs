using System.ComponentModel.DataAnnotations;

namespace Wallet.Net.Models;

/// <summary>Validates a documented string value, accepting the casing used by API examples.</summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class WalletAllowedValuesAttribute(params string[] values) : ValidationAttribute
{
    /// <inheritdoc />
    public override bool IsValid(object? value) => value is null ||
        value is string text && values.Contains(text, StringComparer.OrdinalIgnoreCase);

    /// <inheritdoc />
    public override string FormatErrorMessage(string name) => $"{name} must be one of: {string.Join(", ", values)}.";
}
