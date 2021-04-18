using System;

namespace AlhambraScoringAndroid.Attributes
{
    public class SettingNameAttribute : Attribute
    {
        public string Name { get; set; }
        public bool DefaultValue { get; set; }

        public SettingNameAttribute(string name, bool defaultValue)
        {
            Name = name;
            DefaultValue = defaultValue;
        }
    }
}