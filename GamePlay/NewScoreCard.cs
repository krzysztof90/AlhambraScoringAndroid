using AlhambraScoringAndroid.Attributes;
using AndroidBase.Attributes;

namespace AlhambraScoringAndroid.GamePlay
{
    public enum NewScoreCard
    {
        [NewScoreCardAttribute(BuildingType.Pavilion, BuildingType.Chambers, BuildingType.Tower, BuildingType.Seraglio, BuildingType.Arcades, BuildingType.Garden)]
        [DescriptionResourceAttribute(Resource.String.new_score_card_1)]
        Card1,
        [NewScoreCardAttribute(BuildingType.Pavilion, BuildingType.Chambers, BuildingType.Garden, BuildingType.Seraglio, BuildingType.Tower, BuildingType.Arcades)]
        [DescriptionResourceAttribute(Resource.String.new_score_card_2)]
        Card2,
        [NewScoreCardAttribute(BuildingType.Pavilion, BuildingType.Garden, BuildingType.Tower, BuildingType.Arcades, BuildingType.Seraglio, BuildingType.Chambers)]
        [DescriptionResourceAttribute(Resource.String.new_score_card_3)]
        Card3,
        [NewScoreCardAttribute(BuildingType.Seraglio, BuildingType.Garden, BuildingType.Tower, BuildingType.Chambers, BuildingType.Arcades, BuildingType.Pavilion)]
        [DescriptionResourceAttribute(Resource.String.new_score_card_4)]
        Card4,
        [NewScoreCardAttribute(BuildingType.Seraglio, BuildingType.Arcades, BuildingType.Chambers, BuildingType.Tower, BuildingType.Garden, BuildingType.Pavilion)]
        [DescriptionResourceAttribute(Resource.String.new_score_card_5)]
        Card5,
        [NewScoreCardAttribute(BuildingType.Seraglio, BuildingType.Tower, BuildingType.Arcades, BuildingType.Garden, BuildingType.Chambers, BuildingType.Pavilion)]
        [DescriptionResourceAttribute(Resource.String.new_score_card_6)]
        Card6,
        [NewScoreCardAttribute(BuildingType.Arcades, BuildingType.Pavilion, BuildingType.Seraglio, BuildingType.Tower, BuildingType.Garden, BuildingType.Chambers)]
        [DescriptionResourceAttribute(Resource.String.new_score_card_7)]
        Card7,
        [NewScoreCardAttribute(BuildingType.Arcades, BuildingType.Pavilion, BuildingType.Garden, BuildingType.Chambers, BuildingType.Tower, BuildingType.Seraglio)]
        [DescriptionResourceAttribute(Resource.String.new_score_card_8)]
        Card8,
        [NewScoreCardAttribute(BuildingType.Arcades, BuildingType.Chambers, BuildingType.Garden, BuildingType.Seraglio, BuildingType.Pavilion, BuildingType.Tower)]
        [DescriptionResourceAttribute(Resource.String.new_score_card_9)]
        Card9,
        [NewScoreCardAttribute(BuildingType.Chambers, BuildingType.Arcades, BuildingType.Pavilion, BuildingType.Tower, BuildingType.Garden, BuildingType.Seraglio)]
        [DescriptionResourceAttribute(Resource.String.new_score_card_10)]
        Card10,
        [NewScoreCardAttribute(BuildingType.Chambers, BuildingType.Arcades, BuildingType.Pavilion, BuildingType.Garden, BuildingType.Seraglio, BuildingType.Tower)]
        [DescriptionResourceAttribute(Resource.String.new_score_card_11)]
        Card11,
        [NewScoreCardAttribute(BuildingType.Chambers, BuildingType.Garden, BuildingType.Seraglio, BuildingType.Arcades, BuildingType.Pavilion, BuildingType.Tower)]
        [DescriptionResourceAttribute(Resource.String.new_score_card_12)]
        Card12,
        [NewScoreCardAttribute(BuildingType.Garden, BuildingType.Tower, BuildingType.Arcades, BuildingType.Pavilion, BuildingType.Chambers, BuildingType.Seraglio)]
        [DescriptionResourceAttribute(Resource.String.new_score_card_13)]
        Card13,
        [NewScoreCardAttribute(BuildingType.Garden, BuildingType.Tower, BuildingType.Arcades, BuildingType.Pavilion, BuildingType.Seraglio, BuildingType.Chambers)]
        [DescriptionResourceAttribute(Resource.String.new_score_card_14)]
        Card14,
        [NewScoreCardAttribute(BuildingType.Garden, BuildingType.Seraglio, BuildingType.Chambers, BuildingType.Pavilion, BuildingType.Tower, BuildingType.Arcades)]
        [DescriptionResourceAttribute(Resource.String.new_score_card_15)]
        Card15,
        [NewScoreCardAttribute(BuildingType.Tower, BuildingType.Seraglio, BuildingType.Pavilion, BuildingType.Garden, BuildingType.Chambers, BuildingType.Arcades)]
        [DescriptionResourceAttribute(Resource.String.new_score_card_16)]
        Card16,
        [NewScoreCardAttribute(BuildingType.Tower, BuildingType.Seraglio, BuildingType.Chambers, BuildingType.Arcades, BuildingType.Pavilion, BuildingType.Garden)]
        [DescriptionResourceAttribute(Resource.String.new_score_card_17)]
        Card17,
        [NewScoreCardAttribute(BuildingType.Tower, BuildingType.Pavilion, BuildingType.Seraglio, BuildingType.Chambers, BuildingType.Arcades, BuildingType.Garden)]
        [DescriptionResourceAttribute(Resource.String.new_score_card_18)]
        Card18,
    }
}