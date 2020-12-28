using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lingos.Core.Services;
using Lingos.Core.Utilities;
using Lingos.Generator.Base;
using Lingos.Source.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Lingos.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddConfigPlugins(this IServiceCollection container, Config config)
        {
            Assembly sourcePlugin = PluginFactory.LoadPlugin(config.Source);
            Type sourcePluginType = PluginFactory.GetPluginType<ISource>(sourcePlugin);
            container
                .AddPluginServices(sourcePlugin)
                .AddSingleton(typeof(ISource), sourcePluginType);
            
            IEnumerable<Assembly> generatorPlugins = config.Generators.Select(PluginFactory.LoadPlugin);
            foreach (Assembly generatorPlugin in generatorPlugins)
            {
                container
                    .AddPluginServices(generatorPlugin)
                    .AddSingleton(typeof(IGenerator), PluginFactory.GetPluginType<IGenerator>(generatorPlugin));
            }
            
            return container.AddSingleton(config);
        }


        /// <summary>
        /// Loads plugin, register it as a service and add its own services
        /// </summary>
        /// <param name="container"></param>
        /// <param name="pluginAssembly"></param>
        /// <returns></returns>
        private static IServiceCollection AddPluginServices(this IServiceCollection container, Assembly pluginAssembly)
        {
            try
            {
                IPlugin plugin = PluginFactory.GetPlugin<IPlugin>(pluginAssembly);
                PluginServices pluginServices = plugin.GetPluginServices();

                foreach ((Type serviceType, Type implementationType) in pluginServices.ScopedServices)
                {
                    container.AddScoped(serviceType, implementationType);
                }

                foreach ((Type serviceType, Type implementationType) in pluginServices.TransientServices)
                {
                    container.AddTransient(serviceType, implementationType);
                }

                foreach ((Type serviceType, Type implementationType) in pluginServices.SingletonServices)
                {
                    container.AddSingleton(serviceType, implementationType);
                }
            }
            catch (ApplicationException)
            {
                return container;
            }
            return container;
        }
    }
}