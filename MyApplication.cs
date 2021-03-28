using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.UI;
using AlhambraScoringAndroid.UI.Activities;
using Android.App;
using Android.Content;
using System;
using System.Collections.Generic;

namespace AlhambraScoringAndroid
{
    [Application]
    public class MyApplication : Application
    {
        //TODO porządek w kolejności properties w layout
        //TODO revert submit points / add1/2 points
        //TODO restore state after application close
        //TODO another expansion modules
        //TODO labelki słownik
        //TODO iOS
        //https://docs.microsoft.com/pl-pl/xamarin/
        //TODO google play "I would appreciate any feedback", BGG
        //TODO nieptrzebne referencje, nuget
        //TODO package "companyname"
        //TODO przetestować wszędzie obracanie ekranu
        //TODO guideline górna część min height
        //TODO wysokości i szerokości do zmiennych; 60dp = menu height
        //TODO duży rozmiar aplikacji:
        //https://heartbeat.fritz.ai/reducing-the-app-size-in-xamarin-deep-dive-7ddc9cb12688
        //https://dzone.com/articles/how-to-optimize-and-reduce-android-apps-size-in-xa
        //https://docs.microsoft.com/en-us/xamarin/android/deploy-test/linker


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

        public void NewGamePrompt(Context context)
        {
            if (Game?.GameStarted ?? false)
            {
                new AlertDialog.Builder(context)
                    .SetTitle("Closing Activity")
                    .SetMessage("Are you sure you want to close this activity?")
                    .SetPositiveButton("Yes", new DialogInterfaceOnClickListener((IDialogInterface dialog, int which) => NewGame()))
                    .SetNegativeButton("No", new DialogInterfaceOnClickListener(null))
                    .Show();
            }
            else
                NewGame();
        }

        //TODO private
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

        public void GameShowDetails()
        {
            //TODO  Grid, w kolumnach obrazki
            NewActivity(typeof(GameDetailsActivity));
        }
    }
}