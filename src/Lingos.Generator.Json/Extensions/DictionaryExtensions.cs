using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Lingos.Core.Models;

[assembly: InternalsVisibleTo("Lingos.Generator.Json.Test")]
namespace Lingos.Generator.Json.Extensions
{
    public static class DictionaryExtension
    {
        internal static (IEnumerable<IEnumerable<TranslationValueType>>, IEnumerable<TranslationValueType>) GetWantedFormat(
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
            (IEnumerable<IEnumerable<TranslationValueType>> groupings, IEnumerable<TranslationValueType> wantedValues) = cfgFormat.GetWantedFormat();
            return translations.FormatTranslationBatch(groupings.ToList(), wantedValues, endsIn);
        }
        
        internal static Dictionary<string, T> DeepCast<T>(this Dictionary<object, object> dict) =>
            dict.DeepCast<string, T>();
        
        internal static Dictionary<T1, T2> DeepCast<T1, T2>(this Dictionary<object, object> dict) =>
            dict.ToDictionary(kv => (T1) kv.Key, kv => (T2) kv.Value);
        
    }
}