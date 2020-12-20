using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using Lingos.Common;
using Lingos.Core;
using Lingos.Source.Base;

namespace Lingos.Client.CLI.Subcommands
{
    public class Locales : Command
    {
        private readonly ISource _source;
        
        public Locales(IPluginsService pluginsService) : base("locales", "Add or manage locales")
        {
            _source = pluginsService.Source;
            
            LoadCommands();
        }

        private void LoadCommands()
        {
            Command add = new("add", "Add a new locale")
            {
                new Argument<string>("name", "The name of the locale"),
                new Option<bool>(new[] { "--required", "-r" },
                    "Make keys required to have a translation of this locale"),
            };
            add.Handler = CommandHandler.Create<string, bool>(AddLocale);
            Add(add);

            Command update = new("update", "Update the name of a locale")
            {
                new Argument<string>("oldName", "The old name of the locale"),
                new Argument<string>("newName", "The new name of the locale"),
            };
            update.Handler = CommandHandler.Create<string, string>(UpdateLocale); 
            Add(update);
        } 

        private void AddLocale(string name, bool required)
        {
            Response response = _source.AddLocale(name, required);
            
            Console.WriteLine(response.Message);
        }

        private void UpdateLocale(string oldName, string newName)
        {
            Response response = _source.UpdateLocale(oldName, newName);
            
            Console.WriteLine(response.Message);
        }
    }
}
