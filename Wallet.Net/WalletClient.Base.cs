using Wallet.Net.Http;
using Wallet.Net.Models;
using Wallet.Net.Models.Accounts;
using Wallet.Net.Models.Budgets;
using Wallet.Net.Models.Categories;
using Wallet.Net.Models.Goals;
using Wallet.Net.Models.Labels;
using Wallet.Net.Models.RecordRules;
using Wallet.Net.Models.Records;
using Wallet.Net.Models.StandingOrders;

namespace Wallet.Net
{
    public partial class WalletClient : IWalletClient
    {
        private readonly WalletRequestExecutor _executor;

        public WalletClient(WalletRequestExecutor executor)
        {
            ArgumentNullException.ThrowIfNull(executor);
            _executor = executor;
        }
        public async Task<IReadOnlyDictionary<string, ReferencesResponse>> GetReferencesAsync(
        ReferenceType type,
        IReadOnlyList<string> ids,
        CancellationToken cancellationToken = default)
            => (await GetReferencesWithMetadataAsync(type, ids, cancellationToken).ConfigureAwait(false)).Data;

        /// <inheritdoc />
        public Task<WalletResult<IReadOnlyDictionary<string, ReferencesResponse>>> GetReferencesWithMetadataAsync(
            ReferenceType type, IReadOnlyList<string> ids, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(ids);

            if (ids.Count is < 1 or > 20 || ids.Any(string.IsNullOrWhiteSpace))
            {
                throw new ArgumentException("Supply between 1 and 20 nonblank IDs.", nameof(ids));
            }

            var query = new { id = string.Join(",", ids) };
            return _executor.SendAsync<IReadOnlyDictionary<string, ReferencesResponse>>($"/v1/api/{ToReferencePath(type)}/references", RestSharp.Method.Get, query: query, cancellationToken: cancellationToken);
        }

        private static WalletPage<T> ToPage<T>(PaginatedResponse response, IReadOnlyList<T> items)
        {
            var agentHints = response switch
            {
                AccountsResponse typed => typed.AgentHints,
                RecordsResponse typed => typed.AgentHints,
                CategoriesResponse typed => typed.AgentHints,
                LabelsResponse typed => typed.AgentHints,
                BudgetsResponse typed => typed.AgentHints,
                GoalsResponse typed => typed.AgentHints,
                StandingOrdersResponse typed => typed.AgentHints,
                StandingOrderItemsResponse typed => typed.AgentHints,
                RecordRulesResponse typed => typed.AgentHints,
                _ => []
            };

            return new WalletPage<T>
            {
                Metadata = response.Metadata,
                AppliedRecordDateFilters = response is RecordsResponse records ? records.AppliedRecordDateFilters : [],
                Items = items,
                Limit = response.Limit,
                NextOffset = response.NextOffset,
                Offset = response.Offset,
                Total = response.Total,
                AgentHints = agentHints
            };
        }

        private static string ToReferencePath(ReferenceType type) =>
            type switch
            {
                ReferenceType.Accounts => "accounts",
                ReferenceType.Categories => "categories",
                ReferenceType.Labels => "labels",
                ReferenceType.Records => "records",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unsupported reference type.")
            };
    }
}
