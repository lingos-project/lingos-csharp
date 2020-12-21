using System;
using System.Linq;
using System.Reflection;
using Lingos.Source.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Lingos.Core.Services
{
    public class SourceService
    {
        public SourceService(Assembly plugin)
        {
            Type type = PluginFactory.GetPluginType<ISource>(plugin);
            SourceType = type;
        }

        public Type SourceType { get; }

        internal void AddServices(IServiceCollection container)
        {
            MethodInfo method = SourceType.GetMethod("AddServices", new []{typeof(IServiceCollection)});
            if (method != null)
            {
                method.Invoke(SourceType, new object[] {container});
            }
            container.AddSingleton(typeof(ISource), SourceType);
        }
    }
}