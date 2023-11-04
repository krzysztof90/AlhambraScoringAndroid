using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidBase.Tools;
using AndroidBase.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "@string/results", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class GameInProgressActivity : BaseActivity
    {
        public List<PlayerScoreData> CorrectingScoring { get; private set; }

        private List<PlayerResultPanel> resultPanels;

        private Button roundScoreButton;
        private Button scoreDetailsButton;
        private Button scoreRevertButton;
        private Button blueDicesCombinationsButton;

        protected override int ContentView => Resource.Layout.activity_game_in_progress;

        public GameInProgressActivity()
        {
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            CorrectingScoring = null;

            resultPanels = new List<int>()
            {
                Resource.Id.playerResultPanel1,
                Resource.Id.playerResultPanel2,
                Resource.Id.playerResultPanel3,
                Resource.Id.playerResultPanel4,
                Resource.Id.playerResultPanel5,
                Resource.Id.playerResultPanel6
            }.Select(r => FindViewById<PlayerResultPanel>(r)).ToList();

            for (int i = 0; i < Game.PlayersCount; i++)
            {
                resultPanels[i].Initialize(i + 1);
            }
            for (int i = Game.PlayersCount; i < 6; i++)
            {
                resultPanels[i].SetVisibility(false);
            }

            roundScoreButton = FindViewById<Button>(Resource.Id.roundScoreButton);
            scoreDetailsButton = FindViewById<Button>(Resource.Id.scoreDetailsButton);
            scoreRevertButton = FindViewById<Button>(Resource.Id.scoreRevertButton);
            blueDicesCombinationsButton = FindViewById<Button>(Resource.Id.blueDicesCombinationsButton);

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
                if (Game.ScoreStack.Peek() is ScoreHistoryRound)
                {
                    new AlertDialog.Builder(this)
                    .SetTitle(Resources.GetString(Resource.String.revert_punctation))
                    .SetItems(new string[] { Resources.GetString(Resource.String.correct), Resources.GetString(Resource.String.pullback) }, new DialogInterfaceOnClickListener((IDialogInterface dialog, int which) =>
                    {
                        switch (which)
                        {
                            case 0:
                                CorrectingScoring = (Game.RoundNumber == 3 && Game.ThirdBeforeRoundScoring != null) ? Game.ThirdBeforeRoundScoring : Game.PreviousRoundScoring;
                                Game.BackRound();
                                Application.GameRoundScore(this);
                                break;
                            case 1:
                                Game.RevertScoring();
                                PrepareRound();
                                break;
                        }
                    }))
                    .SetNegativeButton(Resources.GetString(Resource.String.cancel), new DialogInterfaceOnClickListener(null))
                    .Show();
                }
                else
                {
                    Game.RevertScoring();
                    PrepareRound();
                }
            });

            blueDicesCombinationsButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                Application.BlueDicesCombinations();
            });

            PrepareRound();
        }

        public void ResetTemporaryRevert()
        {
            if (CorrectingScoring != null)
            {
                //revert BackRound
                Game.SetNextRound();
                CorrectingScoring = null;
            }
        }

        public void PerformProperRevert()
        {
            if (CorrectingScoring != null)
            {
                ResetTemporaryRevert();
                Game.RevertScoring();
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
            blueDicesCombinationsButton.SetVisibility(Game.ScoreRound != ScoringRound.Finish && Game.HasModule(ExpansionModule.DesignerHandymen));

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