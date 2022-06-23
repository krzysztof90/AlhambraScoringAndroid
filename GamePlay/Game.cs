using AlhambraScoringAndroid.Attributes;
using AlhambraScoringAndroid.Options;
using AlhambraScoringAndroid.Tools;
using AlhambraScoringAndroid.Tools.Enums;
using Android.Content;
using AndroidBase.Tools;
using AndroidBase.Tools.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using static AndroidBase.Tools.AndroidValidateUtils;

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
        public Stack<ScoreHistory> ScoreStack { get; private set; }
        public bool Saved { get; set; }
        public List<PlayerScoreData>[] RoundsScoring { get; private set; }
        public List<PlayerScoreData> ThirdBeforeRoundScoring { get; private set; }
        public List<PlayerScoreData> PreviousRoundScoring => RoundNumber != 1 ? (ScoreRound == ScoringRound.Finish ? RoundsScoring[2] : RoundsScoring[RoundNumber - 2]) : null;

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
        public List<int>[] GetGranadaScoring(List<PlayerScoreData> scoreData, GranadaBuildingType building)
        {
            int sum = scoreData.Sum(p => p.GranadaBuildingsCount[building]);
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
                [BuildingType.Pavilion] = 7 + (HasModule(ExpansionModule.QueenieMagicalBuildings) ? 1 : 0) + (HasModule(ExpansionModule.DesignerNewBuildingGrounds) ? 2 : 0) + (HasModule(ExpansionModule.DesignerMajorConstructionProjects) ? 5 : 0) + (HasModule(ExpansionModule.NewMarket) ? 1 : 0),
                [BuildingType.Seraglio] = 7 + (HasModule(ExpansionModule.QueenieMagicalBuildings) ? 1 : 0) + (HasModule(ExpansionModule.DesignerNewBuildingGrounds) ? 2 : 0) + (HasModule(ExpansionModule.DesignerMajorConstructionProjects) ? 5 : 0) + (HasModule(ExpansionModule.NewMarket) ? 1 : 0),
                [BuildingType.Arcades] = 9 + (HasModule(ExpansionModule.QueenieMagicalBuildings) ? 1 : 0) + (HasModule(ExpansionModule.DesignerNewBuildingGrounds) ? 2 : 0) + (HasModule(ExpansionModule.DesignerMajorConstructionProjects) ? 5 : 0) + (HasModule(ExpansionModule.NewMarket) ? 1 : 0),
                [BuildingType.Chambers] = 9 + (HasModule(ExpansionModule.QueenieMagicalBuildings) ? 1 : 0) + (HasModule(ExpansionModule.DesignerNewBuildingGrounds) ? 2 : 0) + (HasModule(ExpansionModule.DesignerMajorConstructionProjects) ? 5 : 0) + (HasModule(ExpansionModule.NewMarket) ? 1 : 0),
                [BuildingType.Garden] = 11 + (HasModule(ExpansionModule.QueenieMagicalBuildings) ? 1 : 0) + (HasModule(ExpansionModule.DesignerNewBuildingGrounds) ? 2 : 0) + (HasModule(ExpansionModule.DesignerMajorConstructionProjects) ? 5 : 0) + (HasModule(ExpansionModule.NewMarket) ? 1 : 0),
                [BuildingType.Tower] = 11 + (HasModule(ExpansionModule.QueenieMagicalBuildings) ? 1 : 0) + (HasModule(ExpansionModule.DesignerNewBuildingGrounds) ? 2 : 0) + (HasModule(ExpansionModule.DesignerMajorConstructionProjects) ? 5 : 0) + (HasModule(ExpansionModule.NewMarket) ? 1 : 0),
            };

        public int AllBuildingsCount => BuildingsMaxCount.Sum(b => b.Value);

        public int AdditionalAvailableTilesCount
        {
            get
            {
                int availableTilesCount = 1; //fountain
                if (HasModule(ExpansionModule.DesignerBathhouses))
                    availableTilesCount += 6;
                if (HasModule(ExpansionModule.DesignerWishingWell))
                    availableTilesCount += 6;
                if (HasModule(ExpansionModule.DesignerGatesWithoutEnd))
                    availableTilesCount += 6;
                return availableTilesCount;
            }
        }

        public int AllTilesCount
        {
            get
            {
                return AllBuildingsCount + AdditionalAvailableTilesCount;
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
                        //case ScoringRound.Finish:
                        //    return 4;
                }
                return 0;
            }
        }

        public int PlayersCount => Players.Count;
        public bool InvolvedDirk => Players.Any(p => p.Dirk);
        public int PlayersCountWithoutDirk => Players.Count - (InvolvedDirk ? 1 : 0);

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
                    return CheckFailed(Resource.String.message_new_score_cards);
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
                return CheckFailed(Resource.String.message_empty_name);
            if (playersNames.Any(n => n.Equals(Player.DirkName, StringComparison.OrdinalIgnoreCase)) && playersNames.Count == 2)
                return CheckFailed(Resource.String.message_dirk_name);
            if (playersNames.Select(n => n.ToUpper()).Distinct().Count() != playersNames.Count)
                return CheckFailed(Resource.String.message_duplicated_name);
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
            RoundsScoring = new List<PlayerScoreData>[3];
            ThirdBeforeRoundScoring = null;
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

        public bool ValidateMedinasNumbers(Dictionary<int, int> playersHighestPrices)
        {
            if (playersHighestPrices.Select(d => d.Value).Distinct().Count() != playersHighestPrices.Count())
                return CheckFailed(Resource.String.message_medin_same_price);

            return true;
        }
        public bool ValidateGranadaBuildingsNumbers(Dictionary<GranadaBuildingType, Dictionary<int, int>> playersHighestPrices)
        {
            foreach (GranadaBuildingType building in GranadaBuildingsOrder)
            {
                if (playersHighestPrices[building].Select(d => d.Value).Distinct().Count() != playersHighestPrices[building].Count())
                    return CheckFailed(Resource.String.message_building_same_price, building.GetEnumDescription(Context.Resources));
            }
            return true;
        }

        private int GetMinimumNumberFromCount(int count, int interval)
        {
            return count == 0 ? 0 : ((count - 1) / interval + 1);
        }

        private bool ValidatePreviousAvailableLimit(List<PlayerScoreData> scoreData, Func<PlayerScoreData, int> countAmountMethod, int maxAmount, ResourcesFormatData errorMessage)
        {
            if (HasModule(ExpansionModule.FanPersonalBuildingMarket))
                return true;

            return ValidatePreviousAvailableLimit(RoundNumber, scoreData, countAmountMethod, maxAmount, errorMessage);
        }

        private bool ValidatePreviousAvailableLimit(int roundNumber, List<PlayerScoreData> scoreData, Func<PlayerScoreData, int> countAmountMethod, int maxAmount, ResourcesFormatData errorMessage)
        {
            if (roundNumber == 1)
                return true;

            List<PlayerScoreData> previousScoreData = RoundsScoring[roundNumber - 2];

            int playersPreviousAmount = previousScoreData.Sum(p => countAmountMethod(p));
            int leftAmount = maxAmount - playersPreviousAmount;

            int increase = 0;
            foreach (PlayerScoreData playerScoreData in scoreData)
            {
                if (countAmountMethod(playerScoreData) - countAmountMethod(previousScoreData[playerScoreData.PlayerNumber - 1]) > 0)
                    increase += countAmountMethod(playerScoreData) - countAmountMethod(previousScoreData[playerScoreData.PlayerNumber - 1]);
            }

            if (increase > leftAmount)
                return CheckFailed(errorMessage.Message);

            return ValidatePreviousAvailableLimit(roundNumber - 1, scoreData, countAmountMethod, maxAmount, errorMessage);
        }

        private bool CheckPreviousRoundScoringMatch(List<PlayerScoreData> scoreData, SettingsType settingsType, Func<PlayerScoreData, PlayerScoreData, bool> findErrorFunction, Func<string, ResourcesFormatData> errorMessageFunction)
        {
            if (PreviousRoundScoring != null && Settings.Get(settingsType))
            {
                foreach (PlayerScoreData playerScoreData in scoreData)
                {
                    if (findErrorFunction(PreviousRoundScoring[playerScoreData.PlayerNumber - 1], playerScoreData))
                        return CheckFailed(errorMessageFunction(playerScoreData.PlayerName).Message);
                }
            }

            return true;
        }

        public bool ValidateScore(List<PlayerScoreData> scoreData)
        {
            List<PlayerScoreData> previousBeforeScoreData = null;
            if (RoundNumber == 3 && ThirdBeforeRoundScoring != null)
                previousBeforeScoreData = ThirdBeforeRoundScoring;

            foreach (KeyValuePair<BuildingType, int> mapEntry in BuildingsMaxCount)
            {
                int playersBuildings = scoreData.Sum(p => p.BuildingsCount[mapEntry.Key]);

                if (Settings.Get(SettingsType.ValidateBuildingsNumber) && playersBuildings > mapEntry.Value)
                    return CheckFailed(Resource.String.message_building_number_exceed, mapEntry.Key.GetEnumDescription(Context.Resources));

                if (Settings.Get(SettingsType.ValidateBuildingsNumberPrevious) && !ValidatePreviousAvailableLimit(scoreData, p => p.BuildingsCount[mapEntry.Key], mapEntry.Value, CreateResourcesFormatData(Resource.String.message_building_number_previous_exceed, mapEntry.Key.GetEnumDescription(Context.Resources))))
                    return false;
            }

            int playerBonusCardsMax = 1;
            if (PlayersCount < 6)
                playerBonusCardsMax++;
            if (PlayersCount < 4)
                playerBonusCardsMax++;

            foreach (PlayerScoreData playerScoreData in scoreData)
            {
                if (Settings.Get(SettingsType.ValidateBonusCardsPlayer) && playerScoreData.BonusCardsBuildingsCount.Sum(c => c.Value) > playerBonusCardsMax)
                    return CheckFailed(Resource.String.message_bonus_cards_player_exceed, playerScoreData.PlayerName);

                foreach (KeyValuePair<BuildingType, int> mapEntry in BonusCardsMaxCount)
                {
                    if (Settings.Get(SettingsType.ValidateBonusCardsBuildings) && playerScoreData.ExtensionsBuildingsCount[mapEntry.Key] > playerScoreData.BuildingsCount[mapEntry.Key])
                        return CheckFailed(Resource.String.message_bonus_cards_buildings_player_exceed, playerScoreData.PlayerName, mapEntry.Key.GetEnumDescription(Context.Resources));
                }
            }
            foreach (KeyValuePair<BuildingType, int> mapEntry in BonusCardsMaxCount)
            {
                if (Settings.Get(SettingsType.ValidateBonusCards) && scoreData.Sum(p => p.BonusCardsBuildingsCount[mapEntry.Key]) > mapEntry.Value)
                    return CheckFailed(Resource.String.message_bonus_cards_exceed, mapEntry.Key.GetEnumDescription(Context.Resources));
            }

            Dictionary<BuildingType, int> squaresTotalMinimumNumber = new Dictionary<BuildingType, int>();
            foreach (PlayerScoreData playerScoreData in scoreData)
            {
                int playerSquaresTotalMinimumNumber = 0;
                foreach (BuildingType building in BuildingsOrder)
                {
                    int squaresPoints = playerScoreData.SquaresBuildingsCount[building];
                    int squaresMinimumNumber = GetMinimumNumberFromCount(squaresPoints, 3);
                    playerSquaresTotalMinimumNumber += squaresMinimumNumber;

                    int squareTotalMinimumNumber = 0;
                    if (squaresTotalMinimumNumber.ContainsKey(building))
                        squareTotalMinimumNumber = squaresTotalMinimumNumber[building];
                    squareTotalMinimumNumber += squaresMinimumNumber;
                    squaresTotalMinimumNumber[building] = squareTotalMinimumNumber;

                    if (Settings.Get(SettingsType.ValidateSquaresPlayer) && squaresMinimumNumber > 3)
                        return CheckFailed(Resource.String.message_squares_buildings_player_exceed, playerScoreData.PlayerName, building.GetEnumDescription(Context.Resources));
                }

                if (Settings.Get(SettingsType.ValidateSquaresPlayer) && playerSquaresTotalMinimumNumber > 3)
                    return CheckFailed(Resource.String.message_squares_player_exceed, playerScoreData.PlayerName);
            }
            foreach (KeyValuePair<BuildingType, int> mapEntry in SquaresMaxCount)
            {
                if (Settings.Get(SettingsType.ValidateSquares) && squaresTotalMinimumNumber[mapEntry.Key] > mapEntry.Value)
                    return CheckFailed(Resource.String.message_squares_number_exceed, mapEntry.Key.GetEnumDescription(Context.Resources));
            }

            if (Settings.Get(SettingsType.ValidateMultipleWiseman) && scoreData.Count(p => p.OwnedCharacterTheWiseMan) > 1)
                return CheckFailed(Resource.String.message_multiple_wiseman);
            if (Settings.Get(SettingsType.ValidateMultipleCitywatch) && scoreData.Count(p => p.OwnedCharacterTheCityWatch) > 1)
                return CheckFailed(Resource.String.message_multiple_citywatch);

            if (!CheckPreviousRoundScoringMatch(scoreData, SettingsType.ValidatePreviousWiseman, (previousPlayerScoreData, currentPlayerScoreData) => previousPlayerScoreData.OwnedCharacterTheWiseMan && !currentPlayerScoreData.OwnedCharacterTheWiseMan, playerName => CreateResourcesFormatData(Resource.String.message_previous_wiseman, playerName)))
                return false;

            if (!CheckPreviousRoundScoringMatch(scoreData, SettingsType.ValidatePreviousCitywatch, (previousPlayerScoreData, currentPlayerScoreData) => previousPlayerScoreData.OwnedCharacterTheCityWatch && !currentPlayerScoreData.OwnedCharacterTheCityWatch, playerName => CreateResourcesFormatData(Resource.String.message_previous_citywatch, playerName)))
                return false;

            foreach (PlayerScoreData playerScoreData in scoreData)
            {
                foreach (BuildingType building in BuildingsOrder)
                {
                    if (Settings.Get(SettingsType.ValidateCitizensBuildings) && playerScoreData.StreetTradersNumber[building] > playerScoreData.BuildingsCount[building])
                        return CheckFailed(Resource.String.message_citizens_buildings_exceed, playerScoreData.PlayerName, building.GetEnumDescription(Context.Resources));
                }
            }

            foreach (BuildingType building in BuildingsOrder)
            {
                if (Settings.Get(SettingsType.ValidateCitizens) && scoreData.Sum(p => p.StreetTradersNumber[building]) > 7)
                    return CheckFailed(Resource.String.message_citizens_exceed, building.GetEnumDescription(Context.Resources));
            }

            foreach (PlayerScoreData playerScoreData in scoreData)
            {
                if (Settings.Get(SettingsType.ValidateTreasuresPlayer) && playerScoreData.TreasuresCount > playerScoreData.AllBuildingsCount)
                    return CheckFailed(Resource.String.message_treasures_player_exceed, playerScoreData.PlayerName);
            }
            if (Settings.Get(SettingsType.ValidateTreasures) && scoreData.Sum(p => p.TreasuresCount) > 42)
                return CheckFailed(Resource.String.message_treasures_number_exceed);

            //TODO get miminum bazaars number from points combination
            //TODO to method
            //IEnumerable<int> bazaarsAvailablePoints = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 14, 15, 16, 18, 21, 24 }.GetMatrixSums(8).Distinct();

            int bazaarsTotalPointsSum = 0;
            int bazaarsMinimumNumber = 0;
            foreach (PlayerScoreData playerScoreData in scoreData)
            {
                int bazaarsTotalPoints = playerScoreData.BazaarsTotalPoints;
                //if (!bazaarsAvailablePoints.Contains(bazaarsTotalPoints))
                //    return CheckFailed( $"{playerScoreData.PlayerName}: Niedozwolona ilość punktów z bazarów");
                bazaarsMinimumNumber += GetMinimumNumberFromCount(bazaarsTotalPoints, 24);
                bazaarsTotalPointsSum += bazaarsTotalPoints;
            }
            if (Settings.Get(SettingsType.ValidateBazaarsPoints) && bazaarsTotalPointsSum > 192)
                return CheckFailed(Resource.String.message_bazaars_points_exceed);

            //TODO get miminum counters number from points combination, compare all to max 20
            IEnumerable<int> artOfTheMoorsPlayerAvailablePoints = new List<int>() { 0, 3, 6, 10, 15, 21 }.GetMatrixSums(7).Distinct();

            foreach (PlayerScoreData playerScoreData in scoreData)
            {
                if (Settings.Get(SettingsType.ValidateCultureCounters) && !artOfTheMoorsPlayerAvailablePoints.Contains(playerScoreData.ArtOfTheMoorsPoints))
                    return CheckFailed(Resource.String.message_culture_counters_player_mismatch, playerScoreData.PlayerName);
            }

            if (!CheckPreviousRoundScoringMatch(scoreData, SettingsType.ValidateCultureCountersPrevious, (previousPlayerScoreData, currentPlayerScoreData) => currentPlayerScoreData.ArtOfTheMoorsPoints < previousPlayerScoreData.ArtOfTheMoorsPoints || !artOfTheMoorsPlayerAvailablePoints.Contains(currentPlayerScoreData.ArtOfTheMoorsPoints - previousPlayerScoreData.ArtOfTheMoorsPoints), playerName => CreateResourcesFormatData(Resource.String.message_culture_counters_player_previous_mismatch, playerName)))
                return false;

            if (Settings.Get(SettingsType.ValidateFalcons) && scoreData.Sum(p => p.FalconsBlackNumber) > 5)
                return CheckFailed(Resource.String.message_black_falcons_number_exceed);
            if (Settings.Get(SettingsType.ValidateFalcons) && scoreData.Sum(p => p.FalconsBrownNumber) > 5)
                return CheckFailed(Resource.String.message_brown_falcons_number_exceed);
            if (Settings.Get(SettingsType.ValidateFalcons) && scoreData.Sum(p => p.FalconsWhiteNumber) > 5)
                return CheckFailed(Resource.String.message_white_falcons_number_exceed);

            foreach (PlayerScoreData playerScoreData in scoreData)
            {
                if (Settings.Get(SettingsType.ValidateWatchtowerWall) && playerScoreData.WatchtowersNumber > playerScoreData.WallLength)
                    return CheckFailed(Resource.String.message_watchtower_wall_player_exceed, playerScoreData.PlayerName);
            }
            if (Settings.Get(SettingsType.ValidateWatchtower) && scoreData.Sum(p => p.WatchtowersNumber) > 18)
                return CheckFailed(Resource.String.message_watchtower_number_exceed);

            if (Settings.Get(SettingsType.ValidateMedin) && scoreData.Sum(p => p.MedinasNumber) > 9)
                return CheckFailed(Resource.String.message_medin_number_exceed);

            if (Settings.Get(SettingsType.ValidateMedinPrevious) && !ValidatePreviousAvailableLimit(scoreData, p => p.MedinasNumber, 9, CreateResourcesFormatData(Resource.String.message_medin_number_previous_exceed)))
                return false;

            foreach (PlayerScoreData playerScoreData in scoreData)
                if (Settings.Get(SettingsType.ValidateServants) && (playerScoreData.BuildingsWithoutServantTile > playerScoreData.AllBuildingsCount
                    || (previousBeforeScoreData != null && previousBeforeScoreData[playerScoreData.PlayerNumber - 1].BuildingsWithoutServantTile > playerScoreData.AllBuildingsCount)))
                    return CheckFailed(Resource.String.message_servants_buildings_player_exceed, playerScoreData.PlayerName);

            int faceDownFruitsSum = scoreData.Sum(p => p.FaceDownFruitsCount);
            int allFruits = 0;
            foreach (PlayerScoreData playerScoreData in scoreData)
            {
                if (playerScoreData.CompletedGroupOfFruitBoard1)
                    allFruits += 1;
                if (playerScoreData.CompletedGroupOfFruitBoard2)
                    allFruits += 2;
                if (playerScoreData.CompletedGroupOfFruitBoard3)
                    allFruits += 3;
                if (playerScoreData.CompletedGroupOfFruitBoard4)
                    allFruits += 4;
                if (playerScoreData.CompletedGroupOfFruitBoard5)
                    allFruits += 5;
                if (playerScoreData.CompletedGroupOfFruitBoard6)
                    allFruits += 6;
                allFruits += playerScoreData.FaceDownFruitsCount;
            }
            if (Settings.Get(SettingsType.ValidateSingleFruits) && faceDownFruitsSum > 35)
                return CheckFailed(Resource.String.message_single_fruits_number_exceed);
            if (Settings.Get(SettingsType.ValidateFruits) && allFruits > 56)
                return CheckFailed(Resource.String.message_fruits_number_exceed);

            List<int> wishingWellsAvailablePoints = new List<int>() { 3, 3, 4, 4, 5, 5 }.GetCombinationsSums();

            int wishingWellsPointsSum = scoreData.Sum(p => p.WishingWellsPoints);
            foreach (PlayerScoreData playerScoreData in scoreData)
            {
                if (Settings.Get(SettingsType.ValidateWishingWellsPlayer) && !wishingWellsAvailablePoints.Contains(playerScoreData.WishingWellsPoints))
                    return CheckFailed(Resource.String.message_wishing_wells_player_points_mismatch, playerScoreData.PlayerName);
            }
            if (Settings.Get(SettingsType.ValidateWishingWells) && wishingWellsPointsSum > 24)
                return CheckFailed(Resource.String.message_wishing_wells_points_exceed);

            int playerProjectsMax = 3;
            if (PlayersCount < 4)
                playerProjectsMax--;

            foreach (BuildingType building in BuildingsOrder)
            {
                if (Settings.Get(SettingsType.ValidateMultipleCompletedProject) && scoreData.Count(p => p.CompletedProjects[building]) > playerProjectsMax)
                    return CheckFailed(Resource.String.message_multiple_completed_project, building.GetEnumDescription(Context.Resources));

                if (!CheckPreviousRoundScoringMatch(scoreData, SettingsType.ValidatePreviousCompletedProject, (previousPlayerScoreData, currentPlayerScoreData) => previousPlayerScoreData.CompletedProjects[building] && !currentPlayerScoreData.CompletedProjects[building], playerName => CreateResourcesFormatData(Resource.String.message_previous_completed_project, playerName, building.GetEnumDescription(Context.Resources))))
                    return false;
            }

            int animalsPointsSum = scoreData.Sum(p => p.AnimalsPoints);
            if (Settings.Get(SettingsType.ValidateAnimals) && animalsPointsSum > 22)
                return CheckFailed(Resource.String.message_animals_number_exceed);

            if (Settings.Get(SettingsType.ValidateAnimalsPrevious) && !ValidatePreviousAvailableLimit(scoreData, p => p.AnimalsPoints, 24, CreateResourcesFormatData(Resource.String.message_animals_number_previous_exceed)))
                return false;

            foreach (BuildingType building in BuildingsOrder)
            {
                if (Settings.Get(SettingsType.ValidateMultipleSemiBuildings) && scoreData.Count(p => p.OwnedSemiBuildings[building]) > 1)
                    return CheckFailed(Resource.String.message_multiple_semi_buildings, building.GetEnumDescription(Context.Resources));
            }

            foreach (BuildingType building in BuildingsOrder)
            {
                if (!CheckPreviousRoundScoringMatch(scoreData, SettingsType.ValidatePreviousSemiBuildings, (previousPlayerScoreData, currentPlayerScoreData) => previousPlayerScoreData.OwnedSemiBuildings[building] && !currentPlayerScoreData.OwnedSemiBuildings[building], playerName => CreateResourcesFormatData(Resource.String.message_previous_semi_buildings, playerName, building.GetEnumDescription(Context.Resources))))
                    return false;
            }

            int blackDiceTotalPipsSum = 0;
            int blackDicesMinimumNumber = 0;
            foreach (PlayerScoreData playerScoreData in scoreData)
            {
                int blackDiceTotalPips = playerScoreData.BlackDiceTotalPips;
                blackDicesMinimumNumber += GetMinimumNumberFromCount(blackDiceTotalPips, 6);
                blackDiceTotalPipsSum += blackDiceTotalPips;
            }
            if (Settings.Get(SettingsType.ValidateBlackDicePips) && blackDiceTotalPipsSum > 18)
                return CheckFailed(Resource.String.message_black_dice_pips_number_exceed);

            if (Settings.Get(SettingsType.ValidateBlackDicesPrevious) && !ValidatePreviousAvailableLimit(scoreData, p => GetMinimumNumberFromCount(p.BlackDiceTotalPips, 6), 3, CreateResourcesFormatData(Resource.String.message_black_dices_number_previous_exceed)))
                return false;

            foreach (BuildingType building in BuildingsOrder)
            {
                foreach (PlayerScoreData playerScoreData in scoreData)
                {
                    if (Settings.Get(SettingsType.ValidateExtensionsBuildings) && playerScoreData.ExtensionsBuildingsCount[building] > playerScoreData.BuildingsCount[building])
                        return CheckFailed(Resource.String.message_extensions_buildings_player_exceed, playerScoreData.PlayerName, building.GetEnumDescription(Context.Resources));
                }
                if (Settings.Get(SettingsType.ValidateExtensions) && scoreData.Sum(p => p.ExtensionsBuildingsCount[building]) > 2)
                    return CheckFailed(Resource.String.message_extensions_exceed, building.GetEnumDescription(Context.Resources));
            }

            foreach (PlayerScoreData playerScoreData in scoreData)
            {
                if (Settings.Get(SettingsType.ValidateHandymen) && playerScoreData.HandymenTilesHighestNumber > 8)
                    return CheckFailed(Resource.String.message_handymen_exceed, playerScoreData.PlayerName);
            }

            List<int> treasuresAvailableValues = new List<int>() { 1, 2, 3, 4, 5, 6 }.GetCombinationsSums();

            foreach (PlayerScoreData playerScoreData in scoreData)
            {
                if (Settings.Get(SettingsType.ValidateTreasuresPoints) && !treasuresAvailableValues.Contains(playerScoreData.TreasuresPoints))
                    return CheckFailed(Resource.String.message_treasures_points_player_mismatch, playerScoreData.PlayerName);
            }

            foreach (PlayerScoreData playerScoreData in scoreData)
            {
                int wallLength = playerScoreData.WallLength;
                int secondLongestWallLength = playerScoreData.SecondLongestWallLength;
                if (Settings.Get(SettingsType.ValidateSecondLongestWall) && wallLength < secondLongestWallLength)
                    return CheckFailed(Resource.String.message_second_longest_wall_player_exceed, playerScoreData.PlayerName);
            }

            foreach (GranadaBuildingType building in GranadaBuildingsOrder)
            {
                int playersBuildings = scoreData.Sum(p => p.GranadaBuildingsCount[building]);

                if (Settings.Get(SettingsType.ValidateBuildingsNumber) && playersBuildings > 6)
                    return CheckFailed(Resource.String.message_building_number_exceed, building.GetEnumDescription(Context.Resources));

                if (Settings.Get(SettingsType.ValidateBuildingsNumberPrevious) && !ValidatePreviousAvailableLimit(scoreData, p => p.GranadaBuildingsCount[building], 6, CreateResourcesFormatData(Resource.String.message_building_number_previous_exceed, building.GetEnumDescription(Context.Resources))))
                    return false;
            }

            foreach (PlayerScoreData playerScoreData in scoreData)
            {
                if (Settings.Get(SettingsType.ValidateMoatwall) && playerScoreData.WallMoatCombinationLength > playerScoreData.WallLength)
                    return CheckFailed(Resource.String.message_moatwall_wall_player, playerScoreData.PlayerName);
                if (Settings.Get(SettingsType.ValidateMoatwall) && playerScoreData.WallMoatCombinationLength > playerScoreData.MoatLength)
                    return CheckFailed(Resource.String.message_moatwall_moat_player, playerScoreData.PlayerName);
            }

            foreach (PlayerScoreData playerScoreData in scoreData)
            {
                int playerTilesMaxCount = playerScoreData.AllBuildingsCount + AdditionalAvailableTilesCount;

                int otherPlayersMinBathhousesCount = 0;
                int otherPlayersMinWishingWellsCount = 0;
                for (int j = 0; j < PlayersCount; j++)
                    if (j != playerScoreData.PlayerNumber - 1)
                    {
                        if (HasModule(ExpansionModule.DesignerBathhouses))
                            if (scoreData[j].BathhousesPoints > 0)
                                otherPlayersMinBathhousesCount ++;
                        if (HasModule(ExpansionModule.DesignerWishingWell))
                            if (scoreData[j].WishingWellsPoints > 0)
                                otherPlayersMinWishingWellsCount ++;
                    }
                playerTilesMaxCount -= otherPlayersMinBathhousesCount;
                playerTilesMaxCount -= otherPlayersMinWishingWellsCount;

                //TODO walidacja moat length
                if (Settings.Get(SettingsType.ValidateWallLength) && playerScoreData.WallLength > playerTilesMaxCount * 2 + 2)
                    return CheckFailed(Resource.String.message_wall_length_player_exceed, playerScoreData.PlayerName);

                if (HasModule(ExpansionModule.DesignerBathhouses))
                {
                    if (Settings.Get(SettingsType.ValidateBathhouses) && playerScoreData.BathhousesPoints > (playerTilesMaxCount - 1) * 4 * (6 - otherPlayersMinBathhousesCount))
                        return CheckFailed(Resource.String.message_bathhouses_points_player_exceed, playerScoreData.PlayerName);
                }
                if (HasModule(ExpansionModule.FanCaliphsGuidelines))
                {
                    if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission1))
                        if (Settings.Get(SettingsType.ValidateMissions) && playerScoreData.Mission1Count > playerTilesMaxCount / 3)
                            return CheckFailed(Resource.String.message_mission1_player_exceed, playerScoreData.PlayerName);
                    if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission2))
                        if (Settings.Get(SettingsType.ValidateMissions) && playerScoreData.Mission2Count > playerTilesMaxCount / 3)
                            return CheckFailed(Resource.String.message_mission2_player_exceed, playerScoreData.PlayerName);
                    if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission5))
                        if (Settings.Get(SettingsType.ValidateMissions) && playerScoreData.Mission5Count > (playerTilesMaxCount + 1) / 2)
                            return CheckFailed(Resource.String.message_mission5_player_exceed, playerScoreData.PlayerName);
                    if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission8))
                        if (Settings.Get(SettingsType.ValidateMissions) && playerScoreData.Mission8Count > playerTilesMaxCount - 1)
                            return CheckFailed(Resource.String.message_mission8_player_exceed, playerScoreData.PlayerName);
                }
                if (HasModule(ExpansionModule.ExpansionInvaders))
                {
                    if (Settings.Get(SettingsType.ValidateUnprotectedSides) && playerScoreData.UnprotectedSidesCount > playerTilesMaxCount)
                        return CheckFailed(Resource.String.message_unprotected_sides_count_player_exceed, playerScoreData.PlayerName);
                    if (Settings.Get(SettingsType.ValidateUnprotectedSides) && playerScoreData.UnprotectedSidesNeighbouringCount > playerTilesMaxCount)
                        return CheckFailed(Resource.String.message_unprotected_sides_neighbouring_count_player_exceed, playerScoreData.PlayerName);
                }
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

        private double GetBuildingCount(PlayerScoreData scoreData, BuildingType buildingType, bool withBonuses = true)
        {
            double alhambraCount = scoreData.BuildingsCount[buildingType];
            if (withBonuses)
            {
                //Bonus Cards: extra buildings
                if (HasModule(ExpansionModule.ExpansionBonusCards))
                    alhambraCount += scoreData.BonusCardsBuildingsCount[buildingType];
                //Squares: square count value
                if (HasModule(ExpansionModule.ExpansionSquares))
                    alhambraCount += scoreData.SquaresBuildingsCount[buildingType];
                if (HasModule(ExpansionModule.ExpansionCharacters)
                    && scoreData.OwnedCharacterTheWiseMan && scoreData.TheWiseManBuildingType == buildingType)
                    alhambraCount += 0.5;
                //Extensions: extended buildings
                if (HasModule(ExpansionModule.DesignerExtensions))
                    alhambraCount += scoreData.ExtensionsBuildingsCount[buildingType];
                //Gates without End: semi-buildings
                if (alhambraCount >= 1 && HasModule(ExpansionModule.DesignerGatesWithoutEnd)
                    && scoreData.OwnedSemiBuildings[buildingType])
                    alhambraCount += 0.5;
            }

            return alhambraCount;
        }

        private double GetMedinaCount(PlayerScoreData scoreData)
        {
            double medinaCount = scoreData.MedinasNumber;
            if (scoreData.MedinaHighestPrice != null)
                medinaCount += ((double)scoreData.MedinaHighestPrice) / 20;
            return medinaCount;
        }

        private double GetGranadaBuildingCount(PlayerScoreData scoreData, GranadaBuildingType building)
        {
            double buildingCount = scoreData.GranadaBuildingsCount[building];
            if (scoreData.GranadaBuildingsHighestPrices != null && scoreData.GranadaBuildingsHighestPrices.ContainsKey(building))
                buildingCount += ((double)scoreData.GranadaBuildingsHighestPrices[building]) / 20;
            return buildingCount;
        }

        public int GetBuildingScore(List<PlayerScoreData> scoreData, BuildingType buildingType, int playerNumber, bool withBonuses = true)
        {
            return CountRelativeScore(playerNumber, (int i) => GetBuildingCount(scoreData[i], buildingType, withBonuses), Scoring[buildingType], HighestLowest.Highest, UpDown.Down);
        }

        public void Score(List<PlayerScoreData> scoreData)
        {
            List<(ScoreDetails scoreDetails1, ScoreDetails scoreDetails2, ScoreDetails scoreDetails3, ScoreDetails scoreMeantime)> initialScoring = Players.Select(p => (p.ScoreDetails1.Copy(), p.ScoreDetails2.Copy(), p.ScoreDetails3.Copy(), p.ScoreMeantime.Copy())).ToList();

            if (GranadaOption != GranadaOption.Alone)
            {
                //wall
                foreach (PlayerScoreData playerScoreData in scoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.WallLength, ScoreType.WallLength);

                //each kind of building
                foreach (KeyValuePair<BuildingType, List<int>[]> scoring in Scoring)
                {
                    foreach (PlayerScoreData playerScoreData in scoreData)
                    {
                        int buildingScore = GetBuildingScore(scoreData, scoring.Key, playerScoreData.PlayerNumber);
                        int buildingScoreBonus = buildingScore - GetBuildingScore(scoreData, scoring.Key, playerScoreData.PlayerNumber, false);
                        if (buildingScoreBonus < 0)
                            buildingScoreBonus = 0;

                        playerScoreData.Player.AddScore(buildingScore, BuildingBaseScoreType[scoring.Key]);
                        playerScoreData.Player.AddScore(buildingScoreBonus, ScoreType.BuildingsBonuses);
                    }
                }
            }

            if (HasModule(ExpansionModule.ExpansionCharacters))
            {
                //Characters: owned The City Watch
                foreach (PlayerScoreData playerScoreData in scoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.OwnedCharacterTheCityWatch ? playerScoreData.WallLength / 3 : 0, ScoreType.TheCityWatch);
            }

            if (HasModule(ExpansionModule.ExpansionCamps))
            {
                //Camps: buildings joined together in a straight, uninterrupted line
                foreach (PlayerScoreData playerScoreData in scoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.CampsPoints, ScoreType.Camps);
            }

            if (HasModule(ExpansionModule.ExpansionStreetTrader))
            {
                //Street Trader: sets based on the number of different colored citizens
                foreach (PlayerScoreData playerScoreData in scoreData)
                {
                    int pointsSum = 0;
                    for (int j = 0; j < 7; j++)
                    {
                        int setDifferentCitizens = playerScoreData.StreetTradersNumber.Where(t => t.Value > j).Count();
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
                    playerScoreData.Player.AddScore(pointsSum, ScoreType.StreetTraders);
                }
            }

            if (HasModule(ExpansionModule.ExpansionTreasureChamber))
            {
                //Treasure Chamber: chests
                foreach (PlayerScoreData playerScoreData in scoreData)
                {
                    int chestsScore = CountRelativeScore(playerScoreData.PlayerNumber, (int j) => scoreData[j].TreasuresCount, TreasureChamberScoring, HighestLowest.Highest, UpDown.Down);

                    playerScoreData.Player.AddScore(chestsScore, ScoreType.TreasureChamber);
                }
            }

            if (HasModule(ExpansionModule.ExpansionInvaders))
            {
                //Invaders: each side unprotected from the main direction of the attack
                foreach (PlayerScoreData playerScoreData in scoreData)
                    if (!playerScoreData.Player.Dirk)
                    {
                        playerScoreData.Player.RemoveScore(playerScoreData.UnprotectedSidesCount * RoundNumber, false, ScoreType.Invaders);
                        if (RoundNumber == 3)
                            playerScoreData.Player.RemoveScore(playerScoreData.UnprotectedSidesNeighbouringCount, false, ScoreType.Invaders);
                    }
            }

            if (HasModule(ExpansionModule.ExpansionBazaars))
            {
                if (RoundNumber == 3)
                {
                    //Bazaars: points for bazaars
                    foreach (PlayerScoreData playerScoreData in scoreData)
                        if (!playerScoreData.Player.Dirk)
                            playerScoreData.Player.AddScore(playerScoreData.BazaarsTotalPoints, ScoreType.Bazaars);
                }
            }

            if (HasModule(ExpansionModule.ExpansionArtOfTheMoors))
            {
                //Art of the Moors: points for the culture counters
                foreach (PlayerScoreData playerScoreData in scoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.ArtOfTheMoorsPoints, ScoreType.ArtOfTheMoors);
            }

            if (HasModule(ExpansionModule.ExpansionFalconers))
            {
                //Falconers: points for each type of falcons
                foreach (PlayerScoreData playerScoreData in scoreData)
                    if (!playerScoreData.Player.Dirk)
                    {
                        foreach (int falconsNumber in new int[] { playerScoreData.FalconsBlackNumber, playerScoreData.FalconsBrownNumber, playerScoreData.FalconsWhiteNumber })
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
                            playerScoreData.Player.AddScore(points, ScoreType.Falconers);
                        }
                    }
            }

            if (HasModule(ExpansionModule.ExpansionWatchtowers))
            {
                //Watchtowers: Each watchtower that is part of a player’s longest contiguous wall
                foreach (PlayerScoreData playerScoreData in scoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.WatchtowersNumber * 2, ScoreType.Watchtowers);
            }

            if (HasModule(ExpansionModule.QueenieMedina))
            {
                //Medina
                foreach (PlayerScoreData playerScoreData in scoreData)
                {
                    int medinaScore = CountRelativeScore(playerScoreData.PlayerNumber, (int j) => GetMedinaCount(scoreData[j]), MedinaScoring, HighestLowest.Lowest, UpDown.Up, MedinaZeroPenaltiesScoring);

                    playerScoreData.Player.RemoveScore(medinaScore, true, ScoreType.Medina);
                }
            }

            if (HasModule(ExpansionModule.DesignerPalaceStaff))
            {
                if (RoundNumber != 3)
                {
                    //Palace Staff: each building without a servant tile
                    foreach (PlayerScoreData playerScoreData in scoreData)
                        if (!playerScoreData.Player.Dirk)
                            playerScoreData.Player.RemoveScore(playerScoreData.BuildingsWithoutServantTile, false, ScoreType.BuildingsWithoutServantTile);
                }
            }

            if (HasModule(ExpansionModule.DesignerOrchards))
            {
                if (RoundNumber == 3)
                {
                    //Orchards: fruits
                    foreach (PlayerScoreData playerScoreData in scoreData)
                        if (!playerScoreData.Player.Dirk)
                        {
                            if (playerScoreData.CompletedGroupOfFruitBoard1)
                                playerScoreData.Player.AddScore(1, ScoreType.Orchards);
                            if (playerScoreData.CompletedGroupOfFruitBoard2)
                                playerScoreData.Player.AddScore(2, ScoreType.Orchards);
                            if (playerScoreData.CompletedGroupOfFruitBoard3)
                                playerScoreData.Player.AddScore(4, ScoreType.Orchards);
                            if (playerScoreData.CompletedGroupOfFruitBoard4)
                                playerScoreData.Player.AddScore(7, ScoreType.Orchards);
                            if (playerScoreData.CompletedGroupOfFruitBoard5)
                                playerScoreData.Player.AddScore(11, ScoreType.Orchards);
                            if (playerScoreData.CompletedGroupOfFruitBoard6)
                                playerScoreData.Player.AddScore(16, ScoreType.Orchards);
                            playerScoreData.Player.AddScore(playerScoreData.FaceDownFruitsCount, ScoreType.Orchards);
                        }
                }
            }

            if (HasModule(ExpansionModule.DesignerBathhouses))
            {
                //Bathhouses: distances of the first building
                foreach (PlayerScoreData playerScoreData in scoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.BathhousesPoints, ScoreType.Bathhouses);
            }

            if (HasModule(ExpansionModule.DesignerWishingWell))
            {
                //Wishing Well: tiles in a straight line from the waterspout
                foreach (PlayerScoreData playerScoreData in scoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.WishingWellsPoints, ScoreType.WishingWells);
            }

            if (HasModule(ExpansionModule.DesignerFreshColors))
            {
                //Fresh Colors: completed projects
                foreach (PlayerScoreData playerScoreData in scoreData)
                    if (!playerScoreData.Player.Dirk)
                    {
                        foreach (KeyValuePair<BuildingType, bool> completedProject in playerScoreData.CompletedProjects)
                        {
                            if (completedProject.Value)
                                playerScoreData.Player.AddScore(playerScoreData.BuildingsCount[completedProject.Key] * 2, ScoreType.CompletedProjects);
                        }
                    }
            }

            if (HasModule(ExpansionModule.DesignerAlhambraZoo))
            {
                //Alhambra Zoo: animals points
                foreach (PlayerScoreData playerScoreData in scoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.AnimalsPoints, ScoreType.Animals);
            }

            if (HasModule(ExpansionModule.DesignerBuildingsOfPower))
            {
                //Buildings of Power: Building of Strength
                foreach (PlayerScoreData playerScoreData in scoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(Math.Min(playerScoreData.SecondLongestWallLength, playerScoreData.BlackDiceTotalPips), ScoreType.BlackDices);
            }

            if (HasModule(ExpansionModule.DesignerHandymen))
            {
                //Handymen: highest number of adjacent tiles occupied by handymen
                foreach (PlayerScoreData playerScoreData in scoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.HandymenTilesHighestNumber, ScoreType.Handymen);
            }

            if (HasModule(ExpansionModule.FanTreasures))
            {
                //Treasures: treasures' value
                if (RoundNumber == 3)
                {
                    foreach (PlayerScoreData playerScoreData in scoreData)
                        if (!playerScoreData.Player.Dirk)
                            playerScoreData.Player.AddScore(playerScoreData.TreasuresPoints, ScoreType.Treasures);
                }
            }

            if (HasModule(ExpansionModule.FanCaliphsGuidelines))
            {
                if (RoundNumber == 3)
                {
                    foreach (PlayerScoreData playerScoreData in scoreData)
                        if (!playerScoreData.Player.Dirk)
                        {
                            if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission1))
                                //Caliph’s Guidelines: mission 1
                                playerScoreData.Player.AddScore(playerScoreData.Mission1Count * 3, ScoreType.Mission1);
                            if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission2))
                                //Caliph’s Guidelines: mission 2
                                playerScoreData.Player.AddScore(playerScoreData.Mission2Count * 3, ScoreType.Mission2);
                            if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission3))
                                //Caliph’s Guidelines: mission 3
                                playerScoreData.Player.AddScore(playerScoreData.Mission3Count * 3, ScoreType.Mission3);
                            if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission4))
                                //Caliph’s Guidelines: mission 4
                                playerScoreData.Player.AddScore(playerScoreData.SecondLongestWallLength, ScoreType.Mission4);
                            if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission5))
                                //Caliph’s Guidelines: mission 5
                                playerScoreData.Player.AddScore(playerScoreData.Mission5Count * 2, ScoreType.Mission5);
                            if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission6))
                                //Caliph’s Guidelines: mission 6
                                playerScoreData.Player.AddScore(playerScoreData.Mission6Count * 3, ScoreType.Mission6);
                            if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission7))
                                //Caliph’s Guidelines: mission 7
                                switch (playerScoreData.BuildingsCount.Count(b => b.Value != 0))
                                {
                                    case 2:
                                        playerScoreData.Player.AddScore(1, ScoreType.Mission7);
                                        break;
                                    case 3:
                                        playerScoreData.Player.AddScore(3, ScoreType.Mission7);
                                        break;
                                    case 4:
                                        playerScoreData.Player.AddScore(6, ScoreType.Mission7);
                                        break;
                                    case 5:
                                        playerScoreData.Player.AddScore(10, ScoreType.Mission7);
                                        break;
                                    case 6:
                                        playerScoreData.Player.AddScore(15, ScoreType.Mission7);
                                        break;
                                }
                            if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission8))
                                //Caliph’s Guidelines: mission 8
                                playerScoreData.Player.AddScore(playerScoreData.Mission8Count, ScoreType.Mission8);
                            if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission9))
                                //Caliph’s Guidelines: mission 9
                                playerScoreData.Player.AddScore(playerScoreData.Mission9Count * 2, ScoreType.Mission9);
                        }
                }
            }

            if (HasModule(ExpansionModule.Granada))
            {
                //moat
                foreach (PlayerScoreData playerScoreData in scoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.MoatLength, ScoreType.MoatLength);

                //each kind of building
                foreach (GranadaBuildingType building in GranadaBuildingsOrder)
                {
                    foreach (PlayerScoreData playerScoreData in scoreData)
                    {
                        int buildingScore = CountRelativeScore(playerScoreData.PlayerNumber, (int j) => GetGranadaBuildingCount(scoreData[j], building), GetGranadaScoring(scoreData, building), HighestLowest.Highest, UpDown.Down);

                        playerScoreData.Player.AddScore(buildingScore, GranadaBuildingBaseScoreType[building]);
                    }
                }
            }

            if (GranadaOption == GranadaOption.With)
            {
                foreach (PlayerScoreData playerScoreData in scoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.WallMoatCombinationLength * 2, ScoreType.WallMoatCombination);
            }

            ScoreStack.Push(new ScoreHistoryRound(this, ScoreRound, initialScoring));

            RoundsScoring[RoundNumber - 1] = scoreData;
        }

        public bool ValidateScoreBeforeAssignLeftoverBuildings(List<PlayerScoreData> scoreData)
        {
            return true;
        }

        public void ScoreBeforeAssignLeftoverBuildings(List<PlayerScoreData> scoreData)
        {
            List<(ScoreDetails scoreDetails1, ScoreDetails scoreDetails2, ScoreDetails scoreDetails3, ScoreDetails scoreMeantime)> initialScoring = Players.Select(p => (p.ScoreDetails1.Copy(), p.ScoreDetails2.Copy(), p.ScoreDetails3.Copy(), p.ScoreMeantime.Copy())).ToList();

            if (HasModule(ExpansionModule.DesignerPalaceStaff))
            {
                //Palace Staff: each building without a servant tile
                foreach (PlayerScoreData playerScoreData in scoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.RemoveScore(playerScoreData.BuildingsWithoutServantTile, false, ScoreType.BuildingsWithoutServantTile);
            }

            ScoreStack.Push(new ScoreHistoryRound(this, ScoreRound, initialScoring));

            ThirdBeforeRoundScoring = scoreData;
        }

        public void RoundRevertScore(ScoringRound scoreRound, List<(ScoreDetails scoreDetails1, ScoreDetails scoreDetails2, ScoreDetails scoreDetails3, ScoreDetails scoreMeantime)> initialScoring)
        {
            ScoreRound = scoreRound;
            for (int i = 0; i < PlayersCount; i++)
            {
                Players[i].RestoreScore(initialScoring[i]);
            }
        }

        public void BackRound()
        {
            ScoreHistoryRound scoreHistory = (ScoreHistoryRound)ScoreStack.Peek();
            ScoreRound = scoreHistory.ScoreRound;
        }

        public void RevertScoring()
        {
            ScoreStack.Pop().Revert();
            ResetFinish();
        }

        //    RoundsScoring

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

        private ResourcesFormatData CreateResourcesFormatData(int resourceId, params object[] args)
        {
            return new ResourcesFormatData(Context, resourceId, args);
        }
        private bool CheckFailed(int resourceId, params string[] args)
        {
            return CheckFailed(CreateResourcesFormatData(resourceId, args).Message);
        }
        private bool CheckFailed(string text)
        {
            return AndroidValidateUtils.CheckFailed(Context, text);
        }
    }
}