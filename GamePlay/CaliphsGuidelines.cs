using AndroidBase.Attributes;

namespace AlhambraScoringAndroid.GamePlay
{
    public enum CaliphsGuidelinesMission
    {
        [ImageAttribute(Resource.Drawable.Mission1)]
        [DescriptionResourceAttribute(Resource.String.mission1RowsCount)]
        Mission1,
        [ImageAttribute(Resource.Drawable.Mission2)]
        [DescriptionResourceAttribute(Resource.String.mission2ColumnsCount)]
        Mission2,
        [ImageAttribute(Resource.Drawable.Mission3)]
        [DescriptionResourceAttribute(Resource.String.mission3Adjacent2BuildingsCount)]
        Mission3,
        [ImageAttribute(Resource.Drawable.Mission4)]
        [DescriptionResourceAttribute(Resource.String.mission4SecondLongestWall)]
        Mission4,
        [ImageAttribute(Resource.Drawable.Mission5)]
        [DescriptionResourceAttribute(Resource.String.mission5LongestDiagonalLine)]
        Mission5,
        [ImageAttribute(Resource.Drawable.Mission6)]
        [DescriptionResourceAttribute(Resource.String.mission6DoubleWallCount)]
        Mission6,
        [ImageAttribute(Resource.Drawable.Mission7)]
        [DescriptionResourceAttribute(Resource.String.mission7NumberOfDifferentTypesOfBuildings)]
        Mission7,
        [ImageAttribute(Resource.Drawable.Mission8)]
        [DescriptionResourceAttribute(Resource.String.mission8PathBuildingsNumber)]
        Mission8,
        [ImageAttribute(Resource.Drawable.Mission9)]
        [DescriptionResourceAttribute(Resource.String.mission9Grids22Count)]
        Mission9,
    }
}