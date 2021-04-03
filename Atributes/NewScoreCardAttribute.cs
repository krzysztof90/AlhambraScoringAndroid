using AlhambraScoringAndroid.Attributes;
using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.Tools;
using AlhambraScoringAndroid.Tools.Enums;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.Atributes
{
    public class NewScoreCardAttribute : MultipleImageAttribute
    {
        public readonly List<BuildingType> BuildingTypes;

        public NewScoreCardAttribute(BuildingType buildingType1, BuildingType buildingType2, BuildingType buildingType3, BuildingType buildingType4, BuildingType buildingType5, BuildingType buildingType6) : base(HorizontalVertical.Horizontal, GetResource(buildingType1), GetResource(buildingType2), GetResource(buildingType3), GetResource(buildingType4), GetResource(buildingType5), GetResource(buildingType6))
        {
            BuildingTypes = new List<BuildingType>() { buildingType1, buildingType2, buildingType3, buildingType4, buildingType5, buildingType6 };
        }

        private static int GetResource(BuildingType buildingType)
        {
            return buildingType.GetEnumAttribute<BuildingType, ImageAttribute>().Resource;
        }
    }
}