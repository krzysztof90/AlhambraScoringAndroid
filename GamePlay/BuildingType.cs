using AlhambraScoringAndroid.Attributes;

namespace AlhambraScoringAndroid.GamePlay
{
    public enum BuildingType
    {
        [DescriptionResourceAttribute(Resource.String.pavilion)]
        [ImageAttribute(Resource.Drawable.Pavilion)]
        Pavilion, //Blue
        [DescriptionResourceAttribute(Resource.String.seraglio)]
        [ImageAttribute(Resource.Drawable.Seraglio)]
        Seraglio, //Red
        [DescriptionResourceAttribute(Resource.String.arcades)]
        [ImageAttribute(Resource.Drawable.Arcades)]
        Arcades, //Brown
        [DescriptionResourceAttribute(Resource.String.chambers)]
        [ImageAttribute(Resource.Drawable.Chambers)]
        Chambers, //White
        [DescriptionResourceAttribute(Resource.String.garden)]
        [ImageAttribute(Resource.Drawable.Garden)]
        Garden, //Green
        [DescriptionResourceAttribute(Resource.String.tower)]
        [ImageAttribute(Resource.Drawable.Tower)]
        Tower, //Purple
    }
}