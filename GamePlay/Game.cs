using AlhambraScoringAndroid.Tools;
using AlhambraScoringAndroid.UI;
using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;

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

        private readonly Context Context;

        private List<ExpansionModule> Modules;
        private List<Player> Players;
        public ScoringRound ScoreRound { get; private set; }

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

        public int PlayersCount => Players.Count();

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

            AddPlayer(playersNames[0]);
            AddPlayer(playersNames[1]);
            if (playersNames.Count > 2)
            {
                AddPlayer(playersNames[2]);
                if (playersNames.Count > 3)
                {
                    AddPlayer(playersNames[3]);
                    if (playersNames.Count > 4)
                    {
                        AddPlayer(playersNames[4]);
                        if (playersNames.Count > 5)
                        {
                            AddPlayer(playersNames[5]);
                        }
                    }
                }
            }
            else
                Players.Add(Player.CreateDirk());

            ScoreRound = ScoringRound.First;
        }

        private void AddPlayer(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new NameValidationException("Empty name");
            if (name.Equals(Player.DirkName, StringComparison.OrdinalIgnoreCase))
                throw new NameValidationException("Dirk is reserved name");
            if (Players.Any(p => name.Equals(p.Name, StringComparison.OrdinalIgnoreCase)))
                throw new NameValidationException("Duplicates names");
            Players.Add(new Player(name));
        }

        public Player GetPlayer(int playerNumber)
        {
            return Players[playerNumber - 1];
        }

        public void PlayerAddScore(int playerNumber, int score)
        {
            GetPlayer(playerNumber).AddScore(score);
        }

        public bool ValidateScore(List<PlaceholderPlayerScoreFragment> scorePanels)
        {
            foreach (KeyValuePair<BuildingType, int> mapEntry in BuildingsMaxCount)
            {
                int playersBuildings = scorePanels.Sum(p => p.BuildingsCount[mapEntry.Key]);

                if (playersBuildings > mapEntry.Value)
                    return ValidateUtils.CheckFailed(Context, $"Przekroczona łączna maksymalna ilość budynków {mapEntry.Key}");
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
                return ValidateUtils.CheckFailed(Context, $"Przekroczona maksymalna ilość pojedynczych owoców");
            if (allFruits > 56)
                return ValidateUtils.CheckFailed(Context, $"Przekroczona maksymalna ilość owoców");

            List<int> wishingWellsAvailablePoints = new List<int>() { 3, 3, 4, 4, 5, 5 }.GetCombinationsSums();

            int wishingWellsPointsSum = scorePanels.Sum(p => p.WishingWellsPoints);
            for (int i = 0; i < PlayersCount; i++)
            {
                if (!wishingWellsAvailablePoints.Contains(scorePanels[i].WishingWellsPoints))
                    return ValidateUtils.CheckFailed(Context, $"{GetPlayer(i + 1).Name}: Niedozwolona ilość punktów z fontann");
            }
            if (wishingWellsPointsSum > 24)
                return ValidateUtils.CheckFailed(Context, $"Przekroczona maksymalna ilość punktów z fontann");

            int animalsPointsSum = scorePanels.Sum(p => p.AnimalsPoints);
            for (int i = 0; i < PlayersCount; i++)
            {
                if (scorePanels[i].AnimalsPoints > scorePanels[i].BuildingsCount[BuildingType.Garden] * 3)
                    return ValidateUtils.CheckFailed(Context, $"{GetPlayer(i + 1).Name}: Niedozwolona ilość zwierząt");
            }
            if (animalsPointsSum > 24)
                return ValidateUtils.CheckFailed(Context, $"Przekroczona maksymalna ilość zwierząt");

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
                return ValidateUtils.CheckFailed(Context, $"Przekroczona maksymalna liczba oczek na czarnych kostkach");
            if (blackDicesMinimumNumber > 3)
                return ValidateUtils.CheckFailed(Context, $"Przekroczona ilość użytych czarnych kostek");

            int handymenTilesHighestNumberSum = scorePanels.Sum(p => p.HandymenTilesHighestNumber);
            if (handymenTilesHighestNumberSum > 48)
                return ValidateUtils.CheckFailed(Context, $"Przekroczona maksymalna ilość złotych rączek");

            List<int> treasuresAvailableValues = new List<int>() { 1, 2, 3, 4, 5, 6 }.GetCombinationsSums();

            for (int i = 0; i < PlayersCount; i++)
            {
                if (!treasuresAvailableValues.Contains(scorePanels[i].TreasuresValue))
                    return ValidateUtils.CheckFailed(Context, $"{GetPlayer(i + 1).Name}: Niedozwolona ilość punktów ze skarbów");
            }

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

        public void Score(List<PlaceholderPlayerScoreFragment> scorePanels)
        {
            //wall
            for (int i = 0; i < PlayersCount; i++)
                if (!Players[i].Dirk)
                    Players[i].AddScore(scorePanels[i].WallLength);

            //each kind of building
            foreach (KeyValuePair<BuildingType, List<int>[]> scoring in Game.Scoring)
            {
                for (int i = 0; i < PlayersCount; i++)
                {
                    int alhambraCount = scorePanels[i].BuildingsCount[scoring.Key];
                    if (alhambraCount != 0)
                    {
                        int currentPlace = 1;
                        int sharePlaceCount = 0;
                        for (int j = 0; j < PlayersCount; j++)
                            if (j != i)
                            {
                                int otherPlayerAlhambraBuildingsCount = scorePanels[j].BuildingsCount[scoring.Key];
                                if (otherPlayerAlhambraBuildingsCount > alhambraCount)
                                    currentPlace++;
                                else if (otherPlayerAlhambraBuildingsCount == alhambraCount)
                                {
                                    //Gates without End: Semi-buildings
                                    if (HasModule(ExpansionModule.DesignerGatesWithoutEnd) &&
                                            scorePanels[i].OwnedSemiBuildings[scoring.Key])
                                    {
                                        currentPlace++;
                                    }
                                    else if (HasModule(ExpansionModule.DesignerGatesWithoutEnd) &&
                                            scorePanels[j].OwnedSemiBuildings[scoring.Key])
                                    {
                                        continue;
                                    }
                                    else
                                        sharePlaceCount++;
                                }
                            }

                        if (currentPlace <= RoundNumber)
                        {
                            Players[i].AddScore((scoring.Value[RoundNumber - 1][currentPlace - 1]
                                + (RoundNumber > 1 && sharePlaceCount >= 1 && currentPlace <= RoundNumber - 1 ? scoring.Value[RoundNumber - 1][currentPlace] : 0)
                                + (RoundNumber > 2 && sharePlaceCount >= 2 && currentPlace <= RoundNumber - 2 ? scoring.Value[RoundNumber - 1][currentPlace + 1] : 0)
                                ) / (sharePlaceCount + 1));
                        }
                    }
                }
            }

            if (HasModule(ExpansionModule.DesignerPalaceStaff))
            {
                if (RoundNumber != 3)
                {
                    //Palace Staff: each building without a servant tile
                    for (int i = 0; i < PlayersCount; i++)
                        if (!Players[i].Dirk)
                            Players[i].RemoveScore(scorePanels[i].BuildingsWithoutServantTile);
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
                                Players[i].AddScore(1);
                            if (scorePanels[i].CompletedGroupOfFruitBoard2)
                                Players[i].AddScore(2);
                            if (scorePanels[i].CompletedGroupOfFruitBoard3)
                                Players[i].AddScore(4);
                            if (scorePanels[i].CompletedGroupOfFruitBoard4)
                                Players[i].AddScore(7);
                            if (scorePanels[i].CompletedGroupOfFruitBoard5)
                                Players[i].AddScore(11);
                            if (scorePanels[i].CompletedGroupOfFruitBoard6)
                                Players[i].AddScore(16);
                            Players[i].AddScore(scorePanels[i].FaceDownFruitsCount);
                        }
                }
            }

            if (HasModule(ExpansionModule.DesignerBathhouses))
            {
                //Bathhouses: distances of the first building
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                    {
                        Players[i].AddScore(scorePanels[i].BathhousesPoints);
                    }
            }

            if (HasModule(ExpansionModule.DesignerWishingWell))
            {
                //Wishing Well: tiles in a straight line from the waterspout
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                    {
                        Players[i].AddScore(scorePanels[i].WishingWellsPoints);
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
                                Players[i].AddScore(scorePanels[i].BuildingsCount[completedProject.Key] * 2);
                        }
                    }
            }

            if (HasModule(ExpansionModule.DesignerAlhambraZoo))
            {
                //Alhambra Zoo: animals points
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                    {
                        Players[i].AddScore(scorePanels[i].AnimalsPoints);
                    }
            }

            if (HasModule(ExpansionModule.DesignerBuildingsOfPower))
            {
                //Buildings of Power: Building of Strength
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                    {
                        Players[i].AddScore(Math.Min(scorePanels[i].SecondLongestWallLength, scorePanels[i].BlackDiceTotalPips));
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
                        Players[i].AddScore(scorePanels[i].HandymenTilesHighestNumber);
                    }
            }

            if (HasModule(ExpansionModule.DesignerTreasures))
            {
                //Treasures: treasures' value
                if (RoundNumber == 3)
                {
                    for (int i = 0; i < PlayersCount; i++)
                        if (!Players[i].Dirk)
                        {
                            Players[i].AddScore(scorePanels[i].TreasuresValue);
                        }
                }
            }

            if (HasModule(ExpansionModule.DesignerCaliphsGuidelines))
            {
                if (RoundNumber == 3)
                {
                    for (int i = 0; i < PlayersCount; i++)
                        if (!Players[i].Dirk)
                        {
                            //Caliph’s Guidelines: mission 1
                            Players[i].AddScore(scorePanels[i].Mission1Count * 3);
                            //Caliph’s Guidelines: mission 2
                            Players[i].AddScore(scorePanels[i].Mission2Count * 3);
                            //Caliph’s Guidelines: mission 3
                            Players[i].AddScore(scorePanels[i].Mission3Count * 3);
                            //Caliph’s Guidelines: mission 4
                            if (scorePanels[i].Mission4Available)
                                Players[i].AddScore(scorePanels[i].SecondLongestWallLength);
                            //Caliph’s Guidelines: mission 5
                            Players[i].AddScore(scorePanels[i].Mission5Count * 2);
                            //Caliph’s Guidelines: mission 6
                            Players[i].AddScore(scorePanels[i].Mission6Count * 3);
                            //Caliph’s Guidelines: mission 7
                            switch (scorePanels[i].Mission7Count)
                            {
                                case 2:
                                    Players[i].AddScore(1);
                                    break;
                                case 3:
                                    Players[i].AddScore(3);
                                    break;
                                case 4:
                                    Players[i].AddScore(6);
                                    break;
                                case 5:
                                    Players[i].AddScore(10);
                                    break;
                                case 6:
                                    Players[i].AddScore(15);
                                    break;
                            }
                            //Caliph’s Guidelines: mission 8
                            Players[i].AddScore(scorePanels[i].Mission8Count);
                            //Caliph’s Guidelines: mission 9
                            Players[i].AddScore(scorePanels[i].Mission9Count * 2);
                        }
                }
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
                        Players[i].RemoveScore(scorePanels[i].BuildingsWithoutServantTile);
            }
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
    }

}