using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.Options;
using AlhambraScoringAndroid.Tools;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI.Activities
{
    //"provide"
    [Activity(Label = "@string/purchase_price", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MedinaNumberActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_MedinaNumber_players_buildings_chose;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RoundScoring correctingRoundScoring = Application.CorrectingScoring();

            base.OnCreate(savedInstanceState);

            List<int> tiePlayerNumbers = GetTiePlayerNumbers(Application.GameScoreSubmitScoreData, Game.RoundNumber);

            LinearLayout container = FindViewById<LinearLayout>(Resource.Id.container);

            Dictionary<int, int?> correctingPoints = new Dictionary<int, int?>();
            for (int i = 0; i < Game.PlayersCount; i++)
                correctingPoints[i + 1] = correctingRoundScoring != null ? correctingRoundScoring.PlayersScoreData[i].MedinaHighestPrice : null;

            PlayersBuildingChose playersPanel = new PlayersBuildingChose(this, Resources.GetString(Resource.String.medina), Game.MedinaMinPrice, Game.MedinaMaxPrice, new List<int>(), tiePlayerNumbers.ToDictionary(p => p, p => (Game.GetPlayer(p).Name, correctingPoints[p])), SettingsType.ValidateBuildingsPrice);
            container.AddView(playersPanel);
            container.RequestLayout();

            Button confirmButton = FindViewById<Button>(Resource.Id.confirmButton);
            confirmButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                if (playersPanel.PlayersPanels.ValidatePlayerPanels())
                {
                    Dictionary<int, int> playersHighestPrices = new Dictionary<int, int>();
                    foreach (int playerNumber in tiePlayerNumbers)
                        playersHighestPrices[playerNumber] = playersPanel.PlayersPanels[playerNumber - 1].Value;

                    Application.ConfirmMedinasNumber(this, playersHighestPrices);
                }
            });
        }

        public static List<int> GetTiePlayerNumbers(RoundScoring gameScoreSubmitScoreData, int roundNumber)
        {
            List<int> tiePlayerNumbers = new List<int>();

            List<int> duplicatedPoints = gameScoreSubmitScoreData.PlayersScoreData.GroupBy(p => p.MedinasNumber).Where(g => g.Key != 0 && g.Count() > 1).Select(g => g.Key).ToList();

            for (int i = 0; i < gameScoreSubmitScoreData.PlayersScoreData.Count; i++)
            {
                int medinasNumber = gameScoreSubmitScoreData.PlayersScoreData[i].MedinasNumber;
                int lowerPlayersCount = gameScoreSubmitScoreData.PlayersScoreData.Where(p => p.PlayerNumber != i + 1).Count(p => p.MedinasNumber < medinasNumber);
                if (duplicatedPoints.Contains(medinasNumber) && lowerPlayersCount < roundNumber)
                    tiePlayerNumbers.Add(i + 1);
            }

            return tiePlayerNumbers;
        }

        public void SetMedinasNumbers(Dictionary<int, int> playersHighestPrices)
        {
            for (int i = 0; i < Game.PlayersCount; i++)
            {
                if (playersHighestPrices.ContainsKey(i + 1))
                    Application.GameScoreSubmitScoreData.PlayersScoreData[i].MedinaHighestPrice = playersHighestPrices[i + 1];
            }
        }
    }
}