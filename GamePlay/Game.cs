using AlhambraBase;
using AlhambraScoringAndroid.Attributes;
using AlhambraScoringAndroid.Options;
using AlhambraScoringAndroid.Tools;
using Android.Content;
using AndroidBase.Options;
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
        public static Dictionary<AlhambraBase.BuildingType, ScoreType> BuildingBaseScoreType = new Dictionary<AlhambraBase.BuildingType, ScoreType>()
        {
            [AlhambraBase.BuildingType.Pavilion] = ScoreType.PavilionNumber,
            [AlhambraBase.BuildingType.Seraglio] = ScoreType.SeraglioNumber,
            [AlhambraBase.BuildingType.Arcades] = ScoreType.ArcadesNumber,
            [AlhambraBase.BuildingType.Chambers] = ScoreType.ChambersNumber,
            [AlhambraBase.BuildingType.Garden] = ScoreType.GardenNumber,
            [AlhambraBase.BuildingType.Tower] = ScoreType.TowerNumber
        };
        public static Dictionary<AlhambraBase.GranadaBuildingType, ScoreType> GranadaBuildingBaseScoreType = new Dictionary<AlhambraBase.GranadaBuildingType, ScoreType>()
        {
            [AlhambraBase.GranadaBuildingType.Arena] = ScoreType.Arena,
            [AlhambraBase.GranadaBuildingType.BathHouse] = ScoreType.BathHouse,
            [AlhambraBase.GranadaBuildingType.Library] = ScoreType.Library,
            [AlhambraBase.GranadaBuildingType.Hostel] = ScoreType.Hostel,
            [AlhambraBase.GranadaBuildingType.Hospital] = ScoreType.Hospital,
            [AlhambraBase.GranadaBuildingType.Market] = ScoreType.Market,
            [AlhambraBase.GranadaBuildingType.Park] = ScoreType.Park,
            [AlhambraBase.GranadaBuildingType.School] = ScoreType.School,
            [AlhambraBase.GranadaBuildingType.ResidentialArea] = ScoreType.ResidentialArea
        };

        private static readonly List<(int, int)> wishingWellsAvailablePoints = new List<int>() { 3, 3, 4, 4, 5, 5 }.GetCombinationsSumsWithCount();
        private static readonly List<int> treasuresAvailableValues = new List<int>() { 1, 2, 3, 4, 5, 6 }.GetCombinationsSums();
        //private static readonly List<int> bazaarsAvailablePoints = new List<int>() {  1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 14, 15, 16, 18, 21, 24 }.GetMatrixSums(8)/*.Distinct().ToList()*/;
        private static readonly List<(int, int)> artOfTheMoorsPlayerAvailablePoints = new List<int>() { 3, 6, 10, 15, 21 }.GetMatrixSumsWithCount(7)/*.Distinct().ToList()*/;

        public readonly Context Context;

        public DateTime StartDateTime { get; private set; }
        public DateTime? EndDateTime { get; private set; }

        private List<AlhambraBase.ExpansionModule> Modules;
        public GranadaOption GranadaOption { get; private set; }
        public AlcazabaOption AlcazabaOption { get; private set; }
        private List<NewScoreCard> NewScoreCards;
        private List<CaliphsGuidelinesMission> CaliphsGuidelines;
        private List<Player> Players;
        public ScoringRound ScoreRound { get; private set; }
        public Stack<ScoreHistory> ScoreStack { get; private set; }
        public bool Saved { get; set; }
        public RoundScoring[] RoundsScoring { get; private set; }
        public RoundScoring ThirdBeforeRoundScoring { get; private set; }
        public RoundScoring PreviousRoundScoring => RoundNumber != 1 ? (ScoreRound == ScoringRound.Finish ? RoundsScoring[2] : RoundsScoring[RoundNumber - 2]) : null;
        public int PointsGuardsUsingSubstitute => RoundNumber * 1;

        public Dictionary<AlhambraBase.BuildingType, List<int>[]> Scoring => GameConstants.BuildingsOrder.ToDictionary(b => b, b => new List<int>[] { GetBuildingRoundScoring(b, 1), GetBuildingRoundScoring(b, 2), GetBuildingRoundScoring(b, 3) });
        public List<int>[] GetGranadaScoring(List<PlayerScoreData> scoreData, AlhambraBase.GranadaBuildingType building)
        {
            int sum = scoreData.Sum(p => p.GranadaBuildingsCount[building]);
            return new List<int>[] { new List<int> { sum }, new List<int> { sum * 2, sum }, new List<int> { sum * 3, sum * 2, sum } };
        }

        private List<int> GetBuildingRoundScoring(AlhambraBase.BuildingType buildingType, int roundNumber)
        {
            List<AlhambraBase.BuildingType> buildingsOrder = NewScoreCards != null ? NewScoreCards[roundNumber - 1].GetEnumAttribute<NewScoreCard, NewScoreCardAttribute>().BuildingTypes : GameConstants.BuildingsOrder;
            return GameConstants.ScoringByPosition[buildingsOrder.IndexOf(buildingType)][roundNumber - 1];
        }

        public Dictionary<AlhambraBase.BuildingType, int> BaseBuildingsMaxCount
        {
            get
            {
                if (HasModule(AlhambraBase.ExpansionModule.RedPalaceLandTiles))
                    return GameConstants.BaseBuildingsMaxCountRedPalace;

                if (AlcazabaOption == AlcazabaOption.WithTile)
                    return GameConstants.BaseBuildingsMaxCountAlcazaba;

                return GameConstants.BaseBuildingsMaxCount;
            }
        }
        public int GranadaBuildingsTypeMaxCount => 6;
        public int PlayerSquaresPointsMaxCount => 9;
        public int PlayerSquaresMaxCount => 3;
        public int StreetTradersTypeMaxCount => 7;
        public int TreasuresMaxCount => 42;
        public int BazaarsMaxCount => 8;
        public int BazaarPointsMaxCount => 24;
        public int ArtOfTheMoorsMaxCount => 20;
        public int FalconsTypeMaxCount => 5;
        public int AllWatchtowersNumber => 18;
        public int AllMedinasNumber => 9;
        public int MedinaMinPrice => 2;
        public int MedinaMaxPrice => 10;
        public int AllFruitsMaxCount => 56;
        public int FaceDownFruitsMaxCount => 35;
        public int WishingWellsMaxCount => 6;
        public int WishingWellPointsMaxCount => 4;
        public int PlayerProjectsMaxCount
        {
            get
            {
                int playerProjectsMax = 3;
                if (PlayersCount < 4)
                    playerProjectsMax--;
                return playerProjectsMax;
            }
        }
        public int AnimalsMaxCount => 22;
        public int BlackDicesMaxCount => 3;
        public int BlackDicePipsMaxCount => 6;
        public int ExtensionsBuildingsTypeMaxCount => 2;
        public int PlayerAllHandymenCount => 8;
        public int AllBathhousesCount => 6;
        public int PlayerTreasuresPointsMaxCount => 15;
        public int BuildingsAvailableAdjacent => BaseBuildingsMaxCount.Sum(b => GameConstants.GetBuildingsAvailableAdjacent(b.Value));
        public int AllGuardsCount => 32;
        public int GuardsMaxPoints => 8;
        public List<int> GranadaAvailablePrices => new List<int> { 2, 4, 6, 8, 10, 12 };
        public int GranadaMinPrice => GranadaAvailablePrices.Min();
        public int GranadaMaxPrice => GranadaAvailablePrices.Max();
        public List<int> GranadaPricesExcepts => Enumerable.Range(GranadaMinPrice, GranadaMaxPrice - GranadaMinPrice + 1).Except(GranadaAvailablePrices).ToList();

        public Dictionary<AlhambraBase.BuildingType, int> WallBuildingsMaxCount => BaseBuildingsMaxCount.ToDictionary(b => b.Key, b => b.Value + (HasModule(AlhambraBase.ExpansionModule.QueenieMagicalBuildings) ? 1 : 0) + (HasModule(AlhambraBase.ExpansionModule.DesignerMajorConstructionProjects) ? 5 : 0));
        public Dictionary<AlhambraBase.BuildingType, int> BuildingsMaxCount => WallBuildingsMaxCount.ToDictionary(b => b.Key, b => b.Value + (HasModule(AlhambraBase.ExpansionModule.DesignerNewBuildingGrounds) ? 2 : 0) + (HasModule(AlhambraBase.ExpansionModule.NewMarket) ? 1 : 0));

        public int AllBaseBuildingsCount => BaseBuildingsMaxCount.Sum(b => b.Value);
        public int AllWallBuildingsCount => WallBuildingsMaxCount.Sum(b => b.Value);
        public int AllBuildingsCount => BuildingsMaxCount.Sum(b => b.Value);

        //including 1 starting tile
        public int AdditionalTilesCount
        {
            get
            {
                int tilesCount = 1; //starting tile
                tilesCount += AdditionalWallTilesCount;
                return tilesCount;
            }
        }
        public int AdditionalWallTilesCount
        {
            get
            {
                int tilesCount = 0;
                if (HasModule(AlhambraBase.ExpansionModule.QueenieMedina))
                    tilesCount += 9;
                return tilesCount;
            }
        }
        public int AdditionalAvailableTilesCount
        {
            get
            {
                //int availableTilesCount = 1; //starting tile
                int availableTilesCount = 0;
                if (HasModule(AlhambraBase.ExpansionModule.ExpansionBazaars))
                    availableTilesCount += 8;
                if (HasModule(AlhambraBase.ExpansionModule.DesignerBathhouses))
                    availableTilesCount += 6;
                if (HasModule(AlhambraBase.ExpansionModule.DesignerWishingWell))
                    availableTilesCount += 6;
                if (HasModule(AlhambraBase.ExpansionModule.DesignerGatesWithoutEnd))
                    availableTilesCount += 6;
                if (HasModule(AlhambraBase.ExpansionModule.RedPalaceLandTiles))
                    availableTilesCount += 6;
                availableTilesCount += AdditionalAvailableWallTilesCount;
                return availableTilesCount;
            }
        }
        //including max 3 squares
        public int AdditionalAvailableWallTilesCount
        {
            get
            {
                int availableTilesCount = 0;
                if (HasModule(AlhambraBase.ExpansionModule.ExpansionSquares))
                    availableTilesCount += 3;
                //if (HasModule(AlhambraBase.ExpansionModule.QueenieMedina))
                //    availableTilesCount += 9;
                return availableTilesCount;
            }
        }

        //including 1 starting tile and max 3 squares
        public int AllTilesCount => AllBuildingsCount + AdditionalTilesCount + AdditionalAvailableTilesCount;
        //including max 3 squares
        public int AllWallTilesCount => AllWallBuildingsCount + AdditionalWallTilesCount + AdditionalAvailableWallTilesCount;
        //including 1 starting tile
        public int AllGranadaTilesCount => 1 + 54;

        public int WallAdditionalCount
        {
            get
            {
                int wallCount = 0;
                if (HasModule(AlhambraBase.ExpansionModule.ExpansionWatchtowers))
                    wallCount += 18;
                return wallCount;
            }
        }

        public int WallsMaxLength => CountWallMaxLength(AllWallTilesCount, WallAdditionalCount);
        public int MoatMaxLength => CountWallMaxLength(AllGranadaTilesCount - 1);

        //without starting tile
        private int CountWallMaxLength(int allTilesCount)
        {
            return (allTilesCount + 2) * 2;
        }

        //with starting tile
        private int CountWallMaxLength(int wallTilesCount, int wallAdditionalCount)
        {
            int maxWallLength = CountWallMaxLength(wallTilesCount);

            if (!(HasModule(AlhambraBase.ExpansionModule.ExpansionCityWalls) || HasModule(AlhambraBase.ExpansionModule.DesignerGatesWithoutEnd) || HasModule(AlhambraBase.ExpansionModule.RedPalaceLandTiles)))
            {
                if (wallTilesCount <= 3)
                    maxWallLength--;
                if (wallTilesCount <= 2)
                    maxWallLength--;
                if (wallTilesCount <= 1)
                    maxWallLength--;
                if (wallTilesCount == 0)
                    maxWallLength--;
            }

            maxWallLength += wallAdditionalCount;

            return maxWallLength;
        }

        public int RoundNumber => ScoreRound switch
        {
            ScoringRound.First => 1,
            ScoringRound.Second => 2,
            ScoringRound.ThirdBeforeLeftover => 3,
            ScoringRound.Third => 3,
            //ScoringRound.Finish => 4,
            _ => 0,
        };

        public bool HasThirdBeforeLeftoverRound => HasModule(AlhambraBase.ExpansionModule.DesignerPalaceStaff);

        public int PlayersCount => Players.Count;
        public int PlayersCountWithoutDirk => Players.Count - (InvolvedDirk ? 1 : 0);
        public bool InvolvedDirk => Players.Any(p => p.Dirk);

        public bool GameInProgress => (ScoreRound != ScoringRound.First || (Players != null && Players.Sum(p => p.Score) != 0)) && !Saved;

        public Game(Context context)
        {
            Context = context;
        }

        public void SetModules(IEnumerable<AlhambraBase.ExpansionModule> modules)
        {
            Modules = modules.ToList();
        }
        public void SetGranadaOption(GranadaOption granadaOption)
        {
            GranadaOption = granadaOption;
        }
        public void SetAlcazabaOption(AlcazabaOption alcazabaOption)
        {
            AlcazabaOption = alcazabaOption;
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

        public bool HasModule(AlhambraBase.ExpansionModule module)
        {
            if (module == AlhambraBase.ExpansionModule.Granada)
                return GranadaOption != GranadaOption.Without;
            return Modules.Contains(module) && (GranadaOption != GranadaOption.Alone
                || GameConstants.GranadaCompatibleModules.Contains((AlhambraBase.ExpansionModule)module));
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

        public void Start()
        {
            if (HasModule(AlhambraBase.ExpansionModule.RedPalaceLandTiles))
            {
                for (int i = 0; i < PlayersCount; i++)
                    Players[i].AddScore(5, ScoreType.Starting);
            }
        }

        public void Reset(bool resetPlayers)
        {
            ScoreRound = ScoringRound.First;
            ScoreStack = new Stack<ScoreHistory>();
            RoundsScoring = new RoundScoring[3];
            for (int i = 0; i < RoundsScoring.Length; i++)
                RoundsScoring[i] = new RoundScoring();
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

        public bool ValidatePlayerRemoveScore(int playerNumber, int score)
        {
            if (score > GetPlayer(playerNumber).Score)
                return CheckFailed(Resource.String.message_remove_score_exceeded);

            return true;
        }

        public void PlayerRemoveScore(int playerNumber, int score)
        {
            GetPlayer(playerNumber).RemoveScore(-score, false, ScoreType.Immediately);
            ScoreStack.Push(new ScoreHistoryImmediately(this, playerNumber, -score));
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
        public bool ValidateGuardsPoints(int guardsPoints)
        {
            if (SettingsManager.Get(SettingsType.ValidateGuardsPoints) && guardsPoints > GuardsMaxPoints)
                return CheckFailed(Resource.String.message_guards_points_exceeded);

            if (!CheckPreviousRoundScoringMatch(SettingsType.ValidatePreviousGuardsPoints, (previousScoreData) => previousScoreData.GuardsPoints > guardsPoints, previousScoreData => CreateResourcesFormatData(Resource.String.message_previous_guards_points, previousScoreData.GuardsPoints)))
                return false;

            return true;
        }
        public bool ValidateGranadaBuildingsNumbers(Dictionary<AlhambraBase.GranadaBuildingType, Dictionary<int, int>> playersHighestPrices)
        {
            foreach (AlhambraBase.GranadaBuildingType building in GameConstants.GranadaBuildingsOrder)
            {
                if (playersHighestPrices[building].Select(d => d.Value).Distinct().Count() != playersHighestPrices[building].Count())
                    return CheckFailed(Resource.String.message_building_same_price, building.GetEnumDescription(Context.Resources));
            }
            return true;
        }

        private bool ValidatePreviousAvailableLimit(List<PlayerScoreData> scoreData, Func<PlayerScoreData, int> countAmountMethod, int maxAmount, ResourcesFormatData errorMessage)
        {
            return ValidatePreviousAvailableLimit(RoundNumber, scoreData, countAmountMethod, maxAmount, errorMessage);
        }

        private bool ValidatePreviousAvailableLimit(int roundNumber, List<PlayerScoreData> scoreData, Func<PlayerScoreData, int> countAmountMethod, int maxAmount, ResourcesFormatData errorMessage)
        {
            if (roundNumber == 1)
                return true;

            List<PlayerScoreData> previousScoreData = RoundsScoring[roundNumber - 2].PlayersScoreData;

            int playersPreviousAmount = previousScoreData.Sum(p => countAmountMethod(p));
            int leftAmount = maxAmount - playersPreviousAmount;

            int increase = 0;
            foreach (PlayerScoreData playerScoreData in scoreData)
            {
                if (!(HasModule(AlhambraBase.ExpansionModule.FanPersonalBuildingMarket) && !playerScoreData.Player.Dirk))
                {
                    int difference = countAmountMethod(playerScoreData) - countAmountMethod(previousScoreData[playerScoreData.PlayerNumber - 1]);
                    if (difference > 0)
                        increase += difference;
                }
            }

            if (increase > leftAmount)
                return CheckFailed(errorMessage.Message);

            return ValidatePreviousAvailableLimit(roundNumber - 1, scoreData, countAmountMethod, maxAmount, errorMessage);
        }

        private bool CheckPreviousRoundPlayerScoringMatch(List<PlayerScoreData> scoreData, SettingsType settingsType, Func<PlayerScoreData, PlayerScoreData, bool> findErrorFunction, Func<string, ResourcesFormatData> errorMessageFunction)
        {
            if (PreviousRoundScoring != null && SettingsManager.Get(settingsType))
            {
                foreach (PlayerScoreData playerScoreData in scoreData)
                {
                    if (findErrorFunction(PreviousRoundScoring.PlayersScoreData[playerScoreData.PlayerNumber - 1], playerScoreData))
                        return CheckFailed(errorMessageFunction(playerScoreData.PlayerName).Message);
                }
            }

            return true;
        }

        private bool CheckPreviousRoundScoringMatch(SettingsType settingsType, Func<RoundScoring, bool> findErrorFunction, Func<RoundScoring, ResourcesFormatData> errorMessageFunction)
        {
            if (PreviousRoundScoring != null && SettingsManager.Get(settingsType))
            {
                if (findErrorFunction(PreviousRoundScoring))
                    return CheckFailed(errorMessageFunction(PreviousRoundScoring).Message);
            }

            return true;
        }

        //TODO max from all rounds (including personal market extension)
        private (int playerTilesMaxCount, int playerGranadaTilesMaxCount, int otherPlayersMinBathhousesCount, int otherPlayersMinWishingWellsCount, int otherPlayersMinBazaarscount) GetPlayerTilesMaxCount(PlayerScoreData playerScoreData, List<PlayerScoreData> scoreData, bool applyOtherPlayersMin = true)
        {
            int playerTilesMaxCount = playerScoreData.AllTilesCount + AdditionalAvailableTilesCount;
            int playerGranadaTilesMaxCount = playerScoreData.AllGranadaTilesCount + AdditionalAvailableTilesCount;

            int otherPlayersMinBathhousesCount = 0;
            int otherPlayersMinWishingWellsCount = 0;
            int otherPlayersMinBazaarsCount = 0;
            if (applyOtherPlayersMin)
            {
                for (int j = 0; j < PlayersCount; j++)
                    if (j != playerScoreData.PlayerNumber - 1)
                    {
                        if (HasModule(AlhambraBase.ExpansionModule.ExpansionBazaars))
                            otherPlayersMinBazaarsCount += GameConstants.GetMinimumNumberFromCount(playerScoreData.BazaarsTotalPoints, 24);
                        if (HasModule(AlhambraBase.ExpansionModule.DesignerBathhouses))
                            otherPlayersMinBathhousesCount += GetPlayerMinBathhousesCount(scoreData[j], scoreData);
                        if (HasModule(AlhambraBase.ExpansionModule.DesignerWishingWell))
                            otherPlayersMinWishingWellsCount += wishingWellsAvailablePoints.Where(p => p.Item1 == scoreData[j].WishingWellsPoints).Min(p => p.Item2);
                    }
            }
            playerTilesMaxCount -= otherPlayersMinBathhousesCount + otherPlayersMinWishingWellsCount + otherPlayersMinBazaarsCount;
            playerGranadaTilesMaxCount -= otherPlayersMinBathhousesCount + otherPlayersMinWishingWellsCount + otherPlayersMinBazaarsCount;

            return (playerTilesMaxCount, playerGranadaTilesMaxCount, otherPlayersMinBathhousesCount, otherPlayersMinWishingWellsCount, otherPlayersMinBazaarsCount);
        }

        private int GetPlayerMinBathhousesCount(PlayerScoreData playerScoreData, List<PlayerScoreData> scoreData)
        {
            int otherPlayerTilesMaxCount = GetPlayerTilesMaxCount(playerScoreData, scoreData, false).playerTilesMaxCount;
            return playerScoreData.BathhousesPoints > 0 ? ((playerScoreData.BathhousesPoints - 1) / (otherPlayerTilesMaxCount - 1)) + 1 : 0;
        }

        private int CountRelativeScore(int playerNumber, Func<int, double> getCountValue, List<int>[] scoringTable, ScoringHighestLowest highestLowest, UpDown roundMethod, int[] zeroPenaltiesTable = null)
        {
            int result = 0;
            double countValue = getCountValue(playerNumber - 1);
            if (countValue != 0 || highestLowest == ScoringHighestLowest.Lowest)
            {
                int currentPlace = 1;
                int sharePlaceCount = 0;
                for (int j = 0; j < PlayersCount; j++)
                    if (j != playerNumber - 1)
                    {
                        double otherPlayerCountValue = getCountValue(j);

                        if ((highestLowest == ScoringHighestLowest.Highest && otherPlayerCountValue > countValue)
                            || (highestLowest == ScoringHighestLowest.Lowest && otherPlayerCountValue < countValue))
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

                if (highestLowest == ScoringHighestLowest.Lowest && countValue == 0 && zeroPenaltiesTable != null)
                    result += zeroPenaltiesTable[RoundNumber - 1];
            }

            return result;
        }

        private double GetBuildingCount(PlayerScoreData scoreData, AlhambraBase.BuildingType buildingType, bool withBonuses = true)
        {
            double alhambraCount = scoreData.BuildingsCount[buildingType];
            if (withBonuses)
            {
                //Bonus Cards: extra buildings
                if (HasModule(AlhambraBase.ExpansionModule.ExpansionBonusCards))
                    alhambraCount += scoreData.BonusCardsBuildingsCount[buildingType];
                //Squares: square count value
                if (HasModule(AlhambraBase.ExpansionModule.ExpansionSquares))
                    alhambraCount += scoreData.SquaresBuildingsCount[buildingType];
                if (HasModule(AlhambraBase.ExpansionModule.ExpansionCharacters)
                    && scoreData.OwnedCharacterTheWiseMan && (AlhambraBase.BuildingType)scoreData.TheWiseManBuildingType == buildingType)
                    alhambraCount += 0.5;
                //Extensions: extended buildings
                if (HasModule(AlhambraBase.ExpansionModule.DesignerExtensions))
                    alhambraCount += scoreData.ExtensionsBuildingsCount[buildingType];
                //Gates without End: semi-buildings
                if (alhambraCount >= 1 && HasModule(AlhambraBase.ExpansionModule.DesignerGatesWithoutEnd)
                    && scoreData.OwnedSemiBuildings[buildingType])
                    alhambraCount += 0.5;
                if (alhambraCount >= 1 && HasModule(AlhambraBase.ExpansionModule.RedPalaceLandTiles)
                    && scoreData.OwnedHalfBuildings[buildingType])
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

        private double GetGranadaBuildingCount(PlayerScoreData scoreData, AlhambraBase.GranadaBuildingType building)
        {
            double buildingCount = scoreData.GranadaBuildingsCount[building];
            if (scoreData.GranadaBuildingsHighestPrices != null && scoreData.GranadaBuildingsHighestPrices.ContainsKey(building))
                buildingCount += ((double)scoreData.GranadaBuildingsHighestPrices[building]) / 20;
            return buildingCount;
        }

        public int GetBuildingScore(List<PlayerScoreData> scoreData, AlhambraBase.BuildingType buildingType, int playerNumber, bool withBonuses = true)
        {
            return CountRelativeScore(playerNumber, (int i) => GetBuildingCount(scoreData[i], buildingType, withBonuses), Scoring[buildingType], ScoringHighestLowest.Highest, UpDown.Down);
        }

        public bool ValidateScore(RoundScoring scoreData)
        {
            RoundScoring previousBeforeScoreData = null;
            if (RoundNumber == 3 && ThirdBeforeRoundScoring != null)
                previousBeforeScoreData = ThirdBeforeRoundScoring;

            foreach (KeyValuePair<AlhambraBase.BuildingType, int> mapEntry in BuildingsMaxCount)
            {
                int playersBuildings = scoreData.PlayersScoreData.Sum(p => p.BuildingsCount[mapEntry.Key]);

                if (SettingsManager.Get(SettingsType.ValidateBuildingsNumber) && playersBuildings > mapEntry.Value)
                    return CheckFailed(Resource.String.message_building_number_exceed, ((AlhambraScoringAndroid.GamePlay.BuildingType)mapEntry.Key).GetEnumDescription(Context.Resources));

                if (SettingsManager.Get(SettingsType.ValidateBuildingsNumberPrevious) && !ValidatePreviousAvailableLimit(scoreData.PlayersScoreData, p => p.BuildingsCount[mapEntry.Key], mapEntry.Value, CreateResourcesFormatData(Resource.String.message_building_number_previous_exceed, ((AlhambraScoringAndroid.GamePlay.BuildingType)mapEntry.Key).GetEnumDescription(Context.Resources))))
                    return false;
            }

            if (HasModule(AlhambraBase.ExpansionModule.ExpansionBonusCards))
            {
                int playerBonusCardsMax = 1;
                if (PlayersCount < 6)
                    playerBonusCardsMax++;
                if (PlayersCount < 4)
                    playerBonusCardsMax++;

                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                {
                    if (SettingsManager.Get(SettingsType.ValidateBonusCardsPlayer) && playerScoreData.BonusCardsBuildingsCount.Sum(c => c.Value) > playerBonusCardsMax)
                        return CheckFailed(Resource.String.message_bonus_cards_player_exceed, playerScoreData.PlayerName);

                    foreach (KeyValuePair<AlhambraBase.BuildingType, int> mapEntry in GameConstants.BonusCardsMaxCount)
                    {
                        if (SettingsManager.Get(SettingsType.ValidateBonusCardsBuildings) && playerScoreData.ExtensionsBuildingsCount[mapEntry.Key] > playerScoreData.BuildingsCount[mapEntry.Key])
                            return CheckFailed(Resource.String.message_bonus_cards_buildings_player_exceed, playerScoreData.PlayerName, mapEntry.Key.GetEnumDescription(Context.Resources));
                    }
                }
                foreach (KeyValuePair<AlhambraBase.BuildingType, int> mapEntry in GameConstants.BonusCardsMaxCount)
                {
                    if (SettingsManager.Get(SettingsType.ValidateBonusCards) && scoreData.PlayersScoreData.Sum(p => p.BonusCardsBuildingsCount[mapEntry.Key]) > mapEntry.Value)
                        return CheckFailed(Resource.String.message_bonus_cards_exceed, mapEntry.Key.GetEnumDescription(Context.Resources));
                }
            }

            if (HasModule(AlhambraBase.ExpansionModule.ExpansionSquares))
            {
                Dictionary<AlhambraBase.BuildingType, int> squaresTotalMinimumNumber = new Dictionary<AlhambraBase.BuildingType, int>();
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                {
                    int playerSquaresTotalMinimumNumber = 0;
                    foreach (AlhambraBase.BuildingType building in GameConstants.BuildingsOrder)
                    {
                        int squaresPoints = playerScoreData.SquaresBuildingsCount[building];
                        //TODO can be PlayerSquaresMaxCount instead of 3?
                        int squaresMinimumNumber = GameConstants.GetMinimumNumberFromCount(squaresPoints, 3);
                        playerSquaresTotalMinimumNumber += squaresMinimumNumber;

                        int squareTotalMinimumNumber = 0;
                        if (squaresTotalMinimumNumber.ContainsKey(building))
                            squareTotalMinimumNumber = squaresTotalMinimumNumber[building];
                        squareTotalMinimumNumber += squaresMinimumNumber;
                        squaresTotalMinimumNumber[building] = squareTotalMinimumNumber;

                        if (SettingsManager.Get(SettingsType.ValidateSquaresPlayer) && squaresMinimumNumber > PlayerSquaresMaxCount)
                            return CheckFailed(Resource.String.message_squares_buildings_player_exceed, playerScoreData.PlayerName, building.GetEnumDescription(Context.Resources));
                    }

                    if (SettingsManager.Get(SettingsType.ValidateSquaresPlayer) && playerSquaresTotalMinimumNumber > PlayerSquaresMaxCount)
                        return CheckFailed(Resource.String.message_squares_player_exceed, playerScoreData.PlayerName);
                }
                foreach (KeyValuePair<AlhambraBase.BuildingType, int> mapEntry in GameConstants.SquaresMaxCount)
                {
                    if (SettingsManager.Get(SettingsType.ValidateSquares) && squaresTotalMinimumNumber[mapEntry.Key] > mapEntry.Value)
                        return CheckFailed(Resource.String.message_squares_number_exceed, mapEntry.Key.GetEnumDescription(Context.Resources));
                }
            }

            if (HasModule(AlhambraBase.ExpansionModule.ExpansionCharacters))
            {
                if (SettingsManager.Get(SettingsType.ValidateMultipleWiseman) && scoreData.PlayersScoreData.Count(p => p.OwnedCharacterTheWiseMan) > 1)
                    return CheckFailed(Resource.String.message_multiple_wiseman);
                if (SettingsManager.Get(SettingsType.ValidateMultipleCitywatch) && scoreData.PlayersScoreData.Count(p => p.OwnedCharacterTheCityWatch) > 1)
                    return CheckFailed(Resource.String.message_multiple_citywatch);

                if (!CheckPreviousRoundPlayerScoringMatch(scoreData.PlayersScoreData, SettingsType.ValidatePreviousWiseman, (previousPlayerScoreData, currentPlayerScoreData) => previousPlayerScoreData.OwnedCharacterTheWiseMan && !currentPlayerScoreData.OwnedCharacterTheWiseMan, playerName => CreateResourcesFormatData(Resource.String.message_previous_wiseman, playerName)))
                    return false;

                if (!CheckPreviousRoundPlayerScoringMatch(scoreData.PlayersScoreData, SettingsType.ValidatePreviousCitywatch, (previousPlayerScoreData, currentPlayerScoreData) => previousPlayerScoreData.OwnedCharacterTheCityWatch && !currentPlayerScoreData.OwnedCharacterTheCityWatch, playerName => CreateResourcesFormatData(Resource.String.message_previous_citywatch, playerName)))
                    return false;
            }

            if (HasModule(AlhambraBase.ExpansionModule.ExpansionStreetTrader))
            {
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                {
                    foreach (AlhambraBase.BuildingType building in GameConstants.BuildingsOrder)
                    {
                        if (SettingsManager.Get(SettingsType.ValidateCitizensBuildings) && playerScoreData.StreetTradersNumber[building] > playerScoreData.BuildingsCount[building])
                            return CheckFailed(Resource.String.message_citizens_buildings_exceed, playerScoreData.PlayerName, building.GetEnumDescription(Context.Resources));
                    }
                }

                foreach (AlhambraBase.BuildingType building in GameConstants.BuildingsOrder)
                {
                    if (SettingsManager.Get(SettingsType.ValidateCitizens) && scoreData.PlayersScoreData.Sum(p => p.StreetTradersNumber[building]) > StreetTradersTypeMaxCount)
                        return CheckFailed(Resource.String.message_citizens_exceed, building.GetEnumDescription(Context.Resources));
                }
            }

            if (HasModule(AlhambraBase.ExpansionModule.ExpansionTreasureChamber))
            {
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                {
                    if (SettingsManager.Get(SettingsType.ValidateTreasuresPlayer) && playerScoreData.TreasuresCount > playerScoreData.AllBuildingsCount)
                        return CheckFailed(Resource.String.message_treasures_player_exceed, playerScoreData.PlayerName);
                }
                if (SettingsManager.Get(SettingsType.ValidateTreasures) && scoreData.PlayersScoreData.Sum(p => p.TreasuresCount) > TreasuresMaxCount)
                    return CheckFailed(Resource.String.message_treasures_number_exceed);
            }

            if (HasModule(AlhambraBase.ExpansionModule.ExpansionBazaars))
            {
                int bazaarsTotalPointsSum = 0;
                int bazaarsMinimumNumber = 0;
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                {
                    int bazaarsTotalPoints = playerScoreData.BazaarsTotalPoints;
                    //if (Settings.Get(SettingsType.ValidateBazaarsPoints) && !bazaarsAvailablePoints.Contains(bazaarsTotalPoints))
                    //    return CheckFailed( $"{playerScoreData.PlayerName}: Niedozwolona ilość punktów z bazarów");
                    bazaarsMinimumNumber += GameConstants.GetMinimumNumberFromCount(bazaarsTotalPoints, BazaarPointsMaxCount);
                    bazaarsTotalPointsSum += bazaarsTotalPoints;
                }
                if (SettingsManager.Get(SettingsType.ValidateBazaarsPoints) && bazaarsTotalPointsSum > BazaarsMaxCount * BazaarPointsMaxCount)
                    return CheckFailed(Resource.String.message_bazaars_points_exceed);
                if (SettingsManager.Get(SettingsType.ValidateBazaarsPoints) && bazaarsMinimumNumber > BazaarsMaxCount)
                    return CheckFailed(Resource.String.message_bazaars_number_exceed);
            }

            if (HasModule(AlhambraBase.ExpansionModule.ExpansionArtOfTheMoors))
            {
                int artOfTheMoorsPointsSum = 0;
                int artOfTheMoorsMinimumNumber = 0;
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                {
                    int artOfTheMoorsPoints = playerScoreData.ArtOfTheMoorsPoints;
                    if (SettingsManager.Get(SettingsType.ValidateCultureCounters) && !artOfTheMoorsPlayerAvailablePoints.Any(p => p.Item1 == artOfTheMoorsPoints))
                        return CheckFailed(Resource.String.message_culture_counters_player_mismatch, playerScoreData.PlayerName);
                    artOfTheMoorsMinimumNumber += artOfTheMoorsPlayerAvailablePoints.Where(p => p.Item1 == artOfTheMoorsPoints).Min(p => p.Item2);
                    artOfTheMoorsPointsSum += artOfTheMoorsPoints;
                }

                if (SettingsManager.Get(SettingsType.ValidateCultureCounters) && (artOfTheMoorsMinimumNumber > ArtOfTheMoorsMaxCount))
                    return CheckFailed(Resource.String.message_culture_counters_number_exceed);
                if (!CheckPreviousRoundPlayerScoringMatch(scoreData.PlayersScoreData, SettingsType.ValidateCultureCountersPrevious, (previousPlayerScoreData, currentPlayerScoreData) => currentPlayerScoreData.ArtOfTheMoorsPoints < previousPlayerScoreData.ArtOfTheMoorsPoints || !artOfTheMoorsPlayerAvailablePoints.Any(p => p.Item1 == currentPlayerScoreData.ArtOfTheMoorsPoints - previousPlayerScoreData.ArtOfTheMoorsPoints), playerName => CreateResourcesFormatData(Resource.String.message_culture_counters_player_previous_mismatch, playerName)))
                    return false;
            }

            if (HasModule(AlhambraBase.ExpansionModule.ExpansionFalconers))
            {
                if (SettingsManager.Get(SettingsType.ValidateFalcons) && scoreData.PlayersScoreData.Sum(p => p.FalconsBlackNumber) > FalconsTypeMaxCount)
                    return CheckFailed(Resource.String.message_black_falcons_number_exceed);
                if (SettingsManager.Get(SettingsType.ValidateFalcons) && scoreData.PlayersScoreData.Sum(p => p.FalconsBrownNumber) > FalconsTypeMaxCount)
                    return CheckFailed(Resource.String.message_brown_falcons_number_exceed);
                if (SettingsManager.Get(SettingsType.ValidateFalcons) && scoreData.PlayersScoreData.Sum(p => p.FalconsWhiteNumber) > FalconsTypeMaxCount)
                    return CheckFailed(Resource.String.message_white_falcons_number_exceed);
            }

            if (HasModule(AlhambraBase.ExpansionModule.ExpansionWatchtowers))
            {
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                {
                    if (SettingsManager.Get(SettingsType.ValidateWatchtowerWall) && playerScoreData.WatchtowersNumber > playerScoreData.WallLength)
                        return CheckFailed(Resource.String.message_watchtower_wall_player_exceed, playerScoreData.PlayerName);
                }
                if (SettingsManager.Get(SettingsType.ValidateWatchtower) && scoreData.PlayersScoreData.Sum(p => p.WatchtowersNumber) > AllWatchtowersNumber)
                    return CheckFailed(Resource.String.message_watchtower_number_exceed);
            }

            if (HasModule(AlhambraBase.ExpansionModule.QueenieMedina))
            {
                if (SettingsManager.Get(SettingsType.ValidateMedin) && scoreData.PlayersScoreData.Sum(p => p.MedinasNumber) > AllMedinasNumber)
                    return CheckFailed(Resource.String.message_medin_number_exceed);

                if (SettingsManager.Get(SettingsType.ValidateMedinPrevious) && !ValidatePreviousAvailableLimit(scoreData.PlayersScoreData, p => p.MedinasNumber, AllMedinasNumber, CreateResourcesFormatData(Resource.String.message_medin_number_previous_exceed)))
                    return false;
            }

            if (HasModule(AlhambraBase.ExpansionModule.DesignerPalaceStaff))
            {
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    if (SettingsManager.Get(SettingsType.ValidateServants) && (playerScoreData.BuildingsWithoutServantTile > playerScoreData.BaseBuildingsCount
                        || (previousBeforeScoreData != null && previousBeforeScoreData.PlayersScoreData[playerScoreData.PlayerNumber - 1].BuildingsWithoutServantTile > playerScoreData.BaseBuildingsCount)))
                        return CheckFailed(Resource.String.message_servants_buildings_player_exceed, playerScoreData.PlayerName);
            }

            if (HasModule(AlhambraBase.ExpansionModule.DesignerOrchards))
            {
                int faceDownFruitsSum = scoreData.PlayersScoreData.Sum(p => p.FaceDownFruitsCount);
                int allFruits = 0;
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
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
                if (SettingsManager.Get(SettingsType.ValidateSingleFruits) && faceDownFruitsSum > FaceDownFruitsMaxCount)
                    return CheckFailed(Resource.String.message_single_fruits_number_exceed);
                if (SettingsManager.Get(SettingsType.ValidateFruits) && allFruits > AllFruitsMaxCount)
                    return CheckFailed(Resource.String.message_fruits_number_exceed);
            }

            if (HasModule(AlhambraBase.ExpansionModule.DesignerWishingWell))
            {
                int wishingWellsPointsSum = 0;
                int wishingWellsMinimumNumber = 0;
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                {
                    int wishingWellsPoints = playerScoreData.WishingWellsPoints;
                    if (SettingsManager.Get(SettingsType.ValidateWishingWellsPlayer) && !wishingWellsAvailablePoints.Any(p => p.Item1 == wishingWellsPoints))
                        return CheckFailed(Resource.String.message_wishing_wells_player_points_mismatch, playerScoreData.PlayerName);
                    wishingWellsMinimumNumber += wishingWellsAvailablePoints.Where(p => p.Item1 == wishingWellsPoints).Min(p => p.Item2);
                    wishingWellsPointsSum += wishingWellsPoints;
                }
                if (SettingsManager.Get(SettingsType.ValidateWishingWells) && wishingWellsPointsSum > WishingWellsMaxCount * WishingWellPointsMaxCount)
                    return CheckFailed(Resource.String.message_wishing_wells_points_exceed);
                if (SettingsManager.Get(SettingsType.ValidateWishingWells) && wishingWellsMinimumNumber > WishingWellsMaxCount)
                    return CheckFailed(Resource.String.message_wishing_wells_number_exceed);
            }

            if (HasModule(AlhambraBase.ExpansionModule.DesignerFreshColors))
            {
                foreach (AlhambraBase.BuildingType building in GameConstants.BuildingsOrder)
                {
                    if (SettingsManager.Get(SettingsType.ValidateMultipleCompletedProject) && scoreData.PlayersScoreData.Count(p => p.CompletedProjects[building]) > PlayerProjectsMaxCount)
                        return CheckFailed(Resource.String.message_multiple_completed_project, building.GetEnumDescription(Context.Resources));

                    if (!CheckPreviousRoundPlayerScoringMatch(scoreData.PlayersScoreData, SettingsType.ValidatePreviousCompletedProject, (previousPlayerScoreData, currentPlayerScoreData) => previousPlayerScoreData.CompletedProjects[building] && !currentPlayerScoreData.CompletedProjects[building], playerName => CreateResourcesFormatData(Resource.String.message_previous_completed_project, playerName, building.GetEnumDescription(Context.Resources))))
                        return false;
                }
            }

            if (HasModule(AlhambraBase.ExpansionModule.DesignerAlhambraZoo))
            {
                int animalsPointsSum = scoreData.PlayersScoreData.Sum(p => p.AnimalsPoints);
                if (SettingsManager.Get(SettingsType.ValidateAnimals) && animalsPointsSum > AnimalsMaxCount)
                    return CheckFailed(Resource.String.message_animals_number_exceed);

                if (SettingsManager.Get(SettingsType.ValidateAnimalsPrevious) && !ValidatePreviousAvailableLimit(scoreData.PlayersScoreData, p => p.AnimalsPoints, AnimalsMaxCount, CreateResourcesFormatData(Resource.String.message_animals_number_previous_exceed)))
                    return false;
            }

            if (HasModule(AlhambraBase.ExpansionModule.DesignerGatesWithoutEnd))
            {
                foreach (AlhambraBase.BuildingType building in GameConstants.BuildingsOrder)
                {
                    if (SettingsManager.Get(SettingsType.ValidateMultipleSemiBuildings) && scoreData.PlayersScoreData.Count(p => p.OwnedSemiBuildings[building]) > 1)
                        return CheckFailed(Resource.String.message_multiple_semi_buildings, building.GetEnumDescription(Context.Resources));
                }

                foreach (AlhambraBase.BuildingType building in GameConstants.BuildingsOrder)
                {
                    if (!CheckPreviousRoundPlayerScoringMatch(scoreData.PlayersScoreData, SettingsType.ValidatePreviousSemiBuildings, (previousPlayerScoreData, currentPlayerScoreData) => previousPlayerScoreData.OwnedSemiBuildings[building] && !currentPlayerScoreData.OwnedSemiBuildings[building], playerName => CreateResourcesFormatData(Resource.String.message_previous_semi_buildings, playerName, building.GetEnumDescription(Context.Resources))))
                        return false;
                }
            }

            if (HasModule(AlhambraBase.ExpansionModule.DesignerBuildingsOfPower))
            {
                int blackDiceTotalPipsSum = 0;
                int blackDicesMinimumNumber = 0;
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                {
                    int blackDiceTotalPips = playerScoreData.BlackDiceTotalPips;
                    blackDicesMinimumNumber += GameConstants.GetMinimumNumberFromCount(blackDiceTotalPips, BlackDicePipsMaxCount);
                    blackDiceTotalPipsSum += blackDiceTotalPips;
                }
                if (SettingsManager.Get(SettingsType.ValidateBlackDicePips) && blackDiceTotalPipsSum > BlackDicesMaxCount * BlackDicePipsMaxCount)
                    return CheckFailed(Resource.String.message_black_dice_pips_number_exceed);
                if (SettingsManager.Get(SettingsType.ValidateBlackDicePips) && blackDicesMinimumNumber > BlackDicesMaxCount)
                    return CheckFailed(Resource.String.message_black_dice_number_exceed);

                if (SettingsManager.Get(SettingsType.ValidateBlackDicesPrevious) && !ValidatePreviousAvailableLimit(scoreData.PlayersScoreData, p => GameConstants.GetMinimumNumberFromCount(p.BlackDiceTotalPips, BlackDicePipsMaxCount), BlackDicesMaxCount, CreateResourcesFormatData(Resource.String.message_black_dices_number_previous_exceed)))
                    return false;
            }

            if (HasModule(AlhambraBase.ExpansionModule.DesignerExtensions))
            {
                foreach (AlhambraBase.BuildingType building in GameConstants.BuildingsOrder)
                {
                    foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    {
                        if (SettingsManager.Get(SettingsType.ValidateExtensionsBuildings) && playerScoreData.ExtensionsBuildingsCount[building] > playerScoreData.BuildingsCount[building])
                            return CheckFailed(Resource.String.message_extensions_buildings_player_exceed, playerScoreData.PlayerName, building.GetEnumDescription(Context.Resources));
                    }
                    if (SettingsManager.Get(SettingsType.ValidateExtensions) && scoreData.PlayersScoreData.Sum(p => p.ExtensionsBuildingsCount[building]) > ExtensionsBuildingsTypeMaxCount)
                        return CheckFailed(Resource.String.message_extensions_exceed, building.GetEnumDescription(Context.Resources));
                }
            }

            if (HasModule(AlhambraBase.ExpansionModule.DesignerHandymen))
            {
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                {
                    if (SettingsManager.Get(SettingsType.ValidateHandymen) && playerScoreData.HandymenTilesHighestNumber > PlayerAllHandymenCount)
                        return CheckFailed(Resource.String.message_handymen_exceed, playerScoreData.PlayerName);
                }
            }

            if (HasModule(AlhambraBase.ExpansionModule.FanTreasures))
            {
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                {
                    if (SettingsManager.Get(SettingsType.ValidateTreasuresPoints) && !treasuresAvailableValues.Contains(playerScoreData.TreasuresPoints))
                        return CheckFailed(Resource.String.message_treasures_points_player_mismatch, playerScoreData.PlayerName);
                }
            }

            if (HasModule(AlhambraBase.ExpansionModule.FanCaliphsGuidelines))
            {
                if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission3))
                {
                    foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    {
                        if (SettingsManager.Get(SettingsType.ValidateMissions) && playerScoreData.Mission3Count > playerScoreData.BaseBuildings.Sum(b => GameConstants.GetBuildingsAvailableAdjacent(b.Value)))
                            return CheckFailed(Resource.String.message_mission3_player_exceed, playerScoreData.PlayerName);
                    }
                }
            }

            foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
            {
                int wallLength = playerScoreData.WallLength;
                int secondLongestWallLength = playerScoreData.SecondLongestWallLength;
                if (SettingsManager.Get(SettingsType.ValidateSecondLongestWall) && wallLength < secondLongestWallLength)
                    return CheckFailed(Resource.String.message_second_longest_wall_player_exceed, playerScoreData.PlayerName);
            }

            if (HasModule(AlhambraBase.ExpansionModule.Granada))
            {
                foreach (AlhambraBase.GranadaBuildingType building in GameConstants.GranadaBuildingsOrder)
                {
                    int playersBuildings = scoreData.PlayersScoreData.Sum(p => p.GranadaBuildingsCount[building]);

                    if (SettingsManager.Get(SettingsType.ValidateBuildingsNumber) && playersBuildings > GranadaBuildingsTypeMaxCount)
                        return CheckFailed(Resource.String.message_building_number_exceed, building.GetEnumDescription(Context.Resources));

                    if (SettingsManager.Get(SettingsType.ValidateBuildingsNumberPrevious) && !ValidatePreviousAvailableLimit(scoreData.PlayersScoreData, p => p.GranadaBuildingsCount[building], GranadaBuildingsTypeMaxCount, CreateResourcesFormatData(Resource.String.message_building_number_previous_exceed, building.GetEnumDescription(Context.Resources))))
                        return false;
                }

                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                {
                    if (SettingsManager.Get(SettingsType.ValidateMoatwall) && playerScoreData.WallMoatCombinationLength > playerScoreData.WallLength)
                        return CheckFailed(Resource.String.message_moatwall_wall_player, playerScoreData.PlayerName);
                    if (SettingsManager.Get(SettingsType.ValidateMoatwall) && playerScoreData.WallMoatCombinationLength > playerScoreData.MoatLength)
                        return CheckFailed(Resource.String.message_moatwall_moat_player, playerScoreData.PlayerName);
                }
            }

            foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
            {
                (int playerTilesMaxCount, int playerGranadaTilesMaxCount, int otherPlayersMinBathhousesCount, int otherPlayersMinWishingWellsCount, int otherPlayersMinBazaarscount) = GetPlayerTilesMaxCount(playerScoreData, scoreData.PlayersScoreData);
                int playerWallTilesCount = playerScoreData.AllWallTilesCount + AdditionalAvailableWallTilesCount; //dopóki GetPlayerTilesMaxCount używa tylko modułów ExpansionBazaars, DesignerBathhouses i DesignerWishingWell, które nie są ze ścianą - ta linijka osobno. W przeciwnym razie zmienić dodatkowo message_wall_length_player_exceed, message_mission6_player_exceed, message_mission9_player_exceed

                string additionalMessage = String.Empty;
                if (otherPlayersMinBazaarscount != 0)
                    additionalMessage += $" {Context.Resources.GetString(Resource.String.or)} {Context.Resources.GetString(Resource.String.message_check_other_players_bazaars)}";
                if (otherPlayersMinBathhousesCount != 0)
                    additionalMessage += $" {Context.Resources.GetString(Resource.String.or)} {Context.Resources.GetString(Resource.String.message_check_other_players_bathhouses)}";
                if (otherPlayersMinWishingWellsCount != 0)
                    additionalMessage += $" {Context.Resources.GetString(Resource.String.or)} {Context.Resources.GetString(Resource.String.message_check_other_players_wishing_wells)}";

                if (HasModule(AlhambraBase.ExpansionModule.DesignerBathhouses))
                {
                    int playerMinBathhousesCount = GetPlayerMinBathhousesCount(playerScoreData, scoreData.PlayersScoreData);
                    if (SettingsManager.Get(SettingsType.ValidateBathhouses))
                    {
                        if (playerScoreData.BathhousesPoints > (playerTilesMaxCount - 1) * 4 * (AllBathhousesCount - otherPlayersMinBathhousesCount)
                            || playerMinBathhousesCount > playerTilesMaxCount)
                            return CheckFailed(GetResourcesFormatDataMessage(Resource.String.message_bathhouses_points_player_exceed, playerScoreData.PlayerName) + additionalMessage);
                        if (playerMinBathhousesCount > 6)
                            return CheckFailed(Resource.String.message_bathhouses_points_player_exceed, playerScoreData.PlayerName);
                    }
                }

                if (SettingsManager.Get(SettingsType.ValidateWallLength) && playerScoreData.WallLength > CountWallMaxLength(playerWallTilesCount, playerScoreData.WatchtowersNumber))
                    return CheckFailed(Resource.String.message_wall_length_player_exceed, playerScoreData.PlayerName);

                if (HasModule(AlhambraBase.ExpansionModule.Granada))
                {
                    if (SettingsManager.Get(SettingsType.ValidateMoatLength) && playerScoreData.MoatLength > CountWallMaxLength(playerGranadaTilesMaxCount))
                        return CheckFailed(GetResourcesFormatDataMessage(Resource.String.message_moat_length_player_exceed, playerScoreData.PlayerName) + additionalMessage);
                }

                if (HasModule(AlhambraBase.ExpansionModule.ExpansionCamps))
                {
                    //TODO max = total * ilość strzałek (tutaj * max ilość namiotów na podstawie innych graczy * max ilość strzałek); do other players jak bathhouses. + walidacja dla fragment
                    if (SettingsManager.Get(SettingsType.ValidateCamps) && playerScoreData.CampsPoints > playerTilesMaxCount)
                        return CheckFailed(GetResourcesFormatDataMessage(Resource.String.message_camps_points_player_exceed, playerScoreData.PlayerName) + additionalMessage);
                }

                if (HasModule(AlhambraBase.ExpansionModule.ExpansionFalconers))
                {
                    if (SettingsManager.Get(SettingsType.ValidateFalcons) && playerScoreData.FalconsBlackNumber + playerScoreData.FalconsBrownNumber + playerScoreData.FalconsWhiteNumber > GameConstants.GetBuildingsAvailable2x2Grids(playerTilesMaxCount))
                        return CheckFailed(GetResourcesFormatDataMessage(Resource.String.message_falcons_player_exceed, playerScoreData.PlayerName) + additionalMessage);
                }

                if (HasModule(AlhambraBase.ExpansionModule.ExpansionInvaders))
                {
                    if (SettingsManager.Get(SettingsType.ValidateUnprotectedSides) && playerScoreData.UnprotectedSidesCount > playerTilesMaxCount)
                        return CheckFailed(GetResourcesFormatDataMessage(Resource.String.message_unprotected_sides_count_player_exceed, playerScoreData.PlayerName) + additionalMessage);
                    if (SettingsManager.Get(SettingsType.ValidateUnprotectedSides) && playerScoreData.UnprotectedSidesNeighbouringCount > playerTilesMaxCount)
                        return CheckFailed(GetResourcesFormatDataMessage(Resource.String.message_unprotected_sides_neighbouring_count_player_exceed, playerScoreData.PlayerName) + additionalMessage);
                }

                if (HasModule(AlhambraBase.ExpansionModule.FanCaliphsGuidelines))
                {
                    if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission1))
                        if (SettingsManager.Get(SettingsType.ValidateMissions) && playerScoreData.Mission1Count > playerTilesMaxCount / 3)
                            return CheckFailed(GetResourcesFormatDataMessage(Resource.String.message_mission1_player_exceed, playerScoreData.PlayerName) + additionalMessage);
                    if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission2))
                        if (SettingsManager.Get(SettingsType.ValidateMissions) && playerScoreData.Mission2Count > playerTilesMaxCount / 3)
                            return CheckFailed(GetResourcesFormatDataMessage(Resource.String.message_mission2_player_exceed, playerScoreData.PlayerName) + additionalMessage);
                    if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission5))
                        if (SettingsManager.Get(SettingsType.ValidateMissions) && playerScoreData.Mission5Count > (playerTilesMaxCount + 1) / 2)
                            return CheckFailed(GetResourcesFormatDataMessage(Resource.String.message_mission5_player_exceed, playerScoreData.PlayerName) + additionalMessage);
                    if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission6))
                        if (SettingsManager.Get(SettingsType.ValidateMissions) && playerScoreData.Mission6Count > GameConstants.GetBuildingsAvailableAdjacent(playerWallTilesCount))
                            return CheckFailed(Resource.String.message_mission6_player_exceed, playerScoreData.PlayerName);
                    if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission8))
                        if (SettingsManager.Get(SettingsType.ValidateMissions) && playerScoreData.Mission8Count > playerTilesMaxCount - 1)
                            return CheckFailed(GetResourcesFormatDataMessage(Resource.String.message_mission8_player_exceed, playerScoreData.PlayerName) + additionalMessage);
                    if (HasCaliphsGuideline(CaliphsGuidelinesMission.Mission9))
                        if (SettingsManager.Get(SettingsType.ValidateMissions) && playerScoreData.Mission9Count > GameConstants.GetBuildingsAvailable2x2Grids(playerTilesMaxCount))
                            return CheckFailed(Resource.String.message_mission9_player_exceed, playerScoreData.PlayerName);
                }
            }

            if (HasModule(AlhambraBase.ExpansionModule.RedPalaceLandTiles))
            {
                foreach (AlhambraBase.BuildingType building in GameConstants.BuildingsOrder)
                {
                    if (SettingsManager.Get(SettingsType.ValidateMultipleHalfBuildings) && scoreData.PlayersScoreData.Count(p => p.OwnedHalfBuildings[building]) > 1)
                        return CheckFailed(Resource.String.message_multiple_half_buildings, building.GetEnumDescription(Context.Resources));
                }

                foreach (AlhambraBase.BuildingType building in GameConstants.BuildingsOrder)
                {
                    if (!CheckPreviousRoundPlayerScoringMatch(scoreData.PlayersScoreData, SettingsType.ValidatePreviousHalfBuildings, (previousPlayerScoreData, currentPlayerScoreData) => previousPlayerScoreData.OwnedHalfBuildings[building] && !currentPlayerScoreData.OwnedHalfBuildings[building], playerName => CreateResourcesFormatData(Resource.String.message_previous_half_buildings, playerName, building.GetEnumDescription(Context.Resources))))
                        return false;
                }

                if (SettingsManager.Get(SettingsType.ValidateGuardsCount) && scoreData.PlayersScoreData.Sum(p => p.GuardsCount) > AllGuardsCount)
                    return CheckFailed(Resource.String.message_guards_count_exceed);

                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                {
                    if (SettingsManager.Get(SettingsType.ValidateGuardsCount) && playerScoreData.GuardsCount > playerScoreData.WallLength)
                        return CheckFailed(Resource.String.message_guards_count_player_exceed, playerScoreData.PlayerName);
                }
            }

            return true;
        }

        public bool ValidateScoreBeforeAssignLeftoverBuildings(RoundScoring scoreData)
        {
            return true;
        }

        public void Score(RoundScoring scoreData)
        {
            List<(ScoreDetails scoreDetails1, ScoreDetails scoreDetails2, ScoreDetails scoreDetails3, ScoreDetails scoreMeantime)> initialScoring = Players.Select(p => (p.ScoreDetails1.Copy(), p.ScoreDetails2.Copy(), p.ScoreDetails3.Copy(), p.ScoreMeantime.Copy())).ToList();

            if (GranadaOption != GranadaOption.Alone)
            {
                //wall
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.WallLength, ScoreType.WallLength);

                //each kind of building
                foreach (KeyValuePair<AlhambraBase.BuildingType, List<int>[]> scoring in Scoring)
                {
                    foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    {
                        int buildingScore = GetBuildingScore(scoreData.PlayersScoreData, scoring.Key, playerScoreData.PlayerNumber);
                        int buildingScoreBonus = buildingScore - GetBuildingScore(scoreData.PlayersScoreData, scoring.Key, playerScoreData.PlayerNumber, false);
                        if (buildingScoreBonus < 0)
                            buildingScoreBonus = 0;

                        playerScoreData.Player.AddScore(buildingScore, BuildingBaseScoreType[scoring.Key]);
                        playerScoreData.Player.AddScore(buildingScoreBonus, ScoreType.BuildingsBonuses);
                    }
                }
            }

            if (HasModule(AlhambraBase.ExpansionModule.ExpansionCharacters))
            {
                //Characters: owned The City Watch
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.OwnedCharacterTheCityWatch ? playerScoreData.WallLength / 3 : 0, ScoreType.TheCityWatch);
            }

            if (HasModule(AlhambraBase.ExpansionModule.ExpansionCamps))
            {
                //Camps: buildings joined together in a straight, uninterrupted line
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.CampsPoints, ScoreType.Camps);
            }

            if (HasModule(AlhambraBase.ExpansionModule.ExpansionStreetTrader))
            {
                //Street Trader: sets based on the number of different colored citizens
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                {
                    int pointsSum = 0;
                    for (int j = 0; j < 7; j++)
                    {
                        int setDifferentCitizens = playerScoreData.StreetTradersNumber.Where(t => t.Value > j).Count();
                        int points = setDifferentCitizens switch
                        {
                            0 => 0,
                            1 => 1,
                            2 => 3,
                            3 => 6,
                            4 => 10,
                            5 => 15,
                            6 => 21,
                            _ => throw new NotSupportedException(),
                        };
                        pointsSum += points;
                    }
                    playerScoreData.Player.AddScore(pointsSum, ScoreType.StreetTraders);
                }
            }

            if (HasModule(AlhambraBase.ExpansionModule.ExpansionTreasureChamber))
            {
                //Treasure Chamber: chests
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                {
                    int chestsScore = CountRelativeScore(playerScoreData.PlayerNumber, (int j) => scoreData.PlayersScoreData[j].TreasuresCount, GameConstants.TreasureChamberScoring, ScoringHighestLowest.Highest, UpDown.Down);

                    playerScoreData.Player.AddScore(chestsScore, ScoreType.TreasureChamber);
                }
            }

            if (HasModule(AlhambraBase.ExpansionModule.ExpansionInvaders))
            {
                //Invaders: each side unprotected from the main direction of the attack
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    if (!playerScoreData.Player.Dirk)
                    {
                        playerScoreData.Player.RemoveScore(playerScoreData.UnprotectedSidesCount * RoundNumber, false, ScoreType.Invaders);
                        if (RoundNumber == 3)
                            playerScoreData.Player.RemoveScore(playerScoreData.UnprotectedSidesNeighbouringCount, false, ScoreType.Invaders);
                    }
            }

            if (HasModule(AlhambraBase.ExpansionModule.ExpansionBazaars))
            {
                if (RoundNumber == 3)
                {
                    //Bazaars: points for bazaars
                    foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                        if (!playerScoreData.Player.Dirk)
                            playerScoreData.Player.AddScore(playerScoreData.BazaarsTotalPoints, ScoreType.Bazaars);
                }
            }

            if (HasModule(AlhambraBase.ExpansionModule.ExpansionArtOfTheMoors))
            {
                //Art of the Moors: points for the culture counters
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.ArtOfTheMoorsPoints, ScoreType.ArtOfTheMoors);
            }

            if (HasModule(AlhambraBase.ExpansionModule.ExpansionFalconers))
            {
                //Falconers: points for each type of falcons
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    if (!playerScoreData.Player.Dirk)
                    {
                        foreach (int falconsNumber in new int[] { playerScoreData.FalconsBlackNumber, playerScoreData.FalconsBrownNumber, playerScoreData.FalconsWhiteNumber })
                        {
                            int points = falconsNumber switch
                            {
                                0 => 0,
                                1 => 2,
                                2 => 6,
                                3 => 12,
                                4 => 20,
                                5 => 30,
                                _ => throw new NotSupportedException(),
                            };
                            playerScoreData.Player.AddScore(points, ScoreType.Falconers);
                        }
                    }
            }

            if (HasModule(AlhambraBase.ExpansionModule.ExpansionWatchtowers))
            {
                //Watchtowers: Each watchtower that is part of a player’s longest contiguous wall
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.WatchtowersNumber * 2, ScoreType.Watchtowers);
            }

            if (HasModule(AlhambraBase.ExpansionModule.QueenieMedina))
            {
                //Medina
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                {
                    int medinaScore = CountRelativeScore(playerScoreData.PlayerNumber, (int j) => GetMedinaCount(scoreData.PlayersScoreData[j]), GameConstants.MedinaScoring, ScoringHighestLowest.Lowest, UpDown.Up, GameConstants.MedinaZeroPenaltiesScoring);

                    playerScoreData.Player.RemoveScore(medinaScore, true, ScoreType.Medina);
                }
            }

            if (HasModule(AlhambraBase.ExpansionModule.DesignerPalaceStaff))
            {
                if (RoundNumber != 3)
                {
                    //Palace Staff: each building without a servant tile
                    foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                        if (!playerScoreData.Player.Dirk)
                            playerScoreData.Player.RemoveScore(playerScoreData.BuildingsWithoutServantTile, false, ScoreType.BuildingsWithoutServantTile);
                }
            }

            if (HasModule(AlhambraBase.ExpansionModule.DesignerOrchards))
            {
                if (RoundNumber == 3)
                {
                    //Orchards: fruits
                    foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
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

            if (HasModule(AlhambraBase.ExpansionModule.DesignerBathhouses))
            {
                //Bathhouses: distances of the first building
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.BathhousesPoints, ScoreType.Bathhouses);
            }

            if (HasModule(AlhambraBase.ExpansionModule.DesignerWishingWell))
            {
                //Wishing Well: tiles in a straight line from the waterspout
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.WishingWellsPoints, ScoreType.WishingWells);
            }

            if (HasModule(AlhambraBase.ExpansionModule.DesignerFreshColors))
            {
                //Fresh Colors: completed projects
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    if (!playerScoreData.Player.Dirk)
                    {
                        foreach (KeyValuePair<AlhambraBase.BuildingType, bool> completedProject in playerScoreData.CompletedProjects)
                        {
                            if (completedProject.Value)
                                playerScoreData.Player.AddScore(playerScoreData.BuildingsCount[completedProject.Key] * 2, ScoreType.CompletedProjects);
                        }
                    }
            }

            if (HasModule(AlhambraBase.ExpansionModule.DesignerAlhambraZoo))
            {
                //Alhambra Zoo: animals points
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.AnimalsPoints, ScoreType.Animals);
            }

            if (HasModule(AlhambraBase.ExpansionModule.DesignerBuildingsOfPower))
            {
                //Buildings of Power: Building of Strength
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(Math.Min(playerScoreData.SecondLongestWallLength, playerScoreData.BlackDiceTotalPips), ScoreType.BlackDices);
            }

            if (HasModule(AlhambraBase.ExpansionModule.DesignerHandymen))
            {
                //Handymen: highest number of adjacent tiles occupied by handymen
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.HandymenTilesHighestNumber, ScoreType.Handymen);
            }

            if (HasModule(AlhambraBase.ExpansionModule.FanTreasures))
            {
                //Treasures: treasures' value
                if (RoundNumber == 3)
                {
                    foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                        if (!playerScoreData.Player.Dirk)
                            playerScoreData.Player.AddScore(playerScoreData.TreasuresPoints, ScoreType.Treasures);
                }
            }

            if (HasModule(AlhambraBase.ExpansionModule.FanCaliphsGuidelines))
            {
                if (RoundNumber == 3)
                {
                    foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
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
                                playerScoreData.Player.AddScore(playerScoreData.Mission9Count * 3, ScoreType.Mission9);
                        }
                }
            }

            if (HasModule(AlhambraBase.ExpansionModule.RedPalaceLandTiles))
            {
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.GuardsCount * scoreData.GuardsPoints, ScoreType.Guards);
            }

            if (HasModule(AlhambraBase.ExpansionModule.Granada))
            {
                //moat
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    if (!playerScoreData.Player.Dirk)
                        playerScoreData.Player.AddScore(playerScoreData.MoatLength, ScoreType.MoatLength);

                //each kind of building
                foreach (AlhambraBase.GranadaBuildingType building in GameConstants.GranadaBuildingsOrder)
                {
                    foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                    {
                        int buildingScore = CountRelativeScore(playerScoreData.PlayerNumber, (int j) => GetGranadaBuildingCount(scoreData.PlayersScoreData[j], building), GetGranadaScoring(scoreData.PlayersScoreData, building), ScoringHighestLowest.Highest, UpDown.Down);

                        playerScoreData.Player.AddScore(buildingScore, GranadaBuildingBaseScoreType[building]);
                    }
                }

                if (GranadaOption == GranadaOption.With)
                {
                    foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
                        if (!playerScoreData.Player.Dirk)
                            playerScoreData.Player.AddScore(playerScoreData.WallMoatCombinationLength * 2, ScoreType.WallMoatCombination);
                }
            }

            ScoreStack.Push(new ScoreHistoryRound(this, ScoreRound, initialScoring));

            RoundsScoring[RoundNumber - 1] = scoreData;
        }

        public void ScoreBeforeAssignLeftoverBuildings(RoundScoring scoreData)
        {
            List<(ScoreDetails scoreDetails1, ScoreDetails scoreDetails2, ScoreDetails scoreDetails3, ScoreDetails scoreMeantime)> initialScoring = Players.Select(p => (p.ScoreDetails1.Copy(), p.ScoreDetails2.Copy(), p.ScoreDetails3.Copy(), p.ScoreMeantime.Copy())).ToList();

            if (HasModule(AlhambraBase.ExpansionModule.DesignerPalaceStaff))
            {
                //Palace Staff: each building without a servant tile
                foreach (PlayerScoreData playerScoreData in scoreData.PlayersScoreData)
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
                Players[i].RestoreScore(initialScoring[i]);
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

        public void SetNextRound()
        {
            switch (ScoreRound)
            {
                case ScoringRound.First:
                    ScoreRound = ScoringRound.Second;
                    break;
                case ScoringRound.Second:
                    if (HasThirdBeforeLeftoverRound)
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
                Modules = Modules.Cast<ExpansionModule>().ToList(),
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
        private string GetResourcesFormatDataMessage(int resourceId, params string[] args)
        {
            return CreateResourcesFormatData(resourceId, args).Message;
        }
        private bool CheckFailed(int resourceId, params string[] args)
        {
            return CheckFailed(GetResourcesFormatDataMessage(resourceId, args));
        }
        private bool CheckFailed(string text)
        {
            return AndroidValidateUtils.CheckFailed(Context, text);
        }
    }
}