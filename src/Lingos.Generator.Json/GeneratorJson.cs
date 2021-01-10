using System;
using System.Collections.Generic;
using System.Text.Json;
using Lingos.Core.Utilities;
using Lingos.Core;
using Lingos.Core.Extensions;
using Lingos.Core.Models;
using Lingos.Generator.Json.Extensions;

namespace Lingos.Generator.Json
{
    public class GeneratorJson : IGenerator
    {
        private readonly ISource _source;
        private readonly Config _config;

        public GeneratorJson(ISource source, Config config)
        {
            _source = source;
            _config = config;
        }
        
        public void Generate()
        {
            IEnumerable<Translation> translations = _source.GetTranslations();
            Dictionary<string, object> cfg = _config.Plugins["generatorJson"];
            ResultEnding endsIn = Enum.Parse<ResultEnding>(cfg.GetString("ends", "default"), true);
            string outputFile = cfg.GetString("output");
            
            Dictionary<string, object> rootFormat = cfg.Get<Dictionary<object, object>>("format").DeepCast<object>();
            Dictionary<string, object> result = rootFormat.FormatTranslations(translations, endsIn);

            string resultText = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
            
            System.IO.File.WriteAllText(outputFile, resultText);
        }
    }
}