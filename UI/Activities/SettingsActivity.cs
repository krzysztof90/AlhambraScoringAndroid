using AlhambraScoringAndroid.Options;
using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidBase.Tools;
using AndroidBase.UI;
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

            List<(int, SettingsType)> validationCheckBoxes = new List<(int, SettingsType)>()
            {
                (Resource.Id.validateWallLengthCheckBox, SettingsType.ValidateWallLength),
                (Resource.Id.validateBuildingsNumberCheckBox, SettingsType.ValidateBuildingsNumber),
                (Resource.Id.validateBuildingsNumberPreviousCheckBox, SettingsType.ValidateBuildingsNumberPrevious),
                (Resource.Id.validateBuildingsPriceCheckBox, SettingsType.ValidateBuildingsPrice),
                (Resource.Id.validateBonusCardsPlayerCheckBox, SettingsType.ValidateBonusCardsPlayer),
                (Resource.Id.validateBonusCardsBuildingsCheckBox, SettingsType.ValidateBonusCardsBuildings),
                (Resource.Id.validateBonusCardsCheckBox, SettingsType.ValidateBonusCards),
                (Resource.Id.validateSquaresPlayerCheckBox, SettingsType.ValidateSquaresPlayer),
                (Resource.Id.validateSquaresCheckBox, SettingsType.ValidateSquares),
                (Resource.Id.validateMultipleWisemanCheckBox, SettingsType.ValidateMultipleWiseman),
                (Resource.Id.validatePreviousWisemanCheckBox, SettingsType.ValidatePreviousWiseman),
                (Resource.Id.validateMultipleCitywatchCheckBox, SettingsType.ValidateMultipleCitywatch),
                (Resource.Id.validatePreviousCitywatchCheckBox, SettingsType.ValidatePreviousCitywatch),
                (Resource.Id.validateCitizensBuildingsCheckBox, SettingsType.ValidateCitizensBuildings),
                (Resource.Id.validateCitizensCheckBox, SettingsType.ValidateCitizens),
                (Resource.Id.validateTreasuresPlayerCheckBox, SettingsType.ValidateTreasuresPlayer),
                (Resource.Id.validateTreasuresCheckBox, SettingsType.ValidateTreasures),
                (Resource.Id.validateUnprotectedSidesCheckBox, SettingsType.ValidateUnprotectedSides),
                (Resource.Id.validateBazaarsPointsCheckBox, SettingsType.ValidateBazaarsPoints),
                (Resource.Id.validateCultureCountersCheckBox, SettingsType.ValidateCultureCounters),
                (Resource.Id.validateCultureCountersPreviousCheckBox, SettingsType.ValidateCultureCountersPrevious),
                (Resource.Id.validateFalconsCheckBox, SettingsType.ValidateFalcons),
                (Resource.Id.validateWatchtowerWallCheckBox, SettingsType.ValidateWatchtowerWall),
                (Resource.Id.validateWatchtowerCheckBox, SettingsType.ValidateWatchtower),
                (Resource.Id.validateMedinCheckBox, SettingsType.ValidateMedin),
                (Resource.Id.validateMedinPreviousCheckBox, SettingsType.ValidateMedinPrevious),
                (Resource.Id.validateServantsCheckBox, SettingsType.ValidateServants),
                (Resource.Id.validateSingleFruitsCheckBox, SettingsType.ValidateSingleFruits),
                (Resource.Id.validateFruitsCheckBox, SettingsType.ValidateFruits),
                (Resource.Id.validateBathhousesCheckBox, SettingsType.ValidateBathhouses),
                (Resource.Id.validateFontainsPlayerCheckBox, SettingsType.ValidateFontainsPlayer),
                (Resource.Id.validateFontainsCheckBox, SettingsType.ValidateFontains),
                (Resource.Id.validateMultipleCompletedProjectCheckBox, SettingsType.ValidateMultipleCompletedProject),
                (Resource.Id.validatePreviousCompletedProjectCheckBox, SettingsType.ValidatePreviousCompletedProject),
                (Resource.Id.validateAnimalsPlayerCheckBox, SettingsType.ValidateAnimalsPlayer),
                (Resource.Id.validateAnimalsCheckBox, SettingsType.ValidateAnimals),
                (Resource.Id.validateAnimalsPreviousCheckBox, SettingsType.ValidateAnimalsPrevious),
                (Resource.Id.validateMultipleSemiBuildingsCheckBox, SettingsType.ValidateMultipleSemiBuildings),
                (Resource.Id.validatePreviousSemiBuildingsCheckBox, SettingsType.ValidatePreviousSemiBuildings),
                (Resource.Id.validateBlackDicePipsCheckBox, SettingsType.ValidateBlackDicePips),
                (Resource.Id.validateBlackDicesPreviousCheckBox, SettingsType.ValidateBlackDicesPrevious),
                (Resource.Id.validateExtensionsBuildingsCheckBox, SettingsType.ValidateExtensionsBuildings),
                (Resource.Id.validateExtensionsCheckBox, SettingsType.ValidateExtensions),
                (Resource.Id.validateHandymenCheckBox, SettingsType.ValidateHandymen),
                (Resource.Id.validateTreasuresPointsCheckBox, SettingsType.ValidateTreasuresPoints),
                (Resource.Id.validateMissionsCheckBox, SettingsType.ValidateMissions),
                (Resource.Id.validateSecondLongestWallCheckBox, SettingsType.ValidateSecondLongestWall),
                (Resource.Id.validateMoatLengthCheckBox, SettingsType.ValidateMoatLength),
                (Resource.Id.validateMoatwallCheckBox, SettingsType.ValidateMoatwall),
            };

            foreach ((int, SettingsType) validationCheckBox in validationCheckBoxes)
            {
                ControlCheckBoxView checkBox = FindViewById<ControlCheckBoxView>(validationCheckBox.Item1);
                checkBox.SetLabel(validationCheckBox.Item2.GetEnumDescription(Resources));
                checkBox.Value = Settings.Get(validationCheckBox.Item2);
                checkBox.OnValueChange = () => { Settings.Set(validationCheckBox.Item2, checkBox.Value); };
            }
        }
    }
}