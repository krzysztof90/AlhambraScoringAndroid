using AndroidBase.Attributes;

namespace AlhambraScoringAndroid.GamePlay
{
    public enum AlcazabaOption
    {
        [DescriptionResourceAttribute(Resource.String.alcazaba_without)]
        WithoutTile,
        [DescriptionResourceAttribute(Resource.String.alcazaba_with)]
        WithTile,
    }
}