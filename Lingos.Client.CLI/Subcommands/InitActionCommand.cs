using System;
using System.CommandLine.Invocation;
using Lingos.Client.CLI.Services;
using Lingos.Core;
using Lingos.Core.Utilities;
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

            ISource source = services.GetRequiredService<ISource>();

            Response response = source.Initialize();
            
            Console.WriteLine(response.Message);
        }
    }
}