using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlhambraScoringAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class GameInProgressActivity : BaseActivity
    {
        private readonly List<PlayerResultPanel> resultPanels;

        Button roundScoreButton;

        protected override int getContentView()
        {
            return Resource.Layout.activity_gameinprogress;
        }

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
            resultPanel1.initialize(this, 1);
            resultPanels.Add(resultPanel1);
            resultPanel2.initialize(this, 2);
            resultPanels.Add(resultPanel2);
            resultPanel3.initialize(this, 3);
            resultPanels.Add(resultPanel3);
            if (application().getGame().getPlayersCount > 3)
            {
                resultPanel4.initialize(this, 4);
                resultPanels.Add(resultPanel4);
            }
            if (application().getGame().getPlayersCount > 4)
            {
                resultPanel5.initialize(this, 5);
                resultPanels.Add(resultPanel5);
            }
            if (application().getGame().getPlayersCount > 5)
            {
                resultPanel6.initialize(this, 6);
                resultPanels.Add(resultPanel6);
            }

            roundScoreButton = FindViewById<Button>(Resource.Id.roundScoreButton);

            roundScoreButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                application().gameRoundScore(this);
            });

            PrepareRound();
        }

        public void addPoints(int player, int score)
        {
            application().getGame().PlayerAddScore(player, score);
            ShowScore();
        }

        public void PrepareRound()
        {
            ShowScore();

            switch (application().getGame().ScoreRound)
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
                    roundScoreButton.Visibility = ViewStates.Gone;
                    break;
            }
        }

        private void ShowScore()
        {
            resultPanels[0].setScore(application().getGame().getPlayer(1).Score);
            resultPanels[1].setScore(application().getGame().getPlayer(2).Score);
            resultPanels[2].setScore(application().getGame().getPlayer(3).Score);
            if (application().getGame().getPlayersCount > 3)
                resultPanels[3].setScore(application().getGame().getPlayer(4).Score);
            if (application().getGame().getPlayersCount > 4)
                resultPanels[4].setScore(application().getGame().getPlayer(5).Score);
            if (application().getGame().getPlayersCount > 5)
                resultPanels[5].setScore(application().getGame().getPlayer(6).Score);
        }
    }

}