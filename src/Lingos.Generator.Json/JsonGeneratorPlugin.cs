using Lingos.Core;
using Lingos.Core.Services;

namespace Lingos.Generator.Json
{
    public class JsonGeneratorPlugin : IPlugin
    {
        public string Name => "Json Generator";
        public string CoreVersion => "";
        public PluginServices GetPluginServices()
        {
            return new();
        }
    }
}