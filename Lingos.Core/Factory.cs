using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Lingos.Source.Base;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Lingos.Core
{
    public static class Factory
    {
        public static ISource GetSource(string relativePath) => CreatePlugin<ISource>(LoadPlugin(relativePath));

        public static void LoadConfigFile(string filePath = "lingosrc.yml")
        {
            LoadConfig(File.OpenText(filePath));
        }

        private static void LoadConfig(TextReader config)
        {
            IDeserializer deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var x = deserializer.Deserialize(config);
            Console.WriteLine(x);
        }
        
        private static Assembly LoadPlugin(string relativePath)
        {
            string root = Path.GetFullPath(
                Path.Combine(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(
                                    Path.GetDirectoryName(typeof(Factory).Assembly.Location))))) ?? string.Empty));

            string pluginLocation =
                Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
            PluginLoadContext loadContext = new(pluginLocation);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
        }

        private static T CreatePlugin<T>(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
                if (typeof(T).IsAssignableFrom(type))
                    if (Activator.CreateInstance(type) is T result)
                    {
                        return result;
                    }

            string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
            throw new ApplicationException(
                $"Can't find any type which implements {typeof(T).FullName} in {assembly} from {assembly.Location}.\n" +
                $"Available types: {availableTypes}");
        }
    }
}
