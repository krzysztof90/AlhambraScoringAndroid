using AlhambraScoringAndroid.GamePlay;
using Android.Views;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI
{
    public class PlaceholderPlayerScoreFragment : PlaceholderPlayerScoreFragmentBase
    {
        ScoreLineNumberView wallsCountNumericUpDown;
        ScoreLineNumberView pavilionCountNumericUpDown;
        ScoreLineNumberView seraglioCountNumericUpDown;
        ScoreLineNumberView arcadesCountNumericUpDown;
        ScoreLineNumberView chambersCountNumericUpDown;
        ScoreLineNumberView gardenCountNumericUpDown;
        ScoreLineNumberView towerCountNumericUpDown;
        ScoreLineNumberView buildingsWithoutServantTileNumericUpDown;
        ScoreLineCheckBoxView completedGroupOfFruitBoard1CheckBox;
        ScoreLineCheckBoxView completedGroupOfFruitBoard2CheckBox;
        ScoreLineCheckBoxView completedGroupOfFruitBoard3CheckBox;
        ScoreLineCheckBoxView completedGroupOfFruitBoard4CheckBox;
        ScoreLineCheckBoxView completedGroupOfFruitBoard5CheckBox;
        ScoreLineCheckBoxView completedGroupOfFruitBoard6CheckBox;
        ScoreLineNumberView faceDownFruitsCountNumericUpDown;
        ScoreLineNumberView bathhousesPointsNumericUpDown;
        ScoreLineNumberView wishingWellsPointsNumericUpDown;
        ScoreLineCheckBoxView completedProjectPavilionCheckBox;
        ScoreLineCheckBoxView completedProjectSeraglioCheckBox;
        ScoreLineCheckBoxView completedProjectArcadesCheckBox;
        ScoreLineCheckBoxView completedProjectChambersCheckBox;
        ScoreLineCheckBoxView completedProjectGardenCheckBox;
        ScoreLineCheckBoxView completedProjectTowerCheckBox;
        ScoreLineNumberView animalsPointsNumericUpDown;
        ScoreLineCheckBoxView ownedSemiBuildingPavilionCheckBox;
        ScoreLineCheckBoxView ownedSemiBuildingSeraglioCheckBox;
        ScoreLineCheckBoxView ownedSemiBuildingArcadesCheckBox;
        ScoreLineCheckBoxView ownedSemiBuildingChambersCheckBox;
        ScoreLineCheckBoxView ownedSemiBuildingGardenCheckBox;
        ScoreLineCheckBoxView ownedSemiBuildingTowerCheckBox;
        ScoreLineNumberView blackDiceTotalPipsNumericUpDown;
        ScoreLineNumberView handymenTilesHighestNumberNumericUpDown;
        ScoreLineNumberView treasuresValueNumericUpDown;
        ScoreLineNumberView mission1RowsCountNumericUpDown;
        ScoreLineNumberView mission2ColumnsCountNumericUpDown;
        ScoreLineNumberView mission3Adjacent2BuildingsCountNumericUpDown;
        ScoreLineCheckBoxView mission4SecondLongestWallCheckBox;
        ScoreLineNumberView mission5LongestDiagonalLineNumericUpDown;
        ScoreLineNumberView mission6DoubleWallCountNumericUpDown;
        ScoreLineNumberView mission7DiffernetTypesNumberNumericUpDown;
        ScoreLineNumberView mission8PathBuildingsNumberNumericUpDown;
        ScoreLineNumberView mission9Grids22CountNumericUpDown;
        ScoreLineNumberView secondLongestWallNumericUpDown;

        protected override int GetContentLayout()
        {
            return Resource.Layout.fragment_game_score;
        }

        protected override void CreateControls()
        {
            wallsCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.wallsCountNumericUpDown);
            pavilionCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.pavilionCountNumericUpDown);
            seraglioCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.seraglioCountNumericUpDown);
            arcadesCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.arcadesCountNumericUpDown);
            chambersCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.chambersCountNumericUpDown);
            gardenCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.gardenCountNumericUpDown);
            towerCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.towerCountNumericUpDown);
            buildingsWithoutServantTileNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.buildingsWithoutServantTileNumericUpDown);
            completedGroupOfFruitBoard1CheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedGroupOfFruitBoard1CheckBox);
            completedGroupOfFruitBoard2CheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedGroupOfFruitBoard2CheckBox);
            completedGroupOfFruitBoard3CheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedGroupOfFruitBoard3CheckBox);
            completedGroupOfFruitBoard4CheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedGroupOfFruitBoard4CheckBox);
            completedGroupOfFruitBoard5CheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedGroupOfFruitBoard5CheckBox);
            completedGroupOfFruitBoard6CheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedGroupOfFruitBoard6CheckBox);
            faceDownFruitsCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.faceDownFruitsCountNumericUpDown);
            bathhousesPointsNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.bathhousesPointsNumericUpDown);
            wishingWellsPointsNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.wishingWellsPointsNumericUpDown);
            completedProjectPavilionCheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedProjectPavilionCheckBox);
            completedProjectSeraglioCheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedProjectSeraglioCheckBox);
            completedProjectArcadesCheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedProjectArcadesCheckBox);
            completedProjectChambersCheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedProjectChambersCheckBox);
            completedProjectGardenCheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedProjectGardenCheckBox);
            completedProjectTowerCheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedProjectTowerCheckBox);
            animalsPointsNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.animalsPointsNumericUpDown);
            ownedSemiBuildingPavilionCheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.ownedSemiBuildingPavilionCheckBox);
            ownedSemiBuildingSeraglioCheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.ownedSemiBuildingSeraglioCheckBox);
            ownedSemiBuildingArcadesCheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.ownedSemiBuildingArcadesCheckBox);
            ownedSemiBuildingChambersCheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.ownedSemiBuildingChambersCheckBox);
            ownedSemiBuildingGardenCheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.ownedSemiBuildingGardenCheckBox);
            ownedSemiBuildingTowerCheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.ownedSemiBuildingTowerCheckBox);
            blackDiceTotalPipsNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.blackDiceTotalPipsNumericUpDown);
            handymenTilesHighestNumberNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.handymenTilesHighestNumberNumericUpDown);
            treasuresValueNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.treasuresValueNumericUpDown);
            mission1RowsCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.mission1RowsCountNumericUpDown);
            mission2ColumnsCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.mission2ColumnsCountNumericUpDown);
            mission3Adjacent2BuildingsCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.mission3Adjacent2BuildingsCountNumericUpDown);
            mission4SecondLongestWallCheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.mission4SecondLongestWallCheckBox);
            mission5LongestDiagonalLineNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.mission5LongestDiagonalLineNumericUpDown);
            mission6DoubleWallCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.mission6DoubleWallCountNumericUpDown);
            mission7DiffernetTypesNumberNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.mission7DiffernetTypesNumberNumericUpDown);
            mission8PathBuildingsNumberNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.mission8PathBuildingsNumberNumericUpDown);
            mission9Grids22CountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.mission9Grids22CountNumericUpDown);
            secondLongestWallNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.secondLongestWallNumericUpDown);
        }

        protected override void AddControls()
        {
            Controls.Add(wallsCountNumericUpDown);
            Controls.Add(pavilionCountNumericUpDown);
            Controls.Add(seraglioCountNumericUpDown);
            Controls.Add(arcadesCountNumericUpDown);
            Controls.Add(chambersCountNumericUpDown);
            Controls.Add(gardenCountNumericUpDown);
            Controls.Add(towerCountNumericUpDown);
            Controls.Add(buildingsWithoutServantTileNumericUpDown);
            Controls.Add(completedGroupOfFruitBoard1CheckBox);
            Controls.Add(completedGroupOfFruitBoard2CheckBox);
            Controls.Add(completedGroupOfFruitBoard3CheckBox);
            Controls.Add(completedGroupOfFruitBoard4CheckBox);
            Controls.Add(completedGroupOfFruitBoard5CheckBox);
            Controls.Add(completedGroupOfFruitBoard6CheckBox);
            Controls.Add(faceDownFruitsCountNumericUpDown);
            Controls.Add(bathhousesPointsNumericUpDown);
            Controls.Add(wishingWellsPointsNumericUpDown);
            Controls.Add(completedProjectPavilionCheckBox);
            Controls.Add(completedProjectSeraglioCheckBox);
            Controls.Add(completedProjectArcadesCheckBox);
            Controls.Add(completedProjectChambersCheckBox);
            Controls.Add(completedProjectGardenCheckBox);
            Controls.Add(completedProjectTowerCheckBox);
            Controls.Add(animalsPointsNumericUpDown);
            Controls.Add(ownedSemiBuildingPavilionCheckBox);
            Controls.Add(ownedSemiBuildingSeraglioCheckBox);
            Controls.Add(ownedSemiBuildingArcadesCheckBox);
            Controls.Add(ownedSemiBuildingChambersCheckBox);
            Controls.Add(ownedSemiBuildingGardenCheckBox);
            Controls.Add(ownedSemiBuildingTowerCheckBox);
            Controls.Add(blackDiceTotalPipsNumericUpDown);
            Controls.Add(handymenTilesHighestNumberNumericUpDown);
            Controls.Add(treasuresValueNumericUpDown);
            Controls.Add(mission1RowsCountNumericUpDown);
            Controls.Add(mission2ColumnsCountNumericUpDown);
            Controls.Add(mission3Adjacent2BuildingsCountNumericUpDown);
            Controls.Add(mission4SecondLongestWallCheckBox);
            Controls.Add(mission5LongestDiagonalLineNumericUpDown);
            Controls.Add(mission6DoubleWallCountNumericUpDown);
            Controls.Add(mission7DiffernetTypesNumberNumericUpDown);
            Controls.Add(mission8PathBuildingsNumberNumericUpDown);
            Controls.Add(mission9Grids22CountNumericUpDown);
            Controls.Add(secondLongestWallNumericUpDown);
        }

        protected override void SetControlsProperties()
        {
            blackDiceTotalPipsNumericUpDown.OnValueChange = () => { VisibleSecondLongestWall(); };
            mission4SecondLongestWallCheckBox.OnValueChange = () => { VisibleSecondLongestWall(); };

            //TODO maximum for mission3Adjacent2BuildingsCountNumericUpDown, mission6DoubleWallCountNumericUpDown, mission9Grids22CountNumericUpDown
            wallsCountNumericUpDown.SetNumberRange(0, Game.AllTilesCount * 2 + 2);
            pavilionCountNumericUpDown.SetNumberRange(0, Game.BuildingsMaxCount[BuildingType.Pavilion]);
            seraglioCountNumericUpDown.SetNumberRange(0, Game.BuildingsMaxCount[BuildingType.Seraglio]);
            arcadesCountNumericUpDown.SetNumberRange(0, Game.BuildingsMaxCount[BuildingType.Arcades]);
            chambersCountNumericUpDown.SetNumberRange(0, Game.BuildingsMaxCount[BuildingType.Chambers]);
            gardenCountNumericUpDown.SetNumberRange(0, Game.BuildingsMaxCount[BuildingType.Garden]);
            towerCountNumericUpDown.SetNumberRange(0, Game.BuildingsMaxCount[BuildingType.Tower]);
            buildingsWithoutServantTileNumericUpDown.SetNumberRange(0, Game.AllBuildingsCount);
            faceDownFruitsCountNumericUpDown.SetNumberRange(0, 35);
            bathhousesPointsNumericUpDown.SetNumberRange(0, (Game.AllTilesCount - 1) * 4 * 6);
            wishingWellsPointsNumericUpDown.SetNumberRange(0, 24);
            animalsPointsNumericUpDown.SetNumberRange(0, 24);
            blackDiceTotalPipsNumericUpDown.SetNumberRange(0, 18);
            handymenTilesHighestNumberNumericUpDown.SetNumberRange(0, 48);
            treasuresValueNumericUpDown.SetNumberRange(0, 15);
            mission1RowsCountNumericUpDown.SetNumberRange(0, Game.AllTilesCount / 3);
            mission2ColumnsCountNumericUpDown.SetNumberRange(0, Game.AllTilesCount / 3);
            mission5LongestDiagonalLineNumericUpDown.SetNumberRange(0, (Game.AllTilesCount + 1) / 2);
            mission7DiffernetTypesNumberNumericUpDown.SetNumberRange(0, 6);
            mission8PathBuildingsNumberNumericUpDown.SetNumberRange(0, (Game.AllTilesCount + 1) / 2);
            secondLongestWallNumericUpDown.SetNumberRange(0, (Game.AllTilesCount * 2 + 2) / 2 - 2);

            VisibleSecondLongestWall();

            AddConditionToVisible(buildingsWithoutServantTileNumericUpDown, Game.HasModule(ExpansionModule.DesignerPalaceStaff));
            AddConditionToVisible(completedGroupOfFruitBoard1CheckBox, Game.HasModule(ExpansionModule.DesignerOrchards));
            AddConditionToVisible(completedGroupOfFruitBoard2CheckBox, Game.HasModule(ExpansionModule.DesignerOrchards));
            AddConditionToVisible(completedGroupOfFruitBoard3CheckBox, Game.HasModule(ExpansionModule.DesignerOrchards));
            AddConditionToVisible(completedGroupOfFruitBoard4CheckBox, Game.HasModule(ExpansionModule.DesignerOrchards));
            AddConditionToVisible(completedGroupOfFruitBoard5CheckBox, Game.HasModule(ExpansionModule.DesignerOrchards));
            AddConditionToVisible(completedGroupOfFruitBoard6CheckBox, Game.HasModule(ExpansionModule.DesignerOrchards));
            AddConditionToVisible(faceDownFruitsCountNumericUpDown, Game.HasModule(ExpansionModule.DesignerOrchards));
            AddConditionToVisible(bathhousesPointsNumericUpDown, Game.HasModule(ExpansionModule.DesignerBathhouses));
            AddConditionToVisible(wishingWellsPointsNumericUpDown, Game.HasModule(ExpansionModule.DesignerWishingWell));
            AddConditionToVisible(completedProjectPavilionCheckBox, Game.HasModule(ExpansionModule.DesignerFreshColors));
            AddConditionToVisible(completedProjectSeraglioCheckBox, Game.HasModule(ExpansionModule.DesignerFreshColors));
            AddConditionToVisible(completedProjectArcadesCheckBox, Game.HasModule(ExpansionModule.DesignerFreshColors));
            AddConditionToVisible(completedProjectChambersCheckBox, Game.HasModule(ExpansionModule.DesignerFreshColors));
            AddConditionToVisible(completedProjectGardenCheckBox, Game.HasModule(ExpansionModule.DesignerFreshColors));
            AddConditionToVisible(completedProjectTowerCheckBox, Game.HasModule(ExpansionModule.DesignerFreshColors));
            AddConditionToVisible(animalsPointsNumericUpDown, Game.HasModule(ExpansionModule.DesignerAlhambraZoo));
            AddConditionToVisible(ownedSemiBuildingPavilionCheckBox, Game.HasModule(ExpansionModule.DesignerGatesWithoutEnd));
            AddConditionToVisible(ownedSemiBuildingSeraglioCheckBox, Game.HasModule(ExpansionModule.DesignerGatesWithoutEnd));
            AddConditionToVisible(ownedSemiBuildingArcadesCheckBox, Game.HasModule(ExpansionModule.DesignerGatesWithoutEnd));
            AddConditionToVisible(ownedSemiBuildingChambersCheckBox, Game.HasModule(ExpansionModule.DesignerGatesWithoutEnd));
            AddConditionToVisible(ownedSemiBuildingGardenCheckBox, Game.HasModule(ExpansionModule.DesignerGatesWithoutEnd));
            AddConditionToVisible(ownedSemiBuildingTowerCheckBox, Game.HasModule(ExpansionModule.DesignerGatesWithoutEnd));
            AddConditionToVisible(blackDiceTotalPipsNumericUpDown, Game.HasModule(ExpansionModule.DesignerBuildingsOfPower));
            AddConditionToVisible(handymenTilesHighestNumberNumericUpDown, Game.HasModule(ExpansionModule.DesignerHandymen));
            AddConditionToVisible(treasuresValueNumericUpDown, Game.HasModule(ExpansionModule.DesignerTreasures));
            AddConditionToVisible(mission1RowsCountNumericUpDown, Game.HasModule(ExpansionModule.DesignerCaliphsGuidelines));
            AddConditionToVisible(mission2ColumnsCountNumericUpDown, Game.HasModule(ExpansionModule.DesignerCaliphsGuidelines));
            AddConditionToVisible(mission3Adjacent2BuildingsCountNumericUpDown, Game.HasModule(ExpansionModule.DesignerCaliphsGuidelines));
            AddConditionToVisible(mission4SecondLongestWallCheckBox, Game.HasModule(ExpansionModule.DesignerCaliphsGuidelines));
            AddConditionToVisible(mission5LongestDiagonalLineNumericUpDown, Game.HasModule(ExpansionModule.DesignerCaliphsGuidelines));
            AddConditionToVisible(mission6DoubleWallCountNumericUpDown, Game.HasModule(ExpansionModule.DesignerCaliphsGuidelines));
            AddConditionToVisible(mission7DiffernetTypesNumberNumericUpDown, Game.HasModule(ExpansionModule.DesignerCaliphsGuidelines));
            AddConditionToVisible(mission8PathBuildingsNumberNumericUpDown, Game.HasModule(ExpansionModule.DesignerCaliphsGuidelines));
            AddConditionToVisible(mission9Grids22CountNumericUpDown, Game.HasModule(ExpansionModule.DesignerCaliphsGuidelines));

            AddConditionToVisible(buildingsWithoutServantTileNumericUpDown, !IsFinalRound);
            AddConditionToVisible(completedGroupOfFruitBoard1CheckBox, IsFinalRound);
            AddConditionToVisible(completedGroupOfFruitBoard2CheckBox, IsFinalRound);
            AddConditionToVisible(completedGroupOfFruitBoard3CheckBox, IsFinalRound);
            AddConditionToVisible(completedGroupOfFruitBoard4CheckBox, IsFinalRound);
            AddConditionToVisible(completedGroupOfFruitBoard5CheckBox, IsFinalRound);
            AddConditionToVisible(completedGroupOfFruitBoard6CheckBox, IsFinalRound);
            AddConditionToVisible(faceDownFruitsCountNumericUpDown, IsFinalRound);
            AddConditionToVisible(treasuresValueNumericUpDown, IsFinalRound);
            AddConditionToVisible(mission1RowsCountNumericUpDown, IsFinalRound);
            AddConditionToVisible(mission2ColumnsCountNumericUpDown, IsFinalRound);
            AddConditionToVisible(mission3Adjacent2BuildingsCountNumericUpDown, IsFinalRound);
            AddConditionToVisible(mission4SecondLongestWallCheckBox, IsFinalRound);
            AddConditionToVisible(mission5LongestDiagonalLineNumericUpDown, IsFinalRound);
            AddConditionToVisible(mission6DoubleWallCountNumericUpDown, IsFinalRound);
            AddConditionToVisible(mission7DiffernetTypesNumberNumericUpDown, IsFinalRound);
            AddConditionToVisible(mission8PathBuildingsNumberNumericUpDown, IsFinalRound);
            AddConditionToVisible(mission9Grids22CountNumericUpDown, IsFinalRound);

            AddConditionToVisible(wallsCountNumericUpDown, !IsDirk);
            AddConditionToVisible(buildingsWithoutServantTileNumericUpDown, !IsDirk);
            AddConditionToVisible(completedGroupOfFruitBoard1CheckBox, !IsDirk);
            AddConditionToVisible(completedGroupOfFruitBoard2CheckBox, !IsDirk);
            AddConditionToVisible(completedGroupOfFruitBoard3CheckBox, !IsDirk);
            AddConditionToVisible(completedGroupOfFruitBoard4CheckBox, !IsDirk);
            AddConditionToVisible(completedGroupOfFruitBoard5CheckBox, !IsDirk);
            AddConditionToVisible(completedGroupOfFruitBoard6CheckBox, !IsDirk);
            AddConditionToVisible(faceDownFruitsCountNumericUpDown, !IsDirk);
            AddConditionToVisible(bathhousesPointsNumericUpDown, !IsDirk);
            AddConditionToVisible(wishingWellsPointsNumericUpDown, !IsDirk);
            AddConditionToVisible(completedProjectPavilionCheckBox, !IsDirk);
            AddConditionToVisible(completedProjectSeraglioCheckBox, !IsDirk);
            AddConditionToVisible(completedProjectArcadesCheckBox, !IsDirk);
            AddConditionToVisible(completedProjectChambersCheckBox, !IsDirk);
            AddConditionToVisible(completedProjectGardenCheckBox, !IsDirk);
            AddConditionToVisible(completedProjectTowerCheckBox, !IsDirk);
            AddConditionToVisible(animalsPointsNumericUpDown, !IsDirk);
            AddConditionToVisible(ownedSemiBuildingPavilionCheckBox, !IsDirk);
            AddConditionToVisible(ownedSemiBuildingSeraglioCheckBox, !IsDirk);
            AddConditionToVisible(ownedSemiBuildingArcadesCheckBox, !IsDirk);
            AddConditionToVisible(ownedSemiBuildingChambersCheckBox, !IsDirk);
            AddConditionToVisible(ownedSemiBuildingGardenCheckBox, !IsDirk);
            AddConditionToVisible(ownedSemiBuildingTowerCheckBox, !IsDirk);
            AddConditionToVisible(blackDiceTotalPipsNumericUpDown, !IsDirk);
            AddConditionToVisible(handymenTilesHighestNumberNumericUpDown, !IsDirk);
            AddConditionToVisible(treasuresValueNumericUpDown, !IsDirk);
            AddConditionToVisible(mission1RowsCountNumericUpDown, !IsDirk);
            AddConditionToVisible(mission2ColumnsCountNumericUpDown, !IsDirk);
            AddConditionToVisible(mission3Adjacent2BuildingsCountNumericUpDown, !IsDirk);
            AddConditionToVisible(mission4SecondLongestWallCheckBox, !IsDirk);
            AddConditionToVisible(mission5LongestDiagonalLineNumericUpDown, !IsDirk);
            AddConditionToVisible(mission6DoubleWallCountNumericUpDown, !IsDirk);
            AddConditionToVisible(mission7DiffernetTypesNumberNumericUpDown, !IsDirk);
            AddConditionToVisible(mission8PathBuildingsNumberNumericUpDown, !IsDirk);
            AddConditionToVisible(mission9Grids22CountNumericUpDown, !IsDirk);
        }

        public PlaceholderPlayerScoreFragment(int _index, Game game) : base(_index, game)
        {
        }

        private void VisibleSecondLongestWall()
        {
            secondLongestWallNumericUpDown.Visibility = BlackDiceTotalPips != 0 || Mission4Available ? ViewStates.Visible : ViewStates.Gone;
        }

        public int WallLength => GetNumberValue(wallsCountNumericUpDown);
        public int BuildingsWithoutServantTile => GetNumberValue(buildingsWithoutServantTileNumericUpDown);
        public bool CompletedGroupOfFruitBoard1 => GetCheckBoxValue(completedGroupOfFruitBoard1CheckBox);
        public bool CompletedGroupOfFruitBoard2 => GetCheckBoxValue(completedGroupOfFruitBoard2CheckBox);
        public bool CompletedGroupOfFruitBoard3 => GetCheckBoxValue(completedGroupOfFruitBoard3CheckBox);
        public bool CompletedGroupOfFruitBoard4 => GetCheckBoxValue(completedGroupOfFruitBoard4CheckBox);
        public bool CompletedGroupOfFruitBoard5 => GetCheckBoxValue(completedGroupOfFruitBoard5CheckBox);
        public bool CompletedGroupOfFruitBoard6 => GetCheckBoxValue(completedGroupOfFruitBoard6CheckBox);
        public int FaceDownFruitsCount => GetNumberValue(faceDownFruitsCountNumericUpDown);
        public int BathhousesPoints => GetNumberValue(bathhousesPointsNumericUpDown);
        public int WishingWellsPoints => GetNumberValue(wishingWellsPointsNumericUpDown);
        public Dictionary<BuildingType, bool> CompletedProjects =>
            new Dictionary<BuildingType, bool>()
            {
                [BuildingType.Pavilion] = GetCheckBoxValue(completedProjectPavilionCheckBox),
                [BuildingType.Seraglio] = GetCheckBoxValue(completedProjectSeraglioCheckBox),
                [BuildingType.Arcades] = GetCheckBoxValue(completedProjectArcadesCheckBox),
                [BuildingType.Chambers] = GetCheckBoxValue(completedProjectChambersCheckBox),
                [BuildingType.Garden] = GetCheckBoxValue(completedProjectGardenCheckBox),
                [BuildingType.Tower] = GetCheckBoxValue(completedProjectTowerCheckBox),
            };
        public int AnimalsPoints => GetNumberValue(animalsPointsNumericUpDown);
        public Dictionary<BuildingType, bool> OwnedSemiBuildings =>
            new Dictionary<BuildingType, bool>()
            {
                [BuildingType.Pavilion] = GetCheckBoxValue(ownedSemiBuildingPavilionCheckBox),
                [BuildingType.Seraglio] = GetCheckBoxValue(ownedSemiBuildingSeraglioCheckBox),
                [BuildingType.Arcades] = GetCheckBoxValue(ownedSemiBuildingArcadesCheckBox),
                [BuildingType.Chambers] = GetCheckBoxValue(ownedSemiBuildingChambersCheckBox),
                [BuildingType.Garden] = GetCheckBoxValue(ownedSemiBuildingGardenCheckBox),
                [BuildingType.Tower] = GetCheckBoxValue(ownedSemiBuildingTowerCheckBox),
            };
        public int BlackDiceTotalPips => GetNumberValue(blackDiceTotalPipsNumericUpDown);
        public int HandymenTilesHighestNumber => GetNumberValue(handymenTilesHighestNumberNumericUpDown);
        public int TreasuresValue => GetNumberValue(treasuresValueNumericUpDown);
        public int Mission1Count => GetNumberValue(mission1RowsCountNumericUpDown);
        public int Mission2Count => GetNumberValue(mission2ColumnsCountNumericUpDown);
        public int Mission3Count => GetNumberValue(mission3Adjacent2BuildingsCountNumericUpDown);
        public bool Mission4Available => GetCheckBoxValue(mission4SecondLongestWallCheckBox);
        public int Mission5Count => GetNumberValue(mission5LongestDiagonalLineNumericUpDown);
        public int Mission6Count => GetNumberValue(mission6DoubleWallCountNumericUpDown);
        public int Mission7Count => GetNumberValue(mission7DiffernetTypesNumberNumericUpDown);
        public int Mission8Count => GetNumberValue(mission8PathBuildingsNumberNumericUpDown);
        public int Mission9Count => GetNumberValue(mission9Grids22CountNumericUpDown);
        public int SecondLongestWallLength => GetNumberValue(secondLongestWallNumericUpDown);

        public Dictionary<BuildingType, int> BuildingsCount =>
            new Dictionary<BuildingType, int>()
            {
                [BuildingType.Pavilion] = GetNumberValue(pavilionCountNumericUpDown),
                [BuildingType.Seraglio] = GetNumberValue(seraglioCountNumericUpDown),
                [BuildingType.Arcades] = GetNumberValue(arcadesCountNumericUpDown),
                [BuildingType.Chambers] = GetNumberValue(chambersCountNumericUpDown),
                [BuildingType.Garden] = GetNumberValue(gardenCountNumericUpDown),
                [BuildingType.Tower] = GetNumberValue(towerCountNumericUpDown),
            };
        public int AllBuildingsCount => BuildingsCount.Sum(b => b.Value);
    }
}