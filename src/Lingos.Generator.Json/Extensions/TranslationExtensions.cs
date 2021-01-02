using System;
using System.Collections.Generic;
using System.Linq;
using Lingos.Core.Models;

namespace Lingos.Generator.Json.Extensions
{
    internal static class TranslationExtensions
    {
        internal static dynamic GetResult(
            this Translation translation, IEnumerable<TranslationValueType> wantedValues)
        {
            List<TranslationValueType> wantedValuesList = wantedValues.ToList();

            if (!wantedValuesList.Any())
            {
                throw new ArgumentOutOfRangeException(nameof(wantedValues), wantedValues, 
                    "Wanted values should not be empty");
            }

            if (wantedValuesList.Count == 1)
            {
                return translation.GetValue(wantedValuesList.Single());
            }
            
            return translation.CreateResultTranslation(wantedValuesList);
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
                _ => translation.Text,
            };
        }
        
        internal static string GetGroupKey( this Translation translation, IEnumerable<TranslationValueType> grouping)
            => string.Join('-', grouping.Select(translation.GetValue));
    }
}