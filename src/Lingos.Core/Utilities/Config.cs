using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Lingos.Core.Utilities
{
    public class Config
    {
        public string Source { get; init; }
        public IEnumerable<string> Generators { get; init; }
        public Dictionary<string, Dictionary<string, object>> Plugins { get; init; }
        
        public static Config GetConfigFromFile(string filePath)
        {
            string actualFilePath = string.IsNullOrEmpty(filePath) ? "./lingosrc.yml" : filePath;
            return GetConfig(File.OpenText(actualFilePath));
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