using System;

namespace AlhambraScoringAndroid.Attributes
{
    public class SettingNameAttribute : Attribute
    {
        public string Name { get; set; }
        public bool DefaultValue { get; set; }
        public int DescriptionResource { get; set; }

        public SettingNameAttribute(string name, bool defaultValue, int descriptionResource)
        {
            Name = name;
            DefaultValue = defaultValue;
            DescriptionResource = descriptionResource;
        }
    }
}