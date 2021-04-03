using AlhambraScoringAndroid.Atributes;
using System.ComponentModel;

namespace AlhambraScoringAndroid.GamePlay
{
    public enum NewScoreCard
    {
        [NewScoreCardAttribute(BuildingType.Pavilion, BuildingType.Chambers, BuildingType.Tower, BuildingType.Seraglio, BuildingType.Arcades, BuildingType.Garden)]
        [Description("Card 1")]
        Card1,
        [NewScoreCardAttribute(BuildingType.Pavilion, BuildingType.Chambers, BuildingType.Garden, BuildingType.Seraglio, BuildingType.Tower, BuildingType.Arcades)]
        [Description("Card 2")]
        Card2,
        [NewScoreCardAttribute(BuildingType.Pavilion, BuildingType.Garden, BuildingType.Tower, BuildingType.Arcades, BuildingType.Seraglio, BuildingType.Chambers)]
        [Description("Card 3")]
        Card3,
        [NewScoreCardAttribute(BuildingType.Seraglio, BuildingType.Garden, BuildingType.Tower, BuildingType.Chambers, BuildingType.Arcades, BuildingType.Pavilion)]
        [Description("Card 4")]
        Card4,
        [NewScoreCardAttribute(BuildingType.Seraglio, BuildingType.Arcades, BuildingType.Chambers, BuildingType.Tower, BuildingType.Garden, BuildingType.Pavilion)]
        [Description("Card 5")]
        Card5,
        [NewScoreCardAttribute(BuildingType.Seraglio, BuildingType.Tower, BuildingType.Arcades, BuildingType.Garden, BuildingType.Chambers, BuildingType.Pavilion)]
        [Description("Card 6")]
        Card6,
        [NewScoreCardAttribute(BuildingType.Arcades, BuildingType.Pavilion, BuildingType.Seraglio, BuildingType.Tower, BuildingType.Garden, BuildingType.Chambers)]
        [Description("Card 7")]
        Card7,
        [NewScoreCardAttribute(BuildingType.Arcades, BuildingType.Pavilion, BuildingType.Garden, BuildingType.Chambers, BuildingType.Tower, BuildingType.Seraglio)]
        [Description("Card 8")]
        Card8,
        [NewScoreCardAttribute(BuildingType.Arcades, BuildingType.Chambers, BuildingType.Garden, BuildingType.Seraglio, BuildingType.Pavilion, BuildingType.Tower)]
        [Description("Card 9")]
        Card9,
        [NewScoreCardAttribute(BuildingType.Chambers, BuildingType.Arcades, BuildingType.Pavilion, BuildingType.Tower, BuildingType.Garden, BuildingType.Seraglio)]
        [Description("Card 10")]
        Card10,
        [NewScoreCardAttribute(BuildingType.Chambers, BuildingType.Arcades, BuildingType.Pavilion, BuildingType.Garden, BuildingType.Seraglio, BuildingType.Tower)]
        [Description("Card 11")]
        Card11,
        [NewScoreCardAttribute(BuildingType.Chambers, BuildingType.Garden, BuildingType.Seraglio, BuildingType.Arcades, BuildingType.Pavilion, BuildingType.Tower)]
        [Description("Card 12")]
        Card12,
        [NewScoreCardAttribute(BuildingType.Garden, BuildingType.Tower, BuildingType.Arcades, BuildingType.Pavilion, BuildingType.Chambers, BuildingType.Seraglio)]
        [Description("Card 13")]
        Card13,
        [NewScoreCardAttribute(BuildingType.Garden, BuildingType.Tower, BuildingType.Arcades, BuildingType.Pavilion, BuildingType.Seraglio, BuildingType.Chambers)]
        [Description("Card 14")]
        Card14,
        [NewScoreCardAttribute(BuildingType.Garden, BuildingType.Seraglio, BuildingType.Chambers, BuildingType.Pavilion, BuildingType.Tower, BuildingType.Arcades)]
        [Description("Card 15")]
        Card15,
        [NewScoreCardAttribute(BuildingType.Tower, BuildingType.Seraglio, BuildingType.Pavilion, BuildingType.Garden, BuildingType.Chambers, BuildingType.Arcades)]
        [Description("Card 16")]
        Card16,
        [NewScoreCardAttribute(BuildingType.Tower, BuildingType.Seraglio, BuildingType.Chambers, BuildingType.Arcades, BuildingType.Pavilion, BuildingType.Garden)]
        [Description("Card 17")]
        Card17,
        [NewScoreCardAttribute(BuildingType.Tower, BuildingType.Pavilion, BuildingType.Seraglio, BuildingType.Chambers, BuildingType.Arcades, BuildingType.Garden)]
        [Description("Card 18")]
        Card18,
    }
}