using AlhambraScoringAndroid.Activities;
using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.UI;
using Android.App;
using Android.Content;
using System;
using System.Collections.Generic;

namespace AlhambraScoringAndroid
{
    [Application]
    public class MyApplication : Application
    {
        //TODO revert submit points / add1/2 points
        //TODO store results
        //TODO restore state after application close
        //TODO confirm new game
        //TODO confirm back when on score screen
        //TODO another expansion modules
        //TODO labelki słownik
        //TODO google play "I would appreciate any feedback", BGG

        public MyApplication(IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
        public override void OnCreate()
        {
            base.OnCreate();
        }

        public Game Game { get; private set; }
        private GameInProgressActivity gameInProgressActivity;

        private void NewActivity(Type activityType)
        {
            Intent intent = new Intent(ApplicationContext, activityType);
            intent.AddFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }

        public void NewGame()
        {
            Game = new Game(ApplicationContext);
            NewActivity(typeof(NewGameActivity));
        }

        public void GameApplyModules(IEnumerable<ExpansionModule> modules)
        {
            Game.SetModules(modules);
            NewActivity(typeof(GamePlayersChoseActivity));
        }

        public void GameStart(List<string> players)
        {
            Game.SetPlayers(players);
            NewActivity(typeof(GameInProgressActivity));
        }

        public void GameRoundScore(GameInProgressActivity activity)
        {
            gameInProgressActivity = activity;
            NewActivity(typeof(GameScoreActivity));
        }

        public void SubmitScore(GameScoreActivity activity, List<PlaceholderPlayerScoreFragment> scorePanels)
        {
            if (Game.ValidateScore(scorePanels))
            {
                Game.Score(scorePanels);
                Game.SetNextRound();
                activity.Finish();
                gameInProgressActivity.PrepareRound();
            }
        }

        public void SubmitScoreBeforeAssignLeftoverBuildings(GameScoreActivity activity, List<PlaceholderPlayerScoreBeforeAssignLeftoverFragment> scorePanels)
        {
            if (Game.ValidateScoreBeforeAssignLeftoverBuildings(scorePanels))
            {
                Game.ScoreBeforeAssignLeftoverBuildings(scorePanels);
                Game.SetNextRound();
                activity.Finish();
                gameInProgressActivity.PrepareRound();
            }
        }
    }
}