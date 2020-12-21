using System;
using System.Collections.Generic;

namespace Lingos.Core.Services
{
    public class PluginServices
    {
        public IEnumerable<(Type, Type)> SingletonServices { get; init; } = Array.Empty<(Type, Type)>();
        public IEnumerable<(Type, Type)> ScopedServices { get; init; } = Array.Empty<(Type, Type)>();
        public IEnumerable<(Type, Type)> TransientServices { get; init; } = Array.Empty<(Type, Type)>();
    }
}