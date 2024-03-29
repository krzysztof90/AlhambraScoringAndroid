﻿using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.Options;
using AlhambraScoringAndroid.UI.Activities;
using Android.App;
using Android.Content;
using Android.Content.Res;
using AndroidBase;
using AndroidBase.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid
{
    [Application]
    public class MyApplication : AndroidBaseApplication
    {
        //TODO BGG
        //TODO przezroczyste obrazki
        //TODO minSdkVersion
        //TODO restore state after application (automatic) close
        //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        //global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
        //LoadApplication(new App());

        public List<ResultHistory> Results { get; private set; }
        public Game Game { get; private set; }
        private GameInProgressActivity gameInProgressActivity;
        public ResultHistory CurrentResult { get; private set; }
        public bool ArchiveResult { get; private set; }
        public RoundScoring GameScoreSubmitScoreData { get; private set; }

        private readonly List<(Func<bool> condition, Type activityType)> neededScoreAdditionalActions;
        private List<BaseActivity> scoreActivities;

        public MyApplication(IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            neededScoreAdditionalActions = new List<(Func<bool> condition, Type activityType)>()
            {
                (()=> Game.HasModule(ExpansionModule.ExpansionCharacters) && CharacterTheWiseManActivity.Show(GameScoreSubmitScoreData),
                typeof(CharacterTheWiseManActivity)),
                (()=> Game.HasModule(ExpansionModule.QueenieMedina) && MedinaNumberActivity.GetTiePlayerNumbers(GameScoreSubmitScoreData, Game.RoundNumber).Count != 0,
                typeof(MedinaNumberActivity)),
                (()=> Game.HasModule(ExpansionModule.RedPalaceLandTiles) && RoundScoringDataActivity.Show(GameScoreSubmitScoreData),
                typeof(RoundScoringDataActivity)),
                (()=> Game.HasModule(ExpansionModule.Granada) && GranadaBuildingsNumberActivity.GetTiePlayerNumbers(GameScoreSubmitScoreData, Game.RoundNumber).Any(d => d.Value.Count != 0),
                typeof(GranadaBuildingsNumberActivity)),
            };
        }

        public void MainScreen(BaseActivity activity)
        {
            NewActivity(typeof(MainActivity), ActivityFlags.ClearTop);
        }

        public void NewGamePrompt(Context context)
        {
            if ((Game?.GameInProgress ?? false) || context is GameScoreActivity)
            {
                new AlertDialog.Builder(context)
                    .SetTitle(Resources.GetString(Resource.String.game_ending))
                    .SetMessage(Resources.GetString(Resource.String.continue_question))
                    .SetPositiveButton(Resources.GetString(Resource.String.yes), new DialogInterfaceOnClickListener((IDialogInterface dialog, int which) => NewGame()))
                    .SetNegativeButton(Resources.GetString(Resource.String.no), new DialogInterfaceOnClickListener(null))
                    .Show();
            }
            else
                NewGame();
        }

        private void NewGame()
        {
            Game = new Game(ApplicationContext);
            scoreActivities = new List<BaseActivity>();
            NewActivity(typeof(GamePlayersChoseActivity));
        }

        public void GameSetPlayers(List<string> players)
        {
            if (Game.ValidatePlayers(players))
            {
                Game.SetPlayers(players);
                Game.SetStartDateTime(DateTime.Now);
                NewActivity(typeof(GameModulesChoseActivity));
            }
        }

        public void GameApplyModules(IEnumerable<ExpansionModule> modules, GranadaOption granadaOption, AlcazabaOption alcazabaOption)
        {
            Game.SetModules(modules);
            Game.SetGranadaOption(granadaOption);
            Game.SetAlcazabaOption(alcazabaOption);
            if (modules.Contains(ExpansionModule.ExpansionNewScoreCards) || modules.Contains(ExpansionModule.FanCaliphsGuidelines))
                NewActivity(typeof(GameModulesDetailsChoseActivity));
            else
                NewActivity(typeof(GameSetupInstructionActivity));
        }

        public void GameApplyModulesDetails(IEnumerable<CaliphsGuidelinesMission> caliphsGuidelines, List<NewScoreCard> newScoreCards)
        {
            if (Game.ValidateNewScoreCards(newScoreCards))
            {
                Game.SetModulesDetails(caliphsGuidelines, newScoreCards);
                NewActivity(typeof(GameSetupInstructionActivity));
            }
        }

        public void GameStart()
        {
            Game.Start();
            NewActivity(typeof(GameInProgressActivity));
        }

        public void GameRoundScore(GameInProgressActivity activity)
        {
            gameInProgressActivity = activity;
            NewActivity(typeof(GameScoreActivity));
        }

        public RoundScoring CorrectingScoring()
        {
            return gameInProgressActivity.CorrectingScoring;
        }

        public void ResetGameInProgress()
        {
            gameInProgressActivity.ResetTemporaryRevert();
        }

        public void SubmitScore(GameScoreActivity activity, List<PlayerScoreData> scoreData)
        {
            RoundScoring roundScoring = new RoundScoring() { PlayersScoreData = scoreData };

            if (Game.ScoreRound != ScoringRound.ThirdBeforeLeftover ? Game.ValidateScore(roundScoring) : Game.ValidateScoreBeforeAssignLeftoverBuildings(roundScoring))
            {
                GameScoreSubmitScoreData = roundScoring;
                TryScore(activity);
            }
        }

        private void TryScore(BaseActivity activity)
        {
            int activityPosition = scoreActivities.FindIndex(a => a?.GetType() == activity.GetType());
            if (activityPosition != -1)
                scoreActivities.RemoveRange(activityPosition, scoreActivities.Count - activityPosition);
            scoreActivities.Add(activity);

            bool runAdditionalActivity = false;
            for (int i = scoreActivities.Count - 1; i < neededScoreAdditionalActions.Count; i++)
            {
                if (neededScoreAdditionalActions[i].condition())
                {
                    NewActivity(neededScoreAdditionalActions[i].activityType);

                    runAdditionalActivity = true;
                    break;
                }
                else
                    scoreActivities.Add(null);
            }
            if (!runAdditionalActivity)
            {
                foreach (BaseActivity scoreActivity in scoreActivities)
                    scoreActivity?.Finish();

                scoreActivities = new List<BaseActivity>();

                gameInProgressActivity.PerformProperRevert();

                if (Game.ScoreRound != ScoringRound.ThirdBeforeLeftover)
                    Game.Score(GameScoreSubmitScoreData);
                else
                    Game.ScoreBeforeAssignLeftoverBuildings(GameScoreSubmitScoreData);
                Game.SetNextRound();
                gameInProgressActivity.PrepareRound();
                if (Game.ScoreRound == ScoringRound.Finish)
                    Game.SetEndDateTime(DateTime.Now);
            }
        }

        public void ConfirmTheWiseManChose(CharacterTheWiseManActivity activity, BuildingType? buildingType)
        {
            activity.SetTheWiseManBuildingType(buildingType);
            TryScore(activity);
        }

        public void ConfirmMedinasNumber(MedinaNumberActivity activity, Dictionary<int, int> playersHighestPrices)
        {
            if (Game.ValidateMedinasNumbers(playersHighestPrices))
            {
                activity.SetMedinasNumbers(playersHighestPrices);
                TryScore(activity);
            }
        }
        public void ConfirmRoundScoringData(RoundScoringDataActivity activity, int guardsPoints)
        {
            if (Game.ValidateRoundScoringData(guardsPoints))
            {
                activity.SetRoundScoringData(guardsPoints);
                TryScore(activity);
            }
        }
        public void ConfirmGranadaBuildingsNumbers(GranadaBuildingsNumberActivity activity, Dictionary<GranadaBuildingType, Dictionary<int, int>> playersHighestPrices)
        {
            if (Game.ValidateGranadaBuildingsNumbers(playersHighestPrices))
            {
                activity.SetGranadaBuildingsNumbers(playersHighestPrices);
                TryScore(activity);
            }
        }

        public void BlueDicesCombinations()
        {
            NewActivity(typeof(BlueDicesActivity));
        }

        public void GameShowDetails(GameInProgressActivity gameInProgressActivity)
        {
            CurrentResult = Game.GetResultHistory();
            ArchiveResult = false;

            if (Game.ScoreRound == ScoringRound.Finish && !Game.Saved)
            {
                RemoveResult(CurrentResult.StartDateTime);
                Results.Add(CurrentResult);
                SaveResults();
                Game.Saved = true;
                gameInProgressActivity.PrepareRound();
            }
            NewActivity(typeof(GameDetailsActivity));
        }

        public void ShowResult(DateTime startDateTime)
        {
            CurrentResult = Results.Single(r => r.StartDateTime == startDateTime);
            ArchiveResult = true;
            NewActivity(typeof(GameDetailsActivity));
        }

        public void ShowHistory(Context context)
        {
            if (!(context is ResultsHistoryActivity))
                NewActivity(typeof(ResultsHistoryActivity));
        }

        public void ShowSettings()
        {
            NewActivity(typeof(SettingsActivity));
        }

        public void ShowAbout()
        {
            NewActivity(typeof(AboutActivity));
        }

        public void LoadResults()
        {
            Results = Settings.GetResults();
        }

        public void SaveResults()
        {
            Settings.SaveResults(Results, Context);
        }

        public void RemoveResult(DateTime startDateTime)
        {
            Results.RemoveAll(r => r.StartDateTime == startDateTime);
        }
    }
}