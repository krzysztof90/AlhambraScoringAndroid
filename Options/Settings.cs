using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.Tools;
using Android.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using static AndroidBase.Options.SettingsManager;

namespace AlhambraScoringAndroid.Options
{
    public static class Settings
    {
        private static string settingsFileName = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "results.xml");

        public static List<ResultHistory> GetResults()
        {
            List<ResultHistory> results = new List<ResultHistory>();

            //var a = File.ReadAllText(settingsFileName);
            if (File.Exists(settingsFileName))
            {
                XmlDocument document = new XmlDocument();
                document.Load(settingsFileName);
                XmlNode bodyElement = document.SingleChildNode("body");
                XmlNode resultsElement = bodyElement.SingleChildNode("results");
                string version = bodyElement.SingleChildNode("version").InnerText;
                foreach (XmlNode result in resultsElement.GetChildNodes("result"))
                {
                    ResultHistory resultHistory = new ResultHistory();
                    Deserialize(resultHistory, result);
                    resultHistory.Modules = DeserializeListOfEnums<ExpansionModule>(result, "modules", "module", "value");
                    resultHistory.NewScoreCards = DeserializeListOfEnums<NewScoreCard>(result, "newScoreCards", "newScoreCard", "value");
                    resultHistory.CaliphsGuidelines = DeserializeListOfEnums<CaliphsGuidelinesMission>(result, "caliphsGuidelines", "caliphsGuideline", "value");

                    resultHistory.Players = new List<ResultPlayerHistory>();
                    XmlNode playersElement = result.SingleChildNode("players");
                    foreach (XmlNode player in playersElement.GetChildNodes("player"))
                    {
                        ResultPlayerHistory resultPlayerHistory = new ResultPlayerHistory();
                        Deserialize(resultPlayerHistory, player);

                        ScoreDetails GetScoreDetails(string name)
                        {
                            ScoreDetails result = new ScoreDetails();
                            XmlNode scoreDetailsNode = player.SingleChildNode(name);
                            Deserialize(result, scoreDetailsNode);
                            return result;
                        }

                        resultPlayerHistory.ScoreDetails1 = GetScoreDetails("scoreDetails1");
                        resultPlayerHistory.ScoreDetails2 = GetScoreDetails("scoreDetails2");
                        resultPlayerHistory.ScoreDetails3 = GetScoreDetails("scoreDetails3");
                        resultPlayerHistory.ScoreMeantime = GetScoreDetails("scoreMeantime");

                        resultHistory.Players.Add(resultPlayerHistory);
                    }

                    results.Add(resultHistory);
                }
            }

            return results;
        }

        public static void SaveResults(List<ResultHistory> results, Context context)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration xmlDeclaration = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            document.InsertBefore(xmlDeclaration, document.DocumentElement);
            XmlElement mainElement = document.CreateElement(String.Empty, "body", String.Empty);
            document.AppendChild(mainElement);
            XmlOperations.AddTextChild(document, mainElement, "version", context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName);
            XmlElement resultsElement = document.CreateElement(String.Empty, "results", String.Empty);
            mainElement.AppendChild(resultsElement);

            foreach (ResultHistory resultHistory in results)
            {
                XmlElement resultElement = document.CreateElement(String.Empty, "result", String.Empty);
                resultsElement.AppendChild(resultElement);
                Serialize(resultHistory, document, resultElement);
                SerializeListOfEnums(resultHistory.Modules, resultElement, document, "modules", "module", "value");
                SerializeListOfEnums(resultHistory.CaliphsGuidelines, resultElement, document, "caliphsGuidelines", "caliphsGuideline", "value");
                SerializeListOfEnums(resultHistory.NewScoreCards, resultElement, document, "newScoreCards", "newScoreCard", "value");

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
                        Serialize(scoreDetails, document, playerScoreDetails1Element);
                        playerElement.AppendChild(playerScoreDetails1Element);
                    }

                    AppendScoreDetails(player.ScoreDetails1, "scoreDetails1");
                    AppendScoreDetails(player.ScoreDetails2, "scoreDetails2");
                    AppendScoreDetails(player.ScoreDetails3, "scoreDetails3");
                    AppendScoreDetails(player.ScoreMeantime, "scoreMeantime");
                }
            }

            document.Save(settingsFileName);
        }
    }
}