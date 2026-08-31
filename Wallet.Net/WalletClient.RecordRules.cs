using Wallet.Net.Models;
using Wallet.Net.Models.RecordRules;
using Wallet.Net.Services;

namespace Wallet.Net
{
    public partial class WalletClient : IRecordRuleService
    {
        public async Task<WalletPage<RecordRule>> GetRecordRulesAsync(RecordRuleListOptions? options = null, CancellationToken cancellationToken = default)
    {
        var response = await _executor.GetAsync<RecordRulesResponse>("/v1/api/record-rules", options, cancellationToken).ConfigureAwait(false);
        return ToPage(response, response.RecordRules);
    }
    }
}