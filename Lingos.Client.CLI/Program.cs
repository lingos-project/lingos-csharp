using System.CommandLine;
using Lingos.Client.CLI.Subcommands;

namespace Lingos.Client.CLI
{

    internal static class Program
    {
        private static void Main(string[] args)
        {
            RootCommand root = new("Command Line Interface for lingos")
            {
                new KeysSubcommand(),
                new LocalesSubcommand(),
                new ScopesSubcommand(),
                new TranslationsSubcommand(),
                new InitActionCommand(),
                new GenerateActionCommand(),
            };
            
            root.Invoke(args);
        }
    }
}
