using AndroidBase.Attributes;

namespace AlhambraScoringAndroid.GamePlay
{
    public enum CaliphsGuidelinesMission
    {
        [DescriptionResourceAttribute(Resource.String.mission1RowsCount)]
        [ImageAttribute(Resource.Drawable.Mission1)]
        Mission1,
        [DescriptionResourceAttribute(Resource.String.mission2ColumnsCount)]
        [ImageAttribute(Resource.Drawable.Mission2)]
        Mission2,
        [DescriptionResourceAttribute(Resource.String.mission3Adjacent2BuildingsCount)]
        [ImageAttribute(Resource.Drawable.Mission3)]
        Mission3,
        [DescriptionResourceAttribute(Resource.String.mission4SecondLongestWall)]
        [ImageAttribute(Resource.Drawable.Mission4)]
        Mission4,
        [DescriptionResourceAttribute(Resource.String.mission5LongestDiagonalLine)]
        [ImageAttribute(Resource.Drawable.Mission5)]
        Mission5,
        [DescriptionResourceAttribute(Resource.String.mission6DoubleWallCount)]
        [ImageAttribute(Resource.Drawable.Mission6)]
        Mission6,
        [DescriptionResourceAttribute(Resource.String.mission7NumberOfDifferentTypesOfBuildings)]
        [ImageAttribute(Resource.Drawable.Mission7)]
        Mission7,
        [DescriptionResourceAttribute(Resource.String.mission8PathBuildingsNumber)]
        [ImageAttribute(Resource.Drawable.Mission8)]
        Mission8,
        [DescriptionResourceAttribute(Resource.String.mission9Grids22Count)]
        [ImageAttribute(Resource.Drawable.Mission9)]
        Mission9,
    }
}