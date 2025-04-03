using nanoFramework.MessagePack.Dto;
using System.Collections;
using System.Reflection;
using System;

namespace nanoFramework.MessagePack.Utility
{
    internal static class FieldUtility
    {
#nullable enable
        public static void Map(Type targetType, ArrayList mappings)
        {
            var fields = GetFields(targetType);

            if (fields == null || fields.Length == 0)
                return;

            foreach (var field in fields)
            {
                mappings.Add(new MemberMapping(field));
            }
        }

        private static FieldInfo[] GetFields(Type type)
        {
            var list = new ArrayList();

            GetFields(type, list);

            return (FieldInfo[])list.ToArray(typeof(FieldInfo));
        }

        private static void GetFields(Type? type, ArrayList list)
        {
            if (type == null) return;

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields)
                if (field.DeclaringType == type)
                    list.Add(field);

            GetFields(type.BaseType, list);
        }
    }
}
