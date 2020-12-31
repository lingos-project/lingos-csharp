using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using Lingos.Client.CLI.Services;
using Lingos.Core.Utilities;
using Lingos.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Lingos.Client.CLI.Subcommands
{
    internal class ScopesSubcommand : Command
    {
        internal ScopesSubcommand() : base("scopes", "Add or manage scopes")
        {
            LoadCommands();
        }

        private void LoadCommands()
        {
            ActionCommand add = new("add", "Add a new scope")
            {
                new Argument<string>("name", "The name of the scope"),
            };
            add.Handler = CommandHandler.Create<string, string>(AddScope);
            Add(add);

            ActionCommand update = new("update", "Update the name of a scope")
            {
                new Argument<string>("oldName", "The old name of the scope"),
                new Argument<string>("newName", "The new name of the scope"),
            };
            update.Handler = CommandHandler.Create<string, string, string>(UpdateScope);
            Add(update);
        }

        private static void AddScope(string name, string config)
        {
            ServiceProvider services = LoadServices.AddServices(Config.GetConfigFromFile(config));

            Response response = services.GetRequiredService<ISource>().AddScope(name);
            
            Console.WriteLine(response.Message);
        }
        
        private static void UpdateScope(string oldName, string newName, string config)
        {
            ServiceProvider services = LoadServices.AddServices(Config.GetConfigFromFile(config));

            Response response = services.GetRequiredService<ISource>().UpdateScope(oldName, newName);
            
            Console.WriteLine(response.Message);
        }
    }
}
