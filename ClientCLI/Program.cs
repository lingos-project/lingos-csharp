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
                .AddScoped<Subcommands.Keys>()
                .AddScoped<Subcommands.Locales>()
                .AddScoped<Subcommands.Scopes>()
                .BuildServiceProvider();
            
            RootCommand root = new("Command Line Interface for lingos")
            {
                serviceProvider.GetRequiredService<Subcommands.Keys>(),
                serviceProvider.GetRequiredService<Subcommands.Locales>(),
                serviceProvider.GetRequiredService<Subcommands.Scopes>(),
            };
            
            root.Invoke(args);
        }
    }
}
