using Lingos.Core.Extensions;
using Lingos.Core.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Lingos.Client.CLI.Services
{
    internal static class LoadServices
    {
        internal static ServiceProvider AddServices(Config config)
        {
            return new ServiceCollection()
                .AddConfigPlugins(config)
                .BuildServiceProvider();
        }
    }
}