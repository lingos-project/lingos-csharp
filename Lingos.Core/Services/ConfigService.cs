using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Lingos.Core.Services
{
    public class ConfigService
    {
        public string SourcePath = "Lingos.Source.Postgres/bin/Debug/net5.0/Lingos.Source.Postgres.dll";

        public ConfigService()
        {
            LoadConfigFile("/home/bazoo/documents/projects/lingos-project/lingos-csharp/lingosrc.yml");
        }
        
        public static void LoadConfigFile(string filePath = "lingosrc.yml")
        {
            LoadConfig(File.OpenText(filePath));
        }

        private static void LoadConfig(TextReader textConfig)
        {
            IDeserializer deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            Config config = deserializer.Deserialize<Config>(textConfig);
        }
    }
}