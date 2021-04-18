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
    [Activity(Label = "@string/purchase_price", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class GranadaBuildingsNumberActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_players_granada_buildings_chose;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Dictionary<GranadaBuildingType, List<int>> tiePlayerNumbers = GetTiePlayerNumbers(Application.GameScoreSubmitScoreData, Game.RoundNumber);

            LinearLayout container = FindViewById<LinearLayout>(Resource.Id.container);

            Dictionary<GranadaBuildingType, PlayersBuildingChose> playersPanels = new Dictionary<GranadaBuildingType, PlayersBuildingChose>();

            foreach (GranadaBuildingType building in Game.GranadaBuildingsOrder)
            {
                PlayersBuildingChose playersPanel = null;
                if (tiePlayerNumbers[building].Count != 0)
                {
                    playersPanel = new PlayersBuildingChose(this, building.GetEnumDescription(Resources), 2, 12, new List<int>() { 3, 5, 7, 9, 11 }, tiePlayerNumbers[building].ToDictionary(p => p, p => Game.GetPlayer(p).Name), SettingsType.ValidateBuildingsPrice);
                    container.AddView(playersPanel);
                    container.RequestLayout();
                }

                playersPanels[building] = playersPanel;
            }

            Button confirmButton = FindViewById<Button>(Resource.Id.confirmButton);
            confirmButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                bool correct = true;
                Dictionary<GranadaBuildingType, Dictionary<int, int>> result = new Dictionary<GranadaBuildingType, Dictionary<int, int>>();
                foreach (KeyValuePair<GranadaBuildingType, PlayersBuildingChose> playersPanel in playersPanels)
                {
                    Dictionary<int, int> playersHighestPrices = new Dictionary<int, int>();
                    if (playersPanel.Value != null)
                    {
                        if (!playersPanel.Value.PlayersPanels.ValidatePlayerPanels())
                        {
                            correct = false;
                            break;
                        }
                        foreach (int playerNumber in tiePlayerNumbers[playersPanel.Key])
                            playersHighestPrices[playerNumber] = playersPanel.Value.PlayersPanels[playerNumber - 1].Value;
                    }
                    result[playersPanel.Key] = playersHighestPrices;
                }
                if (correct)
                    Application.ConfirmGranadaBuildingsNumbers(this, result);
            });
        }

        public static Dictionary<GranadaBuildingType, List<int>> GetTiePlayerNumbers(List<PlayerScoreData> gameScoreSubmitScoreData, int roundNumber)
        {
            Dictionary<GranadaBuildingType, List<int>> result = new Dictionary<GranadaBuildingType, List<int>>();

            foreach (GranadaBuildingType building in Game.GranadaBuildingsOrder)
            {
                List<int> tiePlayerNumbers = new List<int>();

                List<int> duplicatedPoints = gameScoreSubmitScoreData.GroupBy(p => p.GranadaBuildingsCount[building]).Where(g => g.Key != 0 && g.Count() > 1).Select(g => g.Key).ToList();

                for (int i = 0; i < gameScoreSubmitScoreData.Count; i++)
                {
                    int buildingsNumber = gameScoreSubmitScoreData[i].GranadaBuildingsCount[building];
                    int higherPlayersCount = gameScoreSubmitScoreData.Where(p => p.PlayerNumber != i + 1).Count(p => p.GranadaBuildingsCount[building] > buildingsNumber);
                    if (duplicatedPoints.Contains(buildingsNumber) && higherPlayersCount < roundNumber)
                        tiePlayerNumbers.Add(i + 1);
                }

                result[building] = tiePlayerNumbers;
            }

            return result;
        }
    }
}