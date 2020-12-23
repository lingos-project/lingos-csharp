using System.CommandLine;

namespace Lingos.Client.CLI.Subcommands
{
    public class Action : Command
    {
        public Action(string name, string description) : base(name, description)
        {
            Add(new Option<string>(new []{"-c", "--config"}, "Configuration file"));
        }
    }
}