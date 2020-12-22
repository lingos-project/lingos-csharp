using System.CommandLine;
using Lingos.Client.CLI.Subcommands;
using Lingos.Core.Extensions;
using Lingos.Core.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Lingos.Client.CLI
{

    internal static class Program
    {
        private static void Main(string[] args)
        {
            Config config = Config.GetConfigFromFile("/home/bazoo/documents/projects/lingos-project/lingos-csharp/lingosrc.yml");
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddConfigPlugins(config)
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
