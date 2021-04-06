using System.ComponentModel;

namespace AlhambraScoringAndroid.GamePlay
{
    public enum SetupInstructions
    {
        [Description("Replace the base tiles with buildings of power")]
        PutBuildingsOfPowerTiles,
        [Description("Put camps to all of the building tiles")]
        PutCampTiles,
        [Description("Put bazaars to all of the building tiles")]
        PutBazaarsTiles,
        [Description("Put Magical buildings to all of the building tiles")]
        PutMagicalBuildingsTiles,
        [Description("Put medina tiles to all of the building tiles")]
        PutMedinaTiles,
        [Description("Put building site tiles to all of the building tiles")]
        PutNewBuildingGroundsTiles,
        [Description("Put Bathhouse tiles to all of the building tiles")]
        PutBathhouseTiles,
        [Description("Put wishing well tiles to all of the building tiles")]
        PutWishingWellTiles,
        [Description("Shuffle building tiles")]
        ShuffleBuildingTiles,
        [Description("Place 4 building tiles on the building market")]
        PlaceBuildings,
        [Description("Shuffle double-sided building tiles")]
        GranadaShuffleBuildingTiles,
        [Description("Place 4 Granada building tiles on the building market")]
        GranadaPlaceBuildings,
        
        [Description("Shuffle each of the squares piles")]
        ShuffleSquares,
        [Description("Place each of the watchtower piles")]
        ShuffleWatchtowers,
        [Description("Shuffle the major project tiles")]
        ShuffleMajorConstructionProjects,
        [Description("The wall pieces, semi-buildings and plaza tiles are kept next to the gate board")]
        ShuffleGateBoard,
        [Description("Shuffle extension tiles")]
        ShuffleExtensions,

        [Description("Put diamond cards to the other money cards")]
        PutDiamondCards,
        [Description("Shuffle cards")]
        ShuffleCards,
        [Description("Deal starting money to each player")]
        DealMoney,
        [Description("Place 4 cards on the card display")]
        PlaceCards,
        [Description("Divide cards into 5 piles")]
        DivideCards,
        [Description("Shuffle 1st scoring card into the 2nd pile and 2nd scoring card into the 4th pile")]
        Put2ScoringCards,
        [Description("Shuffle 1st scoring card into the 3rd pile")]
        Put1ScoringCardMiddle,
        [Description("Shuffle 2 currency exchange cards into the 2nd, 3rd, 4th piles")]
        PutCurrencyExchangeCards,
        [Description("Shuffle city-gate cards into the 3rd, 4th, 5th piles")]
        PutCityGatesCards,
        [Description("Shuffle 2 character cards into the 2nd, 3rd, 4th piles")]
        PutCharacters,
        [Description("Shuffle 2 city-wall cards into the 2nd, 3rd, 4th, 5th piles")]
        PutCityWalls,
        [Description("Shuffle 3 master-builder cards into the 3rd, 5th piles")]
        PutMasterBuilders,
        [Description("Shuffle 3 master-builder cards into the 3rd pile and 1 card into the 5th pile")]
        PutMasterBuilders6,
        [Description("Shuffle 3 sultan cards into the 1st pile, 2 cards into the 2nd pile and 3 cards into the 3rd pile")]
        PutPowerOfSultan,
        [Description("Put the piles on top of one another")]
        JoinPiles,

        [Description("Shuffle and deal 3 bonus cards to each player")]
        BonusCardsDeal3,
        [Description("Shuffle and deal 2 bonus cards to each player")]
        BonusCardsDeal2,
        [Description("Shuffle and deal 1 bonus card to each player")]
        BonusCardsDeal1,
        [Description("Shuffle and deal 4 thieves to each player")]
        ThievesDeal4,
        [Description("Shuffle and deal 3 thieves to each player")]
        ThievesDeal3,
        [Description("Shuffle and deal 2 thieves to each player")]
        ThievesDeal2,
        [Description("Deal 2 master-builder cards to each player")]
        MasterBuildersDeal,
        
        [Description("Shuffle invasion cards")]
        ShuffleInvasion,
        [Description("Shuffle scout cards")]
        ShuffleScout,
        [Description("Shuffle caravanserai cards")]
        ShuffleCaravanserai,
    }
}