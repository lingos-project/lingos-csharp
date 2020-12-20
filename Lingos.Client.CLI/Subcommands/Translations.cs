using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using Lingos.Common;
using Lingos.Core;
using Lingos.Source.Base;

namespace Lingos.Client.CLI.Subcommands
{
    public class Translations : Command
    {
        private readonly ISource _source;
        
        public Translations(IPluginsService pluginsService) : base("translations", "Add or manage translations")
        {
            _source = pluginsService.Source;
            
            LoadCommands();
        }

        private void LoadCommands()
        {
            Command add = new("add", "Add a new translation")
            {
                new Argument<string>("key", "The key of the translation"),
                new Argument<string>("scope", "The scope of the translation"),
                new Argument<string>("locale", "The locale of the translation"),
                new Argument<string>("text", "The key of the translation"),
                new Option<string>(new[] {"--variant", "-v"}, "The variant of the translation"),
            };
            add.Handler = CommandHandler.Create<string, string, string, string, string>(AddTranslation);
            Add(add);
            
            Command update = new("update", "Update a new translation")
            {
                new Argument<string>("key", "The key of the translation"),
                new Argument<string>("scope", "The scope of the translation"),
                new Argument<string>("locale", "The locale of the translation"),
                new Argument<string>("text", "The key of the translation"),
                new Option<string>(new[] {"--variant", "-v"}, "The variant of the translation"),
            };
            update.Handler = CommandHandler.Create<string, string, string, string, string>(UpdateTranslation);
            Add(update);
        } 

        private void AddTranslation(string key, string scope, string locale, string text, string variant)
        {
            Response response = _source.AddTranslation(key, scope, locale, text, variant);
            
            Console.WriteLine(response.Message);
        }

        private void UpdateTranslation(string key, string scope, string locale, string text, string variant)
        {
            Response response = _source.UpdateTranslation(key, scope, locale, text, variant);
            
            Console.WriteLine(response.Message);
        }
    }
}
