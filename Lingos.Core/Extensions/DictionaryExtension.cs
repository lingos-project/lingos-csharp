using System.Collections.Generic;

namespace Lingos.Core.Extensions
{
    public static class DictionaryExtension
    {
        public static string GetString(this Dictionary<string, object> dict, string key)
        {
            return dict.Get<string>(key);
        }

        public static T Get<T>(this Dictionary<string, object> dict, string key)
        {
            return (T) dict[key];
        }
    }
}