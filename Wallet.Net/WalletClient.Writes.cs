using Wallet.Net.Models;
using Wallet.Net.Models.Records;
using Wallet.Net.Models.Labels;
using Wallet.Net.Models.Categories;
using Wallet.Net.Models.Budgets;
using Wallet.Net.Models.Accounts;
using RestSharp;
using Wallet.Net.Http;

namespace Wallet.Net;

public partial class WalletClient
{
    /// <summary>Creates an account. No automatic retries are performed.</summary>
    /// <param name="request">Creation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Creation response, including hints and write summary.</returns>
    public async Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return (await _executor.SendAsync<CreateAccountResponse>("/v1/api/accounts", Method.Post, request, null, cancellationToken).ConfigureAwait(false)).Data;
    }

    /// <summary>Updates between 1 and 10 accounts. Omitted fields remain unchanged.</summary>
    /// <param name="requests">Partial updates.</param>
    /// <param name="options">Write options.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Per-item results; HTTP 207 is returned normally. Batch 400/500 throws a typed WalletBatchException retaining the results.</returns>
    public async Task<PatchAccountsResponse> PatchAccountsAsync(IReadOnlyList<PatchAccountItem> requests, WriteOptions? options = null, CancellationToken cancellationToken = default)
    {
        var body = RequestValidation.Batch(requests, 10);
        return (await _executor.SendAsync<PatchAccountsResponse>("/v1/api/accounts", Method.Patch, body, options, cancellationToken).ConfigureAwait(false)).Data;
    }

    /// <summary>Creates a budget. No automatic retries are performed.</summary>
    /// <param name="request">Creation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Creation response, including hints and write summary.</returns>
    public async Task<CreateBudgetResponse> CreateBudgetAsync(CreateBudgetRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return (await _executor.SendAsync<CreateBudgetResponse>("/v1/api/budgets", Method.Post, request, null, cancellationToken).ConfigureAwait(false)).Data;
    }

    /// <summary>Updates between 1 and 10 budgets. Omitted fields remain unchanged.</summary>
    /// <param name="requests">Partial updates.</param>
    /// <param name="options">Write options.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Per-item results; HTTP 207 is returned normally. Batch 400/500 throws a typed WalletBatchException retaining the results.</returns>
    public async Task<PatchBudgetsResponse> PatchBudgetsAsync(IReadOnlyList<PatchBudgetRequest> requests, WriteOptions? options = null, CancellationToken cancellationToken = default)
    {
        var body = RequestValidation.Batch(requests, 10);
        return (await _executor.SendAsync<PatchBudgetsResponse>("/v1/api/budgets", Method.Patch, body, options, cancellationToken).ConfigureAwait(false)).Data;
    }

    /// <summary>Creates a custom subcategory under a base category. No automatic retries are performed.</summary>
    /// <param name="request">Creation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Creation response, including hints and write summary.</returns>
    public async Task<CreateCategoryResponse> CreateCategoryAsync(CreateSubcategoryRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return (await _executor.SendAsync<CreateCategoryResponse>("/v1/api/categories/custom", Method.Post, request, null, cancellationToken).ConfigureAwait(false)).Data;
    }

    /// <summary>Updates between 1 and 10 categories. Omitted fields remain unchanged.</summary>
    /// <param name="requests">Partial updates.</param>
    /// <param name="options">Write options.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Per-item results; HTTP 207 is returned normally. Batch 400/500 throws a typed WalletBatchException retaining the results.</returns>
    public async Task<PatchCategoriesResponse> PatchCategoriesAsync(IReadOnlyList<PatchCategoryRequest> requests, WriteOptions? options = null, CancellationToken cancellationToken = default)
    {
        var body = RequestValidation.Batch(requests, 10);
        return (await _executor.SendAsync<PatchCategoriesResponse>("/v1/api/categories", Method.Patch, body, options, cancellationToken).ConfigureAwait(false)).Data;
    }

    /// <summary>Creates a label. No automatic retries are performed.</summary>
    /// <param name="request">Creation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Creation response, including hints and write summary.</returns>
    public async Task<CreateLabelResponse> CreateLabelAsync(CreateLabelRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return (await _executor.SendAsync<CreateLabelResponse>("/v1/api/labels", Method.Post, request, null, cancellationToken).ConfigureAwait(false)).Data;
    }

    /// <summary>Updates between 1 and 10 labels. Omitted fields remain unchanged.</summary>
    /// <param name="requests">Partial updates.</param>
    /// <param name="options">Write options.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Per-item results; HTTP 207 is returned normally. Batch 400/500 throws a typed WalletBatchException retaining the results.</returns>
    public async Task<PatchLabelsResponse> PatchLabelsAsync(IReadOnlyList<PatchLabelItem> requests, WriteOptions? options = null, CancellationToken cancellationToken = default)
    {
        var body = RequestValidation.Batch(requests, 10);
        return (await _executor.SendAsync<PatchLabelsResponse>("/v1/api/labels", Method.Patch, body, options, cancellationToken).ConfigureAwait(false)).Data;
    }

    /// <summary>Creates up to 50 transaction records. No automatic retries are performed.</summary>
    /// <param name="requests">Creation requests.</param>
    /// <param name="options">Controls whether full records are returned.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Creation response, including hints and write summary.</returns>
    public async Task<CreateRecordsResponse> CreateRecordsAsync(IReadOnlyList<CreateRecordRequest> requests, WriteOptions? options = null, CancellationToken cancellationToken = default)
    {
        var body = RequestValidation.Batch(requests, 50);
        return (await _executor.SendAsync<CreateRecordsResponse>("/v1/api/records", Method.Post, body, options, cancellationToken).ConfigureAwait(false)).Data;
    }

    /// <summary>Updates between 1 and 10 records. Omitted fields remain unchanged.</summary>
    /// <param name="requests">Partial updates.</param>
    /// <param name="options">Write options.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Per-item results; HTTP 207 is returned normally. Batch 400/500 throws a typed WalletBatchException retaining the results.</returns>
    public async Task<PatchRecordsResponse> PatchRecordsAsync(IReadOnlyList<PatchRecordItem> requests, RecordPatchOptions? options = null, CancellationToken cancellationToken = default)
    {
        var body = RequestValidation.Batch(requests, 10);
        return (await _executor.SendAsync<PatchRecordsResponse>("/v1/api/records", Method.Patch, body, options, cancellationToken).ConfigureAwait(false)).Data;
    }

}
