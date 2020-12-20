using Lingos.Source.Base;

namespace Lingos.Core
{

    public class PluginsService : IPluginsService
    {
        public ISource Source
        {
            get
            {
                // todo: get stuff from config
                // Factory.LoadConfigFile("/home/bazoo/documents/projects/lingos-project/lingos-csharp/lingosrc.yml");
                
                string sourcePath = "Lingos.Source.Postgres/bin/Debug/net5.0/Lingos.Source.Postgres.dll";
                return Factory.GetSource(sourcePath);
            }
        }
    }
}
