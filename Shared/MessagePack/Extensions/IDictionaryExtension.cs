using System;
using System.Collections;

namespace nanoFramework.MessagePack.Extensions
{
    internal static class IDictionaryExtension
    {
        internal static object GetOrAdd(this IDictionary dictionary, object key, Func<object, object> creator)
        {
            object temp = null;
            if (dictionary.Contains(key))
            {
                temp = creator(key);
                dictionary.Add(key, temp);
            }
            return temp;
        }
    }
}
