using AlhambraScoringAndroid.Tools;
using AlhambraScoringAndroid.UI;
using Android.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace AlhambraScoringAndroid.GamePlay
{
    public class Game
    {
        public static Dictionary<BuildingType, List<int>[]> Scoring = new Dictionary<BuildingType, List<int>[]>()
        {
            [BuildingType.Pavilion] = new List<int>[] { new List<int> { 1 }, new List<int> { 8, 1 }, new List<int> { 16, 8, 1 } },
            [BuildingType.Seraglio] = new List<int>[] { new List<int> { 2 }, new List<int> { 9, 2 }, new List<int> { 17, 9, 2 } },
            [BuildingType.Arcades] = new List<int>[] { new List<int> { 3 }, new List<int> { 10, 3 }, new List<int> { 18, 10, 3 } },
            [BuildingType.Chambers] = new List<int>[] { new List<int> { 4 }, new List<int> { 11, 4 }, new List<int> { 19, 11, 4 } },
            [BuildingType.Garden] = new List<int>[] { new List<int> { 5 }, new List<int> { 12, 5 }, new List<int> { 20, 12, 5 } },
            [BuildingType.Tower] = new List<int>[] { new List<int> { 6 }, new List<int> { 13, 6 }, new List<int> { 21, 13, 6 } },
        };
        public static Dictionary<BuildingType, ScoreType> BuildingBaseScoreType = new Dictionary<BuildingType, ScoreType>()
        {
            [BuildingType.Pavilion] = ScoreType.PavilionNumber,
            [BuildingType.Seraglio] = ScoreType.SeraglioNumber,
            [BuildingType.Arcades] = ScoreType.ArcadesNumber,
            [BuildingType.Chambers] = ScoreType.ChambersNumber,
            [BuildingType.Garden] = ScoreType.GardenNumber,
            [BuildingType.Tower] = ScoreType.TowerNumber
        };
        public static Dictionary<BuildingType, int> BonusCardsMaxCount = new Dictionary<BuildingType, int>()
        {
            [BuildingType.Pavilion] = 1,
            [BuildingType.Seraglio] = 1,
            [BuildingType.Arcades] = 2,
            [BuildingType.Chambers] = 2,
            [BuildingType.Garden] = 3,
            [BuildingType.Tower] = 3
        };

        private readonly Context Context;

        public DateTime StartDateTime { get; private set; }
        public DateTime? EndDateTime { get; private set; }

        private List<ExpansionModule> Modules;
        private List<Player> Players;
        public ScoringRound ScoreRound { get; private set; }
        public Stack<ScoreHistory> ScoreStack { get; private set; }
        public bool Saved { get; set; }

        public Dictionary<BuildingType, int> BuildingsMaxCount =>
            new Dictionary<BuildingType, int>()
            {
                [BuildingType.Pavilion] = 7 + (HasModule(ExpansionModule.DesignerNewBuildingGrounds) ? 2 : 0) + (HasModule(ExpansionModule.DesignerMajorConstructionProjects) ? 5 : 0),
                [BuildingType.Seraglio] = 7 + (HasModule(ExpansionModule.DesignerNewBuildingGrounds) ? 2 : 0) + (HasModule(ExpansionModule.DesignerMajorConstructionProjects) ? 5 : 0),
                [BuildingType.Arcades] = 9 + (HasModule(ExpansionModule.DesignerNewBuildingGrounds) ? 2 : 0) + (HasModule(ExpansionModule.DesignerMajorConstructionProjects) ? 5 : 0),
                [BuildingType.Chambers] = 9 + (HasModule(ExpansionModule.DesignerNewBuildingGrounds) ? 2 : 0) + (HasModule(ExpansionModule.DesignerMajorConstructionProjects) ? 5 : 0),
                [BuildingType.Garden] = 11 + (HasModule(ExpansionModule.DesignerNewBuildingGrounds) ? 2 : 0) + (HasModule(ExpansionModule.DesignerMajorConstructionProjects) ? 5 : 0),
                [BuildingType.Tower] = 11 + (HasModule(ExpansionModule.DesignerNewBuildingGrounds) ? 2 : 0) + (HasModule(ExpansionModule.DesignerMajorConstructionProjects) ? 5 : 0),
            };

        public int AllBuildingsCount => BuildingsMaxCount.Sum(b => b.Value);

        public int AllTilesCount
        {
            get
            {
                int allTilesCount = 1;
                allTilesCount += AllBuildingsCount;
                if (HasModule(ExpansionModule.DesignerBathhouses))
                    allTilesCount += 6;
                if (HasModule(ExpansionModule.DesignerWishingWell))
                    allTilesCount += 6;
                if (HasModule(ExpansionModule.DesignerGatesWithoutEnd))
                    allTilesCount += 6;
                return allTilesCount;
            }
        }

        public int RoundNumber
        {
            get
            {
                switch (ScoreRound)
                {
                    case ScoringRound.First:
                        return 1;
                    case ScoringRound.Second:
                        return 2;
                    case ScoringRound.ThirdBeforeLeftover:
                        return 3;
                    case ScoringRound.Third:
                        return 3;
                }
                return 0;
            }
        }

        public int PlayersCount => Players.Count;

        public bool GameInProgress => (ScoreRound != ScoringRound.First || (Players != null && Players.Sum(p => p.Score) != 0)) && !Saved;

        public Game(Context context)
        {
            Context = context;
        }

        public void SetModules(IEnumerable<ExpansionModule> modules)
        {
            Modules = modules.ToList();
        }

        public bool HasModule(ExpansionModule module)
        {
            return Modules.Contains(module);
        }

        public void SetPlayers(List<string> playersNames)
        {
            Players = new List<Player>();

            bool twoPlayers = playersNames.Count == 2;

            AddPlayer(playersNames[0], twoPlayers);
            AddPlayer(playersNames[1], twoPlayers);
            if (playersNames.Count > 2)
            {
                AddPlayer(playersNames[2], twoPlayers);
                if (playersNames.Count > 3)
                {
                    AddPlayer(playersNames[3], twoPlayers);
                    if (playersNames.Count > 4)
                    {
                        AddPlayer(playersNames[4], twoPlayers);
                        if (playersNames.Count > 5)
                        {
                            AddPlayer(playersNames[5], twoPlayers);
                        }
                    }
                }
            }
            else
                Players.Add(Player.CreateDirk(this));

            ScoreRound = ScoringRound.First;
            ScoreStack = new Stack<ScoreHistory>();
            Saved = false;
        }

        public void SetStartDateTime(DateTime dateTime)
        {
            StartDateTime = dateTime;
        }

        public void SetEndDateTime(DateTime? dateTime)
        {
            EndDateTime = dateTime;
        }

        private void AddPlayer(string name, bool twoPlayers)
        {
            if (String.IsNullOrEmpty(name))
                throw new NameValidationException("Empty name");
            if (name.Equals(Player.DirkName, StringComparison.OrdinalIgnoreCase) && twoPlayers)
                throw new NameValidationException("Dirk is reserved name");
            if (Players.Any(p => name.Equals(p.Name, StringComparison.OrdinalIgnoreCase)))
                throw new NameValidationException("Duplicates names");
            Players.Add(new Player(this, name));
        }

        public Player GetPlayer(int playerNumber)
        {
            return Players[playerNumber - 1];
        }

        public void PlayerAddScore(int playerNumber, int score)
        {
            GetPlayer(playerNumber).AddScore(score, ScoreType.Immediately);
            ScoreStack.Push(new ScoreHistoryImmediately(this, playerNumber, score));
        }

        public void PlayerRevertScore(int playerNumber, int score)
        {
            GetPlayer(playerNumber).RevertAddScore(score, ScoreType.Immediately);
        }

        public bool ValidateScore(List<PlaceholderPlayerScoreFragment> scorePanels)
        {
            //TODO walidacja danych na podstawie wcześniejszych submitów

            foreach (KeyValuePair<BuildingType, int> mapEntry in BuildingsMaxCount)
            {
                int playersBuildings = scorePanels.Sum(p => p.BuildingsCount[mapEntry.Key]);

                if (playersBuildings > mapEntry.Value)
                    return ValidateUtils.CheckFailed(Context, $"Przekroczona łączna maksymalna ilość budynków {mapEntry.Key}");
            }

            int playerBonusCardsMax = 1;
            if (PlayersCount < 6)
                playerBonusCardsMax++;
            if (PlayersCount < 4)
                playerBonusCardsMax++;

            for (int i = 0; i < PlayersCount; i++)
            {
                if (scorePanels[i].BonusCardsBuildingsCount.Sum(c => c.Value) > playerBonusCardsMax)
                    return ValidateUtils.CheckFailed(Context, $"{GetPlayer(i + 1).Name}: Niedozwolona ilość kart bonusowych");
            }
            foreach (KeyValuePair<BuildingType, int> mapEntry in BonusCardsMaxCount)
            {
                if (scorePanels.Sum(p => p.BonusCardsBuildingsCount[mapEntry.Key]) > mapEntry.Value)
                    return ValidateUtils.CheckFailed(Context, $"Przekroczona łączna maksymalna ilość kart bonusowych {mapEntry.Key}");
            }

            for (int i = 0; i < PlayersCount; i++)
                if (scorePanels[i].BuildingsWithoutServantTile > scorePanels[i].AllBuildingsCount)
                    return ValidateUtils.CheckFailed(Context, $"{GetPlayer(i + 1).Name}: Liczba budynków bez żetonu sługi przekracza liczbę wszystkich budynków");

            int faceDownFruitsSum = scorePanels.Sum(p => p.FaceDownFruitsCount);
            int allFruits = 0;
            for (int i = 0; i < PlayersCount; i++)
            {
                if (scorePanels[i].CompletedGroupOfFruitBoard1)
                    allFruits += 1;
                if (scorePanels[i].CompletedGroupOfFruitBoard2)
                    allFruits += 2;
                if (scorePanels[i].CompletedGroupOfFruitBoard3)
                    allFruits += 4;
                if (scorePanels[i].CompletedGroupOfFruitBoard4)
                    allFruits += 7;
                if (scorePanels[i].CompletedGroupOfFruitBoard5)
                    allFruits += 11;
                if (scorePanels[i].CompletedGroupOfFruitBoard6)
                    allFruits += 16;
                allFruits += scorePanels[i].FaceDownFruitsCount;
            }
            if (faceDownFruitsSum > 35)
                return ValidateUtils.CheckFailed(Context, $"Przekroczona łączna maksymalna ilość pojedynczych owoców");
            if (allFruits > 56)
                return ValidateUtils.CheckFailed(Context, $"Przekroczona łączna maksymalna ilość owoców");

            List<int> wishingWellsAvailablePoints = new List<int>() { 3, 3, 4, 4, 5, 5 }.GetCombinationsSums();

            int wishingWellsPointsSum = scorePanels.Sum(p => p.WishingWellsPoints);
            for (int i = 0; i < PlayersCount; i++)
            {
                if (!wishingWellsAvailablePoints.Contains(scorePanels[i].WishingWellsPoints))
                    return ValidateUtils.CheckFailed(Context, $"{GetPlayer(i + 1).Name}: Niedozwolona ilość punktów z fontann");
            }
            if (wishingWellsPointsSum > 24)
                return ValidateUtils.CheckFailed(Context, $"Przekroczona łączna maksymalna ilość punktów z fontann");

            int animalsPointsSum = scorePanels.Sum(p => p.AnimalsPoints);
            for (int i = 0; i < PlayersCount; i++)
            {
                if (scorePanels[i].AnimalsPoints > scorePanels[i].BuildingsCount[BuildingType.Garden] * 3)
                    return ValidateUtils.CheckFailed(Context, $"{GetPlayer(i + 1).Name}: Niedozwolona ilość zwierząt");
            }
            if (animalsPointsSum > 24)
                return ValidateUtils.CheckFailed(Context, $"Przekroczona łączna maksymalna ilość zwierząt");

            foreach (KeyValuePair<BuildingType, int> mapEntry in BuildingsMaxCount)
            {
                if (scorePanels.Count(p => p.OwnedSemiBuildings[mapEntry.Key]) > 1)
                    return ValidateUtils.CheckFailed(Context, $"Dwóch graczy z tą samą połową budynku {mapEntry.Key}");
            }

            int blackDiceTotalPipsSum = 0;
            int blackDicesMinimumNumber = 0;
            for (int i = 0; i < PlayersCount; i++)
            {
                int blackDiceTotalPips = scorePanels[i].BlackDiceTotalPips;
                if (blackDiceTotalPips > 0)
                    blackDicesMinimumNumber++;
                if (blackDiceTotalPips > 6)
                    blackDicesMinimumNumber++;
                if (blackDiceTotalPips > 12)
                    blackDicesMinimumNumber++;
                blackDiceTotalPipsSum += blackDiceTotalPips;
            }
            if (blackDiceTotalPipsSum > 18)
                return ValidateUtils.CheckFailed(Context, $"Przekroczona łączna maksymalna liczba oczek na czarnych kostkach");
            if (blackDicesMinimumNumber > 3)
                return ValidateUtils.CheckFailed(Context, $"Przekroczona ilość użytych czarnych kostek");

            foreach (KeyValuePair<BuildingType, int> mapEntry in BuildingsMaxCount)
            {
                for (int i = 0; i < PlayersCount; i++)
                {
                    if (scorePanels[i].ExtensionsBuildingsCount[mapEntry.Key] > scorePanels[i].BuildingsCount[mapEntry.Key])
                        return ValidateUtils.CheckFailed(Context, $"{GetPlayer(i + 1).Name}: Liczba rozszerzeń przekracza liczbę wszystkich budynków {mapEntry.Key}");
                }
                if (scorePanels.Sum(p => p.ExtensionsBuildingsCount[mapEntry.Key]) > 2)
                    return ValidateUtils.CheckFailed(Context, $"Przekroczona łączna maksymalna ilość rozszerzeń {mapEntry.Key}");
            }

            int handymenTilesHighestNumberSum = scorePanels.Sum(p => p.HandymenTilesHighestNumber);
            if (handymenTilesHighestNumberSum > 48)
                return ValidateUtils.CheckFailed(Context, $"Przekroczona łączna maksymalna ilość złotych rączek");

            List<int> treasuresAvailableValues = new List<int>() { 1, 2, 3, 4, 5, 6 }.GetCombinationsSums();

            for (int i = 0; i < PlayersCount; i++)
            {
                if (!treasuresAvailableValues.Contains(scorePanels[i].TreasuresValue))
                    return ValidateUtils.CheckFailed(Context, $"{GetPlayer(i + 1).Name}: Niedozwolona ilość punktów ze skarbów");
            }

            //TODO caliphs validation: 3 and the same

            for (int i = 0; i < PlayersCount; i++)
            {
                int wallLength = scorePanels[i].WallLength;
                int secondLongestWallLength = scorePanels[i].SecondLongestWallLength;
                if (wallLength < secondLongestWallLength)
                    return ValidateUtils.CheckFailed(Context, $"{GetPlayer(i + 1).Name}: Drugi co do wielkości mur nie może być dłuższy niż najdłuższy mur");

                int maximumTilesCount = AllTilesCount;
                maximumTilesCount -= scorePanels.Where(p => p.PlayerNumber != i + 1).Sum(p => p.AllBuildingsCount);
                if (wallLength + secondLongestWallLength > maximumTilesCount - (secondLongestWallLength != 0 ? 2 : 0))
                    return ValidateUtils.CheckFailed(Context, $"{GetPlayer(i + 1).Name}: Suma najdłuższego muru i drugiego co do wielkości muru przekracza maksymalną możliwą");
            }

            return true;
        }

        private double GetBuildingCount(PlaceholderPlayerScoreFragment scorePanel, BuildingType buildingType, bool withBonuses = true)
        {
            double alhambraCount = scorePanel.BuildingsCount[buildingType];
            if (withBonuses)
            {
                //Extensions: extended buildings
                if (HasModule(ExpansionModule.DesignerExtensions))
                    alhambraCount += scorePanel.ExtensionsBuildingsCount[buildingType];
                //Bonus Cards: extra buildings
                if (HasModule(ExpansionModule.ExpansionBonusCards))
                    alhambraCount += scorePanel.BonusCardsBuildingsCount[buildingType];
                if (alhambraCount != 0 && HasModule(ExpansionModule.DesignerGatesWithoutEnd)
                        && scorePanel.OwnedSemiBuildings[buildingType])
                    alhambraCount += 0.5;
            }

            return alhambraCount;
        }

        private int GetBuildingScore(List<PlaceholderPlayerScoreFragment> scorePanels, BuildingType buildingType, int i, bool withBonuses = true)
        {
            double alhambraCount = GetBuildingCount(scorePanels[i], buildingType, withBonuses);
            if (alhambraCount != 0)
            {
                int currentPlace = 1;
                int sharePlaceCount = 0;
                for (int j = 0; j < PlayersCount; j++)
                    if (j != i)
                    {
                        double otherPlayerAlhambraBuildingsCount = GetBuildingCount(scorePanels[j], buildingType, withBonuses);

                        if (otherPlayerAlhambraBuildingsCount > alhambraCount)
                            currentPlace++;
                        else if (otherPlayerAlhambraBuildingsCount == alhambraCount)
                            sharePlaceCount++;
                    }

                if (currentPlace <= RoundNumber)
                {
                    return (Scoring[buildingType][RoundNumber - 1][currentPlace - 1]
                        + (RoundNumber > 1 && sharePlaceCount >= 1 && currentPlace <= RoundNumber - 1 ? Scoring[buildingType][RoundNumber - 1][currentPlace] : 0)
                        + (RoundNumber > 2 && sharePlaceCount >= 2 && currentPlace <= RoundNumber - 2 ? Scoring[buildingType][RoundNumber - 1][currentPlace + 1] : 0)
                        ) / (sharePlaceCount + 1);
                }
            }

            return 0;
        }

        public void Score(List<PlaceholderPlayerScoreFragment> scorePanels)
        {
            List<(ScoreDetails scoreDetails1, ScoreDetails scoreDetails2, ScoreDetails scoreDetails3, ScoreDetails scoreMeantime)> initialScoring = Players.Select(p => (p.ScoreDetails1.Copy(), p.ScoreDetails2.Copy(), p.ScoreDetails3.Copy(), p.ScoreMeantime.Copy())).ToList();

            //wall
            for (int i = 0; i < PlayersCount; i++)
                if (!Players[i].Dirk)
                    Players[i].AddScore(scorePanels[i].WallLength, ScoreType.WallLength);

            //each kind of building
            foreach (KeyValuePair<BuildingType, List<int>[]> scoring in Game.Scoring)
            {
                for (int i = 0; i < PlayersCount; i++)
                {
                    int buildingScore = GetBuildingScore(scorePanels, scoring.Key, i);
                    int buildingScoreBonus = buildingScore - GetBuildingScore(scorePanels, scoring.Key, i, false);
                    if (buildingScoreBonus < 0)
                        buildingScoreBonus = 0;

                    Players[i].AddScore(buildingScore, BuildingBaseScoreType[scoring.Key]);
                    Players[i].AddScore(buildingScoreBonus, ScoreType.BuildingsBonuses);
                }
            }

            if (HasModule(ExpansionModule.DesignerPalaceStaff))
            {
                if (RoundNumber != 3)
                {
                    //Palace Staff: each building without a servant tile
                    for (int i = 0; i < PlayersCount; i++)
                        if (!Players[i].Dirk)
                            Players[i].RemoveScore(scorePanels[i].BuildingsWithoutServantTile, ScoreType.BuildingsWithoutServantTile);
                }
            }

            if (HasModule(ExpansionModule.DesignerOrchards))
            {
                if (RoundNumber == 3)
                {
                    //Orchards: fruits
                    for (int i = 0; i < PlayersCount; i++)
                        if (!Players[i].Dirk)
                        {
                            if (scorePanels[i].CompletedGroupOfFruitBoard1)
                                Players[i].AddScore(1, ScoreType.Orchards);
                            if (scorePanels[i].CompletedGroupOfFruitBoard2)
                                Players[i].AddScore(2, ScoreType.Orchards);
                            if (scorePanels[i].CompletedGroupOfFruitBoard3)
                                Players[i].AddScore(4, ScoreType.Orchards);
                            if (scorePanels[i].CompletedGroupOfFruitBoard4)
                                Players[i].AddScore(7, ScoreType.Orchards);
                            if (scorePanels[i].CompletedGroupOfFruitBoard5)
                                Players[i].AddScore(11, ScoreType.Orchards);
                            if (scorePanels[i].CompletedGroupOfFruitBoard6)
                                Players[i].AddScore(16, ScoreType.Orchards);
                            Players[i].AddScore(scorePanels[i].FaceDownFruitsCount, ScoreType.Orchards);
                        }
                }
            }

            if (HasModule(ExpansionModule.DesignerBathhouses))
            {
                //Bathhouses: distances of the first building
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                    {
                        Players[i].AddScore(scorePanels[i].BathhousesPoints, ScoreType.Bathhouses);
                    }
            }

            if (HasModule(ExpansionModule.DesignerWishingWell))
            {
                //Wishing Well: tiles in a straight line from the waterspout
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                    {
                        Players[i].AddScore(scorePanels[i].WishingWellsPoints, ScoreType.WishingWells);
                    }
            }

            if (HasModule(ExpansionModule.DesignerFreshColors))
            {
                //Fresh Colors: completed projects
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                    {
                        foreach (KeyValuePair<BuildingType, bool> completedProject in scorePanels[i].CompletedProjects)
                        {
                            if (completedProject.Value)
                                Players[i].AddScore(scorePanels[i].BuildingsCount[completedProject.Key] * 2, ScoreType.CompletedProjects);
                        }
                    }
            }

            if (HasModule(ExpansionModule.DesignerAlhambraZoo))
            {
                //Alhambra Zoo: animals points
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                    {
                        Players[i].AddScore(scorePanels[i].AnimalsPoints, ScoreType.Animals);
                    }
            }

            if (HasModule(ExpansionModule.DesignerBuildingsOfPower))
            {
                //Buildings of Power: Building of Strength
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                    {
                        Players[i].AddScore(Math.Min(scorePanels[i].SecondLongestWallLength, scorePanels[i].BlackDiceTotalPips), ScoreType.BlackDices);
                    }
            }

            //            if (hasModule(ExpansionModule.DesignerExtensions))
            //        {
            //            //Extensions: extended buildings
            //            for (int i = 0; i < getPlayersCount(); i++)
            ////                Players[i].AddScore(scorePanels[i].ExtensionsCount());
            //        }

            if (HasModule(ExpansionModule.DesignerHandymen))
            {
                //Handymen: highest number of adjacent tiles occupied by handymen
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                    {
                        Players[i].AddScore(scorePanels[i].HandymenTilesHighestNumber, ScoreType.Handymen);
                    }
            }

            if (HasModule(ExpansionModule.FanTreasures))
            {
                //Treasures: treasures' value
                if (RoundNumber == 3)
                {
                    for (int i = 0; i < PlayersCount; i++)
                        if (!Players[i].Dirk)
                        {
                            Players[i].AddScore(scorePanels[i].TreasuresValue, ScoreType.Treasures);
                        }
                }
            }

            if (HasModule(ExpansionModule.FanCaliphsGuidelines))
            {
                if (RoundNumber == 3)
                {
                    for (int i = 0; i < PlayersCount; i++)
                        if (!Players[i].Dirk)
                        {
                            //Caliph’s Guidelines: mission 1
                            Players[i].AddScore(scorePanels[i].Mission1Count * 3, ScoreType.Mission1);
                            //Caliph’s Guidelines: mission 2
                            Players[i].AddScore(scorePanels[i].Mission2Count * 3, ScoreType.Mission2);
                            //Caliph’s Guidelines: mission 3
                            Players[i].AddScore(scorePanels[i].Mission3Count * 3, ScoreType.Mission3);
                            //Caliph’s Guidelines: mission 4
                            if (scorePanels[i].Mission4Available)
                                Players[i].AddScore(scorePanels[i].SecondLongestWallLength, ScoreType.Mission4);
                            //Caliph’s Guidelines: mission 5
                            Players[i].AddScore(scorePanels[i].Mission5Count * 2, ScoreType.Mission5);
                            //Caliph’s Guidelines: mission 6
                            Players[i].AddScore(scorePanels[i].Mission6Count * 3, ScoreType.Mission6);
                            //Caliph’s Guidelines: mission 7
                            switch (scorePanels[i].Mission7Count)
                            {
                                case 2:
                                    Players[i].AddScore(1, ScoreType.Mission7);
                                    break;
                                case 3:
                                    Players[i].AddScore(3, ScoreType.Mission7);
                                    break;
                                case 4:
                                    Players[i].AddScore(6, ScoreType.Mission7);
                                    break;
                                case 5:
                                    Players[i].AddScore(10, ScoreType.Mission7);
                                    break;
                                case 6:
                                    Players[i].AddScore(15, ScoreType.Mission7);
                                    break;
                            }
                            //Caliph’s Guidelines: mission 8
                            Players[i].AddScore(scorePanels[i].Mission8Count, ScoreType.Mission8);
                            //Caliph’s Guidelines: mission 9
                            Players[i].AddScore(scorePanels[i].Mission9Count * 2, ScoreType.Mission9);
                        }
                }
            }

            ScoreStack.Push(new ScoreHistoryRound(this, ScoreRound, initialScoring));
        }

        public void RoundRevertScore(ScoringRound scoreRound, List<(ScoreDetails scoreDetails1, ScoreDetails scoreDetails2, ScoreDetails scoreDetails3, ScoreDetails scoreMeantime)> initialScoring)
        {
            ScoreRound = scoreRound;
            for (int i = 0; i < PlayersCount; i++)
            {
                Players[i].RestoreScore(initialScoring[i]);
            }
        }

        public bool ValidateScoreBeforeAssignLeftoverBuildings(List<PlaceholderPlayerScoreBeforeAssignLeftoverFragment> scorePanels)
        {
            //TODO validate with what will be given in 3rd round

            return true;
        }

        public void ScoreBeforeAssignLeftoverBuildings(List<PlaceholderPlayerScoreBeforeAssignLeftoverFragment> scorePanels)
        {
            if (HasModule(ExpansionModule.DesignerPalaceStaff))
            {
                //Palace Staff: each building without a servant tile
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                        Players[i].RemoveScore(scorePanels[i].BuildingsWithoutServantTile, ScoreType.BuildingsWithoutServantTile);
            }
        }

        public void RevertScoring()
        {
            ScoreStack.Pop().Revert();
            SetEndDateTime(null);
            Saved = false;
        }

        public void SetNextRound()
        {
            switch (ScoreRound)
            {
                case ScoringRound.First:
                    ScoreRound = ScoringRound.Second;
                    break;
                case ScoringRound.Second:
                    if (HasModule(ExpansionModule.DesignerPalaceStaff))
                        ScoreRound = ScoringRound.ThirdBeforeLeftover;
                    else
                        ScoreRound = ScoringRound.Third;
                    break;
                case ScoringRound.ThirdBeforeLeftover:
                    ScoreRound = ScoringRound.Third;
                    break;
                case ScoringRound.Third:
                    ScoreRound = ScoringRound.Finish;
                    break;
            }
        }

        public ResultHistory GetResultHistory()
        {
            return new ResultHistory()
            {
                StartDateTime = StartDateTime,
                EndDateTime = EndDateTime,
                Modules = Modules,
                ScoreRound = ScoreRound,
                Players = Players.Select(p => new ResultPlayerHistory() { Name = p.Name, ScoreDetails1 = p.ScoreDetails1, ScoreDetails2 = p.ScoreDetails2, ScoreDetails3 = p.ScoreDetails3, ScoreMeantime = p.ScoreMeantime }).ToList()
            };
        }

    }
}