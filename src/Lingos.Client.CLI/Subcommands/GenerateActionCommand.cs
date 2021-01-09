using System.Collections.Generic;
using System.CommandLine.Invocation;
using Lingos.Client.CLI.Services;
using Lingos.Core;
using Lingos.Core.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Lingos.Client.CLI.Subcommands
{
    internal class GenerateActionCommand : ActionCommand
    {
        internal GenerateActionCommand() : base("generate", "Generates an output")
        {
            Handler = CommandHandler.Create<string>(Generate);
        }

        private static void Generate(string config)
        {
            ServiceProvider services = LoadServices.AddServices(Config.GetConfigFromFile(config));

            IEnumerable<IGenerator> generators = services.GetRequiredService<IEnumerable<IGenerator>>();

            foreach (IGenerator generator in generators)
            {
                generator.Generate();
            }
        }
    }
}