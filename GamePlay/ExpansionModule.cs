using System.ComponentModel;

namespace AlhambraScoringAndroid.GamePlay
{
    public enum ExpansionModule
    {
        [Description("Vizier’s Favour")]
        ExpansionViziersFavour,
        [Description("Currency Exchange Cards")]
        ExpansionCurrencyExchangeCards,
        [Description("Bonus Cards")]
        ExpansionBonusCards,
        [Description("Squares")]
        ExpansionSquares,
        [Description("City Gates")]
        ExpansionCityGates,
        [Description("Diamonds")]
        ExpansionDiamonds,
        [Description("Characters")]
        ExpansionCharacters,
        [Description("Camps")]
        ExpansionCamps,
        [Description("City Walls")]
        ExpansionCityWalls,
        [Description("Thieves")]
        ExpansionThieves,
        [Description("Change")]
        ExpansionChange,
        [Description("Street Trader")]
        ExpansionStreetTrader,
        [Description("Treasure Chamber")]
        ExpansionTreasureChamber,
        [Description("Master Builders")]
        ExpansionMasterBuilders,
        [Description("Invaders")]
        ExpansionInvaders,
        [Description("Bazaars")]
        ExpansionBazaars,
        [Description("New Score Cards")]
        ExpansionNewScoreCards,
        [Description("Power of Sultan")]
        ExpansionPowerOfSultan,
        [Description("Caravanserai")]
        ExpansionCaravanserai,
        [Description("Art of the Moors")]
        ExpansionArtOfTheMoors,
        [Description("Falconers")]
        ExpansionFalconers,
        [Description("Watchtowers")]
        ExpansionWatchtowers,
        [Description("Building Sites")]
        ExpansionBuildingSites,
        [Description("Exchange Certificates")]
        ExpansionExchangeCertificates,
        [Description("Magical Buildings")]
        QueenieMagicalBuildings,
        [Description("Medina")]
        QueenieMedina,
        [Description("New Building Grounds")]
        DesignerNewBuildingGrounds,
        [Description("Major Construction Projects")]
        DesignerMajorConstructionProjects,
        [Description("Palace Staff")]
        DesignerPalaceStaff,
        [Description("Orchards")]
        DesignerOrchards,
        [Description("Travelling Craftsmen")]
        DesignerTravellingCraftsmen,
        [Description("Bathhouses")]
        DesignerBathhouses,
        [Description("Wishing Well")]
        DesignerWishingWell,
        [Description("Fresh Colors")]
        DesignerFreshColors,
        [Description("Palace Designers")]
        DesignerPalaceDesigners,
        [Description("Alhambra Zoo")]
        DesignerAlhambraZoo,
        [Description("Gates without End")]
        DesignerGatesWithoutEnd,
        [Description("Buildings of Power")]
        DesignerBuildingsOfPower,
        [Description("Extensions")]
        DesignerExtensions,
        [Description("Handymen")]
        DesignerHandymen,
        [Description("Personal Building Market")]
        FanPersonalBuildingMarket,
        [Description("Treasures")]
        FanTreasures,
        [Description("Caliph’s Guidelines")]
        FanCaliphsGuidelines,
    }
}