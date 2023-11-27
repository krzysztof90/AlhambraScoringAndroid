using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using AndroidBase.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "@string/building_type_chose", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class CharacterTheWiseManActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_CharacterTheWiseMan_building_type_chose;

        private int ChosePlayerNumber;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RoundScoring correctingRoundScoring = Application.CorrectingScoring();

            base.OnCreate(savedInstanceState);

            RadioGroup radioButtonGroup = FindViewById<RadioGroup>(Resource.Id.buttonGroup);

            for (int i = 0; i < Game.PlayersCount; i++)
                if (Application.GameScoreSubmitScoreData.PlayersScoreData[i].OwnedCharacterTheWiseMan)
                {
                    ChosePlayerNumber = i + 1;
                    break;
                }

            FindViewById<TextView>(Resource.Id.playerName).Text = Game.GetPlayer(ChosePlayerNumber).Name;

            Dictionary<BuildingType, int> radioButtons = new Dictionary<BuildingType, int>()
            {
                [BuildingType.Pavilion] = Resource.Id.buttonPavilion,
                [BuildingType.Seraglio] = Resource.Id.buttonSeraglio,
                [BuildingType.Arcades] = Resource.Id.buttonArcades,
                [BuildingType.Chambers] = Resource.Id.buttonChambers,
                [BuildingType.Garden] = Resource.Id.buttonGarden,
                [BuildingType.Tower] = Resource.Id.buttonTower,
            };
            foreach (KeyValuePair<BuildingType, int> radioButtonPair in radioButtons)
            {
                StringBuilder radioButtonText = new StringBuilder();
                SetTheWiseManBuildingType(null);
                int pointsNotUsingBonus = 0;
                Dictionary<int, int> opponentsPoints = new Dictionary<int, int>();
                for (int i = 0; i < Game.PlayersCount; i++)
                {
                    int points = Game.GetBuildingScore(Application.GameScoreSubmitScoreData.PlayersScoreData, radioButtonPair.Key, i + 1);
                    if (i == ChosePlayerNumber - 1)
                        pointsNotUsingBonus = points;
                    else
                        opponentsPoints[i] = points;
                }
                SetTheWiseManBuildingType(radioButtonPair.Key);
                int pointsUsingBonus = 0;
                for (int i = 0; i < Game.PlayersCount; i++)
                {
                    int points = Game.GetBuildingScore(Application.GameScoreSubmitScoreData.PlayersScoreData, radioButtonPair.Key, i + 1);
                    if (i == ChosePlayerNumber - 1)
                        pointsUsingBonus = points;
                    else
                    {
                        int opponentPoints = opponentsPoints[i];
                        opponentsPoints[i] = opponentPoints - points;
                    }
                }
                SetTheWiseManBuildingType(null);

                radioButtonText.Append($"{radioButtonPair.Key.GetEnumDescription(Resources)} (+{pointsUsingBonus - pointsNotUsingBonus})");
                for (int i = 0; i < Game.PlayersCount; i++)
                {
                    if (i != ChosePlayerNumber - 1)
                    {
                        if (opponentsPoints[i] != 0)
                            radioButtonText.Append($" ({Game.GetPlayer(i + 1).Name}: -{opponentsPoints[i]})");
                    }
                }

                RadioButton radioButton = FindViewById<RadioButton>(radioButtonPair.Value);

                radioButton.Text = radioButtonText.ToString();

                if (radioButton.CurrentTextColor == -658699)
                    radioButton.SetShadowLayer(1, 1, 1, Android.Graphics.Color.Black);
            }
            if (correctingRoundScoring != null)
            {
                radioButtonGroup.Check(radioButtons[(BuildingType)correctingRoundScoring.PlayersScoreData[ChosePlayerNumber - 1].TheWiseManBuildingType]);
            }

            Button confirmButton = FindViewById<Button>(Resource.Id.confirmButton);
            confirmButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                int radioButtonID = radioButtonGroup.CheckedRadioButtonId;
                if (radioButtonID != -1)
                {
                    BuildingType buildingType = radioButtons.Single(r => r.Value == radioButtonID).Key;

                    Application.ConfirmTheWiseManChose(this, buildingType);
                }
            });
        }

        public static bool Show(RoundScoring gameScoreSubmitScoreData)
        {
            return gameScoreSubmitScoreData.PlayersScoreData.Any(p => p.OwnedCharacterTheWiseMan);
        }

        public void SetTheWiseManBuildingType(BuildingType? buildingType)
        {
            Application.GameScoreSubmitScoreData.PlayersScoreData[ChosePlayerNumber - 1].TheWiseManBuildingType = buildingType;
        }
    }
}