using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SourceBase;

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 1 && args[0] == "/d")
                {
                    Console.WriteLine("Waiting for any key...");
                    Console.ReadLine();
                }

                string[] sourcePaths = new string[]
                {
                    // paths to sources to load.
                };

                IEnumerable<ISource> sources = sourcePaths.SelectMany(sourcePath =>
                {
                    Assembly sourceAssembly = LoadSource(sourcePath);
                    return CreateSources(sourceAssembly);
                }).ToList();

                if (args.Length == 0)
                {
                    Console.WriteLine("Commands: ");

                    foreach (ISource source in sources)
                    {
                        Console.WriteLine(source.GetTranslation("something", "more", "en", null).Text);
                    }
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

        static Assembly LoadSource(string relativePath)
        {
            string root = Path.GetFullPath(
                Path.Combine(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(
                                    Path.GetDirectoryName(typeof(Program).Assembly.Location)))))!));

            string sourceLocation =
                Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
            Console.WriteLine($"Loading commands from: {sourceLocation}");
            SourceLoadContext loadContext = new SourceLoadContext(sourceLocation);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(sourceLocation)));
        }

        static IEnumerable<ISource> CreateSources(Assembly assembly)
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(ISource).IsAssignableFrom(type))
                {
                    if (Activator.CreateInstance(type) is ISource result)
                    {
                        count++;
                        yield return result;
                    }
                }
            }

            if (count == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Can't find any type which implements ICommand in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }
        }
    }
}
