using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.Tools;
using Android.App;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "Wybór typu budynku", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class CharacterTheWiseManActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_building_type_chose;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            int chosePlayerNumber = 0;
            for (int i = 0; i < Game.PlayersCount; i++)
                if (Application.GameScoreSubmitScorePanels[i].OwnedCharacterTheWiseMan)
                {
                    chosePlayerNumber = i + 1;
                    break;
                }

            FindViewById<TextView>(Resource.Id.playerName).Text = Game.GetPlayer(chosePlayerNumber).Name;

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
                Game.SetTheWiseManBuildingType(null);
                int pointsNotUsingBonus = 0;
                Dictionary<int, int> opponentsPoints = new Dictionary<int, int>();
                for (int i = 0; i < Game.PlayersCount; i++)
                {
                    int points = Game.GetBuildingScore(Application.GameScoreSubmitScorePanels, radioButtonPair.Key, i + 1);
                    if (i == chosePlayerNumber - 1)
                        pointsNotUsingBonus = points;
                    else
                        opponentsPoints[i] = points;
                }
                Game.SetTheWiseManBuildingType(radioButtonPair.Key);
                int pointsUsingBonus = 0;
                for (int i = 0; i < Game.PlayersCount; i++)
                {
                    int points = Game.GetBuildingScore(Application.GameScoreSubmitScorePanels, radioButtonPair.Key, i + 1);
                    if (i == chosePlayerNumber - 1)
                        pointsUsingBonus = points;
                    else
                    {
                        int opponentPoints = opponentsPoints[i];
                        opponentsPoints[i] = opponentPoints - points;
                    }
                }
                Game.SetTheWiseManBuildingType(null);

                radioButtonText.Append($"{radioButtonPair.Key.GetEnumDescription()} (+{pointsUsingBonus - pointsNotUsingBonus})");
                for (int i = 0; i < Game.PlayersCount; i++)
                {
                    if (i != chosePlayerNumber - 1)
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
    }
}