namespace Wallet.Net.Tests.Models
{
    public class ResultBasedTestModel
    {
        public bool ExpectedResult { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TestComment { get; set; } = string.Empty; 
    }
}
