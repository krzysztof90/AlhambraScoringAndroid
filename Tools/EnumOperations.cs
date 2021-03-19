using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AlhambraScoringAndroid.Tools
{
    public static class EnumOperations
    {
        public static string GetEnumDescription<EnumType>(this EnumType value) where EnumType : struct, IConvertible, IComparable, IFormattable
        {
            return value.GetEnumAttribute<EnumType, DescriptionAttribute>().Description;
        }

        private static T GetEnumAttribute<EnumType, T>(this EnumType value, T defaultValue = default(T)) where EnumType : struct, IConvertible, IComparable, IFormattable
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            return GetFieldAttribute(fieldInfo, defaultValue);
        }

        private static T GetFieldAttribute<T>(MemberInfo memberInfo, T defaultValue = default(T))
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