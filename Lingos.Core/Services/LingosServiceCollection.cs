using System.Reflection;
using Lingos.Core.Extensions;
using Lingos.Source.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Lingos.Core.Services
{
    public class LingosServiceCollection : ServiceCollection
    {
        public LingosServiceCollection()
        {
            ConfigService config = new();
            Assembly sourcePlugin = PluginFactory.LoadPlugin(config.SourcePath);
            
            this.AddPluginServices<ISource>(sourcePlugin);
            this.AddSingleton(config);
            // todo: add plugins
        }
    }
}