using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.Tools;
using AlhambraScoringAndroid.UI;
using AlhambraScoringAndroid.UI.Activities;
using Android.App;
using Android.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace AlhambraScoringAndroid
{
    [Application]
    public class MyApplication : Application
    {
        //TODO podgląd wyników

        //TODO porządek w kolejności properties w layout
        //TODO restore state after application close
        //TODO another expansion modules
        //TODO labelki słownik
        //TODO iOS
        //https://docs.microsoft.com/pl-pl/xamarin/
        //TODO google play "I would appreciate any feedback", BGG
        //TODO niepotrzebne referencje, nuget, aktualizacja
        //TODO minSdkVersion
        //TODO przetestować wszędzie obracanie ekranu
        //TODO wysokości i szerokości do zmiennych; 60dp = menu height
        //TODO BP
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

        public List<ResultHistory> Results { get; private set; }
        public Game Game { get; private set; }
        private GameInProgressActivity gameInProgressActivity;
        public ResultHistory CurrentResult { get; private set; }

        private void NewActivity(Type activityType)
        {
            Intent intent = new Intent(ApplicationContext, activityType);
            intent.AddFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }

        public void NewGamePrompt(Context context)
        {
            if ( Game?.GameInProgress  ??false)
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
            Game.SetStartDateTime(DateTime.Now);
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
                if (Game.ScoreRound == ScoringRound.Finish)
                    Game.SetEndDateTime(DateTime.Now);
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

        public void GameShowDetails(GameInProgressActivity gameInProgressActivity)
        {
            CurrentResult = Game.GetResultHistory();

            if (Game.ScoreRound == ScoringRound.Finish && !Game.Saved)
            {
                if (Results[Results.Count - 1].StartDateTime == CurrentResult.StartDateTime)
                    Results.RemoveAt(Results.Count - 1);
                Results.Add(CurrentResult);
                SaveResults();
                Game.Saved = true;
                gameInProgressActivity.PrepareRound();
            }
            NewActivity(typeof(GameDetailsActivity));
        }

        public void LoadResults()
        {
            Results = new List<ResultHistory>();

            string fileName = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "results.xml");

            if (File.Exists(fileName))
            {
                XmlDocument document = new XmlDocument();
                document.Load(fileName);
                XmlNode resultsElement = document.SingleChildNode("body").SingleChildNode("results");
                foreach (XmlNode result in resultsElement.GetChildNodes("result"))
                {
                    ResultHistory resultHistory = new ResultHistory();

                    resultHistory.StartDateTime = DateTime.Parse(result.SingleChildNode("startTime").InnerText);
                    resultHistory.EndDateTime = DateTime.Parse(result.SingleChildNode("endTime").InnerText);
                    resultHistory.Modules = new List<ExpansionModule>();
                    resultHistory.Players = new List<ResultPlayerHistory>();
                    //resultHistory.ScoreRound = result.SingleChildNode("scoreRound").InnerText.GetEnumByDescriptionValue<ScoringRound>());
                    resultHistory.ScoreRound = Enum.Parse<ScoringRound>(result.SingleChildNode("scoreRound").InnerText);
                    XmlNode modulesElement = result.SingleChildNode("modules");
                    foreach (XmlNode module in modulesElement.GetChildNodes("module"))
                    {
                        //resultHistory.Modules.Add(module.SingleChildNode("value").InnerText.GetEnumByDescriptionValue<ExpansionModule>());
                        resultHistory.Modules.Add(Enum.Parse<ExpansionModule>(module.SingleChildNode("value").InnerText));
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
                            result.PavilionNumber = Int32.Parse(scoreDetailsNode.SingleChildNode("PavilionNumber").InnerText);
                            result.SeraglioNumber = Int32.Parse(scoreDetailsNode.SingleChildNode("SeraglioNumber").InnerText);
                            result.ArcadesNumber = Int32.Parse(scoreDetailsNode.SingleChildNode("ArcadesNumber").InnerText);
                            result.ChambersNumber = Int32.Parse(scoreDetailsNode.SingleChildNode("ChambersNumber").InnerText);
                            result.GardenNumber = Int32.Parse(scoreDetailsNode.SingleChildNode("GardenNumber").InnerText);
                            result.TowerNumber = Int32.Parse(scoreDetailsNode.SingleChildNode("TowerNumber").InnerText);
                            result.BuildingsBonuses = Int32.Parse(scoreDetailsNode.SingleChildNode("BuildingsBonuses").InnerText);
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
            XmlElement resultsElement = document.CreateElement(String.Empty, "results", String.Empty);
            mainElement.AppendChild(resultsElement);

            foreach (ResultHistory resultHistory in Results)
            {
                XmlElement resultElement = document.CreateElement(String.Empty, "result", String.Empty);
                resultsElement.AppendChild(resultElement);
                XmlOperations.AddTextChild(document, resultElement, "startTime", resultHistory.StartDateTime.ToString());
                XmlOperations.AddTextChild(document, resultElement, "endTime", resultHistory.EndDateTime.ToString());
                XmlElement modulesElement = document.CreateElement(String.Empty, "modules", String.Empty);
                resultElement.AppendChild(modulesElement);
                foreach (ExpansionModule module in resultHistory.Modules)
                {
                    XmlElement moduleElement = document.CreateElement(String.Empty, "module", String.Empty);
                    //XmlOperations.AddTextChild(document, moduleElement, "value", module.GetEnumDescription());
                    XmlOperations.AddTextChild(document, moduleElement, "value", module.ToString());
                    modulesElement.AppendChild(moduleElement);
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
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "PavilionNumber", scoreDetails.PavilionNumber.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "SeraglioNumber", scoreDetails.SeraglioNumber.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "ArcadesNumber", scoreDetails.ArcadesNumber.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "ChambersNumber", scoreDetails.ChambersNumber.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "GardenNumber", scoreDetails.GardenNumber.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "TowerNumber", scoreDetails.TowerNumber.ToString());
                        XmlOperations.AddTextChild(document, playerScoreDetails1Element, "BuildingsBonuses", scoreDetails.BuildingsBonuses.ToString());
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
    }
}