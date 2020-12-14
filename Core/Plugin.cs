using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Core
{
    public static class Plugin
    {
        public static Assembly Load(string relativePath)
        {
            string root = Path.GetFullPath(
                Path.Combine(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(
                                    Path.GetDirectoryName(typeof(Plugin).Assembly.Location))))) ?? string.Empty));

            string pluginLocation =
                Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
            PluginLoadContext loadContext = new(pluginLocation);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
        }

        public static IEnumerable<T> Create<T>(Assembly assembly)
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
                if (typeof(T).IsAssignableFrom(type))
                    if (Activator.CreateInstance(type) is T result)
                    {
                        count++;
                        yield return result;
                    }

            if (count == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Can't find any type which implements {typeof(T).FullName} in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }
        }
    }
}