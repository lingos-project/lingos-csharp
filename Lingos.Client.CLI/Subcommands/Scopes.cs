using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using Lingos.Common;
using Lingos.Core.Services;
using Lingos.Source.Base;

namespace Lingos.Client.CLI.Subcommands
{
    public class Scopes : Command
    {
        private readonly ISource _source;
        
        public Scopes(ISource source) : base("scopes", "Add or manage scopes")
        {
            _source = source;
            
            LoadCommands();
        }

        private void LoadCommands()
        {
            Command add = new("add", "Add a new scope")
            {
                new Argument<string>("name", "The name of the scope")
            };
            add.Handler = CommandHandler.Create<string>(AddScope);
            Add(add);

            Command update = new("update", "Update the name of a scope")
            {
                new Argument<string>("oldName", "The old name of the scope"),
                new Argument<string>("newName", "The new name of the scope"),
            };
            update.Handler = CommandHandler.Create<string, string>(UpdateScope); 
            Add(update);
        } 

        private void AddScope(string name)
        {
            Response response = _source.AddScope(name);
            
            Console.WriteLine(response.Message);
        }

        private void UpdateScope(string oldName, string newName)
        {
            Response response = _source.UpdateScope(oldName, newName);
            
            Console.WriteLine(response.Message);
        }
    }
}
