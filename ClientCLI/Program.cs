using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;

namespace ClientCLI
{

    internal static class Program
    {
        private static void Main(string[] args)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddSingleton<IPluginsService, PluginsService>()
                .AddSingleton<Scopes>()
                .BuildServiceProvider();
            
            RootCommand root = new("Command Line Interface for lingos")
            {
                serviceProvider.GetRequiredService<Scopes>(),
            };
            
            root.Invoke(args);
        }
    }
}
