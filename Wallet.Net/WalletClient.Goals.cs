using Wallet.Net.Models;
using Wallet.Net.Models.Goals;
using Wallet.Net.Services;

namespace Wallet.Net
{
    public partial class WalletClient : IGoalService
    {
        public async Task<WalletPage<Goal>> GetGoalsAsync(GoalListOptions? options = null, CancellationToken cancellationToken = default)
    {
        var response = await _executor.GetAsync<GoalsResponse>("/v1/api/goals", options, cancellationToken).ConfigureAwait(false);
        return ToPage(response, response.Goals);
    }
    }
}