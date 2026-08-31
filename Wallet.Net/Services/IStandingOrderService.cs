using Wallet.Net.Models;
using Wallet.Net.Models.StandingOrders;

namespace Wallet.Net.Services
{
    public interface IStandingOrderService
    {
        Task<WalletPage<StandingOrder>> GetStandingOrdersAsync(StandingOrderListOptions? options = null, CancellationToken cancellationToken = default);
        Task<WalletPage<StandingOrderItem>> GetStandingOrderItemsAsync(StandingOrderItemListOptions? options = null, CancellationToken cancellationToken = default);
    }
}

