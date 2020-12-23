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
    internal class TranslationsSubcommand : Command
    {
        internal TranslationsSubcommand() : base("translations", "Add or manage translations")
        {
            LoadCommands();
        }

        private void LoadCommands()
        {
            ActionCommand add = new("add", "Add a new translation")
            {
                new Argument<string>("key", "The key of the translation"),
                new Argument<string>("scope", "The scope of the translation"),
                new Argument<string>("locale", "The locale of the translation"),
                new Argument<string>("text", "The key of the translation"),
                new Option<string>(new[] {"--variant", "-v"}, "The variant of the translation"),
            };
            add.Handler = CommandHandler.Create<string, string, string, string, string, string>(AddTranslation);
            Add(add);
            
            ActionCommand update = new("update", "Update a new translation")
            {
                new Argument<string>("key", "The key of the translation"),
                new Argument<string>("scope", "The scope of the translation"),
                new Argument<string>("locale", "The locale of the translation"),
                new Argument<string>("text", "The key of the translation"),
                new Option<string>(new[] {"--variant", "-v"}, "The variant of the translation"),
            };
            update.Handler = CommandHandler.Create<string, string, string, string, string, string>(UpdateTranslation);
            Add(update);
        }

        private static void AddTranslation(string key, string scope, string locale, string text, string variant, string config)
        {
            ServiceProvider services = LoadServices.AddServices(Config.GetConfigFromFile(config));

            Response response = services.GetRequiredService<ISource>().AddTranslation(key, scope, locale, text, variant);
            
            Console.WriteLine(response.Message);
        }

        private static void UpdateTranslation(string key, string scope, string locale, string text, string variant,
            string config)
        {
            ServiceProvider services = LoadServices.AddServices(Config.GetConfigFromFile(config));

            Response response = services.GetRequiredService<ISource>().UpdateTranslation(key, scope, locale, text, variant);
            
            Console.WriteLine(response.Message);
        }
    }
}
