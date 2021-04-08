using AlhambraScoringAndroid.Attributes;

namespace AlhambraScoringAndroid.GamePlay
{
    public enum ScoringRound
    {
        [DescriptionResourceAttribute(Resource.String.round_1)]
        First,
        [DescriptionResourceAttribute(Resource.String.round_2)]
        Second,
        [DescriptionResourceAttribute(Resource.String.round_3_before)]
        ThirdBeforeLeftover,
        [DescriptionResourceAttribute(Resource.String.round_3)]
        Third,
        [DescriptionResourceAttribute(Resource.String.finish)]
        Finish
    }
}