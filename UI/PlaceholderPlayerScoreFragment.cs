using AlhambraScoringAndroid.GamePlay;
using Android.Views;
using System;
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
        ScoreLineNumberView bonusCardsPavilionCountNumericUpDown;
        ScoreLineNumberView bonusCardsSeraglioCountNumericUpDown;
        ScoreLineNumberView bonusCardsArcadesCountNumericUpDown;
        ScoreLineNumberView bonusCardsChambersCountNumericUpDown;
        ScoreLineNumberView bonusCardsGardenCountNumericUpDown;
        ScoreLineNumberView bonusCardsTowerCountNumericUpDown;
        ScoreLineNumberView squaresPavilionCountNumericUpDown;
        ScoreLineNumberView squaresSeraglioCountNumericUpDown;
        ScoreLineNumberView squaresArcadesCountNumericUpDown;
        ScoreLineNumberView squaresChambersCountNumericUpDown;
        ScoreLineNumberView squaresGardenCountNumericUpDown;
        ScoreLineNumberView squaresTowerCountNumericUpDown;
        ScoreLineCheckBoxView ownedCharacterTheWiseManCheckBox;
        ScoreLineCheckBoxView ownedCharacterTheCityWatchCheckBox;
        ScoreLineNumberView campsPointsNumericUpDown;
        ScoreLineNumberView streetTradersPavilionCountNumericUpDown;
        ScoreLineNumberView streetTradersSeraglioCountNumericUpDown;
        ScoreLineNumberView streetTradersArcadesCountNumericUpDown;
        ScoreLineNumberView streetTradersChambersCountNumericUpDown;
        ScoreLineNumberView streetTradersGardenCountNumericUpDown;
        ScoreLineNumberView streetTradersTowerCountNumericUpDown;
        ScoreLineNumberView treasuresCountNumericUpDown;
        ScoreLineNumberView unprotectedSidesCountNumericUpDown;
        ScoreLineNumberView unprotectedSidesNeighbouringCountNumericUpDown;
        ScoreLineNumberView bazaarsPointsNumericUpDown;
        ScoreLineNumberView artOfTheMoorsPointsNumericUpDown;
        ScoreLineNumberView falconsBlackNumberNumericUpDown;
        ScoreLineNumberView falconsBrownNumberNumericUpDown;
        ScoreLineNumberView falconsWhiteNumberNumericUpDown;
        ScoreLineNumberView watchtowersNumberNumericUpDown;
        ScoreLineNumberView medinasNumberNumericUpDown;
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
        ScoreLineNumberView extensionsPavilionCountNumericUpDown;
        ScoreLineNumberView extensionsSeraglioCountNumericUpDown;
        ScoreLineNumberView extensionsArcadesCountNumericUpDown;
        ScoreLineNumberView extensionsChambersCountNumericUpDown;
        ScoreLineNumberView extensionsGardenCountNumericUpDown;
        ScoreLineNumberView extensionsTowerCountNumericUpDown;
        ScoreLineNumberView handymenTilesHighestNumberNumericUpDown;
        ScoreLineNumberView treasuresValueNumericUpDown;
        ScoreLineNumberView mission1RowsCountNumericUpDown;
        ScoreLineNumberView mission2ColumnsCountNumericUpDown;
        ScoreLineNumberView mission3Adjacent2BuildingsCountNumericUpDown;
        ScoreLineNumberView mission5LongestDiagonalLineNumericUpDown;
        ScoreLineNumberView mission6DoubleWallCountNumericUpDown;
        ScoreLineNumberView mission7DifferentTypesNumberNumericUpDown;
        ScoreLineNumberView mission8PathBuildingsNumberNumericUpDown;
        ScoreLineNumberView mission9Grids22CountNumericUpDown;
        ScoreLineNumberView secondLongestWallNumericUpDown;
        ScoreLineNumberView moatLengthNumericUpDown;
        ScoreLineNumberView arenaNumericUpDown;
        ScoreLineNumberView bathHouseNumericUpDown;
        ScoreLineNumberView libraryNumericUpDown;
        ScoreLineNumberView hostelNumericUpDown;
        ScoreLineNumberView hospitalNumericUpDown;
        ScoreLineNumberView marketNumericUpDown;
        ScoreLineNumberView parkNumericUpDown;
        ScoreLineNumberView schoolNumericUpDown;
        ScoreLineNumberView residentialAreaNumericUpDown;
        ScoreLineNumberView wallMoatCombinationNumericUpDown;

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
            bonusCardsPavilionCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.bonusCardsPavilionCountNumericUpDown);
            bonusCardsSeraglioCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.bonusCardsSeraglioCountNumericUpDown);
            bonusCardsArcadesCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.bonusCardsArcadesCountNumericUpDown);
            bonusCardsChambersCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.bonusCardsChambersCountNumericUpDown);
            bonusCardsGardenCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.bonusCardsGardenCountNumericUpDown);
            bonusCardsTowerCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.bonusCardsTowerCountNumericUpDown);
            squaresPavilionCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.squaresPavilionCountNumericUpDown);
            squaresSeraglioCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.squaresSeraglioCountNumericUpDown);
            squaresArcadesCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.squaresArcadesCountNumericUpDown);
            squaresChambersCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.squaresChambersCountNumericUpDown);
            squaresGardenCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.squaresGardenCountNumericUpDown);
            squaresTowerCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.squaresTowerCountNumericUpDown);
            ownedCharacterTheWiseManCheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.ownedCharacterTheWiseManCheckBox);
            ownedCharacterTheCityWatchCheckBox = Root.FindViewById<ScoreLineCheckBoxView>(Resource.Id.ownedCharacterTheCityWatchCheckBox);
            campsPointsNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.campsPointsNumericUpDown);
            streetTradersPavilionCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.streetTradersPavilionCountNumericUpDown);
            streetTradersSeraglioCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.streetTradersSeraglioCountNumericUpDown);
            streetTradersArcadesCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.streetTradersArcadesCountNumericUpDown);
            streetTradersChambersCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.streetTradersChambersCountNumericUpDown);
            streetTradersGardenCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.streetTradersGardenCountNumericUpDown);
            streetTradersTowerCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.streetTradersTowerCountNumericUpDown);
            treasuresCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.treasuresCountNumericUpDown);
            unprotectedSidesCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.unprotectedSidesCountNumericUpDown);
            unprotectedSidesNeighbouringCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.unprotectedSidesNeighbouringCountNumericUpDown);
            bazaarsPointsNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.bazaarsPointsNumericUpDown);
            artOfTheMoorsPointsNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.artOfTheMoorsPointsNumericUpDown);
            falconsBlackNumberNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.falconsBlackNumberNumericUpDown);
            falconsBrownNumberNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.falconsBrownNumberNumericUpDown);
            falconsWhiteNumberNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.falconsWhiteNumberNumericUpDown);
            watchtowersNumberNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.watchtowersNumberNumericUpDown);
            medinasNumberNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.medinasNumberNumericUpDown);
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
            extensionsPavilionCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.extensionsPavilionCountNumericUpDown);
            extensionsSeraglioCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.extensionsSeraglioCountNumericUpDown);
            extensionsArcadesCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.extensionsArcadesCountNumericUpDown);
            extensionsChambersCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.extensionsChambersCountNumericUpDown);
            extensionsGardenCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.extensionsGardenCountNumericUpDown);
            extensionsTowerCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.extensionsTowerCountNumericUpDown);
            handymenTilesHighestNumberNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.handymenTilesHighestNumberNumericUpDown);
            treasuresValueNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.treasuresValueNumericUpDown);
            mission1RowsCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.mission1RowsCountNumericUpDown);
            mission2ColumnsCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.mission2ColumnsCountNumericUpDown);
            mission3Adjacent2BuildingsCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.mission3Adjacent2BuildingsCountNumericUpDown);
            mission5LongestDiagonalLineNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.mission5LongestDiagonalLineNumericUpDown);
            mission6DoubleWallCountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.mission6DoubleWallCountNumericUpDown);
            mission7DifferentTypesNumberNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.mission7DifferentTypesNumberNumericUpDown);
            mission8PathBuildingsNumberNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.mission8PathBuildingsNumberNumericUpDown);
            mission9Grids22CountNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.mission9Grids22CountNumericUpDown);
            secondLongestWallNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.secondLongestWallNumericUpDown);
            moatLengthNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.moatLengthNumericUpDown);
            arenaNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.arenaNumericUpDown);
            bathHouseNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.bathHouseNumericUpDown);
            libraryNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.libraryNumericUpDown);
            hostelNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.hostelNumericUpDown);
            hospitalNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.hospitalNumericUpDown);
            marketNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.marketNumericUpDown);
            parkNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.parkNumericUpDown);
            schoolNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.schoolNumericUpDown);
            residentialAreaNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.residentialAreaNumericUpDown);
            wallMoatCombinationNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.wallMoatCombinationNumericUpDown);
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
            Controls.Add(bonusCardsPavilionCountNumericUpDown);
            Controls.Add(bonusCardsSeraglioCountNumericUpDown);
            Controls.Add(bonusCardsArcadesCountNumericUpDown);
            Controls.Add(bonusCardsChambersCountNumericUpDown);
            Controls.Add(bonusCardsGardenCountNumericUpDown);
            Controls.Add(bonusCardsTowerCountNumericUpDown);
            Controls.Add(squaresPavilionCountNumericUpDown);
            Controls.Add(squaresSeraglioCountNumericUpDown);
            Controls.Add(squaresArcadesCountNumericUpDown);
            Controls.Add(squaresChambersCountNumericUpDown);
            Controls.Add(squaresGardenCountNumericUpDown);
            Controls.Add(squaresTowerCountNumericUpDown);
            Controls.Add(ownedCharacterTheWiseManCheckBox);
            Controls.Add(ownedCharacterTheCityWatchCheckBox);
            Controls.Add(campsPointsNumericUpDown);
            Controls.Add(streetTradersPavilionCountNumericUpDown);
            Controls.Add(streetTradersSeraglioCountNumericUpDown);
            Controls.Add(streetTradersArcadesCountNumericUpDown);
            Controls.Add(streetTradersChambersCountNumericUpDown);
            Controls.Add(streetTradersGardenCountNumericUpDown);
            Controls.Add(streetTradersTowerCountNumericUpDown);
            Controls.Add(treasuresCountNumericUpDown);
            Controls.Add(unprotectedSidesCountNumericUpDown);
            Controls.Add(unprotectedSidesNeighbouringCountNumericUpDown);
            Controls.Add(bazaarsPointsNumericUpDown);
            Controls.Add(artOfTheMoorsPointsNumericUpDown);
            Controls.Add(falconsBlackNumberNumericUpDown);
            Controls.Add(falconsBrownNumberNumericUpDown);
            Controls.Add(falconsWhiteNumberNumericUpDown);
            Controls.Add(watchtowersNumberNumericUpDown);
            Controls.Add(medinasNumberNumericUpDown);
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
            Controls.Add(extensionsPavilionCountNumericUpDown);
            Controls.Add(extensionsSeraglioCountNumericUpDown);
            Controls.Add(extensionsArcadesCountNumericUpDown);
            Controls.Add(extensionsChambersCountNumericUpDown);
            Controls.Add(extensionsGardenCountNumericUpDown);
            Controls.Add(extensionsTowerCountNumericUpDown);
            Controls.Add(handymenTilesHighestNumberNumericUpDown);
            Controls.Add(treasuresValueNumericUpDown);
            Controls.Add(mission1RowsCountNumericUpDown);
            Controls.Add(mission2ColumnsCountNumericUpDown);
            Controls.Add(mission3Adjacent2BuildingsCountNumericUpDown);
            Controls.Add(mission5LongestDiagonalLineNumericUpDown);
            Controls.Add(mission6DoubleWallCountNumericUpDown);
            Controls.Add(mission7DifferentTypesNumberNumericUpDown);
            Controls.Add(mission8PathBuildingsNumberNumericUpDown);
            Controls.Add(mission9Grids22CountNumericUpDown);
            Controls.Add(secondLongestWallNumericUpDown);
            Controls.Add(moatLengthNumericUpDown);
            Controls.Add(arenaNumericUpDown);
            Controls.Add(bathHouseNumericUpDown);
            Controls.Add(libraryNumericUpDown);
            Controls.Add(hostelNumericUpDown);
            Controls.Add(hospitalNumericUpDown);
            Controls.Add(marketNumericUpDown);
            Controls.Add(parkNumericUpDown);
            Controls.Add(schoolNumericUpDown);
            Controls.Add(residentialAreaNumericUpDown);
            Controls.Add(wallMoatCombinationNumericUpDown);
        }

        protected override void SetControlsProperties()
        {
            blackDiceTotalPipsNumericUpDown.OnValueChange = () => { VisibleSecondLongestWall(); };

            //TODO maximum for campsPointsNumericUpDown, mission3Adjacent2BuildingsCountNumericUpDown, mission6DoubleWallCountNumericUpDown, mission9Grids22CountNumericUpDown
            wallsCountNumericUpDown.SetNumberRange(0, Game.WallsMaxLength);
            pavilionCountNumericUpDown.SetNumberRange(0, Game.BuildingsMaxCount[BuildingType.Pavilion]);
            seraglioCountNumericUpDown.SetNumberRange(0, Game.BuildingsMaxCount[BuildingType.Seraglio]);
            arcadesCountNumericUpDown.SetNumberRange(0, Game.BuildingsMaxCount[BuildingType.Arcades]);
            chambersCountNumericUpDown.SetNumberRange(0, Game.BuildingsMaxCount[BuildingType.Chambers]);
            gardenCountNumericUpDown.SetNumberRange(0, Game.BuildingsMaxCount[BuildingType.Garden]);
            towerCountNumericUpDown.SetNumberRange(0, Game.BuildingsMaxCount[BuildingType.Tower]);
            bonusCardsPavilionCountNumericUpDown.SetNumberRange(0, Game.BonusCardsMaxCount[BuildingType.Pavilion]);
            bonusCardsSeraglioCountNumericUpDown.SetNumberRange(0, Game.BonusCardsMaxCount[BuildingType.Seraglio]);
            bonusCardsArcadesCountNumericUpDown.SetNumberRange(0, Game.BonusCardsMaxCount[BuildingType.Arcades]);
            bonusCardsChambersCountNumericUpDown.SetNumberRange(0, Game.BonusCardsMaxCount[BuildingType.Chambers]);
            bonusCardsGardenCountNumericUpDown.SetNumberRange(0, Game.BonusCardsMaxCount[BuildingType.Garden]);
            bonusCardsTowerCountNumericUpDown.SetNumberRange(0, Game.BonusCardsMaxCount[BuildingType.Tower]);
            squaresPavilionCountNumericUpDown.SetNumberRange(0, 9);
            squaresSeraglioCountNumericUpDown.SetNumberRange(0, 9);
            squaresArcadesCountNumericUpDown.SetNumberRange(0, 9);
            squaresChambersCountNumericUpDown.SetNumberRange(0, 9);
            squaresGardenCountNumericUpDown.SetNumberRange(0, 9);
            squaresTowerCountNumericUpDown.SetNumberRange(0, 9);
            streetTradersPavilionCountNumericUpDown.SetNumberRange(0, 7);
            streetTradersSeraglioCountNumericUpDown.SetNumberRange(0, 7);
            streetTradersArcadesCountNumericUpDown.SetNumberRange(0, 7);
            streetTradersChambersCountNumericUpDown.SetNumberRange(0, 7);
            streetTradersGardenCountNumericUpDown.SetNumberRange(0, 7);
            streetTradersTowerCountNumericUpDown.SetNumberRange(0, 7);
            treasuresCountNumericUpDown.SetNumberRange(0, 42);
            unprotectedSidesCountNumericUpDown.SetNumberRange(0, Game.AllTilesCount);
            unprotectedSidesNeighbouringCountNumericUpDown.SetNumberRange(0, Game.AllTilesCount);
            bazaarsPointsNumericUpDown.SetNumberRange(0, 192);
            artOfTheMoorsPointsNumericUpDown.SetNumberRange(0, 147);
            falconsBlackNumberNumericUpDown.SetNumberRange(0, 5);
            falconsBrownNumberNumericUpDown.SetNumberRange(0, 5);
            falconsWhiteNumberNumericUpDown.SetNumberRange(0, 5);
            watchtowersNumberNumericUpDown.SetNumberRange(0, 18);
            medinasNumberNumericUpDown.SetNumberRange(0, 9);
            buildingsWithoutServantTileNumericUpDown.SetNumberRange(0, Game.AllBuildingsCount);
            faceDownFruitsCountNumericUpDown.SetNumberRange(0, 35);
            bathhousesPointsNumericUpDown.SetNumberRange(0, (Game.AllTilesCount - 1) * 4 * 6);
            wishingWellsPointsNumericUpDown.SetNumberRange(0, 24);
            animalsPointsNumericUpDown.SetNumberRange(0, 24);
            blackDiceTotalPipsNumericUpDown.SetNumberRange(0, 18);
            extensionsPavilionCountNumericUpDown.SetNumberRange(0, 2);
            extensionsSeraglioCountNumericUpDown.SetNumberRange(0, 2);
            extensionsArcadesCountNumericUpDown.SetNumberRange(0, 2);
            extensionsChambersCountNumericUpDown.SetNumberRange(0, 2);
            extensionsGardenCountNumericUpDown.SetNumberRange(0, 2);
            extensionsTowerCountNumericUpDown.SetNumberRange(0, 2);
            handymenTilesHighestNumberNumericUpDown.SetNumberRange(0, 48);
            treasuresValueNumericUpDown.SetNumberRange(0, 15);
            mission1RowsCountNumericUpDown.SetNumberRange(0, Game.AllTilesCount / 3);
            mission2ColumnsCountNumericUpDown.SetNumberRange(0, Game.AllTilesCount / 3);
            mission5LongestDiagonalLineNumericUpDown.SetNumberRange(0, (Game.AllTilesCount + 1) / 2);
            mission7DifferentTypesNumberNumericUpDown.SetNumberRange(0, 6);
            mission8PathBuildingsNumberNumericUpDown.SetNumberRange(0, (Game.AllTilesCount + 1) / 2);
            secondLongestWallNumericUpDown.SetNumberRange(0, Game.WallsMaxLength / 2 - 2);
            moatLengthNumericUpDown.SetNumberRange(0, Game.MoatMaxLength);
            arenaNumericUpDown.SetNumberRange(0, 6);
            bathHouseNumericUpDown.SetNumberRange(0, 6);
            libraryNumericUpDown.SetNumberRange(0, 6);
            hostelNumericUpDown.SetNumberRange(0, 6);
            hospitalNumericUpDown.SetNumberRange(0, 6);
            marketNumericUpDown.SetNumberRange(0, 6);
            parkNumericUpDown.SetNumberRange(0, 6);
            schoolNumericUpDown.SetNumberRange(0, 6);
            residentialAreaNumericUpDown.SetNumberRange(0, 6);
            wallMoatCombinationNumericUpDown.SetNumberRange(0, Math.Min(Game.WallsMaxLength, Game.MoatMaxLength));

            VisibleSecondLongestWall();

            AddConditionToVisible(wallsCountNumericUpDown, Game.GranadaOption != GranadaOption.Alone);
            AddConditionToVisible(pavilionCountNumericUpDown, Game.GranadaOption != GranadaOption.Alone);
            AddConditionToVisible(seraglioCountNumericUpDown, Game.GranadaOption != GranadaOption.Alone);
            AddConditionToVisible(arcadesCountNumericUpDown, Game.GranadaOption != GranadaOption.Alone);
            AddConditionToVisible(chambersCountNumericUpDown, Game.GranadaOption != GranadaOption.Alone);
            AddConditionToVisible(gardenCountNumericUpDown, Game.GranadaOption != GranadaOption.Alone);
            AddConditionToVisible(towerCountNumericUpDown, Game.GranadaOption != GranadaOption.Alone);
            AddConditionToVisible(bonusCardsPavilionCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionBonusCards));
            AddConditionToVisible(bonusCardsSeraglioCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionBonusCards));
            AddConditionToVisible(bonusCardsArcadesCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionBonusCards));
            AddConditionToVisible(bonusCardsChambersCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionBonusCards));
            AddConditionToVisible(bonusCardsGardenCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionBonusCards));
            AddConditionToVisible(bonusCardsTowerCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionBonusCards));
            AddConditionToVisible(squaresPavilionCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionSquares));
            AddConditionToVisible(squaresSeraglioCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionSquares));
            AddConditionToVisible(squaresArcadesCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionSquares));
            AddConditionToVisible(squaresChambersCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionSquares));
            AddConditionToVisible(squaresGardenCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionSquares));
            AddConditionToVisible(squaresTowerCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionSquares));
            AddConditionToVisible(ownedCharacterTheWiseManCheckBox, Game.HasModule(ExpansionModule.ExpansionCharacters));
            AddConditionToVisible(ownedCharacterTheCityWatchCheckBox, Game.HasModule(ExpansionModule.ExpansionCharacters));
            AddConditionToVisible(campsPointsNumericUpDown, Game.HasModule(ExpansionModule.ExpansionCamps));
            AddConditionToVisible(streetTradersPavilionCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionStreetTrader));
            AddConditionToVisible(streetTradersSeraglioCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionStreetTrader));
            AddConditionToVisible(streetTradersArcadesCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionStreetTrader));
            AddConditionToVisible(streetTradersChambersCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionStreetTrader));
            AddConditionToVisible(streetTradersGardenCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionStreetTrader));
            AddConditionToVisible(streetTradersTowerCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionStreetTrader));
            AddConditionToVisible(treasuresCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionTreasureChamber));
            AddConditionToVisible(unprotectedSidesCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionInvaders));
            AddConditionToVisible(unprotectedSidesNeighbouringCountNumericUpDown, Game.HasModule(ExpansionModule.ExpansionInvaders));
            AddConditionToVisible(bazaarsPointsNumericUpDown, Game.HasModule(ExpansionModule.ExpansionBazaars));
            AddConditionToVisible(artOfTheMoorsPointsNumericUpDown, Game.HasModule(ExpansionModule.ExpansionArtOfTheMoors));
            AddConditionToVisible(falconsBlackNumberNumericUpDown, Game.HasModule(ExpansionModule.ExpansionFalconers));
            AddConditionToVisible(falconsBrownNumberNumericUpDown, Game.HasModule(ExpansionModule.ExpansionFalconers));
            AddConditionToVisible(falconsWhiteNumberNumericUpDown, Game.HasModule(ExpansionModule.ExpansionFalconers));
            AddConditionToVisible(watchtowersNumberNumericUpDown, Game.HasModule(ExpansionModule.ExpansionWatchtowers));
            AddConditionToVisible(medinasNumberNumericUpDown, Game.HasModule(ExpansionModule.QueenieMedina));
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
            AddConditionToVisible(extensionsPavilionCountNumericUpDown, Game.HasModule(ExpansionModule.DesignerExtensions));
            AddConditionToVisible(extensionsSeraglioCountNumericUpDown, Game.HasModule(ExpansionModule.DesignerExtensions));
            AddConditionToVisible(extensionsArcadesCountNumericUpDown, Game.HasModule(ExpansionModule.DesignerExtensions));
            AddConditionToVisible(extensionsChambersCountNumericUpDown, Game.HasModule(ExpansionModule.DesignerExtensions));
            AddConditionToVisible(extensionsGardenCountNumericUpDown, Game.HasModule(ExpansionModule.DesignerExtensions));
            AddConditionToVisible(extensionsTowerCountNumericUpDown, Game.HasModule(ExpansionModule.DesignerExtensions));
            AddConditionToVisible(handymenTilesHighestNumberNumericUpDown, Game.HasModule(ExpansionModule.DesignerHandymen));
            AddConditionToVisible(treasuresValueNumericUpDown, Game.HasModule(ExpansionModule.FanTreasures));
            AddConditionToVisible(mission1RowsCountNumericUpDown, Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission1));
            AddConditionToVisible(mission2ColumnsCountNumericUpDown, Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission2));
            AddConditionToVisible(mission3Adjacent2BuildingsCountNumericUpDown, Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission3));
            AddConditionToVisible(mission5LongestDiagonalLineNumericUpDown, Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission5));
            AddConditionToVisible(mission6DoubleWallCountNumericUpDown, Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission6));
            AddConditionToVisible(mission7DifferentTypesNumberNumericUpDown, Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission7));
            AddConditionToVisible(mission8PathBuildingsNumberNumericUpDown, Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission8));
            AddConditionToVisible(mission9Grids22CountNumericUpDown, Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission9));
            AddConditionToVisible(moatLengthNumericUpDown, Game.HasModule(ExpansionModule.Granada));
            AddConditionToVisible(arenaNumericUpDown, Game.HasModule(ExpansionModule.Granada));
            AddConditionToVisible(bathHouseNumericUpDown, Game.HasModule(ExpansionModule.Granada));
            AddConditionToVisible(libraryNumericUpDown, Game.HasModule(ExpansionModule.Granada));
            AddConditionToVisible(hostelNumericUpDown, Game.HasModule(ExpansionModule.Granada));
            AddConditionToVisible(hospitalNumericUpDown, Game.HasModule(ExpansionModule.Granada));
            AddConditionToVisible(marketNumericUpDown, Game.HasModule(ExpansionModule.Granada));
            AddConditionToVisible(parkNumericUpDown, Game.HasModule(ExpansionModule.Granada));
            AddConditionToVisible(schoolNumericUpDown, Game.HasModule(ExpansionModule.Granada));
            AddConditionToVisible(residentialAreaNumericUpDown, Game.HasModule(ExpansionModule.Granada));
            AddConditionToVisible(wallMoatCombinationNumericUpDown, Game.GranadaOption == GranadaOption.With);

            AddConditionToVisible(unprotectedSidesNeighbouringCountNumericUpDown, IsFinalRound);
            AddConditionToVisible(bazaarsPointsNumericUpDown, IsFinalRound);
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
            AddConditionToVisible(mission5LongestDiagonalLineNumericUpDown, IsFinalRound);
            AddConditionToVisible(mission6DoubleWallCountNumericUpDown, IsFinalRound);
            AddConditionToVisible(mission7DifferentTypesNumberNumericUpDown, IsFinalRound);
            AddConditionToVisible(mission8PathBuildingsNumberNumericUpDown, IsFinalRound);
            AddConditionToVisible(mission9Grids22CountNumericUpDown, IsFinalRound);

            AddConditionToVisible(wallsCountNumericUpDown, !IsDirk);
            AddConditionToVisible(bonusCardsPavilionCountNumericUpDown, !IsDirk);
            AddConditionToVisible(bonusCardsSeraglioCountNumericUpDown, !IsDirk);
            AddConditionToVisible(bonusCardsArcadesCountNumericUpDown, !IsDirk);
            AddConditionToVisible(bonusCardsChambersCountNumericUpDown, !IsDirk);
            AddConditionToVisible(bonusCardsGardenCountNumericUpDown, !IsDirk);
            AddConditionToVisible(bonusCardsTowerCountNumericUpDown, !IsDirk);
            AddConditionToVisible(squaresPavilionCountNumericUpDown, !IsDirk);
            AddConditionToVisible(squaresSeraglioCountNumericUpDown, !IsDirk);
            AddConditionToVisible(squaresArcadesCountNumericUpDown, !IsDirk);
            AddConditionToVisible(squaresChambersCountNumericUpDown, !IsDirk);
            AddConditionToVisible(squaresGardenCountNumericUpDown, !IsDirk);
            AddConditionToVisible(squaresTowerCountNumericUpDown, !IsDirk);
            AddConditionToVisible(ownedCharacterTheWiseManCheckBox, !IsDirk);
            AddConditionToVisible(ownedCharacterTheCityWatchCheckBox, !IsDirk);
            AddConditionToVisible(campsPointsNumericUpDown, !IsDirk);
            AddConditionToVisible(streetTradersPavilionCountNumericUpDown, !IsDirk);
            AddConditionToVisible(streetTradersSeraglioCountNumericUpDown, !IsDirk);
            AddConditionToVisible(streetTradersArcadesCountNumericUpDown, !IsDirk);
            AddConditionToVisible(streetTradersChambersCountNumericUpDown, !IsDirk);
            AddConditionToVisible(streetTradersGardenCountNumericUpDown, !IsDirk);
            AddConditionToVisible(streetTradersTowerCountNumericUpDown, !IsDirk);
            AddConditionToVisible(unprotectedSidesCountNumericUpDown, !IsDirk);
            AddConditionToVisible(unprotectedSidesNeighbouringCountNumericUpDown, !IsDirk);
            AddConditionToVisible(bazaarsPointsNumericUpDown, !IsDirk);
            AddConditionToVisible(artOfTheMoorsPointsNumericUpDown, !IsDirk);
            AddConditionToVisible(falconsBlackNumberNumericUpDown, !IsDirk);
            AddConditionToVisible(falconsBrownNumberNumericUpDown, !IsDirk);
            AddConditionToVisible(falconsWhiteNumberNumericUpDown, !IsDirk);
            AddConditionToVisible(watchtowersNumberNumericUpDown, !IsDirk);
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
            AddConditionToVisible(mission5LongestDiagonalLineNumericUpDown, !IsDirk);
            AddConditionToVisible(mission6DoubleWallCountNumericUpDown, !IsDirk);
            AddConditionToVisible(mission7DifferentTypesNumberNumericUpDown, !IsDirk);
            AddConditionToVisible(mission8PathBuildingsNumberNumericUpDown, !IsDirk);
            AddConditionToVisible(mission9Grids22CountNumericUpDown, !IsDirk);
            AddConditionToVisible(moatLengthNumericUpDown, !IsDirk);
            AddConditionToVisible(wallMoatCombinationNumericUpDown, !IsDirk);
        }

        public PlaceholderPlayerScoreFragment(int _index, Game game, PlayersScoreSectionsPagerAdapter adapter) : base(_index, game, adapter)
        {
        }

        private void VisibleSecondLongestWall()
        {
            secondLongestWallNumericUpDown.Visibility = BlackDiceTotalPips != 0 || (Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission4)) ? ViewStates.Visible : ViewStates.Gone;
        }

        public int WallLength => wallsCountNumericUpDown.Value;
        public Dictionary<BuildingType, int> BuildingsCount =>
            new Dictionary<BuildingType, int>()
            {
                [BuildingType.Pavilion] = pavilionCountNumericUpDown.Value,
                [BuildingType.Seraglio] = seraglioCountNumericUpDown.Value,
                [BuildingType.Arcades] = arcadesCountNumericUpDown.Value,
                [BuildingType.Chambers] = chambersCountNumericUpDown.Value,
                [BuildingType.Garden] = gardenCountNumericUpDown.Value,
                [BuildingType.Tower] = towerCountNumericUpDown.Value,
            };
        public Dictionary<BuildingType, int> BonusCardsBuildingsCount =>
            new Dictionary<BuildingType, int>()
            {
                [BuildingType.Pavilion] = bonusCardsPavilionCountNumericUpDown.Value,
                [BuildingType.Seraglio] = bonusCardsSeraglioCountNumericUpDown.Value,
                [BuildingType.Arcades] = bonusCardsArcadesCountNumericUpDown.Value,
                [BuildingType.Chambers] = bonusCardsChambersCountNumericUpDown.Value,
                [BuildingType.Garden] = bonusCardsGardenCountNumericUpDown.Value,
                [BuildingType.Tower] = bonusCardsTowerCountNumericUpDown.Value,
            };
        public Dictionary<BuildingType, int> SquaresBuildingsCount =>
            new Dictionary<BuildingType, int>()
            {
                [BuildingType.Pavilion] = squaresPavilionCountNumericUpDown.Value,
                [BuildingType.Seraglio] = squaresSeraglioCountNumericUpDown.Value,
                [BuildingType.Arcades] = squaresArcadesCountNumericUpDown.Value,
                [BuildingType.Chambers] = squaresChambersCountNumericUpDown.Value,
                [BuildingType.Garden] = squaresGardenCountNumericUpDown.Value,
                [BuildingType.Tower] = squaresTowerCountNumericUpDown.Value,
            };
        public bool OwnedCharacterTheWiseMan => ownedCharacterTheWiseManCheckBox.Value;
        public bool OwnedCharacterTheCityWatch => ownedCharacterTheCityWatchCheckBox.Value;
        public int CampsPoints => campsPointsNumericUpDown.Value;
        public Dictionary<BuildingType, int> StreetTradersNumber =>
            new Dictionary<BuildingType, int>()
            {
                [BuildingType.Pavilion] = streetTradersPavilionCountNumericUpDown.Value,
                [BuildingType.Seraglio] = streetTradersSeraglioCountNumericUpDown.Value,
                [BuildingType.Arcades] = streetTradersArcadesCountNumericUpDown.Value,
                [BuildingType.Chambers] = streetTradersChambersCountNumericUpDown.Value,
                [BuildingType.Garden] = streetTradersGardenCountNumericUpDown.Value,
                [BuildingType.Tower] = streetTradersTowerCountNumericUpDown.Value,
            };
        public int TreasuresCount => treasuresCountNumericUpDown.Value;
        public int UnprotectedSidesCount => unprotectedSidesCountNumericUpDown.Value;
        public int UnprotectedSidesNeighbouringCount => unprotectedSidesNeighbouringCountNumericUpDown.Value;
        public int BazaarsTotalPoints => bazaarsPointsNumericUpDown.Value;
        public int ArtOfTheMoorsPoints => artOfTheMoorsPointsNumericUpDown.Value;
        public int FalconsBlackNumber => falconsBlackNumberNumericUpDown.Value;
        public int FalconsBrownNumber => falconsBrownNumberNumericUpDown.Value;
        public int FalconsWhiteNumber => falconsWhiteNumberNumericUpDown.Value;
        public int WatchtowersNumber => watchtowersNumberNumericUpDown.Value;
        public int MedinasNumber => medinasNumberNumericUpDown.Value;
        public int BuildingsWithoutServantTile => buildingsWithoutServantTileNumericUpDown.Value;
        public bool CompletedGroupOfFruitBoard1 => completedGroupOfFruitBoard1CheckBox.Value;
        public bool CompletedGroupOfFruitBoard2 => completedGroupOfFruitBoard2CheckBox.Value;
        public bool CompletedGroupOfFruitBoard3 => completedGroupOfFruitBoard3CheckBox.Value;
        public bool CompletedGroupOfFruitBoard4 => completedGroupOfFruitBoard4CheckBox.Value;
        public bool CompletedGroupOfFruitBoard5 => completedGroupOfFruitBoard5CheckBox.Value;
        public bool CompletedGroupOfFruitBoard6 => completedGroupOfFruitBoard6CheckBox.Value;
        public int FaceDownFruitsCount => faceDownFruitsCountNumericUpDown.Value;
        public int BathhousesPoints => bathhousesPointsNumericUpDown.Value;
        public int WishingWellsPoints => wishingWellsPointsNumericUpDown.Value;
        public Dictionary<BuildingType, bool> CompletedProjects =>
            new Dictionary<BuildingType, bool>()
            {
                [BuildingType.Pavilion] = completedProjectPavilionCheckBox.Value,
                [BuildingType.Seraglio] = completedProjectSeraglioCheckBox.Value,
                [BuildingType.Arcades] = completedProjectArcadesCheckBox.Value,
                [BuildingType.Chambers] = completedProjectChambersCheckBox.Value,
                [BuildingType.Garden] = completedProjectGardenCheckBox.Value,
                [BuildingType.Tower] = completedProjectTowerCheckBox.Value,
            };
        public int AnimalsPoints => animalsPointsNumericUpDown.Value;
        public Dictionary<BuildingType, bool> OwnedSemiBuildings =>
            new Dictionary<BuildingType, bool>()
            {
                [BuildingType.Pavilion] = ownedSemiBuildingPavilionCheckBox.Value,
                [BuildingType.Seraglio] = ownedSemiBuildingSeraglioCheckBox.Value,
                [BuildingType.Arcades] = ownedSemiBuildingArcadesCheckBox.Value,
                [BuildingType.Chambers] = ownedSemiBuildingChambersCheckBox.Value,
                [BuildingType.Garden] = ownedSemiBuildingGardenCheckBox.Value,
                [BuildingType.Tower] = ownedSemiBuildingTowerCheckBox.Value,
            };
        public int BlackDiceTotalPips => blackDiceTotalPipsNumericUpDown.Value;
        public Dictionary<BuildingType, int> ExtensionsBuildingsCount =>
            new Dictionary<BuildingType, int>()
            {
                [BuildingType.Pavilion] = extensionsPavilionCountNumericUpDown.Value,
                [BuildingType.Seraglio] = extensionsSeraglioCountNumericUpDown.Value,
                [BuildingType.Arcades] = extensionsArcadesCountNumericUpDown.Value,
                [BuildingType.Chambers] = extensionsChambersCountNumericUpDown.Value,
                [BuildingType.Garden] = extensionsGardenCountNumericUpDown.Value,
                [BuildingType.Tower] = extensionsTowerCountNumericUpDown.Value,
            };
        public int HandymenTilesHighestNumber => handymenTilesHighestNumberNumericUpDown.Value;
        public int TreasuresValue => treasuresValueNumericUpDown.Value;
        public int Mission1Count => mission1RowsCountNumericUpDown.Value;
        public int Mission2Count => mission2ColumnsCountNumericUpDown.Value;
        public int Mission3Count => mission3Adjacent2BuildingsCountNumericUpDown.Value;
        public int Mission5Count => mission5LongestDiagonalLineNumericUpDown.Value;
        public int Mission6Count => mission6DoubleWallCountNumericUpDown.Value;
        public int Mission7Count => mission7DifferentTypesNumberNumericUpDown.Value;
        public int Mission8Count => mission8PathBuildingsNumberNumericUpDown.Value;
        public int Mission9Count => mission9Grids22CountNumericUpDown.Value;
        public int SecondLongestWallLength => secondLongestWallNumericUpDown.Value;
        public int MoatLength => moatLengthNumericUpDown.Value;
        public Dictionary<GranadaBuildingType, int> GranadaBuildingsCount =>
            new Dictionary<GranadaBuildingType, int>()
            {
                [GranadaBuildingType.Arena] = arenaNumericUpDown.Value,
                [GranadaBuildingType.BathHouse] = bathHouseNumericUpDown.Value,
                [GranadaBuildingType.Library] = libraryNumericUpDown.Value,
                [GranadaBuildingType.Hostel] = hostelNumericUpDown.Value,
                [GranadaBuildingType.Hospital] = hospitalNumericUpDown.Value,
                [GranadaBuildingType.Market] = marketNumericUpDown.Value,
                [GranadaBuildingType.Park] = parkNumericUpDown.Value,
                [GranadaBuildingType.School] = schoolNumericUpDown.Value,
                [GranadaBuildingType.ResidentialArea] = residentialAreaNumericUpDown.Value
            };
        public int WallMoatCombinationLength => wallMoatCombinationNumericUpDown.Value;

        public int AllBuildingsCount => BuildingsCount.Sum(b => b.Value);
    }
}