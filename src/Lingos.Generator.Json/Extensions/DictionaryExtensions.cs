using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Lingos.Core.Models;

[assembly: InternalsVisibleTo("Lingos.Generator.Json.Test")]
namespace Lingos.Generator.Json.Extensions
{
    public static class DictionaryExtension
    {
        internal static (
            IEnumerable<IEnumerable<TranslationValueType>>, IEnumerable<TranslationValueType>) Parse(
            this Dictionary<string, object> cfgFormat)
        {
            List<IEnumerable<TranslationValueType>> grouping = new();
            Dictionary<string, object> current = cfgFormat;

            while (true)
            {
                IEnumerable<TranslationValueType> groupingTypes =
                    current.Keys.Select(key => Enum.Parse<TranslationValueType>(key, true)).ToList();
                grouping.Add(groupingTypes);
                
                object data = groupingTypes.Count() > 1 ? current.Last().Value : current.First().Value;

                switch (data)
                {
                    case Dictionary<object, object> nextFormat:
                        current = nextFormat.DeepCast<object>();
                        break;
                    case IEnumerable<object> resultValues:
                        IEnumerable<TranslationValueType> wantedValues =
                            resultValues.Cast<string>()
                                .Select(key => Enum.Parse<TranslationValueType>(key, true))
                                .ToList();
                        return (grouping, wantedValues);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(cfgFormat), cfgFormat, "Wrong value format for config");
                }
            }
        }
        
        internal static Dictionary<string, object> FormatTranslations(
            this Dictionary<string, object> cfgFormat,
            IEnumerable<Translation> translations,
            ResultEnding endsIn)
        {
            (IEnumerable<IEnumerable<TranslationValueType>> groupings, IEnumerable<TranslationValueType> wantedValues) = cfgFormat.Parse();
            List<IEnumerable<TranslationValueType>> groupingsList = groupings.ToList();
            List<TranslationValueType> wantedValuesList = wantedValues.ToList();
            Dictionary<string, object> result = new();

            foreach (Translation translation in translations)
            {
                result.InsertTranslation(translation, groupingsList, wantedValuesList, endsIn);
            }

            return result;
        }

        internal static void InsertTranslation(
            this Dictionary<string, object> dictionary,
            Translation translation,
            IEnumerable<IEnumerable<TranslationValueType>> groupings,
            IEnumerable<TranslationValueType> wantedValues,
            ResultEnding endsIn)
        {
            List<string> groupKeys = groupings.Select(translation.GetGroupKey).ToList();
            dynamic resultTranslation = translation.GetResult(wantedValues);
            Dictionary<string, object> deepDictionary = dictionary;
            
            for (int i = 0; i < groupKeys.Count - 1; i++)
            {
                string groupKey = groupKeys[i];

                if (!deepDictionary.ContainsKey(groupKey))
                {
                    deepDictionary[groupKey] = new Dictionary<string, object>();
                } 
                deepDictionary = (Dictionary<string, object>) deepDictionary[groupKey];
            }
            
            string lastGroupKey = groupKeys.Last();
            if (deepDictionary.ContainsKey(lastGroupKey))
            {
                if (endsIn != ResultEnding.Multiple)
                {
                    throw new InvalidDataException("Format configuration does not return single results, " +
                                                   "fix the format or change ending to 'multiple'.");
                }

                ((List<object>) deepDictionary[lastGroupKey]).Add(resultTranslation);
            }
            else
            {
                deepDictionary[lastGroupKey] =
                    endsIn == ResultEnding.Multiple
                        ? new List<object> {resultTranslation}
                        : resultTranslation;
            }
        }
        
        internal static Dictionary<string, T> DeepCast<T>(this Dictionary<object, object> dict) =>
            dict.DeepCast<string, T>();
        
        internal static Dictionary<T1, T2> DeepCast<T1, T2>(this Dictionary<object, object> dict) =>
            dict.ToDictionary(kv => (T1) kv.Key, kv => (T2) kv.Value);
        
    }
}