using System;
using System.Collections.Generic;
using System.Reflection;
using Lingos.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Lingos.Core.Extensions
{
    internal static class ServiceCollectionExtension
    {
        /// <summary>
        /// Loads plugin, register it as a service and add its own services
        /// </summary>
        /// <param name="container"></param>
        /// <param name="plugin"></param>
        /// <typeparam name="T"></typeparam>
        internal static void AddPluginServices<T>(this IServiceCollection container, Assembly plugin)
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
            container.AddSingleton(typeof(T), pluginType);
        }
    }
}