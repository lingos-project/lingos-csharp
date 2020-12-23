using System.CommandLine.Invocation;
using Lingos.Client.CLI.Services;
using Lingos.Core.Utilities;
using Lingos.Source.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Lingos.Client.CLI.Subcommands
{
    internal class InitActionCommand : ActionCommand
    {
        internal InitActionCommand() : base("init", "Initializes the sources")
        {
            Handler = CommandHandler.Create<string>(Initialize);
        }

        private static void Initialize(string config)
        {
            ServiceProvider services = LoadServices.AddServices(Config.GetConfigFromFile(config));

            services.GetRequiredService<ISource>().Initialize();
        }
    }
}