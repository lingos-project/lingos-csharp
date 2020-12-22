using System;
using System.Reflection;
using Lingos.Core.Services;
using Lingos.Core.Utilities;
using Lingos.Source.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Lingos.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddConfigPlugins(this IServiceCollection container, Config config)
        {
            Assembly sourcePlugin = PluginFactory.LoadPlugin(config.Source);
            
            return container
                .AddPluginServices<ISource>(sourcePlugin)
                .AddSingleton(config);
        }
        
         
        /// <summary>
        /// Loads plugin, register it as a service and add its own services
        /// </summary>
        /// <param name="container"></param>
        /// <param name="plugin"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal static IServiceCollection AddPluginServices<T>(this IServiceCollection container, Assembly plugin)
        {
            Type pluginType = PluginFactory.GetPluginType<T>(plugin);
            MethodInfo method = pluginType.GetMethod("GetPluginServices");
            if (method != null && method.Invoke(pluginType, Array.Empty<object>()) is PluginServices pluginServices)
            {
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
            
            return container.AddSingleton(typeof(T), pluginType);
        }
    }
}