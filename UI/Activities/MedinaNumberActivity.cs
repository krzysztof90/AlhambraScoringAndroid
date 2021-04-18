using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.Options;
using AlhambraScoringAndroid.Tools;
using Android.App;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI.Activities
{
    //"provide"
    [Activity(Label = "@string/purchase_price", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class MedinaNumberActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_players_buildings_chose;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            List<int> tiePlayerNumbers = GetTiePlayerNumbers(Application.GameScoreSubmitScoreData, Game.RoundNumber);

            LinearLayout container = FindViewById<LinearLayout>(Resource.Id.container);

            PlayersBuildingChose playersPanel = new PlayersBuildingChose(this, Resources.GetString(Resource.String.medina), 2, 10, new List<int>(), tiePlayerNumbers.ToDictionary(p => p, p => Game.GetPlayer(p).Name), SettingsType.ValidateBuildingsPrice);
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

        public static List<int> GetTiePlayerNumbers(List<PlayerScoreData> gameScoreSubmitScoreData, int roundNumber)
        {
            List<int> tiePlayerNumbers = new List<int>();

            List<int> duplicatedPoints = gameScoreSubmitScoreData.GroupBy(p => p.MedinasNumber).Where(g => g.Key != 0 && g.Count() > 1).Select(g => g.Key).ToList();

            for (int i = 0; i < gameScoreSubmitScoreData.Count; i++)
            {
                int medinasNumber = gameScoreSubmitScoreData[i].MedinasNumber;
                int lowerPlayersCount = gameScoreSubmitScoreData.Where(p => p.PlayerNumber != i + 1).Count(p => p.MedinasNumber < medinasNumber);
                if (duplicatedPoints.Contains(medinasNumber) && lowerPlayersCount < roundNumber)
                    tiePlayerNumbers.Add(i + 1);
            }

            return tiePlayerNumbers;
        }
    }
}