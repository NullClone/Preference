using System.Collections.Generic;
using System.Reflection;

namespace Preference.Utilities
{
    public static class ReflectionExtensions
    {
        private static readonly Dictionary<string, FieldInfo> FieldInfoCache = new();

        private static readonly Dictionary<string, PropertyInfo> PropertyInfoCache = new();


        public static object GetFieldValue(this object value, string name)
        {
            var type = value.GetType();

            FieldInfo fieldInfo;

            if (FieldInfoCache.TryGetValue(name, out var result))
            {
                fieldInfo = result;
            }
            else
            {
                fieldInfo = FieldInfoCache[name] = type.GetField(name, (BindingFlags)62);
            }

            return fieldInfo.GetValue(value);
        }

        public static object GetPropertyValue(this object value, string name)
        {
            var type = value.GetType();

            PropertyInfo propertyInfo;

            if (PropertyInfoCache.TryGetValue(name, out var result))
            {
                propertyInfo = result;
            }
            else
            {
                propertyInfo = PropertyInfoCache[name] = type.GetProperty(name, (BindingFlags)62);
            }

            return propertyInfo.GetValue(value);
        }
    }
}