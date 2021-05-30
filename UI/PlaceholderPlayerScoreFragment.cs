using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.Options;
using AndroidBase.UI;
using System;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.UI
{
    public class PlaceholderPlayerScoreFragment : PlaceholderPlayerScoreFragmentBase
    {
        ControlNumberView wallsCountNumericUpDown;
        ControlNumberView pavilionCountNumericUpDown;
        ControlNumberView seraglioCountNumericUpDown;
        ControlNumberView arcadesCountNumericUpDown;
        ControlNumberView chambersCountNumericUpDown;
        ControlNumberView gardenCountNumericUpDown;
        ControlNumberView towerCountNumericUpDown;
        ControlNumberView bonusCardsPavilionCountNumericUpDown;
        ControlNumberView bonusCardsSeraglioCountNumericUpDown;
        ControlNumberView bonusCardsArcadesCountNumericUpDown;
        ControlNumberView bonusCardsChambersCountNumericUpDown;
        ControlNumberView bonusCardsGardenCountNumericUpDown;
        ControlNumberView bonusCardsTowerCountNumericUpDown;
        ControlNumberView squaresPavilionCountNumericUpDown;
        ControlNumberView squaresSeraglioCountNumericUpDown;
        ControlNumberView squaresArcadesCountNumericUpDown;
        ControlNumberView squaresChambersCountNumericUpDown;
        ControlNumberView squaresGardenCountNumericUpDown;
        ControlNumberView squaresTowerCountNumericUpDown;
        ControlCheckBoxView ownedCharacterTheWiseManCheckBox;
        ControlCheckBoxView ownedCharacterTheCityWatchCheckBox;
        ControlNumberView campsPointsNumericUpDown;
        ControlNumberView streetTradersPavilionCountNumericUpDown;
        ControlNumberView streetTradersSeraglioCountNumericUpDown;
        ControlNumberView streetTradersArcadesCountNumericUpDown;
        ControlNumberView streetTradersChambersCountNumericUpDown;
        ControlNumberView streetTradersGardenCountNumericUpDown;
        ControlNumberView streetTradersTowerCountNumericUpDown;
        ControlNumberView treasuresCountNumericUpDown;
        ControlNumberView unprotectedSidesCountNumericUpDown;
        ControlNumberView unprotectedSidesNeighbouringCountNumericUpDown;
        ControlNumberView bazaarsPointsNumericUpDown;
        ControlNumberView artOfTheMoorsPointsNumericUpDown;
        ControlNumberView falconsBlackNumberNumericUpDown;
        ControlNumberView falconsBrownNumberNumericUpDown;
        ControlNumberView falconsWhiteNumberNumericUpDown;
        ControlNumberView watchtowersNumberNumericUpDown;
        ControlNumberView medinasNumberNumericUpDown;
        ControlNumberView buildingsWithoutServantTileNumericUpDown;
        ControlCheckBoxView completedGroupOfFruitBoard1CheckBox;
        ControlCheckBoxView completedGroupOfFruitBoard2CheckBox;
        ControlCheckBoxView completedGroupOfFruitBoard3CheckBox;
        ControlCheckBoxView completedGroupOfFruitBoard4CheckBox;
        ControlCheckBoxView completedGroupOfFruitBoard5CheckBox;
        ControlCheckBoxView completedGroupOfFruitBoard6CheckBox;
        ControlNumberView faceDownFruitsCountNumericUpDown;
        ControlNumberView bathhousesPointsNumericUpDown;
        ControlNumberView wishingWellsPointsNumericUpDown;
        ControlCheckBoxView completedProjectPavilionCheckBox;
        ControlCheckBoxView completedProjectSeraglioCheckBox;
        ControlCheckBoxView completedProjectArcadesCheckBox;
        ControlCheckBoxView completedProjectChambersCheckBox;
        ControlCheckBoxView completedProjectGardenCheckBox;
        ControlCheckBoxView completedProjectTowerCheckBox;
        ControlNumberView animalsPointsNumericUpDown;
        ControlCheckBoxView ownedSemiBuildingPavilionCheckBox;
        ControlCheckBoxView ownedSemiBuildingSeraglioCheckBox;
        ControlCheckBoxView ownedSemiBuildingArcadesCheckBox;
        ControlCheckBoxView ownedSemiBuildingChambersCheckBox;
        ControlCheckBoxView ownedSemiBuildingGardenCheckBox;
        ControlCheckBoxView ownedSemiBuildingTowerCheckBox;
        ControlNumberView blackDiceTotalPipsNumericUpDown;
        ControlNumberView extensionsPavilionCountNumericUpDown;
        ControlNumberView extensionsSeraglioCountNumericUpDown;
        ControlNumberView extensionsArcadesCountNumericUpDown;
        ControlNumberView extensionsChambersCountNumericUpDown;
        ControlNumberView extensionsGardenCountNumericUpDown;
        ControlNumberView extensionsTowerCountNumericUpDown;
        ControlNumberView handymenTilesHighestNumberNumericUpDown;
        ControlNumberView treasuresValueNumericUpDown;
        ControlNumberView mission1RowsCountNumericUpDown;
        ControlNumberView mission2ColumnsCountNumericUpDown;
        ControlNumberView mission3Adjacent2BuildingsCountNumericUpDown;
        ControlNumberView mission5LongestDiagonalLineNumericUpDown;
        ControlNumberView mission6DoubleWallCountNumericUpDown;
        ControlNumberView mission8PathBuildingsNumberNumericUpDown;
        ControlNumberView mission9Grids22CountNumericUpDown;
        ControlNumberView secondLongestWallNumericUpDown;
        ControlNumberView moatLengthNumericUpDown;
        ControlNumberView arenaNumericUpDown;
        ControlNumberView bathHouseNumericUpDown;
        ControlNumberView libraryNumericUpDown;
        ControlNumberView hostelNumericUpDown;
        ControlNumberView hospitalNumericUpDown;
        ControlNumberView marketNumericUpDown;
        ControlNumberView parkNumericUpDown;
        ControlNumberView schoolNumericUpDown;
        ControlNumberView residentialAreaNumericUpDown;
        ControlNumberView wallMoatCombinationNumericUpDown;

        protected override int GetContentLayout()
        {
            return Resource.Layout.fragment_game_score;
        }

        protected override void CreateControls()
        {
            wallsCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.wallsCountNumericUpDown);
            pavilionCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.pavilionCountNumericUpDown);
            seraglioCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.seraglioCountNumericUpDown);
            arcadesCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.arcadesCountNumericUpDown);
            chambersCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.chambersCountNumericUpDown);
            gardenCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.gardenCountNumericUpDown);
            towerCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.towerCountNumericUpDown);
            bonusCardsPavilionCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.bonusCardsPavilionCountNumericUpDown);
            bonusCardsSeraglioCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.bonusCardsSeraglioCountNumericUpDown);
            bonusCardsArcadesCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.bonusCardsArcadesCountNumericUpDown);
            bonusCardsChambersCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.bonusCardsChambersCountNumericUpDown);
            bonusCardsGardenCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.bonusCardsGardenCountNumericUpDown);
            bonusCardsTowerCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.bonusCardsTowerCountNumericUpDown);
            squaresPavilionCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.squaresPavilionCountNumericUpDown);
            squaresSeraglioCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.squaresSeraglioCountNumericUpDown);
            squaresArcadesCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.squaresArcadesCountNumericUpDown);
            squaresChambersCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.squaresChambersCountNumericUpDown);
            squaresGardenCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.squaresGardenCountNumericUpDown);
            squaresTowerCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.squaresTowerCountNumericUpDown);
            ownedCharacterTheWiseManCheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.ownedCharacterTheWiseManCheckBox);
            ownedCharacterTheCityWatchCheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.ownedCharacterTheCityWatchCheckBox);
            campsPointsNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.campsPointsNumericUpDown);
            streetTradersPavilionCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.streetTradersPavilionCountNumericUpDown);
            streetTradersSeraglioCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.streetTradersSeraglioCountNumericUpDown);
            streetTradersArcadesCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.streetTradersArcadesCountNumericUpDown);
            streetTradersChambersCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.streetTradersChambersCountNumericUpDown);
            streetTradersGardenCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.streetTradersGardenCountNumericUpDown);
            streetTradersTowerCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.streetTradersTowerCountNumericUpDown);
            treasuresCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.treasuresCountNumericUpDown);
            unprotectedSidesCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.unprotectedSidesCountNumericUpDown);
            unprotectedSidesNeighbouringCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.unprotectedSidesNeighbouringCountNumericUpDown);
            bazaarsPointsNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.bazaarsPointsNumericUpDown);
            artOfTheMoorsPointsNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.artOfTheMoorsPointsNumericUpDown);
            falconsBlackNumberNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.falconsBlackNumberNumericUpDown);
            falconsBrownNumberNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.falconsBrownNumberNumericUpDown);
            falconsWhiteNumberNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.falconsWhiteNumberNumericUpDown);
            watchtowersNumberNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.watchtowersNumberNumericUpDown);
            medinasNumberNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.medinasNumberNumericUpDown);
            buildingsWithoutServantTileNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.buildingsWithoutServantTileNumericUpDown);
            completedGroupOfFruitBoard1CheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.completedGroupOfFruitBoard1CheckBox);
            completedGroupOfFruitBoard2CheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.completedGroupOfFruitBoard2CheckBox);
            completedGroupOfFruitBoard3CheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.completedGroupOfFruitBoard3CheckBox);
            completedGroupOfFruitBoard4CheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.completedGroupOfFruitBoard4CheckBox);
            completedGroupOfFruitBoard5CheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.completedGroupOfFruitBoard5CheckBox);
            completedGroupOfFruitBoard6CheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.completedGroupOfFruitBoard6CheckBox);
            faceDownFruitsCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.faceDownFruitsCountNumericUpDown);
            bathhousesPointsNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.bathhousesPointsNumericUpDown);
            wishingWellsPointsNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.wishingWellsPointsNumericUpDown);
            completedProjectPavilionCheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.completedProjectPavilionCheckBox);
            completedProjectSeraglioCheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.completedProjectSeraglioCheckBox);
            completedProjectArcadesCheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.completedProjectArcadesCheckBox);
            completedProjectChambersCheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.completedProjectChambersCheckBox);
            completedProjectGardenCheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.completedProjectGardenCheckBox);
            completedProjectTowerCheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.completedProjectTowerCheckBox);
            animalsPointsNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.animalsPointsNumericUpDown);
            ownedSemiBuildingPavilionCheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.ownedSemiBuildingPavilionCheckBox);
            ownedSemiBuildingSeraglioCheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.ownedSemiBuildingSeraglioCheckBox);
            ownedSemiBuildingArcadesCheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.ownedSemiBuildingArcadesCheckBox);
            ownedSemiBuildingChambersCheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.ownedSemiBuildingChambersCheckBox);
            ownedSemiBuildingGardenCheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.ownedSemiBuildingGardenCheckBox);
            ownedSemiBuildingTowerCheckBox = Root.FindViewById<ControlCheckBoxView>(Resource.Id.ownedSemiBuildingTowerCheckBox);
            blackDiceTotalPipsNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.blackDiceTotalPipsNumericUpDown);
            extensionsPavilionCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.extensionsPavilionCountNumericUpDown);
            extensionsSeraglioCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.extensionsSeraglioCountNumericUpDown);
            extensionsArcadesCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.extensionsArcadesCountNumericUpDown);
            extensionsChambersCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.extensionsChambersCountNumericUpDown);
            extensionsGardenCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.extensionsGardenCountNumericUpDown);
            extensionsTowerCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.extensionsTowerCountNumericUpDown);
            handymenTilesHighestNumberNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.handymenTilesHighestNumberNumericUpDown);
            treasuresValueNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.treasuresValueNumericUpDown);
            mission1RowsCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.mission1RowsCountNumericUpDown);
            mission2ColumnsCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.mission2ColumnsCountNumericUpDown);
            mission3Adjacent2BuildingsCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.mission3Adjacent2BuildingsCountNumericUpDown);
            mission5LongestDiagonalLineNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.mission5LongestDiagonalLineNumericUpDown);
            mission6DoubleWallCountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.mission6DoubleWallCountNumericUpDown);
            mission8PathBuildingsNumberNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.mission8PathBuildingsNumberNumericUpDown);
            mission9Grids22CountNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.mission9Grids22CountNumericUpDown);
            secondLongestWallNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.secondLongestWallNumericUpDown);
            moatLengthNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.moatLengthNumericUpDown);
            arenaNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.arenaNumericUpDown);
            bathHouseNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.bathHouseNumericUpDown);
            libraryNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.libraryNumericUpDown);
            hostelNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.hostelNumericUpDown);
            hospitalNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.hospitalNumericUpDown);
            marketNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.marketNumericUpDown);
            parkNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.parkNumericUpDown);
            schoolNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.schoolNumericUpDown);
            residentialAreaNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.residentialAreaNumericUpDown);
            wallMoatCombinationNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.wallMoatCombinationNumericUpDown);
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
            blackDiceTotalPipsNumericUpDown.OnValueChange = () => { EnabledSecondLongestWall(); };

            //TODO maximum for campsPointsNumericUpDown, mission3Adjacent2BuildingsCountNumericUpDown, mission6DoubleWallCountNumericUpDown, mission9Grids22CountNumericUpDown
            wallsCountNumericUpDown.SetNumberRange<SettingsType>(0, Game.WallsMaxLength, SettingsType.ValidateWallLength);
            pavilionCountNumericUpDown.SetNumberRange<SettingsType>(0, Game.BuildingsMaxCount[BuildingType.Pavilion], SettingsType.ValidateBuildingsNumber);
            seraglioCountNumericUpDown.SetNumberRange<SettingsType>(0, Game.BuildingsMaxCount[BuildingType.Seraglio], SettingsType.ValidateBuildingsNumber);
            arcadesCountNumericUpDown.SetNumberRange<SettingsType>(0, Game.BuildingsMaxCount[BuildingType.Arcades], SettingsType.ValidateBuildingsNumber);
            chambersCountNumericUpDown.SetNumberRange<SettingsType>(0, Game.BuildingsMaxCount[BuildingType.Chambers], SettingsType.ValidateBuildingsNumber);
            gardenCountNumericUpDown.SetNumberRange<SettingsType>(0, Game.BuildingsMaxCount[BuildingType.Garden], SettingsType.ValidateBuildingsNumber);
            towerCountNumericUpDown.SetNumberRange<SettingsType>(0, Game.BuildingsMaxCount[BuildingType.Tower], SettingsType.ValidateBuildingsNumber);
            bonusCardsPavilionCountNumericUpDown.SetNumberRange<SettingsType>(0, Game.BonusCardsMaxCount[BuildingType.Pavilion], SettingsType.ValidateBonusCards);
            bonusCardsSeraglioCountNumericUpDown.SetNumberRange<SettingsType>(0, Game.BonusCardsMaxCount[BuildingType.Seraglio], SettingsType.ValidateBonusCards);
            bonusCardsArcadesCountNumericUpDown.SetNumberRange<SettingsType>(0, Game.BonusCardsMaxCount[BuildingType.Arcades], SettingsType.ValidateBonusCards);
            bonusCardsChambersCountNumericUpDown.SetNumberRange<SettingsType>(0, Game.BonusCardsMaxCount[BuildingType.Chambers], SettingsType.ValidateBonusCards);
            bonusCardsGardenCountNumericUpDown.SetNumberRange<SettingsType>(0, Game.BonusCardsMaxCount[BuildingType.Garden], SettingsType.ValidateBonusCards);
            bonusCardsTowerCountNumericUpDown.SetNumberRange<SettingsType>(0, Game.BonusCardsMaxCount[BuildingType.Tower], SettingsType.ValidateBonusCards);
            squaresPavilionCountNumericUpDown.SetNumberRange<SettingsType>(0, 9, SettingsType.ValidateSquares);
            squaresSeraglioCountNumericUpDown.SetNumberRange<SettingsType>(0, 9, SettingsType.ValidateSquares);
            squaresArcadesCountNumericUpDown.SetNumberRange<SettingsType>(0, 9, SettingsType.ValidateSquares);
            squaresChambersCountNumericUpDown.SetNumberRange<SettingsType>(0, 9, SettingsType.ValidateSquares);
            squaresGardenCountNumericUpDown.SetNumberRange<SettingsType>(0, 9, SettingsType.ValidateSquares);
            squaresTowerCountNumericUpDown.SetNumberRange<SettingsType>(0, 9, SettingsType.ValidateSquares);
            streetTradersPavilionCountNumericUpDown.SetNumberRange<SettingsType>(0, 7, SettingsType.ValidateCitizens);
            streetTradersSeraglioCountNumericUpDown.SetNumberRange<SettingsType>(0, 7, SettingsType.ValidateCitizens);
            streetTradersArcadesCountNumericUpDown.SetNumberRange<SettingsType>(0, 7, SettingsType.ValidateCitizens);
            streetTradersChambersCountNumericUpDown.SetNumberRange<SettingsType>(0, 7, SettingsType.ValidateCitizens);
            streetTradersGardenCountNumericUpDown.SetNumberRange<SettingsType>(0, 7, SettingsType.ValidateCitizens);
            streetTradersTowerCountNumericUpDown.SetNumberRange<SettingsType>(0, 7, SettingsType.ValidateCitizens);
            treasuresCountNumericUpDown.SetNumberRange<SettingsType>(0, 42, SettingsType.ValidateTreasures);
            unprotectedSidesCountNumericUpDown.SetNumberRange<SettingsType>(0, Game.AllTilesCount, SettingsType.ValidateUnprotectedSides);
            unprotectedSidesNeighbouringCountNumericUpDown.SetNumberRange<SettingsType>(0, Game.AllTilesCount, SettingsType.ValidateUnprotectedSides);
            bazaarsPointsNumericUpDown.SetNumberRange<SettingsType>(0, 192, SettingsType.ValidateBazaarsPoints);
            artOfTheMoorsPointsNumericUpDown.SetNumberRange<SettingsType>(0, 147, SettingsType.ValidateCultureCounters);
            falconsBlackNumberNumericUpDown.SetNumberRange<SettingsType>(0, 5, SettingsType.ValidateFalcons);
            falconsBrownNumberNumericUpDown.SetNumberRange<SettingsType>(0, 5, SettingsType.ValidateFalcons);
            falconsWhiteNumberNumericUpDown.SetNumberRange<SettingsType>(0, 5, SettingsType.ValidateFalcons);
            watchtowersNumberNumericUpDown.SetNumberRange<SettingsType>(0, 18, SettingsType.ValidateWatchtower);
            medinasNumberNumericUpDown.SetNumberRange<SettingsType>(0, 9, SettingsType.ValidateMedin);
            buildingsWithoutServantTileNumericUpDown.SetNumberRange<SettingsType>(0, Game.AllBuildingsCount, SettingsType.ValidateServants);
            faceDownFruitsCountNumericUpDown.SetNumberRange<SettingsType>(0, 35, SettingsType.ValidateSingleFruits);
            bathhousesPointsNumericUpDown.SetNumberRange<SettingsType>(0, (Game.AllTilesCount - 1) * 4 * 6, SettingsType.ValidateBathhouses);
            wishingWellsPointsNumericUpDown.SetNumberRange<SettingsType>(0, 24, SettingsType.ValidateFontains);
            animalsPointsNumericUpDown.SetNumberRange<SettingsType>(0, 24, SettingsType.ValidateAnimals);
            blackDiceTotalPipsNumericUpDown.SetNumberRange<SettingsType>(0, 18, SettingsType.ValidateBlackDicePips);
            extensionsPavilionCountNumericUpDown.SetNumberRange<SettingsType>(0, 2, SettingsType.ValidateExtensions);
            extensionsSeraglioCountNumericUpDown.SetNumberRange<SettingsType>(0, 2, SettingsType.ValidateExtensions);
            extensionsArcadesCountNumericUpDown.SetNumberRange<SettingsType>(0, 2, SettingsType.ValidateExtensions);
            extensionsChambersCountNumericUpDown.SetNumberRange<SettingsType>(0, 2, SettingsType.ValidateExtensions);
            extensionsGardenCountNumericUpDown.SetNumberRange<SettingsType>(0, 2, SettingsType.ValidateExtensions);
            extensionsTowerCountNumericUpDown.SetNumberRange<SettingsType>(0, 2, SettingsType.ValidateExtensions);
            handymenTilesHighestNumberNumericUpDown.SetNumberRange<SettingsType>(0, 48, SettingsType.ValidateHandymen);
            treasuresValueNumericUpDown.SetNumberRange<SettingsType>(0, 15, SettingsType.ValidateTreasuresPoints);
            mission1RowsCountNumericUpDown.SetNumberRange<SettingsType>(0, Game.AllTilesCount / 3, SettingsType.ValidateMissions);
            mission2ColumnsCountNumericUpDown.SetNumberRange<SettingsType>(0, Game.AllTilesCount / 3, SettingsType.ValidateMissions);
            mission5LongestDiagonalLineNumericUpDown.SetNumberRange<SettingsType>(0, (Game.AllTilesCount + 1) / 2, SettingsType.ValidateMissions);
            mission8PathBuildingsNumberNumericUpDown.SetNumberRange<SettingsType>(0, (Game.AllTilesCount + 1) / 2, SettingsType.ValidateMissions);
            secondLongestWallNumericUpDown.SetNumberRange<SettingsType>(0, Game.WallsMaxLength / 2 - 2, SettingsType.ValidateSecondLongestWall);
            moatLengthNumericUpDown.SetNumberRange<SettingsType>(0, Game.MoatMaxLength, SettingsType.ValidateMoatLength);
            arenaNumericUpDown.SetNumberRange<SettingsType>(0, 6, SettingsType.ValidateBuildingsNumber);
            bathHouseNumericUpDown.SetNumberRange<SettingsType>(0, 6, SettingsType.ValidateBuildingsNumber);
            libraryNumericUpDown.SetNumberRange<SettingsType>(0, 6, SettingsType.ValidateBuildingsNumber);
            hostelNumericUpDown.SetNumberRange<SettingsType>(0, 6, SettingsType.ValidateBuildingsNumber);
            hospitalNumericUpDown.SetNumberRange<SettingsType>(0, 6, SettingsType.ValidateBuildingsNumber);
            marketNumericUpDown.SetNumberRange<SettingsType>(0, 6, SettingsType.ValidateBuildingsNumber);
            parkNumericUpDown.SetNumberRange<SettingsType>(0, 6, SettingsType.ValidateBuildingsNumber);
            schoolNumericUpDown.SetNumberRange<SettingsType>(0, 6, SettingsType.ValidateBuildingsNumber);
            residentialAreaNumericUpDown.SetNumberRange<SettingsType>(0, 6, SettingsType.ValidateBuildingsNumber);
            wallMoatCombinationNumericUpDown.SetNumberRange<SettingsType>(0, Math.Min(Game.WallsMaxLength, Game.MoatMaxLength), SettingsType.ValidateMoatwall);

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
            AddConditionToVisible(mission8PathBuildingsNumberNumericUpDown, IsFinalRound);
            AddConditionToVisible(mission9Grids22CountNumericUpDown, IsFinalRound);

            AddConditionToVisible(secondLongestWallNumericUpDown, Game.HasModule(ExpansionModule.DesignerBuildingsOfPower) || (Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission4) && IsFinalRound));

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
            AddConditionToVisible(mission8PathBuildingsNumberNumericUpDown, !IsDirk);
            AddConditionToVisible(mission9Grids22CountNumericUpDown, !IsDirk);
            AddConditionToVisible(moatLengthNumericUpDown, !IsDirk);
            AddConditionToVisible(wallMoatCombinationNumericUpDown, !IsDirk);
            AddConditionToVisible(secondLongestWallNumericUpDown, !IsDirk);

            EnabledSecondLongestWall();
        }

        protected override void ApplyCorrectingRoundScoring()
        {
            wallsCountNumericUpDown.Value = CorrectingRoundScoring.WallLength;
            pavilionCountNumericUpDown.Value = CorrectingRoundScoring.BuildingsCount[BuildingType.Pavilion];
            seraglioCountNumericUpDown.Value = CorrectingRoundScoring.BuildingsCount[BuildingType.Seraglio];
            arcadesCountNumericUpDown.Value = CorrectingRoundScoring.BuildingsCount[BuildingType.Arcades];
            chambersCountNumericUpDown.Value = CorrectingRoundScoring.BuildingsCount[BuildingType.Chambers];
            gardenCountNumericUpDown.Value = CorrectingRoundScoring.BuildingsCount[BuildingType.Garden];
            towerCountNumericUpDown.Value = CorrectingRoundScoring.BuildingsCount[BuildingType.Tower];
            bonusCardsPavilionCountNumericUpDown.Value = CorrectingRoundScoring.BonusCardsBuildingsCount[BuildingType.Pavilion];
            bonusCardsSeraglioCountNumericUpDown.Value = CorrectingRoundScoring.BonusCardsBuildingsCount[BuildingType.Seraglio];
            bonusCardsArcadesCountNumericUpDown.Value = CorrectingRoundScoring.BonusCardsBuildingsCount[BuildingType.Arcades];
            bonusCardsChambersCountNumericUpDown.Value = CorrectingRoundScoring.BonusCardsBuildingsCount[BuildingType.Chambers];
            bonusCardsGardenCountNumericUpDown.Value = CorrectingRoundScoring.BonusCardsBuildingsCount[BuildingType.Garden];
            bonusCardsTowerCountNumericUpDown.Value = CorrectingRoundScoring.BonusCardsBuildingsCount[BuildingType.Tower];
            squaresPavilionCountNumericUpDown.Value = CorrectingRoundScoring.SquaresBuildingsCount[BuildingType.Pavilion];
            squaresSeraglioCountNumericUpDown.Value = CorrectingRoundScoring.SquaresBuildingsCount[BuildingType.Seraglio];
            squaresArcadesCountNumericUpDown.Value = CorrectingRoundScoring.SquaresBuildingsCount[BuildingType.Arcades];
            squaresChambersCountNumericUpDown.Value = CorrectingRoundScoring.SquaresBuildingsCount[BuildingType.Chambers];
            squaresGardenCountNumericUpDown.Value = CorrectingRoundScoring.SquaresBuildingsCount[BuildingType.Garden];
            squaresTowerCountNumericUpDown.Value = CorrectingRoundScoring.SquaresBuildingsCount[BuildingType.Tower];
            ownedCharacterTheWiseManCheckBox.Value = CorrectingRoundScoring.OwnedCharacterTheWiseMan;
            ownedCharacterTheCityWatchCheckBox.Value = CorrectingRoundScoring.OwnedCharacterTheCityWatch;
            campsPointsNumericUpDown.Value = CorrectingRoundScoring.CampsPoints;
            streetTradersPavilionCountNumericUpDown.Value = CorrectingRoundScoring.StreetTradersNumber[BuildingType.Pavilion];
            streetTradersSeraglioCountNumericUpDown.Value = CorrectingRoundScoring.StreetTradersNumber[BuildingType.Seraglio];
            streetTradersArcadesCountNumericUpDown.Value = CorrectingRoundScoring.StreetTradersNumber[BuildingType.Arcades];
            streetTradersChambersCountNumericUpDown.Value = CorrectingRoundScoring.StreetTradersNumber[BuildingType.Chambers];
            streetTradersGardenCountNumericUpDown.Value = CorrectingRoundScoring.StreetTradersNumber[BuildingType.Garden];
            streetTradersTowerCountNumericUpDown.Value = CorrectingRoundScoring.StreetTradersNumber[BuildingType.Tower];
            treasuresCountNumericUpDown.Value = CorrectingRoundScoring.TreasuresCount;
            unprotectedSidesCountNumericUpDown.Value = CorrectingRoundScoring.UnprotectedSidesCount;
            unprotectedSidesNeighbouringCountNumericUpDown.Value = CorrectingRoundScoring.UnprotectedSidesNeighbouringCount;
            bazaarsPointsNumericUpDown.Value = CorrectingRoundScoring.BazaarsTotalPoints;
            artOfTheMoorsPointsNumericUpDown.Value = CorrectingRoundScoring.ArtOfTheMoorsPoints;
            falconsBlackNumberNumericUpDown.Value = CorrectingRoundScoring.FalconsBlackNumber;
            falconsBrownNumberNumericUpDown.Value = CorrectingRoundScoring.FalconsBrownNumber;
            falconsWhiteNumberNumericUpDown.Value = CorrectingRoundScoring.FalconsWhiteNumber;
            watchtowersNumberNumericUpDown.Value = CorrectingRoundScoring.WatchtowersNumber;
            medinasNumberNumericUpDown.Value = CorrectingRoundScoring.MedinasNumber;
            buildingsWithoutServantTileNumericUpDown.Value = CorrectingRoundScoring.BuildingsWithoutServantTile;
            completedGroupOfFruitBoard1CheckBox.Value = CorrectingRoundScoring.CompletedGroupOfFruitBoard1;
            completedGroupOfFruitBoard2CheckBox.Value = CorrectingRoundScoring.CompletedGroupOfFruitBoard2;
            completedGroupOfFruitBoard3CheckBox.Value = CorrectingRoundScoring.CompletedGroupOfFruitBoard3;
            completedGroupOfFruitBoard4CheckBox.Value = CorrectingRoundScoring.CompletedGroupOfFruitBoard4;
            completedGroupOfFruitBoard5CheckBox.Value = CorrectingRoundScoring.CompletedGroupOfFruitBoard5;
            completedGroupOfFruitBoard6CheckBox.Value = CorrectingRoundScoring.CompletedGroupOfFruitBoard6;
            faceDownFruitsCountNumericUpDown.Value = CorrectingRoundScoring.FaceDownFruitsCount;
            bathhousesPointsNumericUpDown.Value = CorrectingRoundScoring.BathhousesPoints;
            wishingWellsPointsNumericUpDown.Value = CorrectingRoundScoring.WishingWellsPoints;
            completedProjectPavilionCheckBox.Value = CorrectingRoundScoring.CompletedProjects[BuildingType.Pavilion];
            completedProjectSeraglioCheckBox.Value = CorrectingRoundScoring.CompletedProjects[BuildingType.Seraglio];
            completedProjectArcadesCheckBox.Value = CorrectingRoundScoring.CompletedProjects[BuildingType.Arcades];
            completedProjectChambersCheckBox.Value = CorrectingRoundScoring.CompletedProjects[BuildingType.Chambers];
            completedProjectGardenCheckBox.Value = CorrectingRoundScoring.CompletedProjects[BuildingType.Garden];
            completedProjectTowerCheckBox.Value = CorrectingRoundScoring.CompletedProjects[BuildingType.Tower];
            animalsPointsNumericUpDown.Value = CorrectingRoundScoring.AnimalsPoints;
            ownedSemiBuildingPavilionCheckBox.Value = CorrectingRoundScoring.OwnedSemiBuildings[BuildingType.Pavilion];
            ownedSemiBuildingSeraglioCheckBox.Value = CorrectingRoundScoring.OwnedSemiBuildings[BuildingType.Seraglio];
            ownedSemiBuildingArcadesCheckBox.Value = CorrectingRoundScoring.OwnedSemiBuildings[BuildingType.Arcades];
            ownedSemiBuildingChambersCheckBox.Value = CorrectingRoundScoring.OwnedSemiBuildings[BuildingType.Chambers];
            ownedSemiBuildingGardenCheckBox.Value = CorrectingRoundScoring.OwnedSemiBuildings[BuildingType.Garden];
            ownedSemiBuildingTowerCheckBox.Value = CorrectingRoundScoring.OwnedSemiBuildings[BuildingType.Tower];
            blackDiceTotalPipsNumericUpDown.Value = CorrectingRoundScoring.BlackDiceTotalPips;
            extensionsPavilionCountNumericUpDown.Value = CorrectingRoundScoring.ExtensionsBuildingsCount[BuildingType.Pavilion];
            extensionsSeraglioCountNumericUpDown.Value = CorrectingRoundScoring.ExtensionsBuildingsCount[BuildingType.Seraglio];
            extensionsArcadesCountNumericUpDown.Value = CorrectingRoundScoring.ExtensionsBuildingsCount[BuildingType.Arcades];
            extensionsChambersCountNumericUpDown.Value = CorrectingRoundScoring.ExtensionsBuildingsCount[BuildingType.Chambers];
            extensionsGardenCountNumericUpDown.Value = CorrectingRoundScoring.ExtensionsBuildingsCount[BuildingType.Garden];
            extensionsTowerCountNumericUpDown.Value = CorrectingRoundScoring.ExtensionsBuildingsCount[BuildingType.Tower];
            handymenTilesHighestNumberNumericUpDown.Value = CorrectingRoundScoring.HandymenTilesHighestNumber;
            treasuresValueNumericUpDown.Value = CorrectingRoundScoring.TreasuresValue;
            mission1RowsCountNumericUpDown.Value = CorrectingRoundScoring.Mission1Count;
            mission2ColumnsCountNumericUpDown.Value = CorrectingRoundScoring.Mission2Count;
            mission3Adjacent2BuildingsCountNumericUpDown.Value = CorrectingRoundScoring.Mission3Count;
            mission5LongestDiagonalLineNumericUpDown.Value = CorrectingRoundScoring.Mission5Count;
            mission6DoubleWallCountNumericUpDown.Value = CorrectingRoundScoring.Mission6Count;
            mission8PathBuildingsNumberNumericUpDown.Value = CorrectingRoundScoring.Mission8Count;
            mission9Grids22CountNumericUpDown.Value = CorrectingRoundScoring.Mission9Count;
            secondLongestWallNumericUpDown.Value = CorrectingRoundScoring.SecondLongestWallLength;
            moatLengthNumericUpDown.Value = CorrectingRoundScoring.MoatLength;
            arenaNumericUpDown.Value = CorrectingRoundScoring.GranadaBuildingsCount[GranadaBuildingType.Arena];
            bathHouseNumericUpDown.Value = CorrectingRoundScoring.GranadaBuildingsCount[GranadaBuildingType.BathHouse];
            libraryNumericUpDown.Value = CorrectingRoundScoring.GranadaBuildingsCount[GranadaBuildingType.Library];
            hostelNumericUpDown.Value = CorrectingRoundScoring.GranadaBuildingsCount[GranadaBuildingType.Hostel];
            hospitalNumericUpDown.Value = CorrectingRoundScoring.GranadaBuildingsCount[GranadaBuildingType.Hospital];
            marketNumericUpDown.Value = CorrectingRoundScoring.GranadaBuildingsCount[GranadaBuildingType.Market];
            parkNumericUpDown.Value = CorrectingRoundScoring.GranadaBuildingsCount[GranadaBuildingType.Park];
            schoolNumericUpDown.Value = CorrectingRoundScoring.GranadaBuildingsCount[GranadaBuildingType.School];
            residentialAreaNumericUpDown.Value = CorrectingRoundScoring.GranadaBuildingsCount[GranadaBuildingType.ResidentialArea];
            wallMoatCombinationNumericUpDown.Value = CorrectingRoundScoring.WallMoatCombinationLength;
        }

        protected override void ApplyPreviousRoundScoring()
        {
            ownedCharacterTheWiseManCheckBox.Value = PreviousRoundScoring.OwnedCharacterTheWiseMan;
            ownedCharacterTheCityWatchCheckBox.Value = PreviousRoundScoring.OwnedCharacterTheCityWatch;
            completedProjectPavilionCheckBox.Value = PreviousRoundScoring.CompletedProjects[BuildingType.Pavilion];
            completedProjectSeraglioCheckBox.Value = PreviousRoundScoring.CompletedProjects[BuildingType.Seraglio];
            completedProjectArcadesCheckBox.Value = PreviousRoundScoring.CompletedProjects[BuildingType.Arcades];
            completedProjectChambersCheckBox.Value = PreviousRoundScoring.CompletedProjects[BuildingType.Chambers];
            completedProjectGardenCheckBox.Value = PreviousRoundScoring.CompletedProjects[BuildingType.Garden];
            completedProjectTowerCheckBox.Value = PreviousRoundScoring.CompletedProjects[BuildingType.Tower];
            ownedSemiBuildingPavilionCheckBox.Value = PreviousRoundScoring.OwnedSemiBuildings[BuildingType.Pavilion];
            ownedSemiBuildingSeraglioCheckBox.Value = PreviousRoundScoring.OwnedSemiBuildings[BuildingType.Seraglio];
            ownedSemiBuildingArcadesCheckBox.Value = PreviousRoundScoring.OwnedSemiBuildings[BuildingType.Arcades];
            ownedSemiBuildingChambersCheckBox.Value = PreviousRoundScoring.OwnedSemiBuildings[BuildingType.Chambers];
            ownedSemiBuildingGardenCheckBox.Value = PreviousRoundScoring.OwnedSemiBuildings[BuildingType.Garden];
            ownedSemiBuildingTowerCheckBox.Value = PreviousRoundScoring.OwnedSemiBuildings[BuildingType.Tower];
        }

        public PlaceholderPlayerScoreFragment(int _index, Game game, List<PlayerScoreData> correctingRoundScoring, PlayersScoreSectionsPagerAdapter adapter) : base(_index, game, correctingRoundScoring, adapter)
        {
        }

        private void EnabledSecondLongestWall()
        {
            secondLongestWallNumericUpDown.Enabled = BlackDiceTotalPips != 0 || (Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission4) && IsFinalRound);
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
    }
}