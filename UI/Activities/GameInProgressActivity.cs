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
    [Activity(Label = "@string/results", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class GameInProgressActivity : BaseActivity
    {
        private List<PlayerResultPanel> resultPanels;

        private Button roundScoreButton;
        private Button scoreDetailsButton;
        private Button scoreRevertButton;

        protected override int ContentView => Resource.Layout.activity_game_in_progress;

        public GameInProgressActivity()
        {
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            resultPanels = new List<PlayerResultPanel>()
            {
                FindViewById<PlayerResultPanel>(Resource.Id.playerResultPanel1),
                FindViewById<PlayerResultPanel>(Resource.Id.playerResultPanel2),
                FindViewById<PlayerResultPanel>(Resource.Id.playerResultPanel3),
                FindViewById<PlayerResultPanel>(Resource.Id.playerResultPanel4),
                FindViewById<PlayerResultPanel>(Resource.Id.playerResultPanel5),
                FindViewById<PlayerResultPanel>(Resource.Id.playerResultPanel6),
            };
            for (int i = 0; i < Game.PlayersCount; i++)
            {
                resultPanels[i].Initialize(i + 1);
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

        //public override void OnBackPressed()
        //{
        //    if (Game.GameInProgress)
        //    {
        //        new AlertDialog.Builder(this)
        //            .SetTitle(Resources.GetString(Resource.String.game_ending))
        //            .SetMessage(Resources.GetString(Resource.String.continue_question))
        //            .SetPositiveButton(Resources.GetString(Resource.String.yes), new DialogInterfaceOnClickListener((IDialogInterface dialog, int which) =>
        //            {
        //                base.OnBackPressed();
        //                Game.Reset(true);
        //            }))
        //            .SetNegativeButton(Resources.GetString(Resource.String.no), new DialogInterfaceOnClickListener(null))
        //            .Show();
        //    }
        //    else
        //    {
        //        base.OnBackPressed();
        //        Game.Reset(true);
        //    }
        //}

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
                    roundScoreButton.Text = Resources.GetString(Resource.String.round_1);
                    break;
                case ScoringRound.Second:
                    roundScoreButton.Text = Resources.GetString(Resource.String.round_2);
                    break;
                case ScoringRound.ThirdBeforeLeftover:
                    roundScoreButton.Text = Resources.GetString(Resource.String.round_3_before);
                    break;
                case ScoringRound.Third:
                    roundScoreButton.Text = Resources.GetString(Resource.String.round_3);
                    break;
                case ScoringRound.Finish:
                    break;
            }
            scoreDetailsButton.Visibility = Game.ScoreRound == ScoringRound.First ? ViewStates.Invisible : ViewStates.Visible;
            if (!Game.Saved && Game.ScoreRound == ScoringRound.Finish)
                scoreDetailsButton.Text = Resources.GetString(Resource.String.save_and_show_details);
            else
                scoreDetailsButton.Text = Resources.GetString(Resource.String.show_details);
            roundScoreButton.Visibility = Game.ScoreRound == ScoringRound.Finish ? ViewStates.Invisible : ViewStates.Visible;

            for (int i = 0; i < Game.PlayersCount; i++)
            {
                if (Game.ScoreRound == ScoringRound.Finish)
                    resultPanels[i].ShowPointButtons(false);
                else
                    resultPanels[i].ShowPointButtons(true);
            }
        }

        private void ShowScore()
        {
            //TODO sort
            for (int i = 0; i < Game.PlayersCount; i++)
            {
                resultPanels[i].SetScore(Game.GetPlayer(i + 1).Score);
            }

            if (Game.ScoreStack.Count != 0)
            {
                scoreRevertButton.Visibility = ViewStates.Visible;
                scoreRevertButton.Text = String.Format(Resources.GetString(Resource.String.revert), Game.ScoreStack.Peek().FullDisplayName());
            }
            else
                scoreRevertButton.Visibility = ViewStates.Invisible;
        }
    }
}