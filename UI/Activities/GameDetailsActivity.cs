using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "Szczegóły", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class GameDetailsActivity : BaseActivity
    {
        public ResultHistory Result => Application.CurrentResult;
        public int PlayersCount => Result.Players.Count;
        public ScoringRound ScoreRound => Result.ScoreRound;

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
            return Result.Modules.Contains(module);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //TODO pokaż startTime i endTime

            base.OnCreate(savedInstanceState);

            TextView titleDate = FindViewById<TextView>(Resource.Id.titleDate);
            titleDate.Text = $"{Result.StartDateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("es-ES"))} - {(Result.EndDateTime!=null? ((DateTime)Result.EndDateTime).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("es-ES")):String.Empty)}";

            contentTable = FindViewById<TableLayout>(Resource.Id.contentTable);

            //TODO w headerRow na ikonach help text po kliknięciu

            TableRow headerPlayer11 = FindViewById<TableRow>(Resource.Id.headerPlayer11);
            TableRow headerPlayer12 = FindViewById<TableRow>(Resource.Id.headerPlayer12);
            TableRow headerPlayer13 = FindViewById<TableRow>(Resource.Id.headerPlayer13);
            TableRow headerPlayer14 = FindViewById<TableRow>(Resource.Id.headerPlayer14);
            TableRow headerPlayer15 = FindViewById<TableRow>(Resource.Id.headerPlayer15);
            TableRow headerPlayer16 = FindViewById<TableRow>(Resource.Id.headerPlayer16);
            TextView headerTextPlayer11 = FindViewById<TextView>(Resource.Id.headerTextPlayer11);
            TextView headerTextPlayer12 = FindViewById<TextView>(Resource.Id.headerTextPlayer12);
            TextView headerTextPlayer13 = FindViewById<TextView>(Resource.Id.headerTextPlayer13);
            TextView headerTextPlayer14 = FindViewById<TextView>(Resource.Id.headerTextPlayer14);
            TextView headerTextPlayer15 = FindViewById<TextView>(Resource.Id.headerTextPlayer15);
            TextView headerTextPlayer16 = FindViewById<TextView>(Resource.Id.headerTextPlayer16);
            TableRow headerPlayer21 = FindViewById<TableRow>(Resource.Id.headerPlayer21);
            TableRow headerPlayer22 = FindViewById<TableRow>(Resource.Id.headerPlayer22);
            TableRow headerPlayer23 = FindViewById<TableRow>(Resource.Id.headerPlayer23);
            TableRow headerPlayer24 = FindViewById<TableRow>(Resource.Id.headerPlayer24);
            TableRow headerPlayer25 = FindViewById<TableRow>(Resource.Id.headerPlayer25);
            TableRow headerPlayer26 = FindViewById<TableRow>(Resource.Id.headerPlayer26);
            TextView headerTextPlayer21 = FindViewById<TextView>(Resource.Id.headerTextPlayer21);
            TextView headerTextPlayer22 = FindViewById<TextView>(Resource.Id.headerTextPlayer22);
            TextView headerTextPlayer23 = FindViewById<TextView>(Resource.Id.headerTextPlayer23);
            TextView headerTextPlayer24 = FindViewById<TextView>(Resource.Id.headerTextPlayer24);
            TextView headerTextPlayer25 = FindViewById<TextView>(Resource.Id.headerTextPlayer25);
            TextView headerTextPlayer26 = FindViewById<TextView>(Resource.Id.headerTextPlayer26);
            TableRow headerPlayer31 = FindViewById<TableRow>(Resource.Id.headerPlayer31);
            TableRow headerPlayer32 = FindViewById<TableRow>(Resource.Id.headerPlayer32);
            TableRow headerPlayer33 = FindViewById<TableRow>(Resource.Id.headerPlayer33);
            TableRow headerPlayer34 = FindViewById<TableRow>(Resource.Id.headerPlayer34);
            TableRow headerPlayer35 = FindViewById<TableRow>(Resource.Id.headerPlayer35);
            TableRow headerPlayer36 = FindViewById<TableRow>(Resource.Id.headerPlayer36);
            TextView headerTextPlayer31 = FindViewById<TextView>(Resource.Id.headerTextPlayer31);
            TextView headerTextPlayer32 = FindViewById<TextView>(Resource.Id.headerTextPlayer32);
            TextView headerTextPlayer33 = FindViewById<TextView>(Resource.Id.headerTextPlayer33);
            TextView headerTextPlayer34 = FindViewById<TextView>(Resource.Id.headerTextPlayer34);
            TextView headerTextPlayer35 = FindViewById<TextView>(Resource.Id.headerTextPlayer35);
            TextView headerTextPlayer36 = FindViewById<TextView>(Resource.Id.headerTextPlayer36);
            TableRow headerPlayerSum1 = FindViewById<TableRow>(Resource.Id.headerPlayerSum1);
            TableRow headerPlayerSum2 = FindViewById<TableRow>(Resource.Id.headerPlayerSum2);
            TableRow headerPlayerSum3 = FindViewById<TableRow>(Resource.Id.headerPlayerSum3);
            TableRow headerPlayerSum4 = FindViewById<TableRow>(Resource.Id.headerPlayerSum4);
            TableRow headerPlayerSum5 = FindViewById<TableRow>(Resource.Id.headerPlayerSum5);
            TableRow headerPlayerSum6 = FindViewById<TableRow>(Resource.Id.headerPlayerSum6);
            TextView headerTextPlayerSum1 = FindViewById<TextView>(Resource.Id.headerTextPlayerSum1);
            TextView headerTextPlayerSum2 = FindViewById<TextView>(Resource.Id.headerTextPlayerSum2);
            TextView headerTextPlayerSum3 = FindViewById<TextView>(Resource.Id.headerTextPlayerSum3);
            TextView headerTextPlayerSum4 = FindViewById<TextView>(Resource.Id.headerTextPlayerSum4);
            TextView headerTextPlayerSum5 = FindViewById<TextView>(Resource.Id.headerTextPlayerSum5);
            TextView headerTextPlayerSum6 = FindViewById<TextView>(Resource.Id.headerTextPlayerSum6);

            if (PlayersCount < 6)
            {
                headerPlayer16.Visibility = ViewStates.Gone;
                headerPlayer26.Visibility = ViewStates.Gone;
                headerPlayer36.Visibility = ViewStates.Gone;
                headerPlayerSum6.Visibility = ViewStates.Gone;
            }
            if (PlayersCount < 5)
            {
                headerPlayer15.Visibility = ViewStates.Gone;
                headerPlayer25.Visibility = ViewStates.Gone;
                headerPlayer35.Visibility = ViewStates.Gone;
                headerPlayerSum5.Visibility = ViewStates.Gone;
            }
            if (PlayersCount < 4)
            {
                headerPlayer14.Visibility = ViewStates.Gone;
                headerPlayer24.Visibility = ViewStates.Gone;
                headerPlayer34.Visibility = ViewStates.Gone;
                headerPlayerSum4.Visibility = ViewStates.Gone;
            }

            headerTextPlayer11.Text = GetPlayer(1).Name;
            headerTextPlayer21.Text = GetPlayer(1).Name;
            headerTextPlayer31.Text = GetPlayer(1).Name;
            headerTextPlayerSum1.Text = GetPlayer(1).Name;
            headerTextPlayer12.Text = GetPlayer(2).Name;
            headerTextPlayer22.Text = GetPlayer(2).Name;
            headerTextPlayer32.Text = GetPlayer(2).Name;
            headerTextPlayerSum2.Text = GetPlayer(2).Name;
            headerTextPlayer13.Text = GetPlayer(3).Name;
            headerTextPlayer23.Text = GetPlayer(3).Name;
            headerTextPlayer33.Text = GetPlayer(3).Name;
            headerTextPlayerSum3.Text = GetPlayer(3).Name;
            if (PlayersCount > 3)
            {
                headerTextPlayer14.Text = GetPlayer(4).Name;
                headerTextPlayer24.Text = GetPlayer(4).Name;
                headerTextPlayer34.Text = GetPlayer(4).Name;
                headerTextPlayerSum4.Text = GetPlayer(4).Name;
            }
            if (PlayersCount > 4)
            {
                headerTextPlayer15.Text = GetPlayer(5).Name;
                headerTextPlayer25.Text = GetPlayer(5).Name;
                headerTextPlayer35.Text = GetPlayer(5).Name;
                headerTextPlayerSum5.Text = GetPlayer(5).Name;
            }
            if (PlayersCount > 5)
            {
                headerTextPlayer16.Text = GetPlayer(6).Name;
                headerTextPlayer26.Text = GetPlayer(6).Name;
                headerTextPlayer36.Text = GetPlayer(6).Name;
                headerTextPlayerSum6.Text = GetPlayer(6).Name;
            }

            TableRow headerRow1 = FindViewById<TableRow>(Resource.Id.headerRow1);
            TableRow headerRow2 = FindViewById<TableRow>(Resource.Id.headerRow2);
            foreach (TableRow headerRow in new TableRow[] { headerRow1, headerRow2 })
            {
                SetVisibility(headerRow.FindViewById<LinearLayout>(Resource.Id.headerImmediatelyPoints), HasModule(ExpansionModule.DesignerPalaceDesigners) || HasModule(ExpansionModule.DesignerGatesWithoutEnd));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerImmediatelyPointsPalaceDesigners), HasModule(ExpansionModule.DesignerPalaceDesigners));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerImmediatelyPointsGatesWithoutEnd),  HasModule(ExpansionModule.DesignerGatesWithoutEnd));
                SetVisibility(headerRow.FindViewById<LinearLayout>(Resource.Id.headerBonuses), HasModule(ExpansionModule.ExpansionBonusCards) || HasModule(ExpansionModule.DesignerExtensions) || HasModule(ExpansionModule.DesignerGatesWithoutEnd));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerBonusesBonusCards), HasModule(ExpansionModule.ExpansionBonusCards));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerBonusesExtensions), HasModule(ExpansionModule.DesignerExtensions));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerBonusesGatesWithoutEnd), HasModule(ExpansionModule.DesignerGatesWithoutEnd));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerBuildingsWithoutServantTile), HasModule(ExpansionModule.DesignerPalaceStaff));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerOrchards), HasModule(ExpansionModule.DesignerOrchards) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerBathhouses), HasModule(ExpansionModule.DesignerBathhouses));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerWishingWells), HasModule(ExpansionModule.DesignerWishingWell));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerCompletedProjects), HasModule(ExpansionModule.DesignerFreshColors));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerAnimals), HasModule(ExpansionModule.DesignerAlhambraZoo));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerBlackDices), HasModule(ExpansionModule.DesignerBuildingsOfPower));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerHandymen), HasModule(ExpansionModule.DesignerHandymen));
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerTreasures), HasModule(ExpansionModule.FanTreasures) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMission1), HasModule(ExpansionModule.FanCaliphsGuidelines) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMission2), HasModule(ExpansionModule.FanCaliphsGuidelines) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMission3), HasModule(ExpansionModule.FanCaliphsGuidelines) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMission4), HasModule(ExpansionModule.FanCaliphsGuidelines) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMission5), HasModule(ExpansionModule.FanCaliphsGuidelines) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMission6), HasModule(ExpansionModule.FanCaliphsGuidelines) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMission7), HasModule(ExpansionModule.FanCaliphsGuidelines) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMission8), HasModule(ExpansionModule.FanCaliphsGuidelines) && ScoreRound == ScoringRound.Finish);
                SetVisibility(headerRow.FindViewById<ImageView>(Resource.Id.headerMission9), HasModule(ExpansionModule.FanCaliphsGuidelines) && ScoreRound == ScoringRound.Finish);
            }

            AddPlayerDetailsRoundBlock(ScoringRound.First);
            if (ScoreRound == ScoringRound.ThirdBeforeLeftover || ScoreRound == ScoringRound.Third || ScoreRound == ScoringRound.Finish)
            {
                AddPlayerDetailsRoundBlock(ScoringRound.Second);
            }
            else
            {
                FindViewById<TableRow>(Resource.Id.headerRound2).Visibility = ViewStates.Gone;
                headerPlayer21.Visibility = ViewStates.Gone;
                headerPlayer22.Visibility = ViewStates.Gone;
                headerPlayer23.Visibility = ViewStates.Gone;
                headerPlayer24.Visibility = ViewStates.Gone;
                headerPlayer25.Visibility = ViewStates.Gone;
                headerPlayer26.Visibility = ViewStates.Gone;
            }
            if (ScoreRound == ScoringRound.Finish)
            {
                AddPlayerDetailsRoundBlock(ScoringRound.Third);
            }
            else
            {
                FindViewById<TableRow>(Resource.Id.headerRound3).Visibility = ViewStates.Gone;
                headerPlayer31.Visibility = ViewStates.Gone;
                headerPlayer32.Visibility = ViewStates.Gone;
                headerPlayer33.Visibility = ViewStates.Gone;
                headerPlayer34.Visibility = ViewStates.Gone;
                headerPlayer35.Visibility = ViewStates.Gone;
                headerPlayer36.Visibility = ViewStates.Gone;
            }
            AddPlayerDetailsRoundBlock(ScoringRound.Finish);
        }

        private void AddPlayerDetailsRoundBlock(ScoringRound round)
        {
            TableRow emptyrow = (TableRow)LayoutInflater.From(this).Inflate(Resource.Layout.details_row, null);
            contentTable.AddView(emptyrow, contentTable.ChildCount - 1);
            contentTable.RequestLayout();

            AddPlayerDetailsRow(GetPlayer(1).GetScoreDetails(round));
            AddPlayerDetailsRow(GetPlayer(2).GetScoreDetails(round));
            AddPlayerDetailsRow(GetPlayer(3).GetScoreDetails(round));

            if (PlayersCount > 3)
            {
                AddPlayerDetailsRow(GetPlayer(4).GetScoreDetails(round));
            }
            if (PlayersCount > 4)
            {
                AddPlayerDetailsRow(GetPlayer(5).GetScoreDetails(round));
            }
            if (PlayersCount > 5)
            {
                AddPlayerDetailsRow(GetPlayer(6).GetScoreDetails(round));
            }
        }

        private void AddPlayerDetailsRow(ScoreDetails scoreDetails)
        {
            TableRow row = (TableRow)LayoutInflater.From(this).Inflate(Resource.Layout.details_row, null);
            row.FindViewById<TextView>(Resource.Id.resultSum).Text = scoreDetails.Sum.ToString();
            row.FindViewById<TextView>(Resource.Id.resultImmediatelyPoints).Text = scoreDetails.ImmediatelyPoints.ToString();
            row.FindViewById<TextView>(Resource.Id.resultWalls).Text = scoreDetails.WallLength.ToString();
            row.FindViewById<TextView>(Resource.Id.resultPavilion).Text = scoreDetails.PavilionNumber.ToString();
            row.FindViewById<TextView>(Resource.Id.resultSeraglio).Text = scoreDetails.SeraglioNumber.ToString();
            row.FindViewById<TextView>(Resource.Id.resultArcades).Text = scoreDetails.ArcadesNumber.ToString();
            row.FindViewById<TextView>(Resource.Id.resultChambers).Text = scoreDetails.ChambersNumber.ToString();
            row.FindViewById<TextView>(Resource.Id.resultGarden).Text = scoreDetails.GardenNumber.ToString();
            row.FindViewById<TextView>(Resource.Id.resultTower).Text = scoreDetails.TowerNumber.ToString();
            row.FindViewById<TextView>(Resource.Id.resultBonuses).Text = $"(+{scoreDetails.BuildingsBonuses})";
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

            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultImmediatelyPoints), HasModule(ExpansionModule.DesignerPalaceDesigners) || HasModule(ExpansionModule.DesignerGatesWithoutEnd));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultBonuses), HasModule(ExpansionModule.DesignerExtensions) || HasModule(ExpansionModule.ExpansionBonusCards) || HasModule(ExpansionModule.DesignerGatesWithoutEnd));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultBuildingsWithoutServantTile), HasModule(ExpansionModule.DesignerPalaceStaff));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultOrchards), HasModule(ExpansionModule.DesignerOrchards) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultBathhouses), HasModule(ExpansionModule.DesignerBathhouses));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultWishingWells), HasModule(ExpansionModule.DesignerWishingWell));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultCompletedProjects), HasModule(ExpansionModule.DesignerFreshColors));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultAnimals), HasModule(ExpansionModule.DesignerAlhambraZoo));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultBlackDices), HasModule(ExpansionModule.DesignerBuildingsOfPower));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultHandymen), HasModule(ExpansionModule.DesignerHandymen));
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultTreasures), HasModule(ExpansionModule.FanTreasures) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMission1), HasModule(ExpansionModule.FanCaliphsGuidelines) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMission2), HasModule(ExpansionModule.FanCaliphsGuidelines) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMission3), HasModule(ExpansionModule.FanCaliphsGuidelines) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMission4), HasModule(ExpansionModule.FanCaliphsGuidelines) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMission5), HasModule(ExpansionModule.FanCaliphsGuidelines) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMission6), HasModule(ExpansionModule.FanCaliphsGuidelines) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMission7), HasModule(ExpansionModule.FanCaliphsGuidelines) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMission8), HasModule(ExpansionModule.FanCaliphsGuidelines) && ScoreRound == ScoringRound.Finish);
            SetVisibility(row.FindViewById<TextView>(Resource.Id.resultMission9), HasModule(ExpansionModule.FanCaliphsGuidelines) && ScoreRound == ScoringRound.Finish);

            contentTable.AddView(row, contentTable.ChildCount - 1);
            contentTable.RequestLayout();
        }

        protected void SetVisibility(View view, bool condition)
        {
            view.Visibility = condition ? ViewStates.Visible : ViewStates.Gone;
        }
    }
}