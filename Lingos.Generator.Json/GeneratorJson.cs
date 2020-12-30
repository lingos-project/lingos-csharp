using System;
using System.Collections.Generic;
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
            string outputFile = cfg.GetString("output");
            
            Dictionary<string, object> rootFormat = cfg.Get<Dictionary<object, object>>("format").DeepCast<object>();
            (IEnumerable<TranslationValueType> grouping, Dictionary<string, object> typing, IEnumerable<TranslationValueType> wantedTranslationValues) = rootFormat.GetFormatData();
            Dictionary<string, IEnumerable<Translation>> zz = translations.GroupedBy(grouping);
            
            Console.WriteLine("test");
        }
    }
}