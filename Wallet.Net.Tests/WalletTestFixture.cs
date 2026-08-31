using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Wallet.Net.Tests
{
    public class WalletTestFixture : IAsyncLifetime
    {
        public IConfiguration Configuration { get; private set; }
        public IWalletClient Client { get; private set; } = null!;

        private IServiceProvider _serviceProvider = null!;

        public WalletTestFixture()
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            Configuration = configBuilder
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile("appsettings.Testing.json", false)
                .Build();
        }

        public Task InitializeAsync()
        {
            var services = new ServiceCollection();
            services.AddWalletClient(Configuration);

            _serviceProvider = services.BuildServiceProvider();
            Client = _serviceProvider.GetRequiredService<IWalletClient>();

            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }

            return Task.CompletedTask;
        }
    }
}
