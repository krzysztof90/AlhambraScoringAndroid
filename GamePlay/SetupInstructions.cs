﻿
using AlhambraScoringAndroid.Attributes;

namespace AlhambraScoringAndroid.GamePlay
{
    public enum SetupInstructions
    {
        [DescriptionResourceAttribute(Resource.String.instruction_put_buildings_of_power_tiles)]
        PutBuildingsOfPowerTiles,
        [DescriptionResourceAttribute(Resource.String.instruction_put_camp_tiles)]
        PutCampTiles,
        [DescriptionResourceAttribute(Resource.String.instruction_put_bazaars_tiles)]
        PutBazaarsTiles,
        [DescriptionResourceAttribute(Resource.String.instruction_put_magical_buildings_tiles)]
        PutMagicalBuildingsTiles,
        [DescriptionResourceAttribute(Resource.String.instruction_put_medina_tiles)]
        PutMedinaTiles,
        [DescriptionResourceAttribute(Resource.String.instruction_put_new_building_grounds_tiles)]
        PutNewBuildingGroundsTiles,
        [DescriptionResourceAttribute(Resource.String.instruction_put_bathhouse_tiles)]
        PutBathhouseTiles,
        [DescriptionResourceAttribute(Resource.String.instruction_put_wishing_well_tiles)]
        PutWishingWellTiles,
        [DescriptionResourceAttribute(Resource.String.instruction_shuffle_building_tiles)]
        ShuffleBuildingTiles,
        [DescriptionResourceAttribute(Resource.String.instruction_place_buildings)]
        PlaceBuildings,
        [DescriptionResourceAttribute(Resource.String.instruction_give_buildings_to_dirk)]
        GiveBuildingsToDirk,
        [DescriptionResourceAttribute(Resource.String.instruction_granada_shuffle_building_tiles)]
        GranadaShuffleBuildingTiles,
        [DescriptionResourceAttribute(Resource.String.instruction_granada_place_buildings)]
        GranadaPlaceBuildings,

        [DescriptionResourceAttribute(Resource.String.instruction_shuffle_squares)]
        ShuffleSquares,
        [DescriptionResourceAttribute(Resource.String.instruction_shuffle_watchtowers)]
        ShuffleWatchtowers,
        [DescriptionResourceAttribute(Resource.String.instruction_shuffle_major_construction_projects)]
        ShuffleMajorConstructionProjects,
        [DescriptionResourceAttribute(Resource.String.instruction_shuffle_gate_board)]
        ShuffleGateBoard,
        [DescriptionResourceAttribute(Resource.String.instruction_shuffle_extensions)]
        ShuffleExtensions,

        [DescriptionResourceAttribute(Resource.String.instruction_remove_card_deck)]
        RemoveCardDeck,
        [DescriptionResourceAttribute(Resource.String.instruction_put_diamond_cards)]
        PutDiamondCards,
        [DescriptionResourceAttribute(Resource.String.instruction_shuffle_cards)]
        ShuffleCards,
        [DescriptionResourceAttribute(Resource.String.instruction_deal_money)]
        DealMoney,
        [DescriptionResourceAttribute(Resource.String.instruction_place_cards)]
        PlaceCards,
        [DescriptionResourceAttribute(Resource.String.instruction_divide_cards)]
        DivideCards,
        [DescriptionResourceAttribute(Resource.String.instruction_put_2_scoring_cards)]
        Put2ScoringCards,
        [DescriptionResourceAttribute(Resource.String.instruction_put_1_scoring_card_middle)]
        Put1ScoringCardMiddle,
        [DescriptionResourceAttribute(Resource.String.instruction_put_currency_exchange_cards)]
        PutCurrencyExchangeCards,
        [DescriptionResourceAttribute(Resource.String.instruction_put_city_gates_cards)]
        PutCityGatesCards,
        [DescriptionResourceAttribute(Resource.String.instruction_put_characters)]
        PutCharacters,
        [DescriptionResourceAttribute(Resource.String.instruction_put_city_walls)]
        PutCityWalls,
        [DescriptionResourceAttribute(Resource.String.instruction_put_master_builders)]
        PutMasterBuilders,
        [DescriptionResourceAttribute(Resource.String.instruction_put_master_builders_6)]
        PutMasterBuilders6,
        [DescriptionResourceAttribute(Resource.String.instruction_put_power_of_sultan)]
        PutPowerOfSultan,
        [DescriptionResourceAttribute(Resource.String.instruction_join_piles)]
        JoinPiles,

        [DescriptionResourceAttribute(Resource.String.instruction_bonus_cards_deal_3)]
        BonusCardsDeal3,
        [DescriptionResourceAttribute(Resource.String.instruction_bonus_cards_deal_2)]
        BonusCardsDeal2,
        [DescriptionResourceAttribute(Resource.String.instruction_bonus_cards_deal_1)]
        BonusCardsDeal1,
        [DescriptionResourceAttribute(Resource.String.instruction_thieves_deal_4)]
        ThievesDeal4,
        [DescriptionResourceAttribute(Resource.String.instruction_thieves_deal_3)]
        ThievesDeal3,
        [DescriptionResourceAttribute(Resource.String.instruction_thieves_deal_2)]
        ThievesDeal2,
        [DescriptionResourceAttribute(Resource.String.instruction_master_builders_deal)]
        MasterBuildersDeal,

        [DescriptionResourceAttribute(Resource.String.instruction_shuffle_invasion)]
        ShuffleInvasion,
        [DescriptionResourceAttribute(Resource.String.instruction_shuffle_scout)]
        ShuffleScout,
        [DescriptionResourceAttribute(Resource.String.instruction_shuffle_caravanserai)]
        ShuffleCaravanserai,
    }
}