using System;
using Lingos.Core.Services;

namespace Lingos.Core
{
    public interface IPlugin
    {
        public string Name { get; }
        public string CoreVersion { get; }
        public PluginServices GetPluginServices();
    }
}