using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using Lingos.Client.CLI.Services;
using Lingos.Common;
using Lingos.Core.Utilities;
using Lingos.Source.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Lingos.Client.CLI.Subcommands
{
    internal class KeysSubcommand : Command
    {
        internal KeysSubcommand() : base("keys", "Add or manage keys")
        {
            LoadCommands();
        }

        private void LoadCommands()
        {
            ActionCommand add = new("add", "Add a new key")
            {
                new Argument<string>("name", "The name of the key")
            };
            add.Handler = CommandHandler.Create<string, string>(AddKey);
            Add(add);
            
            ActionCommand update = new("update", "Update the name of a key")
            {
                new Argument<string>("oldName", "The old name of the key"),
                new Argument<string>("newName", "The new name of the key"),
            };
            update.Handler = CommandHandler.Create<string, string, string>(UpdateKey); 
            Add(update);
        }

        private static void AddKey(string name, string config)
        {
            ServiceProvider services = LoadServices.AddServices(Config.GetConfigFromFile(config));
            
            Response response = services.GetRequiredService<ISource>().AddKey(name);
            
            Console.WriteLine(response.Message);
        }

        private static void UpdateKey(string oldName, string newName, string config)
        {
            ServiceProvider services = LoadServices.AddServices(Config.GetConfigFromFile(config));
            
            Response response = services.GetRequiredService<ISource>().UpdateKey(oldName, newName);
            
            Console.WriteLine(response.Message);
        }
    }
}
