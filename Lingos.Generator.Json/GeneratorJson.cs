using System;
using System.Collections.Generic;
using Lingos.Core.Utilities;
using Lingos.Core;
using Lingos.Core.Models;

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
            
            foreach (Translation translation in translations)
            {
                Console.WriteLine($"{translation.Key} : {translation.Text}");
            }
        }
    }
}