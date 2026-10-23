namespace Wallet.Net.Models;

/// <summary>Wallet entity types supported by batch deletion. Goals cannot be deleted through this API.</summary>
public enum DeleteEntityType
{
    /// <summary>Transaction records.</summary>
    Records,
    /// <summary>Budgets.</summary>
    Budgets,
    /// <summary>Standing orders.</summary>
    StandingOrders,
    /// <summary>Record rules.</summary>
    RecordRules,
    /// <summary>Categories.</summary>
    Categories,
    /// <summary>Accounts.</summary>
    Accounts,
    /// <summary>Labels.</summary>
    Labels
}
