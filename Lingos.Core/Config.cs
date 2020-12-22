using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Lingos.Core
{
    public class Config
    {
        public string Source { get; set; }
        public Dictionary<string, Dictionary<string, object>> Plugins { get; set; }
        
        public static Config GetConfigFromFile(string filePath = "lingosrc.yml")
        {
            return GetConfig(File.OpenText(filePath));
        }

        private static Config GetConfig(TextReader textConfig)
        {
            IDeserializer deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            return deserializer.Deserialize<Config>(textConfig);
        }
    }
}