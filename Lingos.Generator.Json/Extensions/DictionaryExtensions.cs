using System;
using System.Collections.Generic;
using System.Linq;
using Lingos.Core.Models;

namespace Lingos.Generator.Json.Extensions
{
    public static class DictionaryExtension
    {
        internal static Dictionary<string, object> FormatTranslations(this Dictionary<string, object> rootFormat, IEnumerable<Translation> translations)
        {
            Dictionary<string, object> result = new();
            
            (IEnumerable<TranslationValueType> grouping, Dictionary<string, object> typing, IEnumerable<TranslationValueType> wantedTranslationValues) = rootFormat.GetFormatData();
            Dictionary<string, IEnumerable<Translation>> zz = translations.GroupedBy(grouping);

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
                IEnumerable<string> resultValues => (translationValueTypes, null, resultValues.Select(Enum.Parse<TranslationValueType>)),
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