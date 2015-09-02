using System.Collections.Generic;

namespace BitbucketSharp.Utils
{
    public static class ObjectToDictionaryConverter
    {
        public static Dictionary<string, string> Convert<T>(T obj)
        {
            var dictionary = new Dictionary<string, string>();
            var properties = obj.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var value = propertyInfo.GetValue(obj, null);
                dictionary.Add(propertyInfo.Name.ToLower(), value == null ? null : value.ToString());
            }
            return dictionary;
        }
    }
}
