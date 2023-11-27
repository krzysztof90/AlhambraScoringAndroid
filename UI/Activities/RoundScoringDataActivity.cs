using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.Options;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using AndroidBase.UI;
using System;
using System.Linq;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "@string/scoring_round_data", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class RoundScoringDataActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_RoundScoringData_chose;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RoundScoring correctingRoundScoring = Application.CorrectingScoring();

            base.OnCreate(savedInstanceState);

        ControlNumberView guardsPointsNumericUpDown = FindViewById<ControlNumberView>(Resource.Id.guardsPointsNumericUpDown);

            guardsPointsNumericUpDown.SetNumberRange<SettingsType>(0, Game.GuardsMaxPoints, SettingsType.ValidateGuardsPoints);

            if (correctingRoundScoring != null)
                guardsPointsNumericUpDown.Value = correctingRoundScoring.GuardsPoints;

            Button confirmButton = FindViewById<Button>(Resource.Id.confirmButton);
            confirmButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                Application.ConfirmRoundScoringData(this, guardsPointsNumericUpDown.Value);
            });
        }

        public static bool Show(RoundScoring gameScoreSubmitScoreData)
        {
            return ShowGuardsPoints(gameScoreSubmitScoreData);
        }

        private static bool ShowGuardsPoints(RoundScoring gameScoreSubmitScoreData)
        {
            return gameScoreSubmitScoreData.PlayersScoreData.Any(p => p.GuardsCount != 0);
        }

        public void SetRoundScoringData(int guardsPoints)
        {
            Application.GameScoreSubmitScoreData.GuardsPoints = guardsPoints;
        }
    }
}