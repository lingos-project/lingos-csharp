using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using Lingos.Client.CLI.Services;
using Lingos.Core;
using Lingos.Core.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Lingos.Client.CLI.Subcommands
{
    internal class LocalesSubcommand : Command
    {
        internal LocalesSubcommand() : base("locales", "Add or manage locales")
        {
            LoadCommands();
        }

        private void LoadCommands()
        {
            ActionCommand add = new("add", "Add a new locale")
            {
                new Argument<string>("name", "The name of the locale"),
                new Option<bool>(new[] { "--required", "-r" },
                    "Make keys required to have a translation of this locale"),
            };
            add.Handler = CommandHandler.Create<string, bool, string>(AddLocale);
            Add(add);
            
            ActionCommand update = new("update", "Update the name of a locale")
            {
                new Argument<string>("oldName", "The old name of the locale"),
                new Argument<string>("newName", "The new name of the locale"),
            };
            update.Handler = CommandHandler.Create<string, string, string>(UpdateLocale); 
            Add(update);
        }

        private static void AddLocale(string name, bool required, string config)
        {
            ServiceProvider services = LoadServices.AddServices(Config.GetConfigFromFile(config));

            Response response = services.GetRequiredService<ISource>().AddLocale(name, required);
            
            Console.WriteLine(response.Message);
        }

        private static void UpdateLocale(string oldName, string newName, string config)
        {
            ServiceProvider services = LoadServices.AddServices(Config.GetConfigFromFile(config));

            Response response = services.GetRequiredService<ISource>().UpdateLocale(oldName, newName);
            
            Console.WriteLine(response.Message);
        }
    }
}
