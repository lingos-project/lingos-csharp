using SourceBase;
using Core;

namespace ClientCLI
{

    public class PluginsService : IPluginsService
    {
        public ISource Source
        {
            get
            {
                // todo: get stuff from config
                string sourcePath = "SourcePostgres/bin/Debug/net5.0/SourcePostgres.dll";
                return Factory.GetSource(sourcePath);
            }
        }
    }
}
