using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Lingos.Core.Utilities
{
    public static class PluginFactory
    {
        internal static Assembly LoadPlugin(string relativePath)
        {
            string root = Path.GetFullPath(
                Path.Combine(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(
                                    Path.GetDirectoryName(typeof(PluginFactory).Assembly.Location))))) ?? string.Empty));

            string pluginLocation =
                Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
            PluginLoadContext loadContext = new(pluginLocation);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
        }

        internal static Type GetPluginType<T>(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(T).IsAssignableFrom(type))
                {
                    return type;
                }
            }

            string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
            throw new ApplicationException(
                $"Can't find any type which implements {typeof(T).FullName} in {assembly} from {assembly.Location}.\n" +
                $"Available types: {availableTypes}");
            
        }
    }
}
