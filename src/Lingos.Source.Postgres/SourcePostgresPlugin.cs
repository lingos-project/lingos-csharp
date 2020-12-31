using Lingos.Core;
using Lingos.Core.Services;

namespace Lingos.Source.Postgres
{
    public class SourcePostgresPlugin : IPlugin
    {
        public PluginServices GetPluginServices()
        {
            return new()
            {
                TransientServices = new []
                {
                    (typeof(DatabaseContext), typeof(DatabaseContext))
                },
            };
        }
    }
}