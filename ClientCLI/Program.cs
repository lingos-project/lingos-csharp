using System.CommandLine;
using Core;
using Microsoft.Extensions.DependencyInjection;

namespace ClientCLI
{

    internal static class Program
    {
        private static void Main(string[] args)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddSingleton<IPluginsService, PluginsService>()
                .AddSingleton<Subcommands.Scopes>()
                .BuildServiceProvider();
            
            RootCommand root = new("Command Line Interface for lingos")
            {
                serviceProvider.GetRequiredService<Subcommands.Scopes>(),
            };
            
            root.Invoke(args);
        }
    }
}
