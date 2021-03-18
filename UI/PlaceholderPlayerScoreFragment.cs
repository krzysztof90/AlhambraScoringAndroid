using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        protected override int getContentLayout()
        {
            return Resource.Layout.fragment_game_score;
        }

        protected override void createControls()
        {
            wallsCountNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.wallsCountNumericUpDown);
            pavilionCountNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.pavilionCountNumericUpDown);
            seraglioCountNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.seraglioCountNumericUpDown);
            arcadesCountNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.arcadesCountNumericUpDown);
            chambersCountNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.chambersCountNumericUpDown);
            gardenCountNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.gardenCountNumericUpDown);
            towerCountNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.towerCountNumericUpDown);
            buildingsWithoutServantTileNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.buildingsWithoutServantTileNumericUpDown);
            completedGroupOfFruitBoard1CheckBox = root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedGroupOfFruitBoard1CheckBox);
            completedGroupOfFruitBoard2CheckBox = root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedGroupOfFruitBoard2CheckBox);
            completedGroupOfFruitBoard3CheckBox = root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedGroupOfFruitBoard3CheckBox);
            completedGroupOfFruitBoard4CheckBox = root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedGroupOfFruitBoard4CheckBox);
            completedGroupOfFruitBoard5CheckBox = root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedGroupOfFruitBoard5CheckBox);
            completedGroupOfFruitBoard6CheckBox = root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedGroupOfFruitBoard6CheckBox);
            faceDownFruitsCountNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.faceDownFruitsCountNumericUpDown);
            bathhousesPointsNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.bathhousesPointsNumericUpDown);
            wishingWellsPointsNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.wishingWellsPointsNumericUpDown);
            completedProjectPavilionCheckBox = root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedProjectPavilionCheckBox);
            completedProjectSeraglioCheckBox = root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedProjectSeraglioCheckBox);
            completedProjectArcadesCheckBox = root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedProjectArcadesCheckBox);
            completedProjectChambersCheckBox = root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedProjectChambersCheckBox);
            completedProjectGardenCheckBox = root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedProjectGardenCheckBox);
            completedProjectTowerCheckBox = root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.completedProjectTowerCheckBox);
            animalsPointsNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.animalsPointsNumericUpDown);
            ownedSemiBuildingPavilionCheckBox = root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.ownedSemiBuildingPavilionCheckBox);
            ownedSemiBuildingSeraglioCheckBox = root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.ownedSemiBuildingSeraglioCheckBox);
            ownedSemiBuildingArcadesCheckBox = root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.ownedSemiBuildingArcadesCheckBox);
            ownedSemiBuildingChambersCheckBox = root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.ownedSemiBuildingChambersCheckBox);
            ownedSemiBuildingGardenCheckBox = root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.ownedSemiBuildingGardenCheckBox);
            ownedSemiBuildingTowerCheckBox = root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.ownedSemiBuildingTowerCheckBox);
            blackDiceTotalPipsNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.blackDiceTotalPipsNumericUpDown);
            handymenTilesHighestNumberNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.handymenTilesHighestNumberNumericUpDown);
            treasuresValueNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.treasuresValueNumericUpDown);
            mission1RowsCountNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.mission1RowsCountNumericUpDown);
            mission2ColumnsCountNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.mission2ColumnsCountNumericUpDown);
            mission3Adjacent2BuildingsCountNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.mission3Adjacent2BuildingsCountNumericUpDown);
            mission4SecondLongestWallCheckBox = root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.mission4SecondLongestWallCheckBox);
            mission5LongestDiagonalLineNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.mission5LongestDiagonalLineNumericUpDown);
            mission6DoubleWallCountNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.mission6DoubleWallCountNumericUpDown);
            mission7DiffernetTypesNumberNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.mission7DiffernetTypesNumberNumericUpDown);
            mission8PathBuildingsNumberNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.mission8PathBuildingsNumberNumericUpDown);
            mission9Grids22CountNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.mission9Grids22CountNumericUpDown);
            secondLongestWallNumericUpDown = root.FindViewById<ScoreLineNumberView>(Resource.Id.secondLongestWallNumericUpDown);
        }

        protected override void addControls()
        {
            controls.Add(wallsCountNumericUpDown);
            controls.Add(pavilionCountNumericUpDown);
            controls.Add(seraglioCountNumericUpDown);
            controls.Add(arcadesCountNumericUpDown);
            controls.Add(chambersCountNumericUpDown);
            controls.Add(gardenCountNumericUpDown);
            controls.Add(towerCountNumericUpDown);
            controls.Add(buildingsWithoutServantTileNumericUpDown);
            controls.Add(completedGroupOfFruitBoard1CheckBox);
            controls.Add(completedGroupOfFruitBoard2CheckBox);
            controls.Add(completedGroupOfFruitBoard3CheckBox);
            controls.Add(completedGroupOfFruitBoard4CheckBox);
            controls.Add(completedGroupOfFruitBoard5CheckBox);
            controls.Add(completedGroupOfFruitBoard6CheckBox);
            controls.Add(faceDownFruitsCountNumericUpDown);
            controls.Add(bathhousesPointsNumericUpDown);
            controls.Add(wishingWellsPointsNumericUpDown);
            controls.Add(completedProjectPavilionCheckBox);
            controls.Add(completedProjectSeraglioCheckBox);
            controls.Add(completedProjectArcadesCheckBox);
            controls.Add(completedProjectChambersCheckBox);
            controls.Add(completedProjectGardenCheckBox);
            controls.Add(completedProjectTowerCheckBox);
            controls.Add(animalsPointsNumericUpDown);
            controls.Add(ownedSemiBuildingPavilionCheckBox);
            controls.Add(ownedSemiBuildingSeraglioCheckBox);
            controls.Add(ownedSemiBuildingArcadesCheckBox);
            controls.Add(ownedSemiBuildingChambersCheckBox);
            controls.Add(ownedSemiBuildingGardenCheckBox);
            controls.Add(ownedSemiBuildingTowerCheckBox);
            controls.Add(blackDiceTotalPipsNumericUpDown);
            controls.Add(handymenTilesHighestNumberNumericUpDown);
            controls.Add(treasuresValueNumericUpDown);
            controls.Add(mission1RowsCountNumericUpDown);
            controls.Add(mission2ColumnsCountNumericUpDown);
            controls.Add(mission3Adjacent2BuildingsCountNumericUpDown);
            controls.Add(mission4SecondLongestWallCheckBox);
            controls.Add(mission5LongestDiagonalLineNumericUpDown);
            controls.Add(mission6DoubleWallCountNumericUpDown);
            controls.Add(mission7DiffernetTypesNumberNumericUpDown);
            controls.Add(mission8PathBuildingsNumberNumericUpDown);
            controls.Add(mission9Grids22CountNumericUpDown);
            controls.Add(secondLongestWallNumericUpDown);
        }

        protected override void setControlsProperties()
        {
            blackDiceTotalPipsNumericUpDown.changeMethod = () => { visibleSecondLongestWall(); };
            mission4SecondLongestWallCheckBox.changeMethod = () => { visibleSecondLongestWall(); };

            //TODO maximum for mission3Adjacent2BuildingsCountNumericUpDown, mission6DoubleWallCountNumericUpDown, mission9Grids22CountNumericUpDown
            wallsCountNumericUpDown.setNumberRange(0, Game.GameAllTilesCount * 2 + 2);
            pavilionCountNumericUpDown.setNumberRange(0, Game.getBuildingsMaxCount()[BuildingType.Pavilion]);
            seraglioCountNumericUpDown.setNumberRange(0, Game.getBuildingsMaxCount()[BuildingType.Seraglio]);
            arcadesCountNumericUpDown.setNumberRange(0, Game.getBuildingsMaxCount()[BuildingType.Arcades]);
            chambersCountNumericUpDown.setNumberRange(0, Game.getBuildingsMaxCount()[BuildingType.Chambers]);
            gardenCountNumericUpDown.setNumberRange(0, Game.getBuildingsMaxCount()[BuildingType.Garden]);
            towerCountNumericUpDown.setNumberRange(0, Game.getBuildingsMaxCount()[BuildingType.Tower]);
            buildingsWithoutServantTileNumericUpDown.setNumberRange(0, Game.GameAllBuildingsCount);
            faceDownFruitsCountNumericUpDown.setNumberRange(0, 35);
            bathhousesPointsNumericUpDown.setNumberRange(0, (Game.GameAllTilesCount - 1) * 4 * 6);
            wishingWellsPointsNumericUpDown.setNumberRange(0, 24);
            animalsPointsNumericUpDown.setNumberRange(0, 24);
            blackDiceTotalPipsNumericUpDown.setNumberRange(0, 18);
            handymenTilesHighestNumberNumericUpDown.setNumberRange(0, 48);
            treasuresValueNumericUpDown.setNumberRange(0, 15);
            mission1RowsCountNumericUpDown.setNumberRange(0, Game.GameAllTilesCount / 3);
            mission2ColumnsCountNumericUpDown.setNumberRange(0, Game.GameAllTilesCount / 3);
            mission5LongestDiagonalLineNumericUpDown.setNumberRange(0, (Game.GameAllTilesCount + 1) / 2);
            mission7DiffernetTypesNumberNumericUpDown.setNumberRange(0, 6);
            mission8PathBuildingsNumberNumericUpDown.setNumberRange(0, (Game.GameAllTilesCount + 1) / 2);
            secondLongestWallNumericUpDown.setNumberRange(0, (Game.GameAllTilesCount * 2 + 2) / 2 - 2);

            visibleSecondLongestWall();

            AddConditionToVisible(buildingsWithoutServantTileNumericUpDown, Game.hasModule(ExpansionModule.DesignerPalaceStaff));
            AddConditionToVisible(completedGroupOfFruitBoard1CheckBox, Game.hasModule(ExpansionModule.DesignerOrchards));
            AddConditionToVisible(completedGroupOfFruitBoard2CheckBox, Game.hasModule(ExpansionModule.DesignerOrchards));
            AddConditionToVisible(completedGroupOfFruitBoard3CheckBox, Game.hasModule(ExpansionModule.DesignerOrchards));
            AddConditionToVisible(completedGroupOfFruitBoard4CheckBox, Game.hasModule(ExpansionModule.DesignerOrchards));
            AddConditionToVisible(completedGroupOfFruitBoard5CheckBox, Game.hasModule(ExpansionModule.DesignerOrchards));
            AddConditionToVisible(completedGroupOfFruitBoard6CheckBox, Game.hasModule(ExpansionModule.DesignerOrchards));
            AddConditionToVisible(faceDownFruitsCountNumericUpDown, Game.hasModule(ExpansionModule.DesignerOrchards));
            AddConditionToVisible(bathhousesPointsNumericUpDown, Game.hasModule(ExpansionModule.DesignerBathhouses));
            AddConditionToVisible(wishingWellsPointsNumericUpDown, Game.hasModule(ExpansionModule.DesignerWishingWell));
            AddConditionToVisible(completedProjectPavilionCheckBox, Game.hasModule(ExpansionModule.DesignerFreshColors));
            AddConditionToVisible(completedProjectSeraglioCheckBox, Game.hasModule(ExpansionModule.DesignerFreshColors));
            AddConditionToVisible(completedProjectArcadesCheckBox, Game.hasModule(ExpansionModule.DesignerFreshColors));
            AddConditionToVisible(completedProjectChambersCheckBox, Game.hasModule(ExpansionModule.DesignerFreshColors));
            AddConditionToVisible(completedProjectGardenCheckBox, Game.hasModule(ExpansionModule.DesignerFreshColors));
            AddConditionToVisible(completedProjectTowerCheckBox, Game.hasModule(ExpansionModule.DesignerFreshColors));
            AddConditionToVisible(animalsPointsNumericUpDown, Game.hasModule(ExpansionModule.DesignerAlhambraZoo));
            AddConditionToVisible(ownedSemiBuildingPavilionCheckBox, Game.hasModule(ExpansionModule.DesignerGatesWithoutEnd));
            AddConditionToVisible(ownedSemiBuildingSeraglioCheckBox, Game.hasModule(ExpansionModule.DesignerGatesWithoutEnd));
            AddConditionToVisible(ownedSemiBuildingArcadesCheckBox, Game.hasModule(ExpansionModule.DesignerGatesWithoutEnd));
            AddConditionToVisible(ownedSemiBuildingChambersCheckBox, Game.hasModule(ExpansionModule.DesignerGatesWithoutEnd));
            AddConditionToVisible(ownedSemiBuildingGardenCheckBox, Game.hasModule(ExpansionModule.DesignerGatesWithoutEnd));
            AddConditionToVisible(ownedSemiBuildingTowerCheckBox, Game.hasModule(ExpansionModule.DesignerGatesWithoutEnd));
            AddConditionToVisible(blackDiceTotalPipsNumericUpDown, Game.hasModule(ExpansionModule.DesignerBuildingsOfPower));
            AddConditionToVisible(handymenTilesHighestNumberNumericUpDown, Game.hasModule(ExpansionModule.DesignerHandymen));
            AddConditionToVisible(treasuresValueNumericUpDown, Game.hasModule(ExpansionModule.DesignerTreasures));
            AddConditionToVisible(mission1RowsCountNumericUpDown, Game.hasModule(ExpansionModule.DesignerCaliphsGuidelines));
            AddConditionToVisible(mission2ColumnsCountNumericUpDown, Game.hasModule(ExpansionModule.DesignerCaliphsGuidelines));
            AddConditionToVisible(mission3Adjacent2BuildingsCountNumericUpDown, Game.hasModule(ExpansionModule.DesignerCaliphsGuidelines));
            AddConditionToVisible(mission4SecondLongestWallCheckBox, Game.hasModule(ExpansionModule.DesignerCaliphsGuidelines));
            AddConditionToVisible(mission5LongestDiagonalLineNumericUpDown, Game.hasModule(ExpansionModule.DesignerCaliphsGuidelines));
            AddConditionToVisible(mission6DoubleWallCountNumericUpDown, Game.hasModule(ExpansionModule.DesignerCaliphsGuidelines));
            AddConditionToVisible(mission7DiffernetTypesNumberNumericUpDown, Game.hasModule(ExpansionModule.DesignerCaliphsGuidelines));
            AddConditionToVisible(mission8PathBuildingsNumberNumericUpDown, Game.hasModule(ExpansionModule.DesignerCaliphsGuidelines));
            AddConditionToVisible(mission9Grids22CountNumericUpDown, Game.hasModule(ExpansionModule.DesignerCaliphsGuidelines));
            //        AddConditionToVisible(secondLongestWallNumericUpDown, Game.hasModule(ExpansionModule.DesignerBuildingsOfPower) || Game.hasModule(ExpansionModule.DesignerCaliphsGuidelines));

            AddConditionToVisible(buildingsWithoutServantTileNumericUpDown, !isFinalRound);
            AddConditionToVisible(completedGroupOfFruitBoard1CheckBox, isFinalRound);
            AddConditionToVisible(completedGroupOfFruitBoard2CheckBox, isFinalRound);
            AddConditionToVisible(completedGroupOfFruitBoard3CheckBox, isFinalRound);
            AddConditionToVisible(completedGroupOfFruitBoard4CheckBox, isFinalRound);
            AddConditionToVisible(completedGroupOfFruitBoard5CheckBox, isFinalRound);
            AddConditionToVisible(completedGroupOfFruitBoard6CheckBox, isFinalRound);
            AddConditionToVisible(faceDownFruitsCountNumericUpDown, isFinalRound);
            AddConditionToVisible(treasuresValueNumericUpDown, isFinalRound);
            AddConditionToVisible(mission1RowsCountNumericUpDown, isFinalRound);
            AddConditionToVisible(mission2ColumnsCountNumericUpDown, isFinalRound);
            AddConditionToVisible(mission3Adjacent2BuildingsCountNumericUpDown, isFinalRound);
            AddConditionToVisible(mission4SecondLongestWallCheckBox, isFinalRound);
            AddConditionToVisible(mission5LongestDiagonalLineNumericUpDown, isFinalRound);
            AddConditionToVisible(mission6DoubleWallCountNumericUpDown, isFinalRound);
            AddConditionToVisible(mission7DiffernetTypesNumberNumericUpDown, isFinalRound);
            AddConditionToVisible(mission8PathBuildingsNumberNumericUpDown, isFinalRound);
            AddConditionToVisible(mission9Grids22CountNumericUpDown, isFinalRound);

            AddConditionToVisible(wallsCountNumericUpDown, !isDirk);
            AddConditionToVisible(buildingsWithoutServantTileNumericUpDown, !isDirk);
            AddConditionToVisible(completedGroupOfFruitBoard1CheckBox, !isDirk);
            AddConditionToVisible(completedGroupOfFruitBoard2CheckBox, !isDirk);
            AddConditionToVisible(completedGroupOfFruitBoard3CheckBox, !isDirk);
            AddConditionToVisible(completedGroupOfFruitBoard4CheckBox, !isDirk);
            AddConditionToVisible(completedGroupOfFruitBoard5CheckBox, !isDirk);
            AddConditionToVisible(completedGroupOfFruitBoard6CheckBox, !isDirk);
            AddConditionToVisible(faceDownFruitsCountNumericUpDown, !isDirk);
            AddConditionToVisible(bathhousesPointsNumericUpDown, !isDirk);
            AddConditionToVisible(wishingWellsPointsNumericUpDown, !isDirk);
            AddConditionToVisible(completedProjectPavilionCheckBox, !isDirk);
            AddConditionToVisible(completedProjectSeraglioCheckBox, !isDirk);
            AddConditionToVisible(completedProjectArcadesCheckBox, !isDirk);
            AddConditionToVisible(completedProjectChambersCheckBox, !isDirk);
            AddConditionToVisible(completedProjectGardenCheckBox, !isDirk);
            AddConditionToVisible(completedProjectTowerCheckBox, !isDirk);
            AddConditionToVisible(animalsPointsNumericUpDown, !isDirk);
            AddConditionToVisible(ownedSemiBuildingPavilionCheckBox, !isDirk);
            AddConditionToVisible(ownedSemiBuildingSeraglioCheckBox, !isDirk);
            AddConditionToVisible(ownedSemiBuildingArcadesCheckBox, !isDirk);
            AddConditionToVisible(ownedSemiBuildingChambersCheckBox, !isDirk);
            AddConditionToVisible(ownedSemiBuildingGardenCheckBox, !isDirk);
            AddConditionToVisible(ownedSemiBuildingTowerCheckBox, !isDirk);
            AddConditionToVisible(blackDiceTotalPipsNumericUpDown, !isDirk);
            AddConditionToVisible(handymenTilesHighestNumberNumericUpDown, !isDirk);
            AddConditionToVisible(treasuresValueNumericUpDown, !isDirk);
            AddConditionToVisible(mission1RowsCountNumericUpDown, !isDirk);
            AddConditionToVisible(mission2ColumnsCountNumericUpDown, !isDirk);
            AddConditionToVisible(mission3Adjacent2BuildingsCountNumericUpDown, !isDirk);
            AddConditionToVisible(mission4SecondLongestWallCheckBox, !isDirk);
            AddConditionToVisible(mission5LongestDiagonalLineNumericUpDown, !isDirk);
            AddConditionToVisible(mission6DoubleWallCountNumericUpDown, !isDirk);
            AddConditionToVisible(mission7DiffernetTypesNumberNumericUpDown, !isDirk);
            AddConditionToVisible(mission8PathBuildingsNumberNumericUpDown, !isDirk);
            AddConditionToVisible(mission9Grids22CountNumericUpDown, !isDirk);
            //        AddConditionToVisible(secondLongestWallNumericUpDown, !isDirk);
        }

        public PlaceholderPlayerScoreFragment(int _index, Game game) : base(_index, game)
        {
        }

        private void visibleSecondLongestWall()
        {
            secondLongestWallNumericUpDown.Visibility = BlackDiceTotalPips != 0 || Mission4Available ? ViewStates.Visible : ViewStates.Gone;
        }

        public int WallLength => getNumberValue(wallsCountNumericUpDown);
        public int BuildingsWithoutServantTile => getNumberValue(buildingsWithoutServantTileNumericUpDown);
        public bool CompletedGroupOfFruitBoard1 => getCheckBoxValue(completedGroupOfFruitBoard1CheckBox);
        public bool CompletedGroupOfFruitBoard2 => getCheckBoxValue(completedGroupOfFruitBoard2CheckBox);
        public bool CompletedGroupOfFruitBoard3 => getCheckBoxValue(completedGroupOfFruitBoard3CheckBox);
        public bool CompletedGroupOfFruitBoard4 => getCheckBoxValue(completedGroupOfFruitBoard4CheckBox);
        public bool CompletedGroupOfFruitBoard5 => getCheckBoxValue(completedGroupOfFruitBoard5CheckBox);
        public bool CompletedGroupOfFruitBoard6 => getCheckBoxValue(completedGroupOfFruitBoard6CheckBox);
        public int FaceDownFruitsCount => getNumberValue(faceDownFruitsCountNumericUpDown);
        public int BathhousesPoints => getNumberValue(bathhousesPointsNumericUpDown);
        public int WishingWellsPoints => getNumberValue(wishingWellsPointsNumericUpDown);
        public Dictionary<BuildingType, bool> CompletedProjects =>
            new Dictionary<BuildingType, bool>()
            {
                [BuildingType.Pavilion] = getCheckBoxValue(completedProjectPavilionCheckBox),
                [BuildingType.Seraglio] = getCheckBoxValue(completedProjectSeraglioCheckBox),
                [BuildingType.Arcades] = getCheckBoxValue(completedProjectArcadesCheckBox),
                [BuildingType.Chambers] = getCheckBoxValue(completedProjectChambersCheckBox),
                [BuildingType.Garden] = getCheckBoxValue(completedProjectGardenCheckBox),
                [BuildingType.Tower] = getCheckBoxValue(completedProjectTowerCheckBox),
            };
        public int AnimalsPoints => getNumberValue(animalsPointsNumericUpDown);
        public Dictionary<BuildingType, bool> OwnedSemiBuildings =>
            new Dictionary<BuildingType, bool>()
            {
                [BuildingType.Pavilion] = getCheckBoxValue(ownedSemiBuildingPavilionCheckBox),
                [BuildingType.Seraglio] = getCheckBoxValue(ownedSemiBuildingSeraglioCheckBox),
                [BuildingType.Arcades] = getCheckBoxValue(ownedSemiBuildingArcadesCheckBox),
                [BuildingType.Chambers] = getCheckBoxValue(ownedSemiBuildingChambersCheckBox),
                [BuildingType.Garden] = getCheckBoxValue(ownedSemiBuildingGardenCheckBox),
                [BuildingType.Tower] = getCheckBoxValue(ownedSemiBuildingTowerCheckBox),
            };
        public int BlackDiceTotalPips => getNumberValue(blackDiceTotalPipsNumericUpDown);
        public int HandymenTilesHighestNumber => getNumberValue(handymenTilesHighestNumberNumericUpDown);
        public int TreasuresValue => getNumberValue(treasuresValueNumericUpDown);
        public int Mission1Count => getNumberValue(mission1RowsCountNumericUpDown);
        public int Mission2Count => getNumberValue(mission2ColumnsCountNumericUpDown);
        public int Mission3Count => getNumberValue(mission3Adjacent2BuildingsCountNumericUpDown);
        public bool Mission4Available => getCheckBoxValue(mission4SecondLongestWallCheckBox);
        public int Mission5Count => getNumberValue(mission5LongestDiagonalLineNumericUpDown);
        public int Mission6Count => getNumberValue(mission6DoubleWallCountNumericUpDown);
        public int Mission7Count => getNumberValue(mission7DiffernetTypesNumberNumericUpDown);
        public int Mission8Count => getNumberValue(mission8PathBuildingsNumberNumericUpDown);
        public int Mission9Count => getNumberValue(mission9Grids22CountNumericUpDown);
        public int SecondLongestWallLength => getNumberValue(secondLongestWallNumericUpDown);

        public Dictionary<BuildingType, int> BuildingsCount =>
            new Dictionary<BuildingType, int>()
            {
                [BuildingType.Pavilion] = getNumberValue(pavilionCountNumericUpDown),
                [BuildingType.Seraglio] = getNumberValue(seraglioCountNumericUpDown),
                [BuildingType.Arcades] = getNumberValue(arcadesCountNumericUpDown),
                [BuildingType.Chambers] = getNumberValue(chambersCountNumericUpDown),
                [BuildingType.Garden] = getNumberValue(gardenCountNumericUpDown),
                [BuildingType.Tower] = getNumberValue(towerCountNumericUpDown),
            };
        public int AllBuildingsCount => BuildingsCount.Sum(b => b.Value);
    }
}