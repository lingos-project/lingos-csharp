using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Lingos.Core.Utilities
{
    public static class PluginFactory
    {
        internal static Assembly LoadPlugin(string pluginPath)
        {
            if (string.IsNullOrEmpty(pluginPath))
            {
                throw new ArgumentOutOfRangeException(nameof(pluginPath), pluginPath,
                    "The path of the plugin should not be empty or null");
            }
            
            string pluginLocation = Path.IsPathRooted(pluginPath)
                ? pluginPath
                : Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), pluginPath.Replace('\\', Path.DirectorySeparatorChar)));
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

        internal static T GetPlugin<T>(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(T).IsAssignableFrom(type) && Activator.CreateInstance(type) is T result)
                {
                    return result;
                }
            }

            string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
            throw new ApplicationException(
                $"Can't find any type which implements {typeof(T).FullName} in {assembly} from {assembly.Location}.\n" +
                $"Available types: {availableTypes}");
        }
    }
}
