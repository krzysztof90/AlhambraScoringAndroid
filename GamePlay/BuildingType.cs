using AlhambraScoringAndroid.Attributes;

namespace AlhambraScoringAndroid.GamePlay
{
    public enum BuildingType
    {
        [ImageAttribute(Resource.Drawable.Pavilion)]
        Pavilion, //Blue
        [ImageAttribute(Resource.Drawable.Seraglio)]
        Seraglio, //Red
        [ImageAttribute(Resource.Drawable.Arcades)]
        Arcades, //Brown
        [ImageAttribute(Resource.Drawable.Chambers)]
        Chambers, //White
        [ImageAttribute(Resource.Drawable.Garden)]
        Garden, //Green
        [ImageAttribute(Resource.Drawable.Tower)]
        Tower, //Purple
    }
}