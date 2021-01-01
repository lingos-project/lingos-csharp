using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lingos.Core.Models;

namespace Lingos.Generator.Json.Extensions
{
    internal static class TranslationExtensions
    {
        internal static Dictionary<string, object> FormatTranslationBatch(
            this IEnumerable<Translation> translationBatch,
            List<IEnumerable<TranslationValueType>> groupings,
            IEnumerable<TranslationValueType> wantedValues,
            ResultEnding endsIn,
            int depth = 0)
        {
            if (depth < groupings.Count - 1)
            {
                return translationBatch.GroupedBy(groupings[depth]).ToDictionary(
                    kv => kv.Key,
                    kv => (object) kv.Value.FormatTranslationBatch(groupings, wantedValues, endsIn, depth + 1));
            }

            return translationBatch.GroupedBy(groupings.Last()).ToDictionary(
                kv => kv.Key,
                kv =>
                {
                    dynamic resultValues = kv.Value.GetResult(wantedValues);

                    if (endsIn == ResultEnding.Multiple)
                    {
                        return resultValues;
                    }
                    
                    if (Enumerable.Count(resultValues) > 1)
                    {
                        throw new InvalidDataException(
                            "Format configuration does not return single results, fix the format or change ending to 'multiple'.");
                    }
                    
                    return ((IEnumerable<object>) resultValues).First();
                });
        }
        
        internal static dynamic GetResult(
            this IEnumerable<Translation> translations, IEnumerable<TranslationValueType> wantedValues)
        {
            List<TranslationValueType> wantedValuesList = wantedValues.ToList();

            if (!wantedValuesList.Any())
            {
                throw new ArgumentOutOfRangeException(nameof(wantedValues), wantedValues, 
                    "Wanted values should not be empty");
            }

            if (wantedValuesList.Count == 1)
            {
                return translations.Select(t => t.GetValue(wantedValuesList.Single()));
            }
            
            return translations.Select(t => t.CreateResultTranslation(wantedValuesList));
        }

        internal static ResultTranslation CreateResultTranslation(this Translation translation,
            IEnumerable<TranslationValueType> wantedValues)
        {
            ResultTranslation result = new();

            foreach (TranslationValueType wantedValue in wantedValues)
            {
                string value = translation.GetValue(wantedValue);
                string property = Enum.GetName(wantedValue) ?? throw new InvalidOperationException();
                result.GetType().GetProperty(property)?.SetValue(result, value, null);
            }

            return result;
        }
        
        internal static string GetValue(this Translation translation, TranslationValueType valueType)
        {
            return valueType switch
            {
                TranslationValueType.Key => translation.KeyName,
                TranslationValueType.Locale => translation.LocaleName,
                TranslationValueType.Scope => translation.ScopeName,
                TranslationValueType.Variant => translation.Variant,
                TranslationValueType.Text => translation.Text,
                _ => throw new ArgumentOutOfRangeException(nameof(valueType), valueType, null)
            };
        }

        /// <summary>
        /// Groups by a translation value type, then returns the grouped translations
        /// </summary>
        /// <param name="translations">List of translations to be grouped</param>
        /// <param name="groupBy">Groups the translations with these values</param>
        /// <returns>A dictionary of translations grouped by values</returns>
        internal static Dictionary<string, IEnumerable<Translation>> GroupedBy(
            this IEnumerable<Translation> translations,
            IEnumerable<TranslationValueType> groupBy)
        {
            return translations.GroupBy(
                    t => string.Join("-", groupBy.Select(t.GetValue)),
                    t => t,
                    (key, t) => (key, t))
                .ToDictionary(kv => kv.key, kv => kv.t);
        }
    }
}