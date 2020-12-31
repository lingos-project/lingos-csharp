using System.Collections.Generic;

namespace Lingos.Core.Extensions
{
    public static class DictionaryExtension
    {
        public static string GetString(this Dictionary<string, object> dict, string key, string def = null) => dict.Get(key, def);

        public static T Get<T>(this Dictionary<string, object> dict, string key, T def = default)
        {
            try
            {
                return (T) dict[key];
            }
            catch (KeyNotFoundException)
            {
                if (def != null)
                {
                    return def;
                }

                throw;
            }
        }
    }
}