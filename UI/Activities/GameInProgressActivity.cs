using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.UI;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "Wyniki", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class GameInProgressActivity : BaseActivity
    {
        private readonly List<PlayerResultPanel> resultPanels;

        private Button roundScoreButton;
        private Button scoreDetailsButton;
        private Button scoreRevertButton;

        protected override int ContentView => Resource.Layout.activity_game_in_progress;

        public GameInProgressActivity()
        {
            resultPanels = new List<PlayerResultPanel>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            PlayerResultPanel resultPanel1 = FindViewById<PlayerResultPanel>(Resource.Id.playerResultPanel1);
            PlayerResultPanel resultPanel2 = FindViewById<PlayerResultPanel>(Resource.Id.playerResultPanel2);
            PlayerResultPanel resultPanel3 = FindViewById<PlayerResultPanel>(Resource.Id.playerResultPanel3);
            PlayerResultPanel resultPanel4 = FindViewById<PlayerResultPanel>(Resource.Id.playerResultPanel4);
            PlayerResultPanel resultPanel5 = FindViewById<PlayerResultPanel>(Resource.Id.playerResultPanel5);
            PlayerResultPanel resultPanel6 = FindViewById<PlayerResultPanel>(Resource.Id.playerResultPanel6);
            resultPanel1.Initialize(this, 1);
            resultPanels.Add(resultPanel1);
            resultPanel2.Initialize(this, 2);
            resultPanels.Add(resultPanel2);
            resultPanel3.Initialize(this, 3);
            resultPanels.Add(resultPanel3);
            if (Application.Game.PlayersCount > 3)
            {
                resultPanel4.Initialize(this, 4);
                resultPanels.Add(resultPanel4);
            }
            if (Application.Game.PlayersCount > 4)
            {
                resultPanel5.Initialize(this, 5);
                resultPanels.Add(resultPanel5);
            }
            if (Application.Game.PlayersCount > 5)
            {
                resultPanel6.Initialize(this, 6);
                resultPanels.Add(resultPanel6);
            }

            roundScoreButton = FindViewById<Button>(Resource.Id.roundScoreButton);
            scoreDetailsButton = FindViewById<Button>(Resource.Id.scoreDetailsButton);
            scoreRevertButton = FindViewById<Button>(Resource.Id.scoreRevertButton);

            roundScoreButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                if (Application.Game.ScoreRound != ScoringRound.Finish)
                    Application.GameRoundScore(this);
            });

            scoreDetailsButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                //TODO przechowywanie wyników
                Application.GameShowDetails();
            });

            scoreRevertButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                Application.Game.RevertScoring();
                //ShowScore();
                PrepareRound();
            });

            PrepareRound();
        }

        public override void OnBackPressed()
        {
            if (Game.GameStarted)
            {
                new AlertDialog.Builder(this)
                    .SetTitle("Closing Activity")
                    .SetMessage("Are you sure you want to close this activity?")
                    .SetPositiveButton("Yes", new DialogInterfaceOnClickListener((IDialogInterface dialog, int which) => base.OnBackPressed()))
                    .SetNegativeButton("No", new DialogInterfaceOnClickListener(null))
                    .Show();
            }
            else
                base.OnBackPressed();
        }

        public void AddPoints(int player, int score)
        {
            Application.Game.PlayerAddScore(player, score);
            ShowScore();
        }

        public void PrepareRound()
        {
            ShowScore();

            switch (Application.Game.ScoreRound)
            {
                case ScoringRound.First:
                    roundScoreButton.Text = "1st round";
                    scoreDetailsButton.Visibility = ViewStates.Invisible;
                    break;
                case ScoringRound.Second:
                    roundScoreButton.Text = "2nd round";
                    scoreDetailsButton.Visibility = ViewStates.Visible;
                    break;
                case ScoringRound.ThirdBeforeLeftover:
                    roundScoreButton.Text = "3rd round before leftover buildings are assigned";
                    break;
                case ScoringRound.Third:
                    roundScoreButton.Text = "3rd round";
                    break;
                case ScoringRound.Finish:
                    roundScoreButton.Visibility = ViewStates.Invisible;
                    scoreDetailsButton.Text = "Zapisz i pokaż szczegóły";
                    foreach (PlayerResultPanel resultPanel in resultPanels)
                        resultPanel.HidePointButtons();
                    break;
            }
        }

        private void ShowScore()
        {
            //TODO sort
            resultPanels[0].SetScore(Application.Game.GetPlayer(1).Score);
            resultPanels[1].SetScore(Application.Game.GetPlayer(2).Score);
            resultPanels[2].SetScore(Application.Game.GetPlayer(3).Score);
            if (Application.Game.PlayersCount > 3)
                resultPanels[3].SetScore(Application.Game.GetPlayer(4).Score);
            if (Application.Game.PlayersCount > 4)
                resultPanels[4].SetScore(Application.Game.GetPlayer(5).Score);
            if (Application.Game.PlayersCount > 5)
                resultPanels[5].SetScore(Application.Game.GetPlayer(6).Score);

            //TODO && !saved
            if (Application.Game.ScoreStack.Count != 0)
            {
                scoreRevertButton.Visibility =  ViewStates.Visible ;
                scoreRevertButton.Text = $"Cofnij {Application.Game.ScoreStack.Peek().FullDisplayName()}";
            }
            else
                scoreRevertButton.Visibility = ViewStates.Invisible;
        }
    }
}