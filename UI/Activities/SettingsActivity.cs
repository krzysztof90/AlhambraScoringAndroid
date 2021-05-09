using AlhambraScoringAndroid.Attributes;
using AlhambraScoringAndroid.Options;
using AlhambraScoringAndroid.Tools;
using Android.App;
using Android.Content.PM;
using Android.OS;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "@string/action_settings", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SettingsActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_settings;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            List<SettingsType> validationCheckBoxes = new List<SettingsType>()
            {
                SettingsType.ValidateWallLength,
                SettingsType.ValidateBuildingsNumber,
                SettingsType.ValidateBuildingsNumberPrevious,
                SettingsType.ValidateBuildingsPrice,
                SettingsType.ValidateBonusCardsPlayer,
                SettingsType.ValidateBonusCardsBuildings,
                SettingsType.ValidateBonusCards,
                SettingsType.ValidateSquaresPlayer,
                SettingsType.ValidateSquares,
                SettingsType.ValidateMultipleWiseman,
                SettingsType.ValidatePreviousWiseman,
                SettingsType.ValidateMultipleCitywatch,
                SettingsType.ValidatePreviousCitywatch,
                SettingsType.ValidateCitizensBuildings,
                SettingsType.ValidateCitizens,
                SettingsType.ValidateTreasuresPlayer,
                SettingsType.ValidateTreasures,
                SettingsType.ValidateUnprotectedSides,
                SettingsType.ValidateBazaarsPoints,
                SettingsType.ValidateCultureCounters,
                SettingsType.ValidateCultureCountersPrevious,
                SettingsType.ValidateFalcons,
                SettingsType.ValidateWatchtowerWall,
                SettingsType.ValidateWatchtower,
                SettingsType.ValidateMedin,
                SettingsType.ValidateMedinPrevious,
                SettingsType.ValidateServants,
                SettingsType.ValidateSingleFruits,
                SettingsType.ValidateFruits,
                SettingsType.ValidateBathhouses,
                SettingsType.ValidateFontainsPlayer,
                SettingsType.ValidateFontains,
                SettingsType.ValidateMultipleCompletedProject,
                SettingsType.ValidatePreviousCompletedProject,
                SettingsType.ValidateAnimalsPlayer,
                SettingsType.ValidateAnimals,
                SettingsType.ValidateAnimalsPrevious,
                SettingsType.ValidateMultipleSemiBuildings,
                SettingsType.ValidatePreviousSemiBuildings,
                SettingsType.ValidateBlackDicePips,
                SettingsType.ValidateBlackDicesPrevious,
                SettingsType.ValidateExtensionsBuildings,
                SettingsType.ValidateExtensions,
                SettingsType.ValidateHandymen,
                SettingsType.ValidateTreasuresPoints,
                SettingsType.ValidateMissions,
                SettingsType.ValidateSecondLongestWall,
                SettingsType.ValidateMoatLength,
                SettingsType.ValidateMoatwall
            };

            foreach (SettingsType settingsType in validationCheckBoxes)
            {
                ScoreLineCheckBoxView checkBox = FindViewById<ScoreLineCheckBoxView>(settingsType.GetEnumAttribute<SettingsType, SettingNameAttribute>().DescriptionResource);
                checkBox.Value = Settings.IsSet(settingsType);
                checkBox.OnValueChange = () => { Settings.Set(settingsType, checkBox.Value); };
            }
        }
    }
}