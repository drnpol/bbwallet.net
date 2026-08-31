using System.Net;

namespace Wallet.Net.Tests.Fixtures;

public sealed record MockWalletApiResponse(HttpStatusCode StatusCode, string Json);
