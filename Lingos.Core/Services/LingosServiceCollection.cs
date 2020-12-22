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
            Config config = Config.GetConfigFromFile("/home/bazoo/documents/projects/lingos-project/lingos-csharp/lingosrc.yml");
            Assembly sourcePlugin = PluginFactory.LoadPlugin(config.Source);
            
            this.AddPluginServices<ISource>(sourcePlugin);
            this.AddSingleton(config);
            // todo: add plugins
        }
    }
}