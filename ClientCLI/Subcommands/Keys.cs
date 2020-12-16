using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using Common;
using Core;
using SourceBase;

namespace ClientCLI.Subcommands
{
    public class Keys : Command
    {
        private readonly ISource _source;
        
        public Keys(IPluginsService pluginsService) : base("keys", "Add or manage keys")
        {
            _source = pluginsService.Source;
            
            LoadCommands();
        }

        private void LoadCommands()
        {
            Command add = new("add", "Add a new key")
            {
                new Argument<string>("name", "The name of the key")
            };
            add.Handler = CommandHandler.Create<string>(AddKey);
            Add(add);

            Command update = new("update", "Update the name of a key")
            {
                new Argument<string>("oldName", "The old name of the key"),
                new Argument<string>("newName", "The new name of the key"),
            };
            update.Handler = CommandHandler.Create<string, string>(UpdateKey); 
            Add(update);
        } 

        private void AddKey(string name)
        {
            Response response = _source.AddKey(name);
            
            Console.WriteLine(response.Message);
        }

        private void UpdateKey(string oldName, string newName)
        {
            Response response = _source.UpdateKey(oldName, newName);
            
            Console.WriteLine(response.Message);
        }
    }
}
