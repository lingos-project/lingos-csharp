using System;
using System.Collections;
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
        internal static Dictionary<string, object> FormatTranslations(
            this Dictionary<string, object> wantedFormat,
            IEnumerable<Translation> translations,
            ResultEnding endsIn)
        {
            Dictionary<string, object> result = new();
            
            (IEnumerable<TranslationValueType> grouping, Dictionary<string, object> nextRootFormat, IEnumerable<TranslationValueType> wantedTranslationValues) = wantedFormat.GetFormatData();
            Dictionary<string, IEnumerable<Translation>> translationGroups = translations.GroupedBy(grouping);

            if (nextRootFormat != null)
            {
                foreach ((string groupValue, IEnumerable<Translation> groupedTranslations) in translationGroups)
                {
                    result[groupValue] = nextRootFormat.FormatTranslations(groupedTranslations, endsIn);
                }
            }
            else if (wantedTranslationValues != null)
            {
                IEnumerable<TranslationValueType> wantedTranslationValuesList = wantedTranslationValues.ToList();
                foreach ((string groupValue, IEnumerable<Translation> groupedTranslations) in translationGroups)
                {
                    (IEnumerable<ResultTranslation> resultTranslations, IEnumerable<string> resultTranslationsValues) = groupedTranslations.GetResult(wantedTranslationValuesList);
                    dynamic resultValues = (IEnumerable) resultTranslations ?? resultTranslationsValues;

                    if (endsIn == ResultEnding.Default || endsIn == ResultEnding.Single)
                    {
                        if (Enumerable.Count(resultValues) > 1)
                        {
                            throw new InvalidDataException(
                                "Format configuration does not return single results, fix the format or change ending to 'multiple'.");
                        }

                        result[groupValue] = ((IEnumerable<object>) resultValues).First();
                    }
                    else
                    {
                        result[groupValue] = resultValues;
                    }
                }
            }

            return result;
        }
        
        internal static (IEnumerable<TranslationValueType>, Dictionary<string, object>, IEnumerable<TranslationValueType>) GetFormatData(this Dictionary<string, object> dict)
        {
            IEnumerable<TranslationValueType> translationValueTypes = dict.Select(kv => Enum.Parse<TranslationValueType>(kv.Key, true)).ToList();
            
            if (!translationValueTypes.Any())
            {
                throw new ArgumentOutOfRangeException(nameof(dict), dict, "Wrong key format for config");
            }

            object data = translationValueTypes.Count() > 1 ? dict.Last().Value : dict.First().Value; 
            return data switch
            {
                Dictionary<object, object> resultDict => (translationValueTypes, resultDict.DeepCast<object>(), null),
                IEnumerable<object> resultValues => (translationValueTypes, null, resultValues.Select(v => Enum.Parse<TranslationValueType>((string) v, true))),
                _ => throw new ArgumentOutOfRangeException(nameof(dict), dict, "Wrong value format for config")
            };
        }

        internal static Dictionary<string, Dictionary<object, object>> DeepCast(this Dictionary<object, object> dict) =>
            dict.DeepCast<Dictionary<object, object>>();

        internal static Dictionary<string, T> DeepCast<T>(this Dictionary<object, object> dict) =>
            dict.DeepCast<string, T>();
        
        internal static Dictionary<T1, T2> DeepCast<T1, T2>(this Dictionary<object, object> dict) =>
            dict.ToDictionary(kv => (T1) kv.Key, kv => (T2) kv.Value);
        
    }
}