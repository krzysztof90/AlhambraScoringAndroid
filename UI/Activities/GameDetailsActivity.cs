using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "Szczegóły", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class GameDetailsActivity : BaseActivity
    {
        public ResultHistory Result => Application.CurrentResult;
        public int PlayersCount => Result.Players.Count;
        public ScoringRound ScoreRound => Result.ScoreRound;
        public GranadaOption GranadaOption => Result.GranadaOption;

        protected override int ContentView => Resource.Layout.activity_game_details;

        private TableLayout contentTable;

        public GameDetailsActivity()
        {
        }

        private ResultPlayerHistory GetPlayer(int playerNumber)
        {
            return Result.Players[playerNumber - 1];
        }

        public bool HasModule(ExpansionModule module)
        {
            if (module == ExpansionModule.Granada)
                return GranadaOption != GranadaOption.Without;
            return Result.Modules.Contains(module) && (GranadaOption != GranadaOption.Alone
                || (module == ExpansionModule.ExpansionDiamonds
                || module == ExpansionModule.ExpansionCurrencyExchangeCards
                || module == ExpansionModule.ExpansionMasterBuilders
                || module == ExpansionModule.ExpansionCharacters
                || module == ExpansionModule.ExpansionThieves
                || module == ExpansionModule.ExpansionInvaders
                || module == ExpansionModule.ExpansionCaravanserai));
        }

        public bool HasCaliphsGuideline(CaliphsGuidelinesMission module)
        {
            return Result.CaliphsGuidelines.Contains(module);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //TODO przy ukrytych elementach nie zmniejszana szerokość TableRow

            base.OnCreate(savedInstanceState);

            TextView titleDate = FindViewById<TextView>(Resource.Id.titleDate);
            titleDate.Text = $"{Result.StartDateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("es-ES"))} - {(Result.EndDateTime != null ? ((DateTime)Result.EndDateTime).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("es-ES")) : String.Empty)}";

            contentTable = FindViewById<TableLayout>(Resource.Id.contentTable);

            //TODO wyrównanie górnego header do dołu, dolnego do góry

            List<List<TableRow>> tableRows = new List<List<TableRow>>()
            {
                new List<TableRow>()
                {
                    FindViewById<TableRow>(Resource.Id.headerPlayer11),
                    FindViewById<TableRow>(Resource.Id.headerPlayer21),
                    FindViewById<TableRow>(Resource.Id.headerPlayer31),
                    FindViewById<TableRow>(Resource.Id.headerPlayerSum1),
                },
                new List<TableRow>()
                {
                    FindViewById<TableRow>(Resource.Id.headerPlayer12),
                    FindViewById<TableRow>(Resource.Id.headerPlayer22),
                    FindViewById<TableRow>(Resource.Id.headerPlayer32),
                    FindViewById<TableRow>(Resource.Id.headerPlayerSum2),
                },
                new List<TableRow>()
                {
                    FindViewById<TableRow>(Resource.Id.headerPlayer13),
                    FindViewById<TableRow>(Resource.Id.headerPlayer23),
                    FindViewById<TableRow>(Resource.Id.headerPlayer33),
                    FindViewById<TableRow>(Resource.Id.headerPlayerSum3),
                },
                new List<TableRow>()
                {
                    FindViewById<TableRow>(Resource.Id.headerPlayer14),
                    FindViewById<TableRow>(Resource.Id.headerPlayer24),
                    FindViewById<TableRow>(Resource.Id.headerPlayer34),
                    FindViewById<TableRow>(Resource.Id.headerPlayerSum4),
                },
                new List<TableRow>()
                {
                    FindViewById<TableRow>(Resource.Id.headerPlayer15),
                    FindViewById<TableRow>(Resource.Id.headerPlayer25),
                    FindViewById<TableRow>(Resource.Id.headerPlayer35),
                    FindViewById<TableRow>(Resource.Id.headerPlayerSum5),
                },
                new List<TableRow>()
                {
                    FindViewById<TableRow>(Resource.Id.headerPlayer16),
                    FindViewById<TableRow>(Resource.Id.headerPlayer26),
                    FindViewById<TableRow>(Resource.Id.headerPlayer36),
                    FindViewById<TableRow>(Resource.Id.headerPlayerSum6),
                },
            };

            List<List<TextView>> textViews = new List<List<TextView>>()
            {
                new List<TextView>()
                {
                    FindViewById<TextView>(Resource.Id.headerTextPlayer11),
                    FindViewById<TextView>(Resource.Id.headerTextPlayer21),
                    FindViewById<TextView>(Resource.Id.headerTextPlayer31),
                    FindViewById<TextView>(Resource.Id.headerTextPlayerSum1)
                },
                new List<TextView>()
                {
                    FindViewById<TextView>(Resource.Id.headerTextPlayer12),
                    FindViewById<TextView>(Resource.Id.headerTextPlayer22),
                    FindViewById<TextView>(Resource.Id.headerTextPlayer32),
                    FindViewById<TextView>(Resource.Id.headerTextPlayerSum2)
                },
                new List<TextView>()
                {
                    FindViewById<TextView>(Resource.Id.headerTextPlayer13),
                    FindViewById<TextView>(Resource.Id.headerTextPlayer23),
                    FindViewById<TextView>(Resource.Id.headerTextPlayer33),
                    FindViewById<TextView>(Resource.Id.headerTextPlayerSum3)
                },
                new List<TextView>()
                {
                    FindViewById<TextView>(Resource.Id.headerTextPlayer14),
                    FindViewById<TextView>(Resource.Id.headerTextPlayer24),
                    FindViewById<TextView>(Resource.Id.headerTextPlayer34),
                    FindViewById<TextView>(Resource.Id.headerTextPlayerSum4)
                },
                new List<TextView>()
                {
                    FindViewById<TextView>(Resource.Id.headerTextPlayer15),
                    FindViewById<TextView>(Resource.Id.headerTextPlayer25),
                    FindViewById<TextView>(Resource.Id.headerTextPlayer35),
                    FindViewById<TextView>(Resource.Id.headerTextPlayerSum5)
                },
                new List<TextView>()
                {
                    FindViewById<TextView>(Resource.Id.headerTextPlayer16),
                    FindViewById<TextView>(Resource.Id.headerTextPlayer26),
                    FindViewById<TextView>(Resource.Id.headerTextPlayer36),
                    FindViewById<TextView>(Resource.Id.headerTextPlayerSum6)
                },
            };

            for (int i = PlayersCount; i < 6; i++)
            {
                foreach (TableRow tableRow in tableRows[i])
                    tableRow.Visibility = ViewStates.Gone;
            }
            for (int i = 0; i < PlayersCount; i++)
            {
                foreach (TextView textView in textViews[i])
                {
                    textView.Text = GetPlayer(i + 1).Name;
                }
            }

            //tooltipText tylko od API 25
            TableRow headerRow1 = FindViewById<TableRow>(Resource.Id.headerRow1);
            TableRow headerRow2 = FindViewById<TableRow>(Resource.Id.headerRow2);
            foreach (TableRow headerRow in new TableRow[] { headerRow1, headerRow2 })
            {
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerWalls), GranadaOption != GranadaOption.Alone);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerPavilion), GranadaOption != GranadaOption.Alone);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerSeraglio), GranadaOption != GranadaOption.Alone);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerArcades), GranadaOption != GranadaOption.Alone);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerChambers), GranadaOption != GranadaOption.Alone);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerGarden), GranadaOption != GranadaOption.Alone);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerTower), GranadaOption != GranadaOption.Alone);
                SetVisibility(headerRow.FindViewById<LinearLayout>(Resource.Id.headerImmediatelyPoints), (HasModule(ExpansionModule.DesignerPalaceDesigners) || HasModule(ExpansionModule.DesignerGatesWithoutEnd)));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerImmediatelyPointsPalaceDesigners), HasModule(ExpansionModule.DesignerPalaceDesigners));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerImmediatelyPointsGatesWithoutEnd), HasModule(ExpansionModule.DesignerGatesWithoutEnd));
                SetVisibility(headerRow.FindViewById<LinearLayout>(Resource.Id.headerBonuses), (HasModule(ExpansionModule.ExpansionBonusCards) || HasModule(ExpansionModule.ExpansionSquares) || HasModule(ExpansionModule.ExpansionCharacters) || HasModule(ExpansionModule.DesignerExtensions) || HasModule(ExpansionModule.DesignerGatesWithoutEnd)));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerBonusesBonusCards), HasModule(ExpansionModule.ExpansionBonusCards));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerBonusesSquares), HasModule(ExpansionModule.ExpansionSquares));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerBonusesExtensions), HasModule(ExpansionModule.DesignerExtensions));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerBonusesGatesWithoutEnd), HasModule(ExpansionModule.DesignerGatesWithoutEnd));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerBonusesTheWiseMan), HasModule(ExpansionModule.ExpansionCharacters));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerTheCityWatch), HasModule(ExpansionModule.ExpansionCharacters));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerCamps), HasModule(ExpansionModule.ExpansionCamps));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerStreetTraders), HasModule(ExpansionModule.ExpansionStreetTrader));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerTreasureChamber), HasModule(ExpansionModule.ExpansionTreasureChamber));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerInvaders), HasModule(ExpansionModule.ExpansionInvaders));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerBazaars), HasModule(ExpansionModule.ExpansionBazaars) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerArtOfTheMoors), HasModule(ExpansionModule.ExpansionArtOfTheMoors));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerFalconers), HasModule(ExpansionModule.ExpansionFalconers));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerWatchtowers), HasModule(ExpansionModule.ExpansionWatchtowers));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMedina), HasModule(ExpansionModule.QueenieMedina));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerBuildingsWithoutServantTile), HasModule(ExpansionModule.DesignerPalaceStaff));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerOrchards), HasModule(ExpansionModule.DesignerOrchards) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerBathhouses), HasModule(ExpansionModule.DesignerBathhouses));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerWishingWells), HasModule(ExpansionModule.DesignerWishingWell));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerCompletedProjects), HasModule(ExpansionModule.DesignerFreshColors));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerAnimals), HasModule(ExpansionModule.DesignerAlhambraZoo));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerBlackDices), HasModule(ExpansionModule.DesignerBuildingsOfPower));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerHandymen), HasModule(ExpansionModule.DesignerHandymen));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerTreasures), HasModule(ExpansionModule.FanTreasures) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMission1), HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission1) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMission2), HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission2) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMission3), HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission3) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMission4), HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission4) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMission5), HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission5) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMission6), HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission6) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMission7), HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission7) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMission8), HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission8) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMission9), HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission9) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMoatLength), HasModule(ExpansionModule.Granada));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerArena), HasModule(ExpansionModule.Granada));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerBathHouse), HasModule(ExpansionModule.Granada));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerLibrary), HasModule(ExpansionModule.Granada));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerHostel), HasModule(ExpansionModule.Granada));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerHospital), HasModule(ExpansionModule.Granada));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMarket), HasModule(ExpansionModule.Granada));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerPark), HasModule(ExpansionModule.Granada));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerSchool), HasModule(ExpansionModule.Granada));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerResidentialArea), HasModule(ExpansionModule.Granada));
                SetVisibility(headerRow.FindViewById<LinearLayout>(Resource.Id.headerWallMoat), GranadaOption == GranadaOption.With);
            }

            AddPlayerDetailsRoundBlock(ScoringRound.First);
            if (ScoreRound == ScoringRound.ThirdBeforeLeftover || ScoreRound == ScoringRound.Third || ScoreRound == ScoringRound.Finish)
            {
                AddPlayerDetailsRoundBlock(ScoringRound.Second);
            }
            else
            {
                FindViewById<TableRow>(Resource.Id.headerRound2).Visibility = ViewStates.Gone;
                foreach (List<TableRow> tableRowContent in tableRows)
                    tableRowContent[1].Visibility = ViewStates.Gone;
            }
            if (ScoreRound == ScoringRound.Finish)
            {
                AddPlayerDetailsRoundBlock(ScoringRound.Third);
            }
            else
            {
                FindViewById<TableRow>(Resource.Id.headerRound3).Visibility = ViewStates.Gone;
                foreach (List<TableRow> tableRowContent in tableRows)
                    tableRowContent[2].Visibility = ViewStates.Gone;
            }
            AddPlayerDetailsRoundBlock(ScoringRound.Finish);
        }

        private void AddPlayerDetailsRoundBlock(ScoringRound round)
        {
            TableRow emptyrow = (TableRow)LayoutInflater.From(this).Inflate(Resource.Layout.details_row, null);
            contentTable.AddView(emptyrow, contentTable.ChildCount - 1);
            contentTable.RequestLayout();

            bool summary = round == ScoringRound.Finish;

            for (int i = 0; i < PlayersCount; i++)
                AddPlayerDetailsRow(GetPlayer(i + 1).GetScoreDetails(round), summary);
        }

        private void AddPlayerDetailsRow(ScoreDetails scoreDetails, bool summary)
        {
            TableRow row = (TableRow)LayoutInflater.From(this).Inflate(Resource.Layout.details_row, null);
            row.FindViewById<TextView>(Resource.Id.resultSum).Text = scoreDetails.Sum.ToString();
            if (summary)
                row.FindViewById<TextView>(Resource.Id.resultSum).Typeface = Android.Graphics.Typeface.DefaultBold;
            row.FindViewById<TextView>(Resource.Id.resultImmediatelyPoints).Text = scoreDetails.ImmediatelyPoints.ToString();
            row.FindViewById<TextView>(Resource.Id.resultWalls).Text = scoreDetails.WallLength.ToString();
            row.FindViewById<TextView>(Resource.Id.resultPavilion).Text = scoreDetails.Pavilion.ToString();
            row.FindViewById<TextView>(Resource.Id.resultSeraglio).Text = scoreDetails.Seraglio.ToString();
            row.FindViewById<TextView>(Resource.Id.resultArcades).Text = scoreDetails.Arcades.ToString();
            row.FindViewById<TextView>(Resource.Id.resultChambers).Text = scoreDetails.Chambers.ToString();
            row.FindViewById<TextView>(Resource.Id.resultGarden).Text = scoreDetails.Garden.ToString();
            row.FindViewById<TextView>(Resource.Id.resultTower).Text = scoreDetails.Tower.ToString();
            row.FindViewById<TextView>(Resource.Id.resultBonuses).Text = $"(+{scoreDetails.BuildingsBonuses})";
            row.FindViewById<TextView>(Resource.Id.resultTheCityWatch).Text = scoreDetails.TheCityWatch.ToString();
            row.FindViewById<TextView>(Resource.Id.resultCamps).Text = scoreDetails.Camps.ToString();
            row.FindViewById<TextView>(Resource.Id.resultStreetTraders).Text = scoreDetails.StreetTraders.ToString();
            row.FindViewById<TextView>(Resource.Id.resultTreasureChamber).Text = scoreDetails.TreasureChamber.ToString();
            row.FindViewById<TextView>(Resource.Id.resultInvaders).Text = $"-{scoreDetails.Invaders}";
            row.FindViewById<TextView>(Resource.Id.resultBazaars).Text = scoreDetails.Bazaars.ToString();
            row.FindViewById<TextView>(Resource.Id.resultArtOfTheMoors).Text = scoreDetails.ArtOfTheMoors.ToString();
            row.FindViewById<TextView>(Resource.Id.resultFalconers).Text = scoreDetails.Falconers.ToString();
            row.FindViewById<TextView>(Resource.Id.resultWatchtowers).Text = scoreDetails.Watchtowers.ToString();
            row.FindViewById<TextView>(Resource.Id.resultMedina).Text = $"-{scoreDetails.Medina}";
            row.FindViewById<TextView>(Resource.Id.resultBuildingsWithoutServantTile).Text = $"-{scoreDetails.BuildingsWithoutServantTile}";
            row.FindViewById<TextView>(Resource.Id.resultOrchards).Text = scoreDetails.Orchards.ToString();
            row.FindViewById<TextView>(Resource.Id.resultBathhouses).Text = scoreDetails.Bathhouses.ToString();
            row.FindViewById<TextView>(Resource.Id.resultWishingWells).Text = scoreDetails.WishingWells.ToString();
            row.FindViewById<TextView>(Resource.Id.resultCompletedProjects).Text = scoreDetails.CompletedProjects.ToString();
            row.FindViewById<TextView>(Resource.Id.resultAnimals).Text = scoreDetails.Animals.ToString();
            row.FindViewById<TextView>(Resource.Id.resultBlackDices).Text = scoreDetails.BlackDices.ToString();
            row.FindViewById<TextView>(Resource.Id.resultHandymen).Text = scoreDetails.Handymen.ToString();
            row.FindViewById<TextView>(Resource.Id.resultTreasures).Text = scoreDetails.Treasures.ToString();
            row.FindViewById<TextView>(Resource.Id.resultMission1).Text = scoreDetails.Mission1.ToString();
            row.FindViewById<TextView>(Resource.Id.resultMission2).Text = scoreDetails.Mission2.ToString();
            row.FindViewById<TextView>(Resource.Id.resultMission3).Text = scoreDetails.Mission3.ToString();
            row.FindViewById<TextView>(Resource.Id.resultMission4).Text = scoreDetails.Mission4.ToString();
            row.FindViewById<TextView>(Resource.Id.resultMission5).Text = scoreDetails.Mission5.ToString();
            row.FindViewById<TextView>(Resource.Id.resultMission6).Text = scoreDetails.Mission6.ToString();
            row.FindViewById<TextView>(Resource.Id.resultMission7).Text = scoreDetails.Mission7.ToString();
            row.FindViewById<TextView>(Resource.Id.resultMission8).Text = scoreDetails.Mission8.ToString();
            row.FindViewById<TextView>(Resource.Id.resultMission9).Text = scoreDetails.Mission9.ToString();
            row.FindViewById<TextView>(Resource.Id.resultMoatLength).Text = scoreDetails.MoatLength.ToString();
            row.FindViewById<TextView>(Resource.Id.resultArena).Text = scoreDetails.Arena.ToString();
            row.FindViewById<TextView>(Resource.Id.resultBathHouse).Text = scoreDetails.BathHouse.ToString();
            row.FindViewById<TextView>(Resource.Id.resultLibrary).Text = scoreDetails.Library.ToString();
            row.FindViewById<TextView>(Resource.Id.resultHostel).Text = scoreDetails.Hostel.ToString();
            row.FindViewById<TextView>(Resource.Id.resultHospital).Text = scoreDetails.Hospital.ToString();
            row.FindViewById<TextView>(Resource.Id.resultMarket).Text = scoreDetails.Market.ToString();
            row.FindViewById<TextView>(Resource.Id.resultPark).Text = scoreDetails.Park.ToString();
            row.FindViewById<TextView>(Resource.Id.resultSchool).Text = scoreDetails.School.ToString();
            row.FindViewById<TextView>(Resource.Id.resultResidentialArea).Text = scoreDetails.ResidentialArea.ToString();
            row.FindViewById<TextView>(Resource.Id.resultWallMoat).Text = scoreDetails.WallMoatCombination.ToString();

            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultWalls), GranadaOption != GranadaOption.Alone);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultPavilion), GranadaOption != GranadaOption.Alone);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultSeraglio), GranadaOption != GranadaOption.Alone);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultArcades), GranadaOption != GranadaOption.Alone);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultChambers), GranadaOption != GranadaOption.Alone);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultGarden), GranadaOption != GranadaOption.Alone);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultTower), GranadaOption != GranadaOption.Alone);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultImmediatelyPoints), (HasModule(ExpansionModule.DesignerPalaceDesigners) || HasModule(ExpansionModule.DesignerGatesWithoutEnd)));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultBonuses), (HasModule(ExpansionModule.ExpansionBonusCards) || HasModule(ExpansionModule.ExpansionSquares) || HasModule(ExpansionModule.ExpansionCharacters) || HasModule(ExpansionModule.DesignerExtensions) || HasModule(ExpansionModule.DesignerGatesWithoutEnd)));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultTheCityWatch), HasModule(ExpansionModule.ExpansionCharacters));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultCamps), HasModule(ExpansionModule.ExpansionCamps));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultStreetTraders), HasModule(ExpansionModule.ExpansionStreetTrader));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultTreasureChamber), HasModule(ExpansionModule.ExpansionTreasureChamber));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultInvaders), HasModule(ExpansionModule.ExpansionInvaders));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultBazaars), HasModule(ExpansionModule.ExpansionBazaars));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultArtOfTheMoors), HasModule(ExpansionModule.ExpansionArtOfTheMoors));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultFalconers), HasModule(ExpansionModule.ExpansionFalconers));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultWatchtowers), HasModule(ExpansionModule.ExpansionWatchtowers));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMedina), HasModule(ExpansionModule.QueenieMedina));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultBuildingsWithoutServantTile), HasModule(ExpansionModule.DesignerPalaceStaff));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultOrchards), HasModule(ExpansionModule.DesignerOrchards) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultBathhouses), HasModule(ExpansionModule.DesignerBathhouses));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultWishingWells), HasModule(ExpansionModule.DesignerWishingWell));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultCompletedProjects), HasModule(ExpansionModule.DesignerFreshColors));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultAnimals), HasModule(ExpansionModule.DesignerAlhambraZoo));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultBlackDices), HasModule(ExpansionModule.DesignerBuildingsOfPower));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultHandymen), HasModule(ExpansionModule.DesignerHandymen));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultTreasures), HasModule(ExpansionModule.FanTreasures) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMission1), HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission1) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMission2), HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission2) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMission3), HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission3) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMission4), HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission4) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMission5), HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission5) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMission6), HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission6) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMission7), HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission7) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMission8), HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission8) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMission9), HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission9) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMoatLength), HasModule(ExpansionModule.Granada));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultArena), HasModule(ExpansionModule.Granada));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultBathHouse), HasModule(ExpansionModule.Granada));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultLibrary), HasModule(ExpansionModule.Granada));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultHostel), HasModule(ExpansionModule.Granada));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultHospital), HasModule(ExpansionModule.Granada));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMarket), HasModule(ExpansionModule.Granada));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultPark), HasModule(ExpansionModule.Granada));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultSchool), HasModule(ExpansionModule.Granada));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultResidentialArea), HasModule(ExpansionModule.Granada));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultWallMoat), GranadaOption == GranadaOption.With);

            contentTable.AddView(row, contentTable.ChildCount - 1);
            contentTable.RequestLayout();
        }

        protected void SetVisibility(View view, bool condition)
        {
            view.Visibility = condition ? ViewStates.Visible : ViewStates.Gone;
        }
    }
}