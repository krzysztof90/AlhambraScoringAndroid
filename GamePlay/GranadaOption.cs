using AlhambraScoringAndroid.Attributes;

namespace AlhambraScoringAndroid.GamePlay
{
    public enum GranadaOption
    {
        [DescriptionResourceAttribute(Resource.String.granada_without)]
        Without,
        [DescriptionResourceAttribute(Resource.String.granada_with)]
        With,
        [DescriptionResourceAttribute(Resource.String.granada_alone)]
        Alone,
    }
}