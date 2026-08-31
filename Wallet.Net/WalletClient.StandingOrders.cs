using Wallet.Net.Models;
using Wallet.Net.Models.StandingOrders;
using Wallet.Net.Services;

namespace Wallet.Net
{
    public partial class WalletClient : IStandingOrderService
    {
        public async Task<WalletPage<StandingOrder>> GetStandingOrdersAsync(StandingOrderListOptions? options = null, CancellationToken cancellationToken = default)
        {
            var response = await _executor.GetAsync<StandingOrdersResponse>("/v1/api/standing-orders", options, cancellationToken).ConfigureAwait(false);
            return ToPage(response, response.StandingOrders);
        }

        public async Task<WalletPage<StandingOrderItem>> GetStandingOrderItemsAsync(StandingOrderItemListOptions? options = null, CancellationToken cancellationToken = default)
        {
            var response = await _executor.GetAsync<StandingOrderItemsResponse>("/v1/api/standing-orders/items", options, cancellationToken).ConfigureAwait(false);
            return ToPage(response, response.StandingOrderItems);
        }
    }
}