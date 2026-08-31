using Wallet.Net.Models;
using Wallet.Net.Models.Accounts;
using Wallet.Net.Models.Budgets;
using Wallet.Net.Models.Categories;
using Wallet.Net.Models.Goals;
using Wallet.Net.Models.Labels;
using Wallet.Net.Models.RecordRules;
using Wallet.Net.Models.Records;
using Wallet.Net.Models.StandingOrders;
using Wallet.Net.Services;

namespace Wallet.Net
{
    public partial class WalletClient : IRecordService
    {
        public async Task<WalletPage<Record>> GetRecordsAsync(RecordListOptions? options = null, CancellationToken cancellationToken = default)
        {
            var response = await _executor.GetAsync<RecordsResponse>("/v1/api/records", options, cancellationToken).ConfigureAwait(false);
            return ToPage(response, response.Records);
        }
    }
}