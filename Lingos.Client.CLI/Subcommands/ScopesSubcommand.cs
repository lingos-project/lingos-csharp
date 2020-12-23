using System.CommandLine;
using System.CommandLine.Invocation;
using Lingos.Client.CLI.Services;
using Lingos.Core.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Lingos.Client.CLI.Subcommands
{
    public class ScopesSubcommand : Command
    {
        public ScopesSubcommand() : base("scopes", "Add or manage scopes")
        {
            LoadCommands();
        }

        private void LoadCommands()
        {
            Action add = new("add", "Add a new scope")
            {
                new Argument<string>("name", "The name of the scope"),
            };
            add.Handler = CommandHandler.Create<string, string>(AddScope);
            Add(add);

            Action update = new("update", "Update the name of a scope")
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

            services.GetRequiredService<Handlers>().AddScope(name);
        }
        
        private static void UpdateScope(string oldName, string newName, string config)
        {
            ServiceProvider services = LoadServices.AddServices(Config.GetConfigFromFile(config));

            services.GetRequiredService<Handlers>().UpdateScope(oldName, newName);
        }
    }
}
