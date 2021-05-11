using AndroidBase.Attributes;

namespace AlhambraScoringAndroid.GamePlay
{
    public enum GranadaBuildingType
    {
        [DescriptionResourceAttribute(Resource.String.arena)]
        Arena,
        [DescriptionResourceAttribute(Resource.String.bath_house)]
        BathHouse,
        [DescriptionResourceAttribute(Resource.String.library)]
        Library,
        [DescriptionResourceAttribute(Resource.String.hostel)]
        Hostel,
        [DescriptionResourceAttribute(Resource.String.hospital)]
        Hospital,
        [DescriptionResourceAttribute(Resource.String.market)]
        Market,
        [DescriptionResourceAttribute(Resource.String.park)]
        Park,
        [DescriptionResourceAttribute(Resource.String.school)]
        School,
        [DescriptionResourceAttribute(Resource.String.residential_area)]
        ResidentialArea
    }
}