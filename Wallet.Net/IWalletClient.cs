using Wallet.Net.Models;
using Wallet.Net.Services;

namespace Wallet.Net;

public interface IWalletClient 
    // : 
    // IAccountService, 
    // IRecordService, 
    // IBudgetService,
    // ICategoryService,
    // IGoalService,
    // ILabelService,
    // IRecordRuleService,
    // IStandingOrderService
{
    Task<IReadOnlyDictionary<string, ReferencesResponse>> GetReferencesAsync(ReferenceType type, IReadOnlyList<string> ids, CancellationToken cancellationToken = default);
}
