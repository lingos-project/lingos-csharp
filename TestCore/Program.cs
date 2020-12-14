using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Core;
using SourceBase;

namespace TestCore
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                if (args.Length == 1 && args[0] == "/d")
                {
                    Console.WriteLine("Waiting for any key...");
                    Console.ReadLine();
                }

                string[] sourcePaths =
                {
                    @"SourcePostgres/bin/Debug/net5.0/SourcePostgres.dll"
                };
                
                IEnumerable<ISource> sources = sourcePaths.SelectMany(sourcePath =>
                {
                    Assembly sourceAssembly = Plugin.Load(sourcePath);
                    return Plugin.Create<ISource>(sourceAssembly);
                }).ToList();

                if (args.Length == 0)
                {
                    Console.WriteLine("Commands: ");

                    foreach (ISource source in sources) source.AddLocale("en", false);
                }
                else
                {
                    foreach (string commandName in args)
                    {
                        Console.WriteLine($"-- {commandName} --");

                        // execute the command with the name passed as an argument.

                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}