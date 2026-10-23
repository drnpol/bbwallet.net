using Wallet.Net.Models.Accounts;

namespace Wallet.Net.Tests.Unit;

public class MetadataTests
{
    [Fact]
    public async Task All_documented_metadata_is_available_without_affecting_response_JSON()
    {
        using var wallet=new OfflineWallet("{\"accounts\":[]}");
        wallet.Handler.ResponseHeaders["X-RateLimit-Limit"]="500";
        wallet.Handler.ResponseHeaders["X-RateLimit-Remaining"]="487";
        wallet.Handler.ResponseHeaders["X-Last-Data-Change-At"]="2025-01-28T14:23:45.123Z";
        wallet.Handler.ResponseHeaders["X-Last-Data-Change-Rev"]="r1234";
        wallet.Handler.ResponseHeaders["X-Sync-In-Progress"]="true";
        wallet.Handler.ResponseHeaders["Retry-After"]="60";
        var result=await wallet.Client.GetAccountsAsync();
        Assert.Equal(500,result.Metadata.RateLimit);
        Assert.Equal(487,result.Metadata.RateLimitRemaining);
        Assert.Equal(DateTimeOffset.Parse("2025-01-28T14:23:45.123Z"),result.Metadata.LastDataChangeAt);
        Assert.Equal("r1234",result.Metadata.LastDataChangeRevision);
        Assert.True(result.Metadata.SyncInProgress);
        Assert.Equal(60,result.Metadata.RetryAfterSeconds);
        Assert.DoesNotContain("Metadata",Newtonsoft.Json.JsonConvert.SerializeObject(result));
        wallet.Handler.ResponseHeaders["X-RateLimit-Limit"]="invalid";
        wallet.Handler.ResponseHeaders["X-Sync-In-Progress"]="invalid";
        var invalid=await wallet.Client.GetAccountsAsync(new AccountListOptions());
        Assert.Null(invalid.Metadata.RateLimit);
        Assert.Null(invalid.Metadata.SyncInProgress);
    }
}
