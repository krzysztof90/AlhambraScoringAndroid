using AndroidBase.Attributes;

namespace AlhambraScoringAndroid.GamePlay
{
    public enum GranadaBuildingType
    {
        [DescriptionResourceAttribute(Resource.String.arena)]
        Arena = AlhambraBase.GranadaBuildingType.Arena,
        [DescriptionResourceAttribute(Resource.String.bath_house)]
        BathHouse = AlhambraBase.GranadaBuildingType.BathHouse,
        [DescriptionResourceAttribute(Resource.String.library)]
        Library = AlhambraBase.GranadaBuildingType.Library,
        [DescriptionResourceAttribute(Resource.String.hostel)]
        Hostel = AlhambraBase.GranadaBuildingType.Hostel,
        [DescriptionResourceAttribute(Resource.String.hospital)]
        Hospital = AlhambraBase.GranadaBuildingType.Hospital,
        [DescriptionResourceAttribute(Resource.String.market)]
        Market = AlhambraBase.GranadaBuildingType.Market,
        [DescriptionResourceAttribute(Resource.String.park)]
        Park = AlhambraBase.GranadaBuildingType.Park,
        [DescriptionResourceAttribute(Resource.String.school)]
        School = AlhambraBase.GranadaBuildingType.School,
        [DescriptionResourceAttribute(Resource.String.residential_area)]
        ResidentialArea = AlhambraBase.GranadaBuildingType.ResidentialArea
    }
}