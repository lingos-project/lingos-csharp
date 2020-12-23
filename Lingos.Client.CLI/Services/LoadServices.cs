using Lingos.Core.Extensions;
using Lingos.Core.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Lingos.Client.CLI.Services
{
    public static class LoadServices
    {
        public static ServiceProvider AddServices(Config config)
        {
            return new ServiceCollection()
                .AddConfigPlugins(config)
                .AddTransient<Handlers>()
                .BuildServiceProvider();
        }
    }
}