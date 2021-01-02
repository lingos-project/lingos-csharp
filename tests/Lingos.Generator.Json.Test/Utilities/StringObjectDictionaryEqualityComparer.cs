using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lingos.Generator.Json.Test.Utilities
{
    public class StringObjectDictionaryEqualityComparer : IEqualityComparer<Dictionary<string, object>>
    {
        public bool Equals(Dictionary<string, object> dict1, Dictionary<string, object> dict2)
        {
            if (dict1 == null)
            {
                return dict2 == null;
            }

            if (dict2 == null)
            {
                return false;
            }

            if (dict1.Keys.Count != dict2.Keys.Count)
            {
                return false;
            }
            
            foreach ((string key, object value) in dict1)
            {
                if (!dict2.ContainsKey(key))
                {
                    return false;
                }

                if (value is Dictionary<string, object> nestedDict1 && dict2[key] is Dictionary<string, object> nestedDict2)
                {
                    if (!Equals(nestedDict1, nestedDict2))
                    {
                        return false;
                    }
                }
                else if (JsonConvert.SerializeObject(value) != JsonConvert.SerializeObject(dict2[key]))
                {
                    return false;
                }
            }
            
            return true;
        }

        public int GetHashCode(Dictionary<string, object> obj)
        {
            throw new System.NotImplementedException();
        }
    }
}