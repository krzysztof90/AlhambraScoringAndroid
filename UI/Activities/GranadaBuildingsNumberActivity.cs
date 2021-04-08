using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.Tools;
using Android.App;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "@string/buildings_number", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class GranadaBuildingsNumberActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_players_granada_buildings_chose;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Dictionary<GranadaBuildingType, List<int>> tiePlayerNumbers = GetTiePlayerNumbers(Application.GameScoreSubmitScorePanels, Game.RoundNumber);

            LinearLayout container = FindViewById<LinearLayout>(Resource.Id.container);

            Dictionary<GranadaBuildingType, PlayersBuildingChose> playersPanels = new Dictionary<GranadaBuildingType, PlayersBuildingChose>();

            foreach (GranadaBuildingType building in Game.GranadaBuildingsOrder)
            {
                PlayersBuildingChose playersPanel = null;
                if (tiePlayerNumbers[building].Count != 0)
                {
                    //TODO tylko parzyste
                    playersPanel = new PlayersBuildingChose(this, building.GetEnumDescription(Resources), 2, 12, tiePlayerNumbers[building].ToDictionary(p => p, p => Game.GetPlayer(p).Name));
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

        public static Dictionary<GranadaBuildingType, List<int>> GetTiePlayerNumbers(List<PlaceholderPlayerScoreFragment> gameScoreSubmitScorePanels, int roundNumber)
        {
            Dictionary<GranadaBuildingType, List<int>> result = new Dictionary<GranadaBuildingType, List<int>>();

            foreach (GranadaBuildingType building in Game.GranadaBuildingsOrder)
            {
                List<int> tiePlayerNumbers = new List<int>();

                List<int> duplicatedPoints = gameScoreSubmitScorePanels.GroupBy(p => p.GranadaBuildingsCount[building]).Where(g => g.Key != 0 && g.Count() > 1).Select(g => g.Key).ToList();

                for (int i = 0; i < gameScoreSubmitScorePanels.Count; i++)
                {
                    int buildingsNumber = gameScoreSubmitScorePanels[i].GranadaBuildingsCount[building];
                    int higherPlayersCount = gameScoreSubmitScorePanels.Where(p => p.PlayerNumber != i + 1).Count(p => p.GranadaBuildingsCount[building] > buildingsNumber);
                    if (duplicatedPoints.Contains(buildingsNumber) && higherPlayersCount < roundNumber)
                        tiePlayerNumbers.Add(i + 1);
                }

                result[building] = tiePlayerNumbers;
            }

            return result;
        }
    }
}