using AlhambraScoringAndroid.GamePlay;
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
            resultPanel1.Initialize( 1);
            resultPanels.Add(resultPanel1);
            resultPanel2.Initialize( 2);
            resultPanels.Add(resultPanel2);
            resultPanel3.Initialize( 3);
            resultPanels.Add(resultPanel3);
            if (Game.PlayersCount > 3)
            {
                resultPanel4.Initialize( 4);
                resultPanels.Add(resultPanel4);
            }
            if (Game.PlayersCount > 4)
            {
                resultPanel5.Initialize( 5);
                resultPanels.Add(resultPanel5);
            }
            if (Game.PlayersCount > 5)
            {
                resultPanel6.Initialize( 6);
                resultPanels.Add(resultPanel6);
            }

            roundScoreButton = FindViewById<Button>(Resource.Id.roundScoreButton);
            scoreDetailsButton = FindViewById<Button>(Resource.Id.scoreDetailsButton);
            scoreRevertButton = FindViewById<Button>(Resource.Id.scoreRevertButton);

            roundScoreButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                if (Game.ScoreRound != ScoringRound.Finish)
                    Application.GameRoundScore(this);
            });

            scoreDetailsButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                Application.GameShowDetails(this);
            });

            scoreRevertButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                Game.RevertScoring();
                PrepareRound();
            });

            PrepareRound();
        }

        public override void OnBackPressed()
        {
            if (Game.GameInProgress)
            {
                new AlertDialog.Builder(this)
                    .SetTitle("Obecna gra zostanie zakończona")
                    .SetMessage("Czy kontynuować?")
                    .SetPositiveButton("Yes", new DialogInterfaceOnClickListener((IDialogInterface dialog, int which) =>
                    {
                        base.OnBackPressed();
                        Game.Reset(true);
                    }))
                    .SetNegativeButton("No", new DialogInterfaceOnClickListener(null))
                    .Show();
            }
            else
            {
                base.OnBackPressed();
                        Game.Reset(true);
            }
        }

        public void AddPoints(int player, int score)
        {
            Game.PlayerAddScore(player, score);
            ShowScore();
        }

        public void PrepareRound()
        {
            ShowScore();

            switch (Game.ScoreRound)
            {
                case ScoringRound.First:
                    roundScoreButton.Text = "1st round";
                    break;
                case ScoringRound.Second:
                    roundScoreButton.Text = "2nd round";
                    break;
                case ScoringRound.ThirdBeforeLeftover:
                    roundScoreButton.Text = "3rd round before leftover buildings are assigned";
                    break;
                case ScoringRound.Third:
                    roundScoreButton.Text = "3rd round";
                    break;
                case ScoringRound.Finish:
                    break;
            }
            scoreDetailsButton.Visibility = Game.ScoreRound == ScoringRound.First ? ViewStates.Invisible : ViewStates.Visible;
            if (!Game.Saved && Game.ScoreRound == ScoringRound.Finish)
                scoreDetailsButton.Text = "Zapisz i pokaż szczegóły";
            else
                scoreDetailsButton.Text = "Pokaż szczegóły";
            roundScoreButton.Visibility = Game.ScoreRound == ScoringRound.Finish ? ViewStates.Invisible : ViewStates.Visible;
            foreach (PlayerResultPanel resultPanel in resultPanels)
            {
                if (Game.ScoreRound == ScoringRound.Finish)
                    resultPanel.ShowPointButtons(false);
                else
                    resultPanel.ShowPointButtons(true);
            }
        }

        private void ShowScore()
        {
            //TODO sort
            resultPanels[0].SetScore(Game.GetPlayer(1).Score);
            resultPanels[1].SetScore(Game.GetPlayer(2).Score);
            resultPanels[2].SetScore(Game.GetPlayer(3).Score);
            if (Game.PlayersCount > 3)
                resultPanels[3].SetScore(Game.GetPlayer(4).Score);
            if (Game.PlayersCount > 4)
                resultPanels[4].SetScore(Game.GetPlayer(5).Score);
            if (Game.PlayersCount > 5)
                resultPanels[5].SetScore(Game.GetPlayer(6).Score);

            if (Game.ScoreStack.Count != 0)
            {
                scoreRevertButton.Visibility = ViewStates.Visible;
                scoreRevertButton.Text = $"Cofnij {Game.ScoreStack.Peek().FullDisplayName()}";
            }
            else
                scoreRevertButton.Visibility = ViewStates.Invisible;
        }
    }
}