using System;
using Lingos.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Lingos.Core
{
    public class ContainerBuilder
    {
        public IServiceProvider Build(IServiceCollection container)
        {
            // todo: add plugins
            // todo: add sources
            // todo: add config

            return container.BuildServiceProvider();
        }
    }
}