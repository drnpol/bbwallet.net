using Wallet.Net.Models;
using Wallet.Net.Models.Labels;
using Wallet.Net.Services;

namespace Wallet.Net
{
    public partial class WalletClient : ILabelService
    {
        public async Task<WalletPage<Label>> GetLabelsAsync(LabelListOptions? options = null, CancellationToken cancellationToken = default)
        {
            var response = await _executor.GetAsync<LabelsResponse>("/v1/api/labels", options, cancellationToken).ConfigureAwait(false);
            return ToPage(response, response.Labels);
        }
    }
}