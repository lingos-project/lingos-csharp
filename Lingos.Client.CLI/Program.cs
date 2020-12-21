using System.CommandLine;
using Lingos.Client.CLI.Subcommands;
using Lingos.Core;
using Lingos.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Lingos.Client.CLI
{

    internal static class Program
    {
        private static void Main(string[] args)
        {
            ServiceProvider serviceProvider = LingosContainer
                .GetContainer()
                .AddTransient<Keys>()
                .AddTransient<Locales>()
                .AddTransient<Scopes>()
                .AddTransient<Translations>()
                .BuildServiceProvider();
            
            RootCommand root = new("Command Line Interface for lingos")
            {
                serviceProvider.GetRequiredService<Keys>(),
                serviceProvider.GetRequiredService<Locales>(),
                serviceProvider.GetRequiredService<Scopes>(),
                serviceProvider.GetRequiredService<Translations>(),
            };

            
            root.Invoke(args);
        }
    }
}
