using Wallet.Net.Models;
using Wallet.Net.Models.Labels;

namespace Wallet.Net.Services
{
    public interface ILabelService
    {
        Task<WalletPage<Label>> GetLabelsAsync(LabelListOptions? options = null, CancellationToken cancellationToken = default);
    }
}

