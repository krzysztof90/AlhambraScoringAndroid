using AlhambraScoringAndroid.Attributes;

namespace AlhambraScoringAndroid.Options
{
    public enum SettingsType
    {
        [SettingNameAttribute("validate_wall_length", true, Resource.Id.validateWallLengthCheckBox)]
        ValidateWallLength,
        [SettingNameAttribute("validate_buildings_number", true, Resource.Id.validateBuildingsNumberCheckBox)]
        ValidateBuildingsNumber,
        [SettingNameAttribute("validate_buildings_number_previous", true, Resource.Id.validateBuildingsNumberPreviousCheckBox)]
        ValidateBuildingsNumberPrevious,
        [SettingNameAttribute("validate_buildings_price", true, Resource.Id.validateBuildingsPriceCheckBox)]
        ValidateBuildingsPrice,
        [SettingNameAttribute("validate_bonus_cards_player", true, Resource.Id.validateBonusCardsPlayerCheckBox)]
        ValidateBonusCardsPlayer,
        [SettingNameAttribute("validate_bonus_cards_buildings", true, Resource.Id.validateBonusCardsBuildingsCheckBox)]
        ValidateBonusCardsBuildings,
        [SettingNameAttribute("validate_bonus_cards", true, Resource.Id.validateBonusCardsCheckBox)]
        ValidateBonusCards,
        [SettingNameAttribute("validate_squares_player", true, Resource.Id.validateSquaresPlayerCheckBox)]
        ValidateSquaresPlayer,
        [SettingNameAttribute("validate_squares", true, Resource.Id.validateSquaresCheckBox)]
        ValidateSquares,
        [SettingNameAttribute("validate_multiple_wiseman", true, Resource.Id.validateMultipleWisemanCheckBox)]
        ValidateMultipleWiseman,
        [SettingNameAttribute("validate_previous_wiseman", true, Resource.Id.validatePreviousWisemanCheckBox)]
        ValidatePreviousWiseman,
        [SettingNameAttribute("validate_multiple_citywatch", true, Resource.Id.validateMultipleCitywatchCheckBox)]
        ValidateMultipleCitywatch,
        [SettingNameAttribute("validate_previous_citywatch", true, Resource.Id.validatePreviousCitywatchCheckBox)]
        ValidatePreviousCitywatch,
        [SettingNameAttribute("validate_citizens_buildings", true, Resource.Id.validateCitizensBuildingsCheckBox)]
        ValidateCitizensBuildings,
        [SettingNameAttribute("validate_citizens", true, Resource.Id.validateCitizensCheckBox)]
        ValidateCitizens,
        [SettingNameAttribute("validate_treasures_player", true, Resource.Id.validateTreasuresPlayerCheckBox)]
        ValidateTreasuresPlayer,
        [SettingNameAttribute("validate_treasures", true, Resource.Id.validateTreasuresCheckBox)]
        ValidateTreasures,
        [SettingNameAttribute("validate_unprotected_sides", true, Resource.Id.validateUnprotectedSidesCheckBox)]
        ValidateUnprotectedSides,
        [SettingNameAttribute("validate_bazaars_points", true, Resource.Id.validateBazaarsPointsCheckBox)]
        ValidateBazaarsPoints,
        [SettingNameAttribute("validate_culture_counters", true, Resource.Id.validateCultureCountersCheckBox)]
        ValidateCultureCounters,
        [SettingNameAttribute("validate_culture_counters_previous", true, Resource.Id.validateCultureCountersPreviousCheckBox)]
        ValidateCultureCountersPrevious,
        [SettingNameAttribute("validate_falcons", true, Resource.Id.validateFalconsCheckBox)]
        ValidateFalcons,
        [SettingNameAttribute("validate_watchtower_wall", true, Resource.Id.validateWatchtowerWallCheckBox)]
        ValidateWatchtowerWall,
        [SettingNameAttribute("validate_watchtower", true, Resource.Id.validateWatchtowerCheckBox)]
        ValidateWatchtower,
        [SettingNameAttribute("validate_medin", true, Resource.Id.validateMedinCheckBox)]
        ValidateMedin,
        [SettingNameAttribute("validate_medin_previous", true, Resource.Id.validateMedinPreviousCheckBox)]
        ValidateMedinPrevious,
        [SettingNameAttribute("validate_servants", true, Resource.Id.validateServantsCheckBox)]
        ValidateServants,
        [SettingNameAttribute("validate_single_fruits", true, Resource.Id.validateSingleFruitsCheckBox)]
        ValidateSingleFruits,
        [SettingNameAttribute("validate_fruits", true, Resource.Id.validateFruitsCheckBox)]
        ValidateFruits,
        [SettingNameAttribute("validate_bathhouses", true, Resource.Id.validateBathhousesCheckBox)]
        ValidateBathhouses,
        [SettingNameAttribute("validate_fontains_player", true, Resource.Id.validateFontainsPlayerCheckBox)]
        ValidateFontainsPlayer,
        [SettingNameAttribute("validate_fontains", true, Resource.Id.validateFontainsCheckBox)]
        ValidateFontains,
        [SettingNameAttribute("validate_multiple_completed_project", true, Resource.Id.validateMultipleCompletedProjectCheckBox)]
        ValidateMultipleCompletedProject,
        [SettingNameAttribute("validate_previous_completed_project", true, Resource.Id.validatePreviousCompletedProjectCheckBox)]
        ValidatePreviousCompletedProject,
        [SettingNameAttribute("validate_animals_player", true, Resource.Id.validateAnimalsPlayerCheckBox)]
        ValidateAnimalsPlayer,
        [SettingNameAttribute("validate_animals", true, Resource.Id.validateAnimalsCheckBox)]
        ValidateAnimals,
        [SettingNameAttribute("validate_animals_previous", true, Resource.Id.validateAnimalsPreviousCheckBox)]
        ValidateAnimalsPrevious,
        [SettingNameAttribute("validate_multiple_semi_buildings", true, Resource.Id.validateMultipleSemiBuildingsCheckBox)]
        ValidateMultipleSemiBuildings,
        [SettingNameAttribute("validate_previous_semi_buildings", true, Resource.Id.validatePreviousSemiBuildingsCheckBox)]
        ValidatePreviousSemiBuildings,
        [SettingNameAttribute("validate_black_dice_pips", true, Resource.Id.validateBlackDicePipsCheckBox)]
        ValidateBlackDicePips,
        [SettingNameAttribute("validate_black_dices_previous", true, Resource.Id.validateBlackDicesPreviousCheckBox)]
        ValidateBlackDicesPrevious,
        [SettingNameAttribute("validate_extensions_buildings", true, Resource.Id.validateExtensionsBuildingsCheckBox)]
        ValidateExtensionsBuildings,
        [SettingNameAttribute("validate_extensions", true, Resource.Id.validateExtensionsCheckBox)]
        ValidateExtensions,
        [SettingNameAttribute("validate_handymen", true, Resource.Id.validateHandymenCheckBox)]
        ValidateHandymen,
        [SettingNameAttribute("validate_treasures_points", true, Resource.Id.validateTreasuresPointsCheckBox)]
        ValidateTreasuresPoints,
        [SettingNameAttribute("validate_missions", true, Resource.Id.validateMissionsCheckBox)]
        ValidateMissions,
        [SettingNameAttribute("validate_second_longest_wall", true, Resource.Id.validateSecondLongestWallCheckBox)]
        ValidateSecondLongestWall,
        [SettingNameAttribute("validate_moat_length", true, Resource.Id.validateMoatLengthCheckBox)]
        ValidateMoatLength,
        [SettingNameAttribute("validate_moatwall", true, Resource.Id.validateMoatwallCheckBox)]
        ValidateMoatwall,
    }
}