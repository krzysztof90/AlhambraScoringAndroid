using AndroidBase.Attributes;

namespace AlhambraScoringAndroid.Options
{
    public enum SettingsType
    {
        [SettingNameBoolAttribute("validate_wall_length", true, Resource.String.validateWallLength)]
        ValidateWallLength,
        [SettingNameBoolAttribute("validate_buildings_number", true, Resource.String.validateBuildingsNumber)]
        ValidateBuildingsNumber,
        [SettingNameBoolAttribute("validate_buildings_number_previous", true, Resource.String.validateBuildingsNumberPrevious)]
        ValidateBuildingsNumberPrevious,
        [SettingNameBoolAttribute("validate_buildings_price", true, Resource.String.validateBuildingsPrice)]
        ValidateBuildingsPrice,
        [SettingNameBoolAttribute("validate_bonus_cards_player", true, Resource.String.validateBonusCardsPlayer)]
        ValidateBonusCardsPlayer,
        [SettingNameBoolAttribute("validate_bonus_cards_buildings", true, Resource.String.validateBonusCardsBuildings)]
        ValidateBonusCardsBuildings,
        [SettingNameBoolAttribute("validate_bonus_cards", true, Resource.String.validateBonusCards)]
        ValidateBonusCards,
        [SettingNameBoolAttribute("validate_squares_player", true, Resource.String.validateSquaresPlayer)]
        ValidateSquaresPlayer,
        [SettingNameBoolAttribute("validate_squares", true, Resource.String.validateSquares)]
        ValidateSquares,
        [SettingNameBoolAttribute("validate_multiple_wiseman", true, Resource.String.validateMultipleWiseman)]
        ValidateMultipleWiseman,
        [SettingNameBoolAttribute("validate_previous_wiseman", true, Resource.String.validatePreviousWiseman)]
        ValidatePreviousWiseman,
        [SettingNameBoolAttribute("validate_multiple_citywatch", true, Resource.String.validateMultipleCitywatch)]
        ValidateMultipleCitywatch,
        [SettingNameBoolAttribute("validate_previous_citywatch", true, Resource.String.validatePreviousCitywatch)]
        ValidatePreviousCitywatch,
        [SettingNameBoolAttribute("validate_camps", true, Resource.String.validateCamps)]
        ValidateCamps,
        [SettingNameBoolAttribute("validate_citizens_buildings", true, Resource.String.validateCitizensBuildings)]
        ValidateCitizensBuildings,
        [SettingNameBoolAttribute("validate_citizens", true, Resource.String.validateCitizens)]
        ValidateCitizens,
        [SettingNameBoolAttribute("validate_treasures_player", true, Resource.String.validateTreasuresPlayer)]
        ValidateTreasuresPlayer,
        [SettingNameBoolAttribute("validate_treasures", true, Resource.String.validateTreasures)]
        ValidateTreasures,
        [SettingNameBoolAttribute("validate_unprotected_sides", true, Resource.String.validateUnprotectedSides)]
        ValidateUnprotectedSides,
        [SettingNameBoolAttribute("validate_bazaars_points", true, Resource.String.validateBazaarsPoints)]
        ValidateBazaarsPoints,
        [SettingNameBoolAttribute("validate_culture_counters", true, Resource.String.validateCultureCounters)]
        ValidateCultureCounters,
        [SettingNameBoolAttribute("validate_culture_counters_previous", true, Resource.String.validateCultureCountersPrevious)]
        ValidateCultureCountersPrevious,
        [SettingNameBoolAttribute("validate_falcons", true, Resource.String.validateFalcons)]
        ValidateFalcons,
        [SettingNameBoolAttribute("validate_watchtower_wall", true, Resource.String.validateWatchtowerWall)]
        ValidateWatchtowerWall,
        [SettingNameBoolAttribute("validate_watchtower", true, Resource.String.validateWatchtower)]
        ValidateWatchtower,
        [SettingNameBoolAttribute("validate_medin", true, Resource.String.validateMedin)]
        ValidateMedin,
        [SettingNameBoolAttribute("validate_medin_previous", true, Resource.String.validateMedinPrevious)]
        ValidateMedinPrevious,
        [SettingNameBoolAttribute("validate_servants", true, Resource.String.validateServants)]
        ValidateServants,
        [SettingNameBoolAttribute("validate_single_fruits", true, Resource.String.validateSingleFruits)]
        ValidateSingleFruits,
        [SettingNameBoolAttribute("validate_fruits", true, Resource.String.validateFruits)]
        ValidateFruits,
        [SettingNameBoolAttribute("validate_bathhouses", true, Resource.String.validateBathhouses)]
        ValidateBathhouses,
        [SettingNameBoolAttribute("validate_wishing_wells_player", true, Resource.String.validateWishingWellsPlayer)]
        ValidateWishingWellsPlayer,
        [SettingNameBoolAttribute("validate_wishing_wells", true, Resource.String.validateWishingWells)]
        ValidateWishingWells,
        [SettingNameBoolAttribute("validate_multiple_completed_project", true, Resource.String.validateMultipleCompletedProject)]
        ValidateMultipleCompletedProject,
        [SettingNameBoolAttribute("validate_previous_completed_project", true, Resource.String.validatePreviousCompletedProject)]
        ValidatePreviousCompletedProject,
        [SettingNameBoolAttribute("validate_animals_player", true, Resource.String.validateAnimalsPlayer)]
        ValidateAnimalsPlayer,
        [SettingNameBoolAttribute("validate_animals", true, Resource.String.validateAnimals)]
        ValidateAnimals,
        [SettingNameBoolAttribute("validate_animals_previous", true, Resource.String.validateAnimalsPrevious)]
        ValidateAnimalsPrevious,
        [SettingNameBoolAttribute("validate_multiple_semi_buildings", true, Resource.String.validateMultipleSemiBuildings)]
        ValidateMultipleSemiBuildings,
        [SettingNameBoolAttribute("validate_previous_semi_buildings", true, Resource.String.validatePreviousSemiBuildings)]
        ValidatePreviousSemiBuildings,
        [SettingNameBoolAttribute("validate_black_dice_pips", true, Resource.String.validateBlackDicePips)]
        ValidateBlackDicePips,
        [SettingNameBoolAttribute("validate_black_dices_previous", true, Resource.String.validateBlackDicesPrevious)]
        ValidateBlackDicesPrevious,
        [SettingNameBoolAttribute("validate_extensions_buildings", true, Resource.String.validateExtensionsBuildings)]
        ValidateExtensionsBuildings,
        [SettingNameBoolAttribute("validate_extensions", true, Resource.String.validateExtensions)]
        ValidateExtensions,
        [SettingNameBoolAttribute("validate_handymen", true, Resource.String.validateHandymen)]
        ValidateHandymen,
        [SettingNameBoolAttribute("validate_treasures_points", true, Resource.String.validateTreasuresPoints)]
        ValidateTreasuresPoints,
        [SettingNameBoolAttribute("validate_missions", true, Resource.String.validateMissions)]
        ValidateMissions,
        [SettingNameBoolAttribute("validate_second_longest_wall", true, Resource.String.validateSecondLongestWall)]
        ValidateSecondLongestWall,
        [SettingNameBoolAttribute("validate_moat_length", true, Resource.String.validateMoatLength)]
        ValidateMoatLength,
        [SettingNameBoolAttribute("validate_moatwall", true, Resource.String.validateMoatwall)]
        ValidateMoatwall,
    }
}