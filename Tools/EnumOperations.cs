using AlhambraScoringAndroid.Attributes;
using Android.Content.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AlhambraScoringAndroid.Tools
{
    public static class EnumOperations
    {
        public static string GetEnumDescription<EnumType>(this EnumType value, Resources resources) where EnumType : struct, IConvertible, IComparable, IFormattable
        {
            //return value.GetEnumAttribute<EnumType, DescriptionAttribute>().Description;
            return resources.GetString(value.GetEnumAttribute<EnumType, DescriptionResourceAttribute>().Resource);
        }

        public static T GetEnumAttribute<EnumType, T>(this EnumType value, T defaultValue = default) where EnumType : struct, IConvertible, IComparable, IFormattable
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            return GetFieldAttribute(fieldInfo, defaultValue);
        }

        private static T GetFieldAttribute<T>(MemberInfo memberInfo, T defaultValue = default)
        {
            IEnumerable<T> attributes = (T[])memberInfo.GetCustomAttributes(typeof(T), false).Cast<T>();

            if (attributes != null && attributes.Any())
            {
                return attributes.Cast<T>().Single();
            }
            return defaultValue;
        }
    }
}