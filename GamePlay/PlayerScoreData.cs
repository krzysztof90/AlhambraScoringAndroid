using AlhambraScoringAndroid.Attributes;
using AlhambraScoringAndroid.UI;
using AndroidBase.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AlhambraScoringAndroid.GamePlay
{
    public class PlayerScoreData
    {
        protected Game Game { get; private set; }
        public int PlayerNumber { get; private set; }
        public Player Player => Game.GetPlayer(PlayerNumber);
        public string PlayerName => Player.Name;

        [ResultAttribute(ResultType.WallLength)]
        public int WallLength { get; private set; }
        public Dictionary<BuildingType, int> BuildingsCount { get; private set; }
        public Dictionary<BuildingType, int> BonusCardsBuildingsCount { get; private set; }
        public Dictionary<BuildingType, int> SquaresBuildingsCount { get; private set; }
        [ResultAttribute(ResultType.OwnedCharacterTheWiseMan)]
        public bool OwnedCharacterTheWiseMan { get; private set; }
        [ResultAttribute(ResultType.OwnedCharacterTheCityWatch)]
        public bool OwnedCharacterTheCityWatch { get; private set; }
        [ResultAttribute(ResultType.CampsPoints)]
        public int CampsPoints { get; private set; }
        public Dictionary<BuildingType, int> StreetTradersNumber { get; private set; }
        [ResultAttribute(ResultType.TreasuresValue)]
        public int TreasuresCount { get; private set; }
        [ResultAttribute(ResultType.UnprotectedSidesNumber)]
        public int UnprotectedSidesCount { get; private set; }
        [ResultAttribute(ResultType.UnprotectedSidesNeighbouringNumber)]
        public int UnprotectedSidesNeighbouringCount { get; private set; }
        [ResultAttribute(ResultType.BazaarsPoints)]
        public int BazaarsTotalPoints { get; private set; }
        [ResultAttribute(ResultType.ArtOfTheMoorsPoints)]
        public int ArtOfTheMoorsPoints { get; private set; }
        [ResultAttribute(ResultType.FalconsBlackNumber)]
        public int FalconsBlackNumber { get; private set; }
        [ResultAttribute(ResultType.FalconsBrownNumber)]
        public int FalconsBrownNumber { get; private set; }
        [ResultAttribute(ResultType.FalconsWhiteNumber)]
        public int FalconsWhiteNumber { get; private set; }
        [ResultAttribute(ResultType.WatchtowersNumber)]
        public int WatchtowersNumber { get; private set; }
        [ResultAttribute(ResultType.MedinasNumber)]
        public int MedinasNumber { get; private set; }
        [ResultAttribute(ResultType.BuildingsWithoutServantTile)]
        public int BuildingsWithoutServantTile { get; private set; }
        [ResultAttribute(ResultType.CompletedGroupOfFruitBoard1)]
        public bool CompletedGroupOfFruitBoard1 { get; private set; }
        [ResultAttribute(ResultType.CompletedGroupOfFruitBoard2)]
        public bool CompletedGroupOfFruitBoard2 { get; private set; }
        [ResultAttribute(ResultType.CompletedGroupOfFruitBoard3)]
        public bool CompletedGroupOfFruitBoard3 { get; private set; }
        [ResultAttribute(ResultType.CompletedGroupOfFruitBoard4)]
        public bool CompletedGroupOfFruitBoard4 { get; private set; }
        [ResultAttribute(ResultType.CompletedGroupOfFruitBoard5)]
        public bool CompletedGroupOfFruitBoard5 { get; private set; }
        [ResultAttribute(ResultType.CompletedGroupOfFruitBoard6)]
        public bool CompletedGroupOfFruitBoard6 { get; private set; }
        [ResultAttribute(ResultType.FaceDownFruitsNumber)]
        public int FaceDownFruitsCount { get; private set; }
        [ResultAttribute(ResultType.BathhousesPoints)]
        public int BathhousesPoints { get; private set; }
        [ResultAttribute(ResultType.WishingWellsPoints)]
        public int WishingWellsPoints { get; private set; }
        public Dictionary<BuildingType, bool> CompletedProjects { get; private set; }
        [ResultAttribute(ResultType.AnimalsPoints)]
        public int AnimalsPoints { get; private set; }
        public Dictionary<BuildingType, bool> OwnedSemiBuildings { get; private set; }
        [ResultAttribute(ResultType.BlackDiceTotalPips)]
        public int BlackDiceTotalPips { get; private set; }
        public Dictionary<BuildingType, int> ExtensionsBuildingsCount { get; private set; }
        [ResultAttribute(ResultType.HandymenTilesHighestNumber)]
        public int HandymenTilesHighestNumber { get; private set; }
        [ResultAttribute(ResultType.TreasuresPoints)]
        public int TreasuresPoints { get; private set; }
        [ResultAttribute(ResultType.Mission1RowsCount)]
        public int Mission1Count { get; private set; }
        [ResultAttribute(ResultType.Mission2ColumnsCount)]
        public int Mission2Count { get; private set; }
        [ResultAttribute(ResultType.Mission3Adjacent2BuildingsCount)]
        public int Mission3Count { get; private set; }
        [ResultAttribute(ResultType.Mission5LongestDiagonalLine)]
        public int Mission5Count { get; private set; }
        [ResultAttribute(ResultType.Mission6DoubleWallCount)]
        public int Mission6Count { get; private set; }
        [ResultAttribute(ResultType.Mission8PathBuildingsNumber)]
        public int Mission8Count { get; private set; }
        [ResultAttribute(ResultType.Mission9Grids22Count)]
        public int Mission9Count { get; private set; }
        [ResultAttribute(ResultType.SecondLongestWall)]
        public int SecondLongestWallLength { get; private set; }
        [ResultAttribute(ResultType.MoatLength)]
        public int MoatLength { get; private set; }
        public Dictionary<GranadaBuildingType, int> GranadaBuildingsCount { get; private set; }
        [ResultAttribute(ResultType.WallMoatCombination)]
        public int WallMoatCombinationLength { get; private set; }

        public BuildingType? TheWiseManBuildingType { get; set; }
        public int? MedinaHighestPrice { get; set; }
        public Dictionary<GranadaBuildingType, int> GranadaBuildingsHighestPrices { get; set; }

        public int AllBuildingsCount => BuildingsCount.Sum(b => b.Value);
        //including 1 starting tile
        //not 'Available'Tiles
        public int AllTilesCount => AllBuildingsCount + MedinasNumber + 1;
        public int AllWallBuildingsCount => BuildingsCount.Sum(b => Math.Min(b.Value, Game.WallBuildingsMaxCount[b.Key]));
        public int AllWallTilesCount => AllWallBuildingsCount + MedinasNumber;
        public int AllGranadaBuildingsCount => GranadaBuildingsCount.Sum(b => b.Value);
        //including 1 starting tile
        public int AllGranadaTilesCount => GranadaBuildingsCount.Sum(b => b.Value)+1;

        public Dictionary<BuildingType, int> BaseBuildingsCount => BuildingsCount.ToDictionary(b => b.Key, b => Math.Min(b.Value, Game.BaseBuildingsMaxCount[b.Key]));

        public PlayerScoreData(PlaceholderPlayerScoreFragment fragment)
        {
            Game = fragment.Game;
            PlayerNumber = fragment.PlayerNumber;

            foreach (PropertyInfo field in this.GetType().GetProperties().Where(p => p.GetFieldAttribute<ResultAttribute>() != null))
            {
                field.SetValue(this, fragment.GetControlValueObject(field.GetFieldAttribute<ResultAttribute>().ResultType));
            }

            BuildingsCount = new Dictionary<BuildingType, int>()
            {
                [BuildingType.Pavilion] = fragment.GetControlValue<int>(ResultType.PavilionNumber),
                [BuildingType.Seraglio] = fragment.GetControlValue<int>(ResultType.SeraglioNumber),
                [BuildingType.Arcades] = fragment.GetControlValue<int>(ResultType.ArcadesNumber),
                [BuildingType.Chambers] = fragment.GetControlValue<int>(ResultType.ChambersNumber),
                [BuildingType.Garden] = fragment.GetControlValue<int>(ResultType.GardenNumber),
                [BuildingType.Tower] = fragment.GetControlValue<int>(ResultType.TowerNumber),
            };
            BonusCardsBuildingsCount = new Dictionary<BuildingType, int>()
            {
                [BuildingType.Pavilion] = fragment.GetControlValue<int>(ResultType.BonusCardsPavilionNumber),
                [BuildingType.Seraglio] = fragment.GetControlValue<int>(ResultType.BonusCardsSeraglioNumber),
                [BuildingType.Arcades] = fragment.GetControlValue<int>(ResultType.BonusCardsArcadesNumber),
                [BuildingType.Chambers] = fragment.GetControlValue<int>(ResultType.BonusCardsChambersNumber),
                [BuildingType.Garden] = fragment.GetControlValue<int>(ResultType.BonusCardsGardenNumber),
                [BuildingType.Tower] = fragment.GetControlValue<int>(ResultType.BonusCardsTowerNumber),
            };
            SquaresBuildingsCount = new Dictionary<BuildingType, int>()
            {
                [BuildingType.Pavilion] = fragment.GetControlValue<int>(ResultType.SquaresPavilionNumber),
                [BuildingType.Seraglio] = fragment.GetControlValue<int>(ResultType.SquaresSeraglioNumber),
                [BuildingType.Arcades] = fragment.GetControlValue<int>(ResultType.SquaresArcadesNumber),
                [BuildingType.Chambers] = fragment.GetControlValue<int>(ResultType.SquaresChambersNumber),
                [BuildingType.Garden] = fragment.GetControlValue<int>(ResultType.SquaresGardenNumber),
                [BuildingType.Tower] = fragment.GetControlValue<int>(ResultType.SquaresTowerNumber),
            };
            StreetTradersNumber = new Dictionary<BuildingType, int>()
            {
                [BuildingType.Pavilion] = fragment.GetControlValue<int>(ResultType.StreetTradersPavilionNumber),
                [BuildingType.Seraglio] = fragment.GetControlValue<int>(ResultType.StreetTradersSeraglioNumber),
                [BuildingType.Arcades] = fragment.GetControlValue<int>(ResultType.StreetTradersArcadesNumber),
                [BuildingType.Chambers] = fragment.GetControlValue<int>(ResultType.StreetTradersChambersNumber),
                [BuildingType.Garden] = fragment.GetControlValue<int>(ResultType.StreetTradersGardenNumber),
                [BuildingType.Tower] = fragment.GetControlValue<int>(ResultType.StreetTradersTowerNumber),
            };
            CompletedProjects = new Dictionary<BuildingType, bool>()
            {
                [BuildingType.Pavilion] = fragment.GetControlValue<bool>(ResultType.CompletedProjectPavilion),
                [BuildingType.Seraglio] = fragment.GetControlValue<bool>(ResultType.CompletedProjectSeraglio),
                [BuildingType.Arcades] = fragment.GetControlValue<bool>(ResultType.CompletedProjectArcades),
                [BuildingType.Chambers] = fragment.GetControlValue<bool>(ResultType.CompletedProjectChambers),
                [BuildingType.Garden] = fragment.GetControlValue<bool>(ResultType.CompletedProjectGarden),
                [BuildingType.Tower] = fragment.GetControlValue<bool>(ResultType.CompletedProjectTower),
            };
            OwnedSemiBuildings = new Dictionary<BuildingType, bool>()
            {
                [BuildingType.Pavilion] = fragment.GetControlValue<bool>(ResultType.OwnedSemiBuildingPavilion),
                [BuildingType.Seraglio] = fragment.GetControlValue<bool>(ResultType.OwnedSemiBuildingSeraglio),
                [BuildingType.Arcades] = fragment.GetControlValue<bool>(ResultType.OwnedSemiBuildingArcades),
                [BuildingType.Chambers] = fragment.GetControlValue<bool>(ResultType.OwnedSemiBuildingChambers),
                [BuildingType.Garden] = fragment.GetControlValue<bool>(ResultType.OwnedSemiBuildingGarden),
                [BuildingType.Tower] = fragment.GetControlValue<bool>(ResultType.OwnedSemiBuildingTower),
            };
            ExtensionsBuildingsCount = new Dictionary<BuildingType, int>()
            {
                [BuildingType.Pavilion] = fragment.GetControlValue<int>(ResultType.ExtensionsPavilionCount),
                [BuildingType.Seraglio] = fragment.GetControlValue<int>(ResultType.ExtensionsSeraglioCount),
                [BuildingType.Arcades] = fragment.GetControlValue<int>(ResultType.ExtensionsArcadesCount),
                [BuildingType.Chambers] = fragment.GetControlValue<int>(ResultType.ExtensionsChambersCount),
                [BuildingType.Garden] = fragment.GetControlValue<int>(ResultType.ExtensionsGardenCount),
                [BuildingType.Tower] = fragment.GetControlValue<int>(ResultType.ExtensionsTowerCount),
            };
            GranadaBuildingsCount = new Dictionary<GranadaBuildingType, int>()
            {
                [GranadaBuildingType.Arena] = fragment.GetControlValue<int>(ResultType.ArenaCount),
                [GranadaBuildingType.BathHouse] = fragment.GetControlValue<int>(ResultType.BathHouseCount),
                [GranadaBuildingType.Library] = fragment.GetControlValue<int>(ResultType.LibraryCount),
                [GranadaBuildingType.Hostel] = fragment.GetControlValue<int>(ResultType.HostelCount),
                [GranadaBuildingType.Hospital] = fragment.GetControlValue<int>(ResultType.HospitalCount),
                [GranadaBuildingType.Market] = fragment.GetControlValue<int>(ResultType.MarketCount),
                [GranadaBuildingType.Park] = fragment.GetControlValue<int>(ResultType.ParkCount),
                [GranadaBuildingType.School] = fragment.GetControlValue<int>(ResultType.SchoolCount),
                [GranadaBuildingType.ResidentialArea] = fragment.GetControlValue<int>(ResultType.ResidentialAreaCount),
            };
        }
    }
}