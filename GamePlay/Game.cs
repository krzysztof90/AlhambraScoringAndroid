using AlhambraScoringAndroid.Attributes;
using AlhambraScoringAndroid.Tools;
using AlhambraScoringAndroid.Tools.Enums;
using AlhambraScoringAndroid.UI;
using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.GamePlay
{
    public class Game
    {
        public static List<BuildingType> BuildingsOrder = new List<BuildingType>()
        {
            BuildingType.Pavilion, BuildingType.Seraglio, BuildingType.Arcades, BuildingType.Chambers, BuildingType.Garden, BuildingType.Tower
        };
        public static List<GranadaBuildingType> GranadaBuildingsOrder = new List<GranadaBuildingType>()
        {
            GranadaBuildingType.Arena, GranadaBuildingType.BathHouse, GranadaBuildingType.Library, GranadaBuildingType.Hostel, GranadaBuildingType.Hospital, GranadaBuildingType.Market, GranadaBuildingType.Park, GranadaBuildingType.School, GranadaBuildingType.ResidentialArea
        };
        public static List<int>[] TreasureChamberScoring = new List<int>[] { new List<int> { 7 }, new List<int> { 14, 7 }, new List<int> { 22, 14, 7 } };
        public static List<int>[] MedinaScoring = new List<int>[] { new List<int> { 3 }, new List<int> { 6, 3 }, new List<int> { 9, 6, 3 } };
        public static int[] MedinaZeroPenaltiesScoring = new int[] { 1, 2, 3 };
        public static Dictionary<BuildingType, ScoreType> BuildingBaseScoreType = new Dictionary<BuildingType, ScoreType>()
        {
            [BuildingType.Pavilion] = ScoreType.PavilionNumber,
            [BuildingType.Seraglio] = ScoreType.SeraglioNumber,
            [BuildingType.Arcades] = ScoreType.ArcadesNumber,
            [BuildingType.Chambers] = ScoreType.ChambersNumber,
            [BuildingType.Garden] = ScoreType.GardenNumber,
            [BuildingType.Tower] = ScoreType.TowerNumber
        };
        public static Dictionary<GranadaBuildingType, ScoreType> GranadaBuildingBaseScoreType = new Dictionary<GranadaBuildingType, ScoreType>()
        {
            [GranadaBuildingType.Arena] = ScoreType.Arena,
            [GranadaBuildingType.BathHouse] = ScoreType.BathHouse,
            [GranadaBuildingType.Library] = ScoreType.Library,
            [GranadaBuildingType.Hostel] = ScoreType.Hostel,
            [GranadaBuildingType.Hospital] = ScoreType.Hospital,
            [GranadaBuildingType.Market] = ScoreType.Market,
            [GranadaBuildingType.Park] = ScoreType.Park,
            [GranadaBuildingType.School] = ScoreType.School,
            [GranadaBuildingType.ResidentialArea] = ScoreType.ResidentialArea
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
        public static Dictionary<BuildingType, int> SquaresMaxCount = new Dictionary<BuildingType, int>()
        {
            [BuildingType.Pavilion] = 3,
            [BuildingType.Seraglio] = 3,
            [BuildingType.Arcades] = 4,
            [BuildingType.Chambers] = 4,
            [BuildingType.Garden] = 5,
            [BuildingType.Tower] = 5
        };
        public static List<ExpansionModule> GranadaCompatibleModules = new List<ExpansionModule>()
        {
            ExpansionModule.ExpansionDiamonds,
            ExpansionModule.ExpansionCurrencyExchangeCards,
            ExpansionModule.ExpansionMasterBuilders,
            ExpansionModule.ExpansionCharacters,
            ExpansionModule.ExpansionThieves,
            ExpansionModule.ExpansionInvaders,
            ExpansionModule.ExpansionCaravanserai
        };

        public readonly Context Context;

        public DateTime StartDateTime { get; private set; }
        public DateTime? EndDateTime { get; private set; }

        private List<ExpansionModule> Modules;
        public GranadaOption GranadaOption { get; private set; }
        private List<NewScoreCard> NewScoreCards;
        private List<CaliphsGuidelinesMission> CaliphsGuidelines;
        private List<Player> Players;
        public ScoringRound ScoreRound { get; private set; }
        public BuildingType? TheWiseManBuildingType { get; private set; }
        public Dictionary<int, int> PlayersMedinaHighestPrices { get; private set; }
        public Dictionary<GranadaBuildingType, Dictionary<int, int>> PlayersGranadaBuildingsHighestPrices { get; private set; }
        public Stack<ScoreHistory> ScoreStack { get; private set; }
        public bool Saved { get; set; }

        public static List<List<int>[]> ScoringByPosition = new List<List<int>[]>()
        {
            new List<int>[] { new List<int> { 1 }, new List<int> { 8, 1 }, new List<int> { 16, 8, 1 } },
            new List<int>[] { new List<int> { 2 }, new List<int> { 9, 2 }, new List<int> { 17, 9, 2 } },
            new List<int>[] { new List<int> { 3 }, new List<int> { 10, 3 }, new List<int> { 18, 10, 3 } },
            new List<int>[] { new List<int> { 4 }, new List<int> { 11, 4 }, new List<int> { 19, 11, 4 } },
            new List<int>[] { new List<int> { 5 }, new List<int> { 12, 5 }, new List<int> { 20, 12, 5 } },
            new List<int>[] { new List<int> { 6 }, new List<int> { 13, 6 }, new List<int> { 21, 13, 6 } },
        };
        public Dictionary<BuildingType, List<int>[]> Scoring
        {
            get
            {
                return BuildingsOrder.ToDictionary(b => b, b => new List<int>[] { GetBuildingRoundScoring(b, 1), GetBuildingRoundScoring(b, 2), GetBuildingRoundScoring(b, 3) });
            }
        }
        public List<int>[] GetGranadaScoring(List<PlaceholderPlayerScoreFragment> scorePanels, GranadaBuildingType building)
        {
            int sum = scorePanels.Sum(p => p.GranadaBuildingsCount[building]);
            return new List<int>[] { new List<int> { sum }, new List<int> { sum * 2, sum }, new List<int> { sum * 3, sum * 2, sum } };
        }

        private List<int> GetBuildingRoundScoring(BuildingType buildingType, int roundNumber)
        {
            List<BuildingType> buildingsOrder = NewScoreCards != null ? NewScoreCards[roundNumber - 1].GetEnumAttribute<NewScoreCard, NewScoreCardAttribute>().BuildingTypes : BuildingsOrder;
            return ScoringByPosition[buildingsOrder.IndexOf(buildingType)][roundNumber - 1];
        }

        public Dictionary<BuildingType, int> BuildingsMaxCount =>
            new Dictionary<BuildingType, int>()
            {
                [BuildingType.Pavilion] = 7 + (HasModule(ExpansionModule.QueenieMagicalBuildings) ? 1 : 0) + (HasModule(ExpansionModule.DesignerNewBuildingGrounds) ? 2 : 0) + (HasModule(ExpansionModule.DesignerMajorConstructionProjects) ? 5 : 0),
                [BuildingType.Seraglio] = 7 + (HasModule(ExpansionModule.QueenieMagicalBuildings) ? 1 : 0) + (HasModule(ExpansionModule.DesignerNewBuildingGrounds) ? 2 : 0) + (HasModule(ExpansionModule.DesignerMajorConstructionProjects) ? 5 : 0),
                [BuildingType.Arcades] = 9 + (HasModule(ExpansionModule.QueenieMagicalBuildings) ? 1 : 0) + (HasModule(ExpansionModule.DesignerNewBuildingGrounds) ? 2 : 0) + (HasModule(ExpansionModule.DesignerMajorConstructionProjects) ? 5 : 0),
                [BuildingType.Chambers] = 9 + (HasModule(ExpansionModule.QueenieMagicalBuildings) ? 1 : 0) + (HasModule(ExpansionModule.DesignerNewBuildingGrounds) ? 2 : 0) + (HasModule(ExpansionModule.DesignerMajorConstructionProjects) ? 5 : 0),
                [BuildingType.Garden] = 11 + (HasModule(ExpansionModule.QueenieMagicalBuildings) ? 1 : 0) + (HasModule(ExpansionModule.DesignerNewBuildingGrounds) ? 2 : 0) + (HasModule(ExpansionModule.DesignerMajorConstructionProjects) ? 5 : 0),
                [BuildingType.Tower] = 11 + (HasModule(ExpansionModule.QueenieMagicalBuildings) ? 1 : 0) + (HasModule(ExpansionModule.DesignerNewBuildingGrounds) ? 2 : 0) + (HasModule(ExpansionModule.DesignerMajorConstructionProjects) ? 5 : 0),
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
        public int AllGranadaTilesCount
        {
            get
            {
                return 1 + 54;
            }
        }

        public int WallsMaxLength
        {
            get
            {
                int allTilesCount = AllTilesCount;
                if (HasModule(ExpansionModule.ExpansionWatchtowers))
                    allTilesCount += 18;
                return allTilesCount * 2 + 2;
            }
        }
        public int MoatMaxLength
        {
            get
            {
                return AllGranadaTilesCount * 2 + 2;
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
        public void SetGranadaOption(GranadaOption granadaOption)
        {
            GranadaOption = granadaOption;
        }

        public void SetModulesDetails(IEnumerable<CaliphsGuidelinesMission> caliphsGuidelines, List<NewScoreCard> newScoreCards)
        {
            CaliphsGuidelines = caliphsGuidelines?.ToList();
            NewScoreCards = newScoreCards;
        }

        public bool ValidateNewScoreCards(List<NewScoreCard> newScoreCards)
        {
            if (newScoreCards != null)
            {
                if (newScoreCards.Count != 3)
                    throw new NotSupportedException();

                if (newScoreCards.Distinct().Count() != newScoreCards.Count())
                    return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.new_score_cards));
            }

            return true;
        }

        public bool HasModule(ExpansionModule module)
        {
            if (module == ExpansionModule.Granada)
                return GranadaOption != GranadaOption.Without;
            return Modules.Contains(module) && (GranadaOption != GranadaOption.Alone
                || GranadaCompatibleModules.Contains(module));
        }

        public bool HasCaliphsGuideline(CaliphsGuidelinesMission module)
        {
            return CaliphsGuidelines.Contains(module);
        }

        public bool ValidatePlayers(List<string> playersNames)
        {
            if (playersNames.Any(n => String.IsNullOrEmpty(n)))
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_empty_name));
            if (playersNames.Any(n => n.Equals(Player.DirkName, StringComparison.OrdinalIgnoreCase)) && playersNames.Count == 2)
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_dirk_name));
            if (playersNames.Select(n => n.ToUpper()).Distinct().Count() != playersNames.Count)
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_duplicated_name));
            return true;
        }

        public void SetPlayers(List<string> playersNames)
        {
            Players = new List<Player>();

            foreach (string playerName in playersNames)
                Players.Add(new Player(this, playerName));

            if (playersNames.Count == 2)
                Players.Add(Player.CreateDirk(this));

            Reset(false);
        }

        public void Reset(bool resetPlayers)
        {
            ScoreRound = ScoringRound.First;
            ScoreStack = new Stack<ScoreHistory>();
            ResetFinish();
            if (resetPlayers)
                Players = null;
        }

        public void ResetFinish()
        {
            SetEndDateTime(null);
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

        public void SetTheWiseManBuildingType(BuildingType? buildingType)
        {
            TheWiseManBuildingType = buildingType;
        }

        public bool ValidateMedinasNumbers(Dictionary<int, int> playersHighestPrices)
        {
            if (playersHighestPrices.Select(d => d.Value).Distinct().Count() != playersHighestPrices.Count())
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_medin_same_price));

            return true;
        }
        public bool ValidateGranadaBuildingsNumbers(Dictionary<GranadaBuildingType, Dictionary<int, int>> playersHighestPrices)
        {
            foreach (GranadaBuildingType building in GranadaBuildingsOrder)
            {
                if (playersHighestPrices[building].Select(d => d.Value).Distinct().Count() != playersHighestPrices[building].Count())
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_building_same_price), building.GetEnumDescription(Context.Resources)));
            }
            return true;
        }

        public void SetMedinasNumbers(Dictionary<int, int> playersHighestPrices)
        {
            PlayersMedinaHighestPrices = playersHighestPrices;
        }
        public void SetGranadaBuildingsNumbers(Dictionary<GranadaBuildingType, Dictionary<int, int>> playersHighestPrices)
        {
            PlayersGranadaBuildingsHighestPrices = playersHighestPrices;
        }

        public bool ValidateScore(List<PlaceholderPlayerScoreFragment> scorePanels)
        {
            //TODO walidacja danych na podstawie wcześniejszych submitów

            foreach (KeyValuePair<BuildingType, int> mapEntry in BuildingsMaxCount)
            {
                int playersBuildings = scorePanels.Sum(p => p.BuildingsCount[mapEntry.Key]);

                if (playersBuildings > mapEntry.Value)
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_building_number_exceed), mapEntry.Key.GetEnumDescription(Context.Resources)));
            }

            int playerBonusCardsMax = 1;
            if (PlayersCount < 6)
                playerBonusCardsMax++;
            if (PlayersCount < 4)
                playerBonusCardsMax++;

            for (int i = 0; i < PlayersCount; i++)
            {
                if (scorePanels[i].BonusCardsBuildingsCount.Sum(c => c.Value) > playerBonusCardsMax)
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_bonus_cards_player_exceed), GetPlayer(i + 1).Name));

                foreach (KeyValuePair<BuildingType, int> mapEntry in BonusCardsMaxCount)
                {
                    if (scorePanels[i].ExtensionsBuildingsCount[mapEntry.Key] > scorePanels[i].BuildingsCount[mapEntry.Key])
                        return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_bonus_cards_buildings_player_exceed), GetPlayer(i + 1).Name, mapEntry.Key.GetEnumDescription(Context.Resources)));
                }
            }
            foreach (KeyValuePair<BuildingType, int> mapEntry in BonusCardsMaxCount)
            {
                if (scorePanels.Sum(p => p.BonusCardsBuildingsCount[mapEntry.Key]) > mapEntry.Value)
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_bonus_cards_exceed), mapEntry.Key.GetEnumDescription(Context.Resources)));
            }

            Dictionary<BuildingType, int> squaresTotalMinimumNumber = new Dictionary<BuildingType, int>();
            for (int i = 0; i < PlayersCount; i++)
            {
                int playerSquaresTotalMinimumNumber = 0;
                foreach (BuildingType building in BuildingsOrder)
                {
                    int squaresPoints = scorePanels[i].SquaresBuildingsCount[building];
                    int squaresMinimumNumber = 0;
                    if (squaresPoints > 0)
                        squaresMinimumNumber++;
                    if (squaresPoints > 3)
                        squaresMinimumNumber++;
                    if (squaresPoints > 6)
                        squaresMinimumNumber++;

                    playerSquaresTotalMinimumNumber += squaresMinimumNumber;

                    int squareTotalMinimumNumber = 0;
                    if (squaresTotalMinimumNumber.ContainsKey(building))
                        squareTotalMinimumNumber = squaresTotalMinimumNumber[building];
                    squareTotalMinimumNumber += squaresMinimumNumber;
                    squaresTotalMinimumNumber[building] = squareTotalMinimumNumber;

                    if (squaresPoints > 9)
                    {
                        return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_squares_buildings_player_exceed), GetPlayer(i + 1).Name, building.GetEnumDescription(Context.Resources)));
                    }
                }

                if (playerSquaresTotalMinimumNumber > 3)
                {
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_squares_player_exceed), GetPlayer(i + 1).Name));
                }
            }
            foreach (KeyValuePair<BuildingType, int> mapEntry in SquaresMaxCount)
            {
                if (squaresTotalMinimumNumber[mapEntry.Key] > mapEntry.Value)
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_squares_number_exceed), mapEntry.Key.GetEnumDescription(Context.Resources)));
            }

            if (scorePanels.Count(p => p.OwnedCharacterTheWiseMan) > 1)
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_multiple_wiseman));
            if (scorePanels.Count(p => p.OwnedCharacterTheCityWatch) > 1)
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_multiple_citywatch));

            for (int i = 0; i < PlayersCount; i++)
            {
                foreach (KeyValuePair<BuildingType, int> mapEntry in BonusCardsMaxCount)
                {
                    if (scorePanels[i].StreetTradersNumber[mapEntry.Key] > scorePanels[i].BuildingsCount[mapEntry.Key])
                        return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_citizens_buildings_exceed), GetPlayer(i + 1).Name, mapEntry.Key.GetEnumDescription(Context.Resources)));
                }
            }
            foreach (BuildingType building in BuildingsOrder)
            {
                if (scorePanels.Sum(p => p.StreetTradersNumber[building]) > 7)
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_citizens_exceed), building.GetEnumDescription(Context.Resources)));
            }

            for (int i = 0; i < PlayersCount; i++)
            {
                if (scorePanels[i].TreasuresCount > scorePanels[i].AllBuildingsCount)
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_treasures_player_exceed), GetPlayer(i + 1).Name));
            }
            if (scorePanels.Sum(p => p.TreasuresCount) > 42)
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_treasures_number_exceed));

            //TODO get miminum bazaars number from points combination
            //TODO to method
            //IEnumerable<int> bazaarsAvailablePoints = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 14, 15, 16, 18, 21, 24 }.GetMatrixSums(8).Distinct();

            int bazaarsTotalPointsSum = 0;
            int bazaarsMinimumNumber = 0;
            for (int i = 0; i < PlayersCount; i++)
            {
                int bazaarsTotalPoints = scorePanels[i].BazaarsTotalPoints;

                //if (!bazaarsAvailablePoints.Contains(bazaarsTotalPoints))
                //    return ValidateUtils.CheckFailed(Context, $"{GetPlayer(i + 1).Name}: Niedozwolona ilość punktów z bazarów");

                if (bazaarsTotalPoints > 0)
                    bazaarsMinimumNumber++;
                if (bazaarsTotalPoints > 24)
                    bazaarsMinimumNumber++;
                if (bazaarsTotalPoints > 48)
                    bazaarsMinimumNumber++;
                if (bazaarsTotalPoints > 72)
                    bazaarsMinimumNumber++;
                if (bazaarsTotalPoints > 96)
                    bazaarsMinimumNumber++;
                if (bazaarsTotalPoints > 120)
                    bazaarsMinimumNumber++;
                if (bazaarsTotalPoints > 144)
                    bazaarsMinimumNumber++;
                if (bazaarsTotalPoints > 168)
                    bazaarsMinimumNumber++;
                bazaarsTotalPointsSum += bazaarsTotalPoints;
            }
            if (bazaarsTotalPointsSum > 192)
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_bazaars_points_exceed));
            if (bazaarsMinimumNumber > 8)
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_bazaars_number_exceed));

            //TODO get miminum counters number from points combination, compare all to max 20
            IEnumerable<int> artOfTheMoorsPlayerAvailablePoints = new List<int>() { 0, 3, 6, 10, 15, 21 }.GetMatrixSums(7).Distinct();

            for (int i = 0; i < PlayersCount; i++)
            {
                if (!artOfTheMoorsPlayerAvailablePoints.Contains(scorePanels[i].ArtOfTheMoorsPoints))
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_culture_counters_player_mismatch), GetPlayer(i + 1).Name));
            }

            if (scorePanels.Sum(p => p.FalconsBlackNumber) > 5)
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_black_falcons_number_exceed));
            if (scorePanels.Sum(p => p.FalconsBrownNumber) > 5)
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_brown_falcons_number_exceed));
            if (scorePanels.Sum(p => p.FalconsWhiteNumber) > 5)
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_white_falcons_number_exceed));

            for (int i = 0; i < PlayersCount; i++)
            {
                if (scorePanels[i].WatchtowersNumber > scorePanels[i].WallLength)
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_watchtower_wall_player_exceed), GetPlayer(i + 1).Name));
            }
            if (scorePanels.Sum(p => p.WatchtowersNumber) > 18)
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_watchtower_number_exceed));

            if (scorePanels.Sum(p => p.MedinasNumber) > 9)
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_medin_number_exceed));

            for (int i = 0; i < PlayersCount; i++)
                if (scorePanels[i].BuildingsWithoutServantTile > scorePanels[i].AllBuildingsCount)
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_servants_buildings_player_exceed), GetPlayer(i + 1).Name));

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
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_single_fruits_number_exceed));
            if (allFruits > 56)
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_fruits_number_exceed));

            List<int> wishingWellsAvailablePoints = new List<int>() { 3, 3, 4, 4, 5, 5 }.GetCombinationsSums();

            int wishingWellsPointsSum = scorePanels.Sum(p => p.WishingWellsPoints);
            for (int i = 0; i < PlayersCount; i++)
            {
                if (!wishingWellsAvailablePoints.Contains(scorePanels[i].WishingWellsPoints))
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_fontains_player_points_mismatch), GetPlayer(i + 1).Name));
            }
            if (wishingWellsPointsSum > 24)
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_fontains_points_exceed));

            int animalsPointsSum = scorePanels.Sum(p => p.AnimalsPoints);
            for (int i = 0; i < PlayersCount; i++)
            {
                if (scorePanels[i].AnimalsPoints > scorePanels[i].BuildingsCount[BuildingType.Garden] * 3)
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_animals_player_exceed), GetPlayer(i + 1).Name));
            }
            if (animalsPointsSum > 24)
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_animals_number_exceed));

            foreach (BuildingType building in BuildingsOrder)
            {
                if (scorePanels.Count(p => p.OwnedSemiBuildings[building]) > 1)
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_multiple_semi_buildings), building.GetEnumDescription(Context.Resources)));
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
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_black_dice_pips_number_exceed));
            if (blackDicesMinimumNumber > 3)
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_black_dices_number_exceed));
            
            foreach (BuildingType building in BuildingsOrder)
            {
                for (int i = 0; i < PlayersCount; i++)
                {
                    if (scorePanels[i].ExtensionsBuildingsCount[building] > scorePanels[i].BuildingsCount[building])
                        return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_extensions_buildings_player_exceed), GetPlayer(i + 1).Name, building.GetEnumDescription(Context.Resources)));
                }
                if (scorePanels.Sum(p => p.ExtensionsBuildingsCount[building]) > 2)
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_extensions_exceed), building.GetEnumDescription(Context.Resources)));
            }

            int handymenTilesHighestNumberSum = scorePanels.Sum(p => p.HandymenTilesHighestNumber);
            if (handymenTilesHighestNumberSum > 48)
                return ValidateUtils.CheckFailed(Context, Context.Resources.GetString(Resource.String.message_handymen_exceed));

            List<int> treasuresAvailableValues = new List<int>() { 1, 2, 3, 4, 5, 6 }.GetCombinationsSums();

            for (int i = 0; i < PlayersCount; i++)
            {
                if (!treasuresAvailableValues.Contains(scorePanels[i].TreasuresValue))
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_treasures_points_player_mismatch), GetPlayer(i + 1).Name));
            }

            for (int i = 0; i < PlayersCount; i++)
            {
                int wallLength = scorePanels[i].WallLength;
                int secondLongestWallLength = scorePanels[i].SecondLongestWallLength;
                if (wallLength < secondLongestWallLength)
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_second_longest_wall_player_exceed), GetPlayer(i + 1).Name));
            }

            foreach (GranadaBuildingType building in GranadaBuildingsOrder)
            {
                int playersBuildings = scorePanels.Sum(p => p.GranadaBuildingsCount[building]);

                if (playersBuildings > 6)
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_building_number_exceed), building.GetEnumDescription(Context.Resources)));
            }

            for (int i = 0; i < PlayersCount; i++)
            {
                if (scorePanels[i].WallMoatCombinationLength > scorePanels[i].WallLength)
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_moatwall_wall_player), GetPlayer(i + 1).Name));
                if (scorePanels[i].WallMoatCombinationLength > scorePanels[i].MoatLength)
                    return ValidateUtils.CheckFailed(Context, String.Format(Context.Resources.GetString(Resource.String.message_moatwall_moat_player), GetPlayer(i + 1).Name));
            }

            return true;
        }

        private int CountRelativeScore(int playerNumber, Func<int, double> getCountValue, List<int>[] scoringTable, HighestLowest highestLowest, UpDown roundMethod, int[] zeroPenaltiesTable = null)
        {
            int result = 0;
            double countValue = getCountValue(playerNumber - 1);
            if (countValue != 0 || highestLowest == HighestLowest.Lowest)
            {
                int currentPlace = 1;
                int sharePlaceCount = 0;
                for (int j = 0; j < PlayersCount; j++)
                    if (j != playerNumber - 1)
                    {
                        double otherPlayerCountValue = getCountValue(j);

                        if ((highestLowest == HighestLowest.Highest && otherPlayerCountValue > countValue)
                            || (highestLowest == HighestLowest.Lowest && otherPlayerCountValue < countValue))
                            currentPlace++;
                        else if (otherPlayerCountValue == countValue)
                            sharePlaceCount++;
                    }

                int splitPoints = (currentPlace <= RoundNumber ? scoringTable[RoundNumber - 1][currentPlace - 1] : 0)
                    + (RoundNumber > 1 && sharePlaceCount >= 1 && currentPlace <= RoundNumber - 1 ? scoringTable[RoundNumber - 1][currentPlace] : 0)
                    + (RoundNumber > 2 && sharePlaceCount >= 2 && currentPlace <= RoundNumber - 2 ? scoringTable[RoundNumber - 1][currentPlace + 1] : 0);
                result = splitPoints / (sharePlaceCount + 1);
                if (roundMethod == UpDown.Up && splitPoints % (sharePlaceCount + 1) != 0)
                    result += 1;

                if (highestLowest == HighestLowest.Lowest && countValue == 0 && zeroPenaltiesTable != null)
                    result += zeroPenaltiesTable[RoundNumber - 1];
            }

            return result;
        }

        private double GetBuildingCount(PlaceholderPlayerScoreFragment scorePanel, BuildingType buildingType, bool withBonuses = true)
        {
            double alhambraCount = scorePanel.BuildingsCount[buildingType];
            if (withBonuses)
            {
                //Bonus Cards: extra buildings
                if (HasModule(ExpansionModule.ExpansionBonusCards))
                    alhambraCount += scorePanel.BonusCardsBuildingsCount[buildingType];
                //Squares: square count value
                if (HasModule(ExpansionModule.ExpansionSquares))
                    alhambraCount += scorePanel.SquaresBuildingsCount[buildingType];
                if (HasModule(ExpansionModule.ExpansionCharacters)
                    && scorePanel.OwnedCharacterTheWiseMan && TheWiseManBuildingType == buildingType)
                    alhambraCount += 0.5;
                //Extensions: extended buildings
                if (HasModule(ExpansionModule.DesignerExtensions))
                    alhambraCount += scorePanel.ExtensionsBuildingsCount[buildingType];
                //Gates without End: semi-buildings
                if (alhambraCount >= 1 && HasModule(ExpansionModule.DesignerGatesWithoutEnd)
                    && scorePanel.OwnedSemiBuildings[buildingType])
                    alhambraCount += 0.5;
            }

            return alhambraCount;
        }

        private double GetMedinaCount(PlaceholderPlayerScoreFragment scorePanel, int playerNumber)
        {
            double medinaCount = scorePanel.MedinasNumber;
            if (PlayersMedinaHighestPrices != null && PlayersMedinaHighestPrices.ContainsKey(playerNumber))
                medinaCount += ((double)PlayersMedinaHighestPrices[playerNumber]) / 20;
            return medinaCount;
        }
        private double GetGranadaBuildingCount(PlaceholderPlayerScoreFragment scorePanel, int playerNumber, GranadaBuildingType building)
        {
            double buildingCount = scorePanel.GranadaBuildingsCount[building];
            if (PlayersGranadaBuildingsHighestPrices != null && PlayersGranadaBuildingsHighestPrices[building].ContainsKey(playerNumber))
                buildingCount += ((double)PlayersGranadaBuildingsHighestPrices[building][playerNumber]) / 20;
            return buildingCount;
        }

        public int GetBuildingScore(List<PlaceholderPlayerScoreFragment> scorePanels, BuildingType buildingType, int playerNumber, bool withBonuses = true)
        {
            return CountRelativeScore(playerNumber, (int i) => GetBuildingCount(scorePanels[i], buildingType, withBonuses), Scoring[buildingType], HighestLowest.Highest, UpDown.Down);
        }

        public void Score(List<PlaceholderPlayerScoreFragment> scorePanels)
        {
            List<(ScoreDetails scoreDetails1, ScoreDetails scoreDetails2, ScoreDetails scoreDetails3, ScoreDetails scoreMeantime)> initialScoring = Players.Select(p => (p.ScoreDetails1.Copy(), p.ScoreDetails2.Copy(), p.ScoreDetails3.Copy(), p.ScoreMeantime.Copy())).ToList();

            if (GranadaOption != GranadaOption.Alone)
            {
                //wall
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                        Players[i].AddScore(scorePanels[i].WallLength, ScoreType.WallLength);

                //each kind of building
                foreach (KeyValuePair<BuildingType, List<int>[]> scoring in Scoring)
                {
                    for (int i = 0; i < PlayersCount; i++)
                    {
                        int buildingScore = GetBuildingScore(scorePanels, scoring.Key, i + 1);
                        int buildingScoreBonus = buildingScore - GetBuildingScore(scorePanels, scoring.Key, i + 1, false);
                        if (buildingScoreBonus < 0)
                            buildingScoreBonus = 0;

                        Players[i].AddScore(buildingScore, BuildingBaseScoreType[scoring.Key]);
                        Players[i].AddScore(buildingScoreBonus, ScoreType.BuildingsBonuses);
                    }
                }
            }

            if (HasModule(ExpansionModule.ExpansionCharacters))
            {
                //Characters: owned The City Watch
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                        Players[i].AddScore(scorePanels[i].OwnedCharacterTheCityWatch ? scorePanels[i].WallLength / 3 : 0, ScoreType.TheCityWatch);
            }

            if (HasModule(ExpansionModule.ExpansionCamps))
            {
                //Camps: buildings joined together in a straight, uninterrupted line
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                        Players[i].AddScore(scorePanels[i].CampsPoints, ScoreType.Camps);
            }

            if (HasModule(ExpansionModule.ExpansionStreetTrader))
            {
                //Street Trader: sets based on the number of different colored citizens
                for (int i = 0; i < PlayersCount; i++)
                {
                    int pointsSum = 0;
                    for (int j = 0; j < 7; j++)
                    {
                        int setDifferentCitizens = scorePanels[i].StreetTradersNumber.Where(t => t.Value > j).Count();
                        int points;
                        switch (setDifferentCitizens)
                        {
                            case 0:
                                points = 0;
                                break;
                            case 1:
                                points = 1;
                                break;
                            case 2:
                                points = 3;
                                break;
                            case 3:
                                points = 6;
                                break;
                            case 4:
                                points = 10;
                                break;
                            case 5:
                                points = 15;
                                break;
                            case 6:
                                points = 21;
                                break;
                            default:
                                throw new NotSupportedException();
                        }
                        pointsSum += points;
                    }
                    Players[i].AddScore(pointsSum, ScoreType.StreetTraders);
                }
            }

            if (HasModule(ExpansionModule.ExpansionTreasureChamber))
            {
                //Treasure Chamber: chests
                for (int i = 0; i < PlayersCount; i++)
                {
                    int chestsScore = CountRelativeScore(i + 1, (int j) => scorePanels[j].TreasuresCount, TreasureChamberScoring, HighestLowest.Highest, UpDown.Down);

                    Players[i].AddScore(chestsScore, ScoreType.TreasureChamber);
                }
            }

            if (HasModule(ExpansionModule.ExpansionInvaders))
            {
                //Invaders: each side unprotected from the main direction of the attack
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                    {
                        Players[i].RemoveScore(scorePanels[i].UnprotectedSidesCount * RoundNumber, false, ScoreType.Invaders);
                        if (RoundNumber == 3)
                            Players[i].RemoveScore(scorePanels[i].UnprotectedSidesNeighbouringCount, false, ScoreType.Invaders);
                    }
            }

            if (HasModule(ExpansionModule.ExpansionBazaars))
            {
                if (RoundNumber == 3)
                {
                    //Bazaars: points for bazaars
                    for (int i = 0; i < PlayersCount; i++)
                        if (!Players[i].Dirk)
                            Players[i].AddScore(scorePanels[i].BazaarsTotalPoints, ScoreType.Bazaars);
                }
            }

            if (HasModule(ExpansionModule.ExpansionArtOfTheMoors))
            {
                //Art of the Moors: points for the culture counters
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                        Players[i].AddScore(scorePanels[i].ArtOfTheMoorsPoints, ScoreType.ArtOfTheMoors);
            }

            if (HasModule(ExpansionModule.ExpansionFalconers))
            {
                //Falconers: points for each type of falcons
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                    {
                        foreach (int falconsNumber in new int[] { scorePanels[i].FalconsBlackNumber, scorePanels[i].FalconsBrownNumber, scorePanels[i].FalconsWhiteNumber })
                        {
                            int points;
                            switch (falconsNumber)
                            {
                                case 0:
                                    points = 0;
                                    break;
                                case 1:
                                    points = 2;
                                    break;
                                case 2:
                                    points = 6;
                                    break;
                                case 3:
                                    points = 12;
                                    break;
                                case 4:
                                    points = 20;
                                    break;
                                case 5:
                                    points = 30;
                                    break;
                                default:
                                    throw new NotSupportedException();
                            }
                            Players[i].AddScore(points, ScoreType.Falconers);
                        }
                    }
            }

            if (HasModule(ExpansionModule.ExpansionWatchtowers))
            {
                //Watchtowers: Each watchtower that is part of a player’s longest contiguous wall
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                        Players[i].AddScore(scorePanels[i].WatchtowersNumber * 2, ScoreType.Watchtowers);
            }

            if (HasModule(ExpansionModule.QueenieMedina))
            {
                //Medina
                for (int i = 0; i < PlayersCount; i++)
                {
                    int medinaScore = CountRelativeScore(i + 1, (int j) => GetMedinaCount(scorePanels[j], j + 1), MedinaScoring, HighestLowest.Lowest, UpDown.Up, MedinaZeroPenaltiesScoring);

                    Players[i].RemoveScore(medinaScore, true, ScoreType.Medina);
                }
            }

            if (HasModule(ExpansionModule.DesignerPalaceStaff))
            {
                if (RoundNumber != 3)
                {
                    //Palace Staff: each building without a servant tile
                    for (int i = 0; i < PlayersCount; i++)
                        if (!Players[i].Dirk)
                            Players[i].RemoveScore(scorePanels[i].BuildingsWithoutServantTile, false, ScoreType.BuildingsWithoutServantTile);
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
                        Players[i].AddScore(scorePanels[i].BathhousesPoints, ScoreType.Bathhouses);
            }

            if (HasModule(ExpansionModule.DesignerWishingWell))
            {
                //Wishing Well: tiles in a straight line from the waterspout
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                        Players[i].AddScore(scorePanels[i].WishingWellsPoints, ScoreType.WishingWells);
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
                            if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission1))
                                //Caliph’s Guidelines: mission 1
                                Players[i].AddScore(scorePanels[i].Mission1Count * 3, ScoreType.Mission1);
                            if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission2))
                                //Caliph’s Guidelines: mission 2
                                Players[i].AddScore(scorePanels[i].Mission2Count * 3, ScoreType.Mission2);
                            if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission3))
                                //Caliph’s Guidelines: mission 3
                                Players[i].AddScore(scorePanels[i].Mission3Count * 3, ScoreType.Mission3);
                            if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission4))
                                //Caliph’s Guidelines: mission 4
                                if (scorePanels[i].Mission4Available)
                                    Players[i].AddScore(scorePanels[i].SecondLongestWallLength, ScoreType.Mission4);
                            if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission5))
                                //Caliph’s Guidelines: mission 5
                                Players[i].AddScore(scorePanels[i].Mission5Count * 2, ScoreType.Mission5);
                            if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission6))
                                //Caliph’s Guidelines: mission 6
                                Players[i].AddScore(scorePanels[i].Mission6Count * 3, ScoreType.Mission6);
                            if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission7))
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
                            if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission8))
                                //Caliph’s Guidelines: mission 8
                                Players[i].AddScore(scorePanels[i].Mission8Count, ScoreType.Mission8);
                            if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission9))
                                //Caliph’s Guidelines: mission 9
                                Players[i].AddScore(scorePanels[i].Mission9Count * 2, ScoreType.Mission9);
                        }
                }
            }

            if (HasModule(ExpansionModule.Granada))
            {
                //moat
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                        Players[i].AddScore(scorePanels[i].MoatLength, ScoreType.MoatLength);

                //each kind of building
                foreach (GranadaBuildingType building in GranadaBuildingsOrder)
                {
                    for (int i = 0; i < PlayersCount; i++)
                    {
                        int buildingScore = CountRelativeScore(i + 1, (int j) => GetGranadaBuildingCount(scorePanels[j], j + 1, building), GetGranadaScoring(scorePanels, building), HighestLowest.Highest, UpDown.Down);

                        Players[i].AddScore(buildingScore, GranadaBuildingBaseScoreType[building]);
                    }
                }
            }

            if (GranadaOption == GranadaOption.With)
            {
                for (int i = 0; i < PlayersCount; i++)
                    if (!Players[i].Dirk)
                        Players[i].AddScore(scorePanels[i].WallMoatCombinationLength * 2, ScoreType.WallMoatCombination);
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
                        Players[i].RemoveScore(scorePanels[i].BuildingsWithoutServantTile, false, ScoreType.BuildingsWithoutServantTile);
            }
        }

        public void RevertScoring()
        {
            ScoreStack.Pop().Revert();
            ResetFinish();
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
                GranadaOption = GranadaOption,
                NewScoreCards = NewScoreCards,
                CaliphsGuidelines = CaliphsGuidelines,
                ScoreRound = ScoreRound,
                Players = Players.Select(p => new ResultPlayerHistory() { Name = p.Name, ScoreDetails1 = p.ScoreDetails1, ScoreDetails2 = p.ScoreDetails2, ScoreDetails3 = p.ScoreDetails3, ScoreMeantime = p.ScoreMeantime }).ToList()
            };
        }

    }
}