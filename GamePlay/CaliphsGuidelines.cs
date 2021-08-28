using AndroidBase.Attributes;

namespace AlhambraScoringAndroid.GamePlay
{
    public enum CaliphsGuidelinesMission
    {
        [DescriptionResourceAttribute(Resource.String.mission1RowsCount)]
        Mission1,
        [DescriptionResourceAttribute(Resource.String.mission2ColumnsCount)]
        Mission2,
        [DescriptionResourceAttribute(Resource.String.mission3Adjacent2BuildingsCount)]
        Mission3,
        [DescriptionResourceAttribute(Resource.String.mission4SecondLongestWall)]
        Mission4,
        [DescriptionResourceAttribute(Resource.String.mission5LongestDiagonalLine)]
        Mission5,
        [DescriptionResourceAttribute(Resource.String.mission6DoubleWallCount)]
        Mission6,
        [DescriptionResourceAttribute(Resource.String.mission7NumberOfDifferentTypesOfBuildings)]
        Mission7,
        [DescriptionResourceAttribute(Resource.String.mission8PathBuildingsNumber)]
        Mission8,
        [DescriptionResourceAttribute(Resource.String.mission9Grids22Count)]
        Mission9,
    }
}