using AlhambraScoringAndroid.Attributes;
using System.ComponentModel;

namespace AlhambraScoringAndroid.GamePlay
{
    public enum BuildingType
    {
        [Description("Pavilion")]
        [ImageAttribute(Resource.Drawable.Pavilion)]
        Pavilion, //Blue
        [Description("Seraglio")]
        [ImageAttribute(Resource.Drawable.Seraglio)]
        Seraglio, //Red
        [Description("Arcades")]
        [ImageAttribute(Resource.Drawable.Arcades)]
        Arcades, //Brown
        [Description("Chambers")]
        [ImageAttribute(Resource.Drawable.Chambers)]
        Chambers, //White
        [Description("Garden")]
        [ImageAttribute(Resource.Drawable.Garden)]
        Garden, //Green
        [Description("Tower")]
        [ImageAttribute(Resource.Drawable.Tower)]
        Tower, //Purple
    }
}