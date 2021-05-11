using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using AndroidBase.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "@string/building_type_chose", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class CharacterTheWiseManActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_building_type_chose;

        private int ChosePlayerNumber;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            for (int i = 0; i < Game.PlayersCount; i++)
                if (Application.GameScoreSubmitScoreData[i].OwnedCharacterTheWiseMan)
                {
                    ChosePlayerNumber = i + 1;
                    break;
                }

            FindViewById<TextView>(Resource.Id.playerName).Text = Game.GetPlayer(ChosePlayerNumber).Name;

            Dictionary<BuildingType, RadioButton> radioButtons = new Dictionary<BuildingType, RadioButton>()
            {
                [BuildingType.Pavilion] = FindViewById<RadioButton>(Resource.Id.buttonPavilion),
                [BuildingType.Seraglio] = FindViewById<RadioButton>(Resource.Id.buttonSeraglio),
                [BuildingType.Arcades] = FindViewById<RadioButton>(Resource.Id.buttonArcades),
                [BuildingType.Chambers] = FindViewById<RadioButton>(Resource.Id.buttonChambers),
                [BuildingType.Garden] = FindViewById<RadioButton>(Resource.Id.buttonGarden),
                [BuildingType.Tower] = FindViewById<RadioButton>(Resource.Id.buttonTower),
            };
            foreach (KeyValuePair<BuildingType, RadioButton> radioButtonPair in radioButtons)
            {
                StringBuilder radioButtonText = new StringBuilder();
                SetTheWiseManBuildingType(null);
                int pointsNotUsingBonus = 0;
                Dictionary<int, int> opponentsPoints = new Dictionary<int, int>();
                for (int i = 0; i < Game.PlayersCount; i++)
                {
                    int points = Game.GetBuildingScore(Application.GameScoreSubmitScoreData, radioButtonPair.Key, i + 1);
                    if (i == ChosePlayerNumber - 1)
                        pointsNotUsingBonus = points;
                    else
                        opponentsPoints[i] = points;
                }
                SetTheWiseManBuildingType(radioButtonPair.Key);
                int pointsUsingBonus = 0;
                for (int i = 0; i < Game.PlayersCount; i++)
                {
                    int points = Game.GetBuildingScore(Application.GameScoreSubmitScoreData, radioButtonPair.Key, i + 1);
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
                radioButtonPair.Value.Text = radioButtonText.ToString();

                if (radioButtonPair.Value.CurrentTextColor == -658699)
                    radioButtonPair.Value.SetShadowLayer(1, 1, 1, Android.Graphics.Color.Black);
            }

            Button confirmButton = FindViewById<Button>(Resource.Id.confirmButton);
            confirmButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                RadioGroup radioButtonGroup = FindViewById<RadioGroup>(Resource.Id.buttonGroup);

                int radioButtonID = radioButtonGroup.CheckedRadioButtonId;
                if (radioButtonID != -1)
                {
                    BuildingType buildingType;
                    RadioButton radioButton = radioButtonGroup.FindViewById<RadioButton>(radioButtonID);
                    switch (radioButtonGroup.IndexOfChild(radioButton))
                    {
                        case 0:
                            buildingType = BuildingType.Pavilion;
                            break;
                        case 1:
                            buildingType = BuildingType.Seraglio;
                            break;
                        case 2:
                            buildingType = BuildingType.Arcades;
                            break;
                        case 3:
                            buildingType = BuildingType.Chambers;
                            break;
                        case 4:
                            buildingType = BuildingType.Garden;
                            break;
                        case 5:
                            buildingType = BuildingType.Tower;
                            break;
                        default:
                            throw new NotSupportedException();
                    }

                    Application.ConfirmTheWiseManChose(this, buildingType);
                }
            });
        }

        public void SetTheWiseManBuildingType(BuildingType? buildingType)
        {
            Application.GameScoreSubmitScoreData[ChosePlayerNumber - 1].TheWiseManBuildingType = buildingType;
        }
    }
}