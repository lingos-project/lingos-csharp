using System;
using Lingos.Core.Services;

namespace Lingos.Core
{
    public interface IPlugin
    {
        public PluginServices GetPluginServices();
    }
}