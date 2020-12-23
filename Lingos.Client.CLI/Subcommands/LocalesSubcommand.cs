using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using Lingos.Client.CLI.Services;
using Lingos.Common;
using Lingos.Core.Services;
using Lingos.Core.Utilities;
using Lingos.Source.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Lingos.Client.CLI.Subcommands
{
    public class LocalesSubcommand : Command
    {
        public LocalesSubcommand() : base("locales", "Add or manage locales")
        {
            LoadCommands();
        }

        private void LoadCommands()
        {
            Action add = new("add", "Add a new locale")
            {
                new Argument<string>("name", "The name of the locale"),
                new Option<bool>(new[] { "--required", "-r" },
                    "Make keys required to have a translation of this locale"),
            };
            add.Handler = CommandHandler.Create<string, bool, string>(AddLocale);
            Add(add);
            
            Action update = new("update", "Update the name of a locale")
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

            services.GetRequiredService<Handlers>().AddLocale(name, required);
        }

        private static void UpdateLocale(string oldName, string newName, string config)
        {
            ServiceProvider services = LoadServices.AddServices(Config.GetConfigFromFile(config));

            services.GetRequiredService<Handlers>().UpdateLocale(oldName, newName);
        }
    }
}
