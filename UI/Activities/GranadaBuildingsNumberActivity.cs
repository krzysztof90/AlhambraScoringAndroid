using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.Options;
using AlhambraScoringAndroid.Tools;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using AndroidBase.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "@string/purchase_price", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class GranadaBuildingsNumberActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_GranadaBuildingsNumber_players_buildings_chose;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RoundScoring correctingRoundScoring = Application.CorrectingScoring();

            base.OnCreate(savedInstanceState);

            Dictionary<GranadaBuildingType, List<int>> tiePlayerNumbers = GetTiePlayerNumbers(Application.GameScoreSubmitScoreData, Game.RoundNumber);

            LinearLayout container = FindViewById<LinearLayout>(Resource.Id.container);

            Dictionary<GranadaBuildingType, PlayersBuildingChose> playersPanels = new Dictionary<GranadaBuildingType, PlayersBuildingChose>();

            foreach (GranadaBuildingType building in Game.GranadaBuildingsOrder)
            {
                PlayersBuildingChose playersPanel = null;
                if (tiePlayerNumbers[building].Count != 0)
                {
                    Dictionary<int, int?> correctingPoints = new Dictionary<int, int?>();
                    for (int i = 0; i < Game.PlayersCount; i++)
                    {
                        int? points = null;
                        if (correctingRoundScoring != null && correctingRoundScoring.PlayersScoreData[i].GranadaBuildingsHighestPrices.ContainsKey(building))
                            points = correctingRoundScoring.PlayersScoreData[i].GranadaBuildingsHighestPrices[building];
                        correctingPoints[i + 1] = points;
                    }

                    playersPanel = new PlayersBuildingChose(this, building.GetEnumDescription(Resources), Game.GranadaMinPrice, Game.GranadaMaxPrice, Game.GranadaPricesExcepts, tiePlayerNumbers[building].ToDictionary(p => p, p => (Game.GetPlayer(p).Name, correctingPoints[p])), SettingsType.ValidateBuildingsPrice);
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

        public static Dictionary<GranadaBuildingType, List<int>> GetTiePlayerNumbers(RoundScoring gameScoreSubmitScoreData, int roundNumber)
        {
            Dictionary<GranadaBuildingType, List<int>> result = new Dictionary<GranadaBuildingType, List<int>>();

            foreach (GranadaBuildingType building in Game.GranadaBuildingsOrder)
            {
                List<int> tiePlayerNumbers = new List<int>();

                List<int> duplicatedPoints = gameScoreSubmitScoreData.PlayersScoreData.GroupBy(p => p.GranadaBuildingsCount[building]).Where(g => g.Key != 0 && g.Count() > 1).Select(g => g.Key).ToList();

                for (int i = 0; i < gameScoreSubmitScoreData.PlayersScoreData.Count; i++)
                {
                    int buildingsNumber = gameScoreSubmitScoreData.PlayersScoreData[i].GranadaBuildingsCount[building];
                    int higherPlayersCount = gameScoreSubmitScoreData.PlayersScoreData.Where(p => p.PlayerNumber != i + 1).Count(p => p.GranadaBuildingsCount[building] > buildingsNumber);
                    if (duplicatedPoints.Contains(buildingsNumber) && higherPlayersCount < roundNumber)
                        tiePlayerNumbers.Add(i + 1);
                }

                result[building] = tiePlayerNumbers;
            }

            return result;
        }

        public void SetGranadaBuildingsNumbers(Dictionary<GranadaBuildingType, Dictionary<int, int>> playersBuildingsHighestPrices)
        {
            for (int i = 0; i < Game.PlayersCount; i++)
            {
                Dictionary<GranadaBuildingType, int> granadaBuildingsHighestPrices = new Dictionary<GranadaBuildingType, int>();
                foreach (GranadaBuildingType building in Game.GranadaBuildingsOrder)
                {
                    Dictionary<int, int> playersHighestPrices = playersBuildingsHighestPrices[building];
                    if (playersHighestPrices.ContainsKey(i + 1))
                        granadaBuildingsHighestPrices[building] = playersHighestPrices[i + 1];
                }
                Application.GameScoreSubmitScoreData.PlayersScoreData[i].GranadaBuildingsHighestPrices = granadaBuildingsHighestPrices;
            }
        }
    }
}