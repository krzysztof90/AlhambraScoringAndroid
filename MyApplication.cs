using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.Tools;
using AlhambraScoringAndroid.UI;
using AlhambraScoringAndroid.UI.Activities;
using Android.App;
using Android.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace AlhambraScoringAndroid
{
    [Application]
    public class MyApplication : Application
    {
        //TODO nieużywany, zakomentowany kod
        //TODO log unmanaged exceptions, send
        //TODO zamiast layout_constraintTop_toBottomOf włożyć w linearlayout
        //TODO instrukcja przygotowania gry + rund w zależności od wybranych modułów + dirk
        //TODO fill_parent	ffffffff	The view should be as big as its parent (minus padding). This constant is deprecated starting from API Level 8 and is replaced by match_parent.
        //TODO przezroczyste obrazki
        //TODO xmlns:android tylko jedno
        //TODO porządek w kolejności properties w layout
        //TODO restore state after application close
        //TODO labelki słownik
        //TODO iOS
        //https://docs.microsoft.com/pl-pl/xamarin/
        //TODO google play "I would appreciate any feedback", link do github; BGG
        //TODO info o darmowym projekcie, link do paypal
        //TODO niepotrzebne referencje, nuget, aktualizacja
        //TODO minSdkVersion
        //TODO przetestować wszędzie obracanie ekranu
        //TODO wysokości i szerokości do zmiennych; 60dp = menu height
        //TODO BP
        //TODO duży rozmiar aplikacji:
        //https://heartbeat.fritz.ai/reducing-the-app-size-in-xamarin-deep-dive-7ddc9cb12688
        //https://dzone.com/articles/how-to-optimize-and-reduce-android-apps-size-in-xa
        //https://docs.microsoft.com/en-us/xamarin/android/deploy-test/linker

        public List<ResultHistory> Results { get; private set; }
        public Game Game { get; private set; }
        private GameInProgressActivity gameInProgressActivity;
        public ResultHistory CurrentResult { get; private set; }
        public List<PlaceholderPlayerScoreFragment> GameScoreSubmitScorePanels { get; private set; }

        private readonly List<(Func<bool> condition, Type activityType)> neededScoreAdditionalActions;
        private List<BaseActivity> scoreActivities;

        public MyApplication(IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            neededScoreAdditionalActions = new List<(Func<bool> condition, Type activityType)>()
            {
                (()=> Game.HasModule(ExpansionModule.ExpansionCharacters) && GameScoreSubmitScorePanels.Any(p => p.OwnedCharacterTheWiseMan),
                typeof(CharacterTheWiseManActivity)),
                (()=> Game.HasModule(ExpansionModule.QueenieMedina) && MedinaNumberActivity.GetTiePlayerNumbers(GameScoreSubmitScorePanels, Game.RoundNumber).Count!=0,
                typeof(MedinaNumberActivity)),
                (()=> Game.HasModule(ExpansionModule.Granada) && GranadaBuildingsNumberActivity.GetTiePlayerNumbers(GameScoreSubmitScorePanels, Game.RoundNumber).Any(d => d.Value.Count!=0),
                typeof(GranadaBuildingsNumberActivity)),
            };
            scoreActivities = new List<BaseActivity>();
        }
        public override void OnCreate()
        {
            base.OnCreate();
        }

        private void NewActivity(Type activityType)
        {
            Intent intent = new Intent(ApplicationContext, activityType);
            intent.AddFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }

        public void NewGamePrompt(Context context)
        {
            if ((Game?.GameInProgress ?? false) || context is GameScoreActivity)
            {
                new AlertDialog.Builder(context)
                    .SetTitle("Obecna gra zostanie zakończona")
                    .SetMessage("Czy kontynuować?")
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
            //NewActivity(typeof(GameModulesDetailsChoseActivity));
        }

        public void GameApplyModules(IEnumerable<ExpansionModule> modules, GranadaOption granadaOption)
        {
            Game.SetModules(modules);
            Game.SetGranadaOption(granadaOption);
            if (modules.Contains(ExpansionModule.FanCaliphsGuidelines) || modules.Contains(ExpansionModule.ExpansionNewScoreCards))
                NewActivity(typeof(GameModulesDetailsChoseActivity));
            else
                NewActivity(typeof(GamePlayersChoseActivity));
        }

        public void GameApplyModulesDetails(IEnumerable<CaliphsGuidelinesMission> caliphsGuidelines, List<NewScoreCard> newScoreCards)
        {
            if (Game.ValidateNewScoreCards(newScoreCards))
            {
                Game.SetModulesDetails(caliphsGuidelines, newScoreCards);
                NewActivity(typeof(GamePlayersChoseActivity));
            }
        }

        public void GameSetupInstruction(List<string> players)
        {
            if (Game.ValidatePlayers(players))
            {
                Game.SetPlayers(players);
                Game.SetStartDateTime(DateTime.Now);
                NewActivity(typeof(GameSetupInstructionActivity));
            }
        }

        public void GameStart()
        {
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
                GameScoreSubmitScorePanels = scorePanels;
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

                Game.Score(GameScoreSubmitScorePanels);
                Game.SetNextRound();
                gameInProgressActivity.PrepareRound();
                if (Game.ScoreRound == ScoringRound.Finish)
                    Game.SetEndDateTime(DateTime.Now);
            }
        }

        public void ConfirmTheWiseManChose(BaseActivity activity, BuildingType buildingType)
        {
            Game.SetTheWiseManBuildingType(buildingType);
            TryScore(activity);
        }

        public void ConfirmMedinasNumber(BaseActivity activity, Dictionary<int, int> playersHighestPrices)
        {
            if (Game.ValidateMedinasNumbers(playersHighestPrices))
            {
                Game.SetMedinasNumbers(playersHighestPrices);
                TryScore(activity);
            }
        }
        public void ConfirmGranadaBuildingsNumbers(BaseActivity activity, Dictionary<GranadaBuildingType, Dictionary<int, int>> playersHighestPrices)
        {
            if (Game.ValidateGranadaBuildingsNumbers(playersHighestPrices))
            {
                Game.SetGranadaBuildingsNumbers(playersHighestPrices);
                TryScore(activity);
            }
        }

        public void SubmitScoreBeforeAssignLeftoverBuildings(GameScoreActivity activity, List<PlaceholderPlayerScoreBeforeAssignLeftoverFragment> scorePanels)
        {
            if (Game.ValidateScoreBeforeAssignLeftoverBuildings(scorePanels))
            {
                activity.Finish();

                Game.ScoreBeforeAssignLeftoverBuildings(scorePanels);
                Game.SetNextRound();
                gameInProgressActivity.PrepareRound();
            }
        }

        public void GameShowDetails(GameInProgressActivity gameInProgressActivity)
        {
            CurrentResult = Game.GetResultHistory();

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
            NewActivity(typeof(GameDetailsActivity));
        }

        public void ShowHistory(Context context)
        {
            if (!(context is ResultsHistoryActivity))
                NewActivity(typeof(ResultsHistoryActivity));
        }

        public void LoadResults()
        {
            Results = new List<ResultHistory>();

            string fileName = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "results.xml");

            if (File.Exists(fileName))
            {
                XmlDocument document = new XmlDocument();
                document.Load(fileName);
                XmlNode bodyElement = document.SingleChildNode("body");
                XmlNode resultsElement = bodyElement.SingleChildNode("results");
                string version = bodyElement.SingleChildNode("version").InnerText;
                foreach (XmlNode result in resultsElement.GetChildNodes("result"))
                {
                    ResultHistory resultHistory = new ResultHistory();

                    resultHistory.StartDateTime = DateTime.Parse(result.SingleChildNode("startTime").InnerText);
                    resultHistory.EndDateTime = DateTime.Parse(result.SingleChildNode("endTime").InnerText);
                    resultHistory.Modules = new List<ExpansionModule>();
                    resultHistory.NewScoreCards = new List<NewScoreCard>();
                    resultHistory.CaliphsGuidelines = new List<CaliphsGuidelinesMission>();
                    resultHistory.Players = new List<ResultPlayerHistory>();
                    //resultHistory.ScoreRound = result.SingleChildNode("scoreRound").InnerText.GetEnumByDescriptionValue<ScoringRound>());
                    resultHistory.ScoreRound = Enum.Parse<ScoringRound>(result.SingleChildNode("scoreRound").InnerText);
                    XmlNode modulesElement = result.SingleChildNode("modules");
                    XmlNode newScoreCardsElement = result.SingleChildNode("newScoreCards");
                    XmlNode caliphsGuidelinesElement = result.SingleChildNode("caliphsGuidelines");
                    foreach (XmlNode module in modulesElement.GetChildNodes("module"))
                    {
                        //resultHistory.Modules.Add(module.SingleChildNode("value").InnerText.GetEnumByDescriptionValue<ExpansionModule>());
                        resultHistory.Modules.Add(Enum.Parse<ExpansionModule>(module.SingleChildNode("value").InnerText));
                    }
                    resultHistory.GranadaOption = Enum.Parse<GranadaOption>(result.SingleChildNode("granadaOption").InnerText);
                    foreach (XmlNode module in newScoreCardsElement.GetChildNodes("newScoreCard"))
                    {
                        resultHistory.NewScoreCards.Add(Enum.Parse<NewScoreCard>(module.SingleChildNode("value").InnerText));
                    }
                    foreach (XmlNode module in caliphsGuidelinesElement.GetChildNodes("caliphsGuideline"))
                    {
                        resultHistory.CaliphsGuidelines.Add(Enum.Parse<CaliphsGuidelinesMission>(module.SingleChildNode("value").InnerText));
                    }
                    XmlNode playersElement = result.SingleChildNode("players");
                    foreach (XmlNode player in playersElement.GetChildNodes("player"))
                    {
                        ResultPlayerHistory resultPlayerHistory = new ResultPlayerHistory();
                        string playerName = player.SingleChildNode("name").InnerText;

                        ScoreDetails GetScoreDetails(string name)
                        {
                            ScoreDetails result = new ScoreDetails();
                            XmlNode scoreDetailsNode = player.SingleChildNode(name);
                            result.ImmediatelyPoints = Int32.Parse(scoreDetailsNode.SingleChildNode("ImmediatelyPoints").InnerText);
                            result.WallLength = Int32.Parse(scoreDetailsNode.SingleChildNode("WallLength").InnerText);
                            result.Pavilion = Int32.Parse(scoreDetailsNode.SingleChildNode("Pavilion").InnerText);
                            result.Seraglio = Int32.Parse(scoreDetailsNode.SingleChildNode("Seraglio").InnerText);
                            result.Arcades = Int32.Parse(scoreDetailsNode.SingleChildNode("Arcades").InnerText);
                            result.Chambers = Int32.Parse(scoreDetailsNode.SingleChildNode("Chambers").InnerText);
                            result.Garden = Int32.Parse(scoreDetailsNode.SingleChildNode("Garden").InnerText);
                            result.Tower = Int32.Parse(scoreDetailsNode.SingleChildNode("Tower").InnerText);
                            result.BuildingsBonuses = Int32.Parse(scoreDetailsNode.SingleChildNode("BuildingsBonuses").InnerText);
                            result.TheCityWatch = Int32.Parse(scoreDetailsNode.SingleChildNode("TheCityWatch").InnerText);
                            result.Camps = Int32.Parse(scoreDetailsNode.SingleChildNode("Camps").InnerText);
                            result.StreetTraders = Int32.Parse(scoreDetailsNode.SingleChildNode("StreetTraders").InnerText);
                            result.TreasureChamber = Int32.Parse(scoreDetailsNode.SingleChildNode("TreasureChamber").InnerText);
                            result.Invaders = Int32.Parse(scoreDetailsNode.SingleChildNode("Invaders").InnerText);
                            result.Bazaars = Int32.Parse(scoreDetailsNode.SingleChildNode("Bazaars").InnerText);
                            result.ArtOfTheMoors = Int32.Parse(scoreDetailsNode.SingleChildNode("ArtOfTheMoors").InnerText);
                            result.Falconers = Int32.Parse(scoreDetailsNode.SingleChildNode("Falconers").InnerText);
                            result.Watchtowers = Int32.Parse(scoreDetailsNode.SingleChildNode("Watchtowers").InnerText);
                            result.Medina = Int32.Parse(scoreDetailsNode.SingleChildNode("Medina").InnerText);
                            result.BuildingsWithoutServantTile = Int32.Parse(scoreDetailsNode.SingleChildNode("BuildingsWithoutServantTile").InnerText);
                            result.Orchards = Int32.Parse(scoreDetailsNode.SingleChildNode("Orchards").InnerText);
                            result.Bathhouses = Int32.Parse(scoreDetailsNode.SingleChildNode("Bathhouses").InnerText);
                            result.WishingWells = Int32.Parse(scoreDetailsNode.SingleChildNode("WishingWells").InnerText);
                            result.CompletedProjects = Int32.Parse(scoreDetailsNode.SingleChildNode("CompletedProjects").InnerText);
                            result.Animals = Int32.Parse(scoreDetailsNode.SingleChildNode("Animals").InnerText);
                            result.BlackDices = Int32.Parse(scoreDetailsNode.SingleChildNode("BlackDices").InnerText);
                            result.Handymen = Int32.Parse(scoreDetailsNode.SingleChildNode("Handymen").InnerText);
                            result.Treasures = Int32.Parse(scoreDetailsNode.SingleChildNode("Treasures").InnerText);
                            result.Mission1 = Int32.Parse(scoreDetailsNode.SingleChildNode("Mission1").InnerText);
                            result.Mission2 = Int32.Parse(scoreDetailsNode.SingleChildNode("Mission2").InnerText);
                            result.Mission3 = Int32.Parse(scoreDetailsNode.SingleChildNode("Mission3").InnerText);
                            result.Mission4 = Int32.Parse(scoreDetailsNode.SingleChildNode("Mission4").InnerText);
                            result.Mission5 = Int32.Parse(scoreDetailsNode.SingleChildNode("Mission5").InnerText);
                            result.Mission6 = Int32.Parse(scoreDetailsNode.SingleChildNode("Mission6").InnerText);
                            result.Mission7 = Int32.Parse(scoreDetailsNode.SingleChildNode("Mission7").InnerText);
                            result.Mission8 = Int32.Parse(scoreDetailsNode.SingleChildNode("Mission8").InnerText);
                            result.Mission9 = Int32.Parse(scoreDetailsNode.SingleChildNode("Mission9").InnerText);
                            result.MoatLength = Int32.Parse(scoreDetailsNode.SingleChildNode("MoatLength").InnerText);
                            result.Arena = Int32.Parse(scoreDetailsNode.SingleChildNode("Arena").InnerText);
                            result.BathHouse = Int32.Parse(scoreDetailsNode.SingleChildNode("BathHouse").InnerText);
                            result.Library = Int32.Parse(scoreDetailsNode.SingleChildNode("Library").InnerText);
                            result.Hostel = Int32.Parse(scoreDetailsNode.SingleChildNode("Hostel").InnerText);
                            result.Hospital = Int32.Parse(scoreDetailsNode.SingleChildNode("Hospital").InnerText);
                            result.Market = Int32.Parse(scoreDetailsNode.SingleChildNode("Market").InnerText);
                            result.Park = Int32.Parse(scoreDetailsNode.SingleChildNode("Park").InnerText);
                            result.School = Int32.Parse(scoreDetailsNode.SingleChildNode("School").InnerText);
                            result.ResidentialArea = Int32.Parse(scoreDetailsNode.SingleChildNode("ResidentialArea").InnerText);
                            result.WallMoatCombination = Int32.Parse(scoreDetailsNode.SingleChildNode("WallMoatCombination").InnerText);
                            return result;
                        }

                        resultPlayerHistory.ScoreDetails1 = GetScoreDetails("scoreDetails1");
                        resultPlayerHistory.ScoreDetails2 = GetScoreDetails("scoreDetails2");
                        resultPlayerHistory.ScoreDetails3 = GetScoreDetails("scoreDetails3");
                        resultPlayerHistory.ScoreMeantime = GetScoreDetails("scoreMeantime");

                        resultHistory.Players.Add(resultPlayerHistory);
                    }

                    Results.Add(resultHistory);
                }
            }
        }

        public void SaveResults()
        {
            string fileName = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "results.xml");

            XmlDocument document = new XmlDocument();
            XmlDeclaration xmlDeclaration = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            document.InsertBefore(xmlDeclaration, document.DocumentElement);
            XmlElement mainElement = document.CreateElement(String.Empty, "body", String.Empty);
            document.AppendChild(mainElement);
            XmlOperations.AddTextChild(document, mainElement, "version", Context.PackageManager.GetPackageInfo(Context.PackageName, 0).VersionName);
            XmlElement resultsElement = document.CreateElement(String.Empty, "results", String.Empty);
            mainElement.AppendChild(resultsElement);

            foreach (ResultHistory resultHistory in Results)
            {
                XmlElement resultElement = document.CreateElement(String.Empty, "result", String.Empty);
                resultsElement.AppendChild(resultElement);
                XmlOperations.AddTextChild(document, resultElement, "startTime", resultHistory.StartDateTime.ToString());
                XmlOperations.AddTextChild(document, resultElement, "endTime", resultHistory.EndDateTime.ToString());
                XmlElement modulesElement = document.CreateElement(String.Empty, "modules", String.Empty);
                XmlElement caliphsGuidelinesElement = document.CreateElement(String.Empty, "caliphsGuidelines", String.Empty);
                XmlElement newScoreCardsElement = document.CreateElement(String.Empty, "newScoreCards", String.Empty);
                resultElement.AppendChild(modulesElement);
                XmlOperations.AddTextChild(document, resultElement, "granadaOption", resultHistory.GranadaOption.ToString());
                resultElement.AppendChild(caliphsGuidelinesElement);
                resultElement.AppendChild(newScoreCardsElement);
                foreach (ExpansionModule module in resultHistory.Modules)
                {
                    XmlElement moduleElement = document.CreateElement(String.Empty, "module", String.Empty);
                    //XmlOperations.AddTextChild(document, moduleElement, "value", module.GetEnumDescription());
                    XmlOperations.AddTextChild(document, moduleElement, "value", module.ToString());
                    modulesElement.AppendChild(moduleElement);
                }
                if (resultHistory.CaliphsGuidelines != null)
                    foreach (CaliphsGuidelinesMission mission in resultHistory.CaliphsGuidelines)
                    {
                        XmlElement moduleElement = document.CreateElement(String.Empty, "caliphsGuideline", String.Empty);
                        XmlOperations.AddTextChild(document, moduleElement, "value", mission.ToString());
                        caliphsGuidelinesElement.AppendChild(moduleElement);
                    }
                if (resultHistory.NewScoreCards != null)
                    foreach (NewScoreCard newScoreCard in resultHistory.NewScoreCards)
                    {
                        XmlElement moduleElement = document.CreateElement(String.Empty, "newScoreCard", String.Empty);
                        XmlOperations.AddTextChild(document, moduleElement, "value", newScoreCard.ToString());
                        newScoreCardsElement.AppendChild(moduleElement);
                    }
                //XmlOperations.AddTextChild(document, resultElement, "scoreRound", resultHistory.ScoreRound.GetEnumDescription());
                XmlOperations.AddTextChild(document, resultElement, "scoreRound", resultHistory.ScoreRound.ToString());
                XmlElement playersElement = document.CreateElement(String.Empty, "players", String.Empty);
                resultElement.AppendChild(playersElement);
                foreach (ResultPlayerHistory player in resultHistory.Players)
                {
                    XmlElement playerElement = document.CreateElement(String.Empty, "player", String.Empty);
                    playersElement.AppendChild(playerElement);
                    XmlOperations.AddTextChild(document, playerElement, "name", player.Name);

                    void AppendScoreDetails(ScoreDetails scoreDetails, string name)
                    {
                        XmlElement playerScoreDetails1Element = document.CreateElement(String.Empty, name, String.Empty);
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "ImmediatelyPoints", scoreDetails.ImmediatelyPoints.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "WallLength", scoreDetails.WallLength.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Pavilion", scoreDetails.Pavilion.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Seraglio", scoreDetails.Seraglio.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Arcades", scoreDetails.Arcades.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Chambers", scoreDetails.Chambers.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Garden", scoreDetails.Garden.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Tower", scoreDetails.Tower.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "BuildingsBonuses", scoreDetails.BuildingsBonuses.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "TheCityWatch", scoreDetails.TheCityWatch.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Camps", scoreDetails.Camps.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "StreetTraders", scoreDetails.StreetTraders.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "TreasureChamber", scoreDetails.TreasureChamber.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Invaders", scoreDetails.Invaders.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Bazaars", scoreDetails.Bazaars.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "ArtOfTheMoors", scoreDetails.ArtOfTheMoors.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Falconers", scoreDetails.Falconers.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Watchtowers", scoreDetails.Watchtowers.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Medina", scoreDetails.Medina.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "BuildingsWithoutServantTile", scoreDetails.BuildingsWithoutServantTile.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Orchards", scoreDetails.Orchards.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Bathhouses", scoreDetails.Bathhouses.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "WishingWells", scoreDetails.WishingWells.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "CompletedProjects", scoreDetails.CompletedProjects.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Animals", scoreDetails.Animals.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "BlackDices", scoreDetails.BlackDices.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Handymen", scoreDetails.Handymen.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Treasures", scoreDetails.Treasures.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Mission1", scoreDetails.Mission1.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Mission2", scoreDetails.Mission2.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Mission3", scoreDetails.Mission3.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Mission4", scoreDetails.Mission4.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Mission5", scoreDetails.Mission5.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Mission6", scoreDetails.Mission6.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Mission7", scoreDetails.Mission7.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Mission8", scoreDetails.Mission8.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Mission9", scoreDetails.Mission9.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "MoatLength", scoreDetails.MoatLength.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Arena", scoreDetails.Arena.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "BathHouse", scoreDetails.BathHouse.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Library", scoreDetails.Library.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Hostel", scoreDetails.Hostel.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Hospital", scoreDetails.Hospital.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Market", scoreDetails.Market.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "Park", scoreDetails.Park.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "School", scoreDetails.School.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "ResidentialArea", scoreDetails.ResidentialArea.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "WallMoatCombination", scoreDetails.WallMoatCombination.ToString());
                        playerElement.AppendChild(playerScoreDetails1Element);
                    }

                    AppendScoreDetails(player.ScoreDetails1, "scoreDetails1");
                    AppendScoreDetails(player.ScoreDetails2, "scoreDetails2");
                    AppendScoreDetails(player.ScoreDetails3, "scoreDetails3");
                    AppendScoreDetails(player.ScoreMeantime, "scoreMeantime");
                }
            }

            document.Save(fileName);
        }

        public void RemoveResult(DateTime startDateTime)
        {
            Results.RemoveAll(r => r.StartDateTime == startDateTime);
        }
    }
}