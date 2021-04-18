using AlhambraScoringAndroid.Attributes;

namespace AlhambraScoringAndroid.Options
{
    public enum SettingsType
    {
        [SettingNameAttribute("validate_wall_length", true)]
        ValidateWallLength,
        [SettingNameAttribute("validate_buildings_number", true)]
        ValidateBuildingsNumber,
        [SettingNameAttribute("validate_buildings_number_previous", true)]
        ValidateBuildingsNumberPrevious,
        [SettingNameAttribute("validate_buildings_price", true)]
        ValidateBuildingsPrice,
        [SettingNameAttribute("validate_bonus_cards_player", true)]
        ValidateBonusCardsPlayer,
        [SettingNameAttribute("validate_bonus_cards_buildings", true)]
        ValidateBonusCardsBuildings,
        [SettingNameAttribute("validate_bonus_cards", true)]
        ValidateBonusCards,
        [SettingNameAttribute("validate_squares_player", true)]
        ValidateSquaresPlayer,
        [SettingNameAttribute("validate_squares", true)]
        ValidateSquares,
        [SettingNameAttribute("validate_multiple_wiseman", true)]
        ValidateMultipleWiseman,
        [SettingNameAttribute("validate_previous_wiseman", true)]
        ValidatePreviousWiseman,
        [SettingNameAttribute("validate_multiple_citywatch", true)]
        ValidateMultipleCitywatch,
        [SettingNameAttribute("validate_previous_citywatch", true)]
        ValidatePreviousCitywatch,
        [SettingNameAttribute("validate_citizens_buildings", true)]
        ValidateCitizensBuildings,
        [SettingNameAttribute("validate_citizens", true)]
        ValidateCitizens,
        [SettingNameAttribute("validate_treasures_player", true)]
        ValidateTreasuresPlayer,
        [SettingNameAttribute("validate_treasures", true)]
        ValidateTreasures,
        [SettingNameAttribute("validate_unprotected_sides", true)]
        ValidateUnprotectedSides,
        [SettingNameAttribute("validate_bazaars_points", true)]
        ValidateBazaarsPoints,
        [SettingNameAttribute("validate_culture_counters", true)]
        ValidateCultureCounters,
        [SettingNameAttribute("validate_culture_counters_previous", true)]
        ValidateCultureCountersPrevious,
        [SettingNameAttribute("validate_falcons", true)]
        ValidateFalcons,
        [SettingNameAttribute("validate_watchtower_wall", true)]
        ValidateWatchtowerWall,
        [SettingNameAttribute("validate_watchtower", true)]
        ValidateWatchtower,
        [SettingNameAttribute("validate_medin", true)]
        ValidateMedin,
        [SettingNameAttribute("validate_medin_previous", true)]
        ValidateMedinPrevious,
        [SettingNameAttribute("validate_servants", true)]
        ValidateServants,
        [SettingNameAttribute("validate_single_fruits", true)]
        ValidateSingleFruits,
        [SettingNameAttribute("validate_fruits", true)]
        ValidateFruits,
        [SettingNameAttribute("validate_bathhouses", true)]
        ValidateBathhouses,
        [SettingNameAttribute("validate_fontains_player", true)]
        ValidateFontainsPlayer,
        [SettingNameAttribute("validate_fontains", true)]
        ValidateFontains,
        [SettingNameAttribute("validate_multiple_completed_project", true)]
        ValidateMultipleCompletedProject,
        [SettingNameAttribute("validate_previous_completed_project", true)]
        ValidatePreviousCompletedProject,
        [SettingNameAttribute("validate_animals_player", true)]
        ValidateAnimalsPlayer,
        [SettingNameAttribute("validate_animals", true)]
        ValidateAnimals,
        [SettingNameAttribute("validate_animals_previous", true)]
        ValidateAnimalsPrevious,
        [SettingNameAttribute("validate_multiple_semi_buildings", true)]
        ValidateMultipleSemiBuildings,
        [SettingNameAttribute("validate_previous_semi_buildings", true)]
        ValidatePreviousSemiBuildings,
        [SettingNameAttribute("validate_black_dice_pips", true)]
        ValidateBlackDicePips,
        [SettingNameAttribute("validate_black_dices_previous", true)]
        ValidateBlackDicesPrevious,
        [SettingNameAttribute("validate_extensions_buildings", true)]
        ValidateExtensionsBuildings,
        [SettingNameAttribute("validate_extensions", true)]
        ValidateExtensions,
        [SettingNameAttribute("validate_handymen", true)]
        ValidateHandymen,
        [SettingNameAttribute("validate_treasures_points", true)]
        ValidateTreasuresPoints,
        [SettingNameAttribute("validate_missions", true)]
        ValidateMissions,
        [SettingNameAttribute("validate_second_longest_wall", true)]
        ValidateSecondLongestWall,
        [SettingNameAttribute("validate_moat_length", true)]
        ValidateMoatLength,
        [SettingNameAttribute("validate_moatwall", true)]
        ValidateMoatwall,
    }
}