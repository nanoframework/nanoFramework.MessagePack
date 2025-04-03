using System.Reflection;
using System;

namespace nanoFramework.MessagePack.Dto
{
    internal class MemberMapping
    {
#if NANOFRAMEWORK_1_0
        internal const string GET_ = "get_";
        internal const string SET_ = "set_";
#endif

#nullable enable
        public MemberMapping(FieldInfo field) => Field = field;
#if NANOFRAMEWORK_1_0
        public MemberMapping(MethodInfo property) => Property = property;
        private MethodInfo? Property { get; set; }
        public string? Name => Field?.Name ?? Property?.Name.Substring(4);
        public Type? DeclaringType => Field?.DeclaringType ?? Property?.DeclaringType;
        
#else
        public MemberMapping(PropertyInfo property) => Property = property;
        private PropertyInfo? Property { get; set; }
        public string? Name => Field?.Name ?? Property?.Name;
        public Type? DeclaringType => Field?.DeclaringType ?? Property?.DeclaringType;
#endif
        public string? OriginalName => Field?.Name ?? Property?.Name;
        private FieldInfo? Field { get; set; }

        public Type? GetMemberType()
        {
            if (Field != null)
            {
                return Field.FieldType;
            }
            else
#if NANOFRAMEWORK_1_0
            if (Property != null)
            {
                return GetPropertyType(Property);
            }
#else
            if (Property != null)
            {
                return Property.PropertyType;
            }
#endif

            return null;
        }

        public bool TryGetValue(object obj, out object value)
        {
            if (Field != null)
            {
                value = Field.GetValue(obj);
                return true;
            }

            if (Property != null)
            {
#if NANOFRAMEWORK_1_0
                if (Property.Name.StartsWith(GET_))
                {
                    value = Property.Invoke(obj, null);
                    return true;
                }
#else                
                value = Property.GetValue(obj); 
                return true;
#endif           
            }
            value = null!;
            return false;
        }

        public override string ToString()
        {
            return $"{Field?.Name}{Property?.Name}";
        }
        internal void SetValue(object current, object val)
        {
            if (Field != null)
                Field.SetValue(current, val);
            else
#if NANOFRAMEWORK_1_0
            if (Property != null && Property.Name.StartsWith(SET_))
            {
                Property.Invoke(current, new object[] { val });
            }
#else
            if (Property != null)
            {
                Property.SetValue(current, val);
            }
#endif
        }
#if NANOFRAMEWORK_1_0
        internal static Type GetPropertyType(MethodInfo methodInfo)
        {
            if (methodInfo.Name.StartsWith(GET_))
            {
                return methodInfo.ReturnType;
            }
            else if (methodInfo.Name.StartsWith(SET_))
            {
                return methodInfo.GetParameters()[0].ParameterType;
            }

            throw new Exception($"Method {methodInfo.Name} is not Property set or get method.");
        }
#endif
    }
}
