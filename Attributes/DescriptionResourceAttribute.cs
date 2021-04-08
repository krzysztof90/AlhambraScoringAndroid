using System;

namespace AlhambraScoringAndroid.Attributes
{
    public class DescriptionResourceAttribute : Attribute
    {
        public int Resource { get; set; }
        
        public DescriptionResourceAttribute(int resource)
        {
            Resource = resource;
        }
    }
}