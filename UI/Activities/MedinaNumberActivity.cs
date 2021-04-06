﻿using AlhambraScoringAndroid.GamePlay;
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
    [Activity(Label = "Ilość Medyn", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class MedinaNumberActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_players_buildings_chose;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            List<int> tiePlayerNumbers = GetTiePlayerNumbers(Application.GameScoreSubmitScorePanels, Game.RoundNumber);

            LinearLayout container = FindViewById<LinearLayout>(Resource.Id.container);

            PlayersBuildingChose playersPanel = new PlayersBuildingChose(this,"Medina", 2, 10, tiePlayerNumbers.ToDictionary(p => p, p => Game.GetPlayer(p ).Name));
            container.AddView(playersPanel);
            container.RequestLayout();

            Button confirmButton = FindViewById<Button>(Resource.Id.confirmButton);
            confirmButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                if (playersPanel.playersPanels.ValidatePlayerPanels())
                {
                    Dictionary<int, int> playersHighestPrices = new Dictionary<int, int>();
                    foreach (int playerNumber in tiePlayerNumbers)
                        playersHighestPrices[playerNumber] = playersPanel.playersPanels[playerNumber - 1].Value;

                    Application.ConfirmMedinasNumber(this, playersHighestPrices);
                }
            });
        }

        public static List<int> GetTiePlayerNumbers(List<PlaceholderPlayerScoreFragment> gameScoreSubmitScorePanels, int roundNumber)
        {
            List<int> tiePlayerNumbers = new List<int>();

            List<int> duplicatedPoints = gameScoreSubmitScorePanels.GroupBy(p => p.MedinasNumber).Where(g => g.Key != 0 && g.Count() > 1).Select(g => g.Key).ToList();

            for (int i = 0; i < gameScoreSubmitScorePanels.Count; i++)
            {
                int medinasNumber = gameScoreSubmitScorePanels[i].MedinasNumber;
                int lowerPlayersCount = gameScoreSubmitScorePanels.Where(p => p.PlayerNumber != i + 1).Count(p => p.MedinasNumber < medinasNumber);
                if (duplicatedPoints.Contains(medinasNumber) && lowerPlayersCount < roundNumber)
                    tiePlayerNumbers.Add(i + 1);
            }

            return tiePlayerNumbers;
        }
    }
}