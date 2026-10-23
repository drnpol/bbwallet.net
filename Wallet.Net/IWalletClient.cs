using Wallet.Net.Models;
using Wallet.Net.Services;

namespace Wallet.Net;

public interface IWalletClient : 
    IAccountService, 
    IRecordService, 
    IBudgetService,
    ICategoryService,
    IGoalService,
    ILabelService,
    IRecordRuleService,
    IStandingOrderService,
    IApiUsageService,
    IDeleteService
{
    Task<IReadOnlyDictionary<string, ReferencesResponse>> GetReferencesAsync(ReferenceType type, IReadOnlyList<string> ids, CancellationToken cancellationToken = default);

    /// <summary>Checks references and retains rate-limit response headers.</summary>
    /// <param name="type">Referenced entity type.</param>
    /// <param name="ids">Between 1 and 20 IDs.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>References keyed by ID, with HTTP metadata.</returns>
    Task<WalletResult<IReadOnlyDictionary<string, ReferencesResponse>>> GetReferencesWithMetadataAsync(ReferenceType type, IReadOnlyList<string> ids, CancellationToken cancellationToken = default);
}
