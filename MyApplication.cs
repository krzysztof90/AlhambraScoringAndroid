using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.UI;
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
    [Application]
    public class MyApplication : Application
    {
        //TODO revert submit points / add1/2 points
        //TODO store results
        //TODO restore state after application close
        //TODO confirm new game
        //TODO confirm back when on score screen
        //TODO another expansion modules
        //TODO github. "Scoring calculator for Alhambra board game"

        public MyApplication(IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
        public override void OnCreate()
        {
            base.OnCreate();
        }

        //TODO metody z dużej litery
        //TODO property
        public Game game;
        public Game getGame()
        {
            return game;
        }
        GameInProgressActivity gameInProgressActivity;

        public void newGame()
        {
            game = new Game(ApplicationContext);
            StartActivity(new Intent(ApplicationContext, typeof(NewGameActivity)));
        }

        public void gameApplyModules(IEnumerable<ExpansionModule> modules)
        {
            game.setModules(modules);
            StartActivity(new Intent(ApplicationContext, typeof(GamePlayersChoseActivity)));
        }

        public void gameStart(List<String> players)
        {
            game.setPlayers(players);
            StartActivity(new Intent(ApplicationContext, typeof(GameInProgressActivity)));
        }

        public void gameRoundScore(GameInProgressActivity activity)
        {
            gameInProgressActivity = activity;
            StartActivity(new Intent(ApplicationContext, typeof(GameScoreActivity)));
        }

        public void submitScore(GameScoreActivity activity, List<PlaceholderPlayerScoreFragment> scorePanels)
        {
            if (game.validateScore(scorePanels))
            {
                game.score(scorePanels);
                game.setNextRound();
                activity.Finish();
                gameInProgressActivity.PrepareRound();
            }
        }

        public void submitScoreBeforeAssignLeftoverBuildings(GameScoreActivity activity, List<PlaceholderPlayerScoreBeforeAssignLeftoverFragment> scorePanels)
        {
            if (game.validateScoreBeforeAssignLeftoverBuildings(scorePanels))
            {
                game.scoreBeforeAssignLeftoverBuildings(scorePanels);
                game.setNextRound();
                activity.Finish();
                gameInProgressActivity.PrepareRound();
            }
        }
    }
}