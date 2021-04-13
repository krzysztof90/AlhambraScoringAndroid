using AlhambraScoringAndroid.UI;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.GamePlay
{
    public class PlayerScoreData
    {
        public int PlayerNumber { get; private set; }

        public int WallLength { get; private set; }
        public Dictionary<BuildingType, int> BuildingsCount { get; private set; }
        public Dictionary<BuildingType, int> BonusCardsBuildingsCount { get; private set; }
        public Dictionary<BuildingType, int> SquaresBuildingsCount { get; private set; }
        public bool OwnedCharacterTheWiseMan { get; private set; }
        public bool OwnedCharacterTheCityWatch { get; private set; }
        public int CampsPoints { get; private set; }
        public Dictionary<BuildingType, int> StreetTradersNumber { get; private set; }
        public int TreasuresCount { get; private set; }
        public int UnprotectedSidesCount { get; private set; }
        public int UnprotectedSidesNeighbouringCount { get; private set; }
        public int BazaarsTotalPoints { get; private set; }
        public int ArtOfTheMoorsPoints { get; private set; }
        public int FalconsBlackNumber { get; private set; }
        public int FalconsBrownNumber { get; private set; }
        public int FalconsWhiteNumber { get; private set; }
        public int WatchtowersNumber { get; private set; }
        public int MedinasNumber { get; private set; }
        public int BuildingsWithoutServantTile { get; private set; }
        public bool CompletedGroupOfFruitBoard1 { get; private set; }
        public bool CompletedGroupOfFruitBoard2 { get; private set; }
        public bool CompletedGroupOfFruitBoard3 { get; private set; }
        public bool CompletedGroupOfFruitBoard4 { get; private set; }
        public bool CompletedGroupOfFruitBoard5 { get; private set; }
        public bool CompletedGroupOfFruitBoard6 { get; private set; }
        public int FaceDownFruitsCount { get; private set; }
        public int BathhousesPoints { get; private set; }
        public int WishingWellsPoints { get; private set; }
        public Dictionary<BuildingType, bool> CompletedProjects { get; private set; }
        public int AnimalsPoints { get; private set; }
        public Dictionary<BuildingType, bool> OwnedSemiBuildings { get; private set; }
        public int BlackDiceTotalPips { get; private set; }
        public Dictionary<BuildingType, int> ExtensionsBuildingsCount { get; private set; }
        public int HandymenTilesHighestNumber { get; private set; }
        public int TreasuresValue { get; private set; }
        public int Mission1Count { get; private set; }
        public int Mission2Count { get; private set; }
        public int Mission3Count { get; private set; }
        public int Mission5Count { get; private set; }
        public int Mission6Count { get; private set; }
        public int Mission8Count { get; private set; }
        public int Mission9Count { get; private set; }
        public int SecondLongestWallLength { get; private set; }
        public int MoatLength { get; private set; }
        public Dictionary<GranadaBuildingType, int> GranadaBuildingsCount { get; private set; }
        public int WallMoatCombinationLength { get; private set; }

        public int AllBuildingsCount => BuildingsCount.Sum(b => b.Value);

        public PlayerScoreData(PlaceholderPlayerScoreFragment fragment)
        {
            PlayerNumber = fragment.PlayerNumber;
            WallLength = fragment.WallLength;
            BuildingsCount = fragment.BuildingsCount;
            BonusCardsBuildingsCount = fragment.BonusCardsBuildingsCount;
            SquaresBuildingsCount = fragment.SquaresBuildingsCount;
            OwnedCharacterTheWiseMan = fragment.OwnedCharacterTheWiseMan;
            OwnedCharacterTheCityWatch = fragment.OwnedCharacterTheCityWatch;
            CampsPoints = fragment.CampsPoints;
            StreetTradersNumber = fragment.StreetTradersNumber;
            TreasuresCount = fragment.TreasuresCount;
            UnprotectedSidesCount = fragment.UnprotectedSidesCount;
            UnprotectedSidesNeighbouringCount = fragment.UnprotectedSidesNeighbouringCount;
            BazaarsTotalPoints = fragment.BazaarsTotalPoints;
            ArtOfTheMoorsPoints = fragment.ArtOfTheMoorsPoints;
            FalconsBlackNumber = fragment.FalconsBlackNumber;
            FalconsBrownNumber = fragment.FalconsBrownNumber;
            FalconsWhiteNumber = fragment.FalconsWhiteNumber;
            WatchtowersNumber = fragment.WatchtowersNumber;
            MedinasNumber = fragment.MedinasNumber;
            BuildingsWithoutServantTile = fragment.BuildingsWithoutServantTile;
            CompletedGroupOfFruitBoard1 = fragment.CompletedGroupOfFruitBoard1;
            CompletedGroupOfFruitBoard2 = fragment.CompletedGroupOfFruitBoard2;
            CompletedGroupOfFruitBoard3 = fragment.CompletedGroupOfFruitBoard3;
            CompletedGroupOfFruitBoard4 = fragment.CompletedGroupOfFruitBoard4;
            CompletedGroupOfFruitBoard5 = fragment.CompletedGroupOfFruitBoard5;
            CompletedGroupOfFruitBoard6 = fragment.CompletedGroupOfFruitBoard6;
            FaceDownFruitsCount = fragment.FaceDownFruitsCount;
            BathhousesPoints = fragment.BathhousesPoints;
            WishingWellsPoints = fragment.WishingWellsPoints;
            CompletedProjects = fragment.CompletedProjects;
            AnimalsPoints = fragment.AnimalsPoints;
            OwnedSemiBuildings = fragment.OwnedSemiBuildings;
            BlackDiceTotalPips = fragment.BlackDiceTotalPips;
            ExtensionsBuildingsCount = fragment.ExtensionsBuildingsCount;
            HandymenTilesHighestNumber = fragment.HandymenTilesHighestNumber;
            TreasuresValue = fragment.TreasuresValue;
            Mission1Count = fragment.Mission1Count;
            Mission2Count = fragment.Mission2Count;
            Mission3Count = fragment.Mission3Count;
            Mission5Count = fragment.Mission5Count;
            Mission6Count = fragment.Mission6Count;
            Mission8Count = fragment.Mission8Count;
            Mission9Count = fragment.Mission9Count;
            SecondLongestWallLength = fragment.SecondLongestWallLength;
            MoatLength = fragment.MoatLength;
            GranadaBuildingsCount = fragment.GranadaBuildingsCount;
            WallMoatCombinationLength = fragment.WallMoatCombinationLength;
        }

        public PlayerScoreData(PlaceholderPlayerScoreBeforeAssignLeftoverFragment fragment)
        {
            PlayerNumber = fragment.PlayerNumber;
            BuildingsWithoutServantTile = fragment.BuildingsWithoutServantTile;
        }
    }
}