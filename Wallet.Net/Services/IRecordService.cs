using Wallet.Net.Models;
using Wallet.Net.Models.Records;

namespace Wallet.Net.Services
{
    public interface IRecordService
    {
        Task<WalletPage<Record>> GetRecordsAsync(RecordListOptions? options = null, CancellationToken cancellationToken = default);
    }
}

