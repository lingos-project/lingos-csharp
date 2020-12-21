using System.Reflection;
using Lingos.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Lingos.Core
{
    public static class LingosContainer
    {
        public static IServiceCollection GetContainer()
        {
            ServiceCollection container = new();
            ConfigService config = new();
            Assembly sourcePlugin = PluginFactory.LoadPlugin(config.SourcePath);
            SourceService source = new(sourcePlugin);
            
            source.AddServices(container);
            container.AddSingleton(config);
            container.AddSingleton(source);
            // todo: add plugins

            return container;
        }
    }
}