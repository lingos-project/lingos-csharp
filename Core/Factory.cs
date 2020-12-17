using System;
using System.IO;
using System.Linq;
using System.Reflection;
using SourceBase;

namespace Core
{
    public static class Factory
    {
        public static ISource GetSource(string relativePath) => CreatePlugin<ISource>(LoadPlugin(relativePath));
        
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
