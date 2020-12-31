using System.Collections.Generic;

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
            
            foreach ((string key, object value) in dict1)
            {
                bool containsKey = dict2.ContainsKey(key);
                
                if (!containsKey || !value.Equals(dict2[key]))
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