using AndroidBase.Attributes;

namespace AlhambraScoringAndroid.GamePlay
{
    public enum BuildingType
    {
        [DescriptionResourceAttribute(Resource.String.pavilion)]
        [ImageAttribute(Resource.Drawable.Pavilion)]
        Pavilion = AlhambraBase.BuildingType.Pavilion,
        [DescriptionResourceAttribute(Resource.String.seraglio)]
        [ImageAttribute(Resource.Drawable.Seraglio)]
        Seraglio = AlhambraBase.BuildingType.Seraglio,
        [DescriptionResourceAttribute(Resource.String.arcades)]
        [ImageAttribute(Resource.Drawable.Arcades)]
        Arcades = AlhambraBase.BuildingType.Arcades,
        [DescriptionResourceAttribute(Resource.String.chambers)]
        [ImageAttribute(Resource.Drawable.Chambers)]
        Chambers = AlhambraBase.BuildingType.Chambers,
        [DescriptionResourceAttribute(Resource.String.garden)]
        [ImageAttribute(Resource.Drawable.Garden)]
        Garden = AlhambraBase.BuildingType.Garden,
        [DescriptionResourceAttribute(Resource.String.tower)]
        [ImageAttribute(Resource.Drawable.Tower)]
        Tower = AlhambraBase.BuildingType.Tower
    }
}