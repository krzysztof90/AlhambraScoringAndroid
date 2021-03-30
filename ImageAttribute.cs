using System;

namespace AlhambraScoringAndroid
{
    public class ImageAttribute : Attribute
    {
        public int Resource { get; set; }

        public ImageAttribute(int resource)
        {
            Resource = resource;
        }
    }
}