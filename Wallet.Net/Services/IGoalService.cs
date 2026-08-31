using Wallet.Net.Models;
using Wallet.Net.Models.Goals;

namespace Wallet.Net.Services
{
    public interface IGoalService
    {
        Task<WalletPage<Goal>> GetGoalsAsync(GoalListOptions? options = null, CancellationToken cancellationToken = default);
    }
}

