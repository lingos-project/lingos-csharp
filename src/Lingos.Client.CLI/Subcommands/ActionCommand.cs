using System.CommandLine;

namespace Lingos.Client.CLI.Subcommands
{
    internal class ActionCommand : Command
    {
        internal ActionCommand(string name, string description) : base(name, description)
        {
            Add(new Option<string>(new []{"-cfg", "--config"}, "Configuration file"));
        }
    }
}