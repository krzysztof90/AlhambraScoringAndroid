using AlhambraScoringAndroid.GamePlay;
using AndroidBase.Attributes;
using AndroidBase.Tools;
using AndroidBase.Tools.Enums;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.Attributes
{
    public class NewScoreCardAttribute : MultipleImageAttribute
    {
        public readonly List<AlhambraBase.BuildingType> BuildingTypes;

        public NewScoreCardAttribute(BuildingType buildingType1, BuildingType buildingType2, BuildingType buildingType3, BuildingType buildingType4, BuildingType buildingType5, BuildingType buildingType6) : base(HorizontalVertical.Horizontal, GetResource(buildingType1), GetResource(buildingType2), GetResource(buildingType3), GetResource(buildingType4), GetResource(buildingType5), GetResource(buildingType6))
        {
            BuildingTypes = new List<AlhambraBase.BuildingType>() { (AlhambraBase.BuildingType)buildingType1, (AlhambraBase.BuildingType)buildingType2, (AlhambraBase.BuildingType)buildingType3, (AlhambraBase.BuildingType)buildingType4, (AlhambraBase.BuildingType)buildingType5, (AlhambraBase.BuildingType)buildingType6 };
        }

        private static int GetResource(BuildingType buildingType)
        {
            return buildingType.GetEnumAttribute<BuildingType, ImageAttribute>().Resource;
        }
    }
}