using Wallet.Net.Models;
using Wallet.Net.Models.RecordRules;

namespace Wallet.Net.Services
{
    public interface IRecordRuleService
    {
        Task<WalletPage<RecordRule>> GetRecordRulesAsync(RecordRuleListOptions? options = null, CancellationToken cancellationToken = default);
    }
}

