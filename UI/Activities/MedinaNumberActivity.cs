using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.Tools;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI.Activities
{
    //"provide"
    [Activity(Label = "Ilość Medyn", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class MedinaNumberActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_players_buildings_chose;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            List<int> tiePlayerNumbers = GetTiePlayerNumbers(Application.GameScoreSubmitScorePanels, Game.RoundNumber);

            List<ScoreLineNumberView> playersPanels = new List<ScoreLineNumberView>()
            {
                FindViewById<ScoreLineNumberView>(Resource.Id.player1HighestMedinaPurchasePriceNumericUpDown),
                FindViewById<ScoreLineNumberView>(Resource.Id.player2HighestMedinaPurchasePriceNumericUpDown),
                FindViewById<ScoreLineNumberView>(Resource.Id.player3HighestMedinaPurchasePriceNumericUpDown),
                FindViewById<ScoreLineNumberView>(Resource.Id.player4HighestMedinaPurchasePriceNumericUpDown),
                FindViewById<ScoreLineNumberView>(Resource.Id.player5HighestMedinaPurchasePriceNumericUpDown),
                FindViewById<ScoreLineNumberView>(Resource.Id.player6HighestMedinaPurchasePriceNumericUpDown),
            };

            for (int i = 0; i < 6; i++)
            {
                if (tiePlayerNumbers.Contains(i + 1))
                {
                    playersPanels[i].SetLabel(Game.GetPlayer(i + 1).Name);
                    playersPanels[i].SetNumberRange(2, 10);
                }
                else
                    playersPanels[i].Visibility = ViewStates.Gone;
            }

            Button confirmButton = FindViewById<Button>(Resource.Id.confirmButton);
            confirmButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                if (playersPanels.ValidatePlayerPanels())
                {
                    Dictionary<int, int> playersHighestPrices = new Dictionary<int, int>();
                    foreach (int playerNumber in tiePlayerNumbers)
                        playersHighestPrices[playerNumber] = playersPanels[playerNumber - 1].Value;

                    Application.ConfirmMedinasNumber(this, playersHighestPrices);
                }
            });
        }

        public static List<int> GetTiePlayerNumbers(List<PlaceholderPlayerScoreFragment> gameScoreSubmitScorePanels, int roundNumber)
        {
            List<int> duplicatedPoints = gameScoreSubmitScorePanels.GroupBy(p => p.MedinasNumber).Where(g => g.Key != 0 && g.Count() > 1).Select(g => g.Key).ToList();

            List<int> tiePlayerNumbers = new List<int>();
            for (int i = 0; i < gameScoreSubmitScorePanels.Count; i++)
            {
                int medinasNumber = gameScoreSubmitScorePanels[i].MedinasNumber;
                int lowestPlayersCount = 0;
                for (int j = 0; j < gameScoreSubmitScorePanels.Count; j++)
                    if (j != i)
                    {
                        if (gameScoreSubmitScorePanels[j].MedinasNumber < medinasNumber)
                            lowestPlayersCount++;
                    }
                if (duplicatedPoints.Contains(medinasNumber) && lowestPlayersCount < roundNumber)
                    tiePlayerNumbers.Add(i + 1);
            }

            return tiePlayerNumbers;
        }
    }
}