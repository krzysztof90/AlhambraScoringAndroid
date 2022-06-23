using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidBase.Tools;
using AndroidBase.Tools.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "@string/details", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class GameDetailsActivity : BaseActivity
    {
        public ResultHistory Result => Application.CurrentResult;
        public int PlayersCount => Result.Players.Count;
        public ScoringRound ScoreRound => Result.ScoreRound;
        public GranadaOption GranadaOption => Result.GranadaOption;

        protected override int ContentView => Resource.Layout.activity_game_details;

        private TableLayout contentTable;

        private List<(Func<bool> condition, int headerRowResourceId, Func<ScoreDetails, bool, string> resultText)> resultConditions;

        public GameDetailsActivity()
        {
            resultConditions = new List<(Func<bool> condition, int headerRowResourceId, Func<ScoreDetails, bool, string> resultText)>()
            {
                (() => true, Resource.Id.headerSum, (scoreDetails, summary) => scoreDetails.Sum.ToString()),
                (() => HasModule(ExpansionModule.DesignerPalaceDesigners) || HasModule(ExpansionModule.DesignerGatesWithoutEnd),Resource.Id.headerImmediatelyPoints, (scoreDetails, summary) => !summary ? String.Empty : scoreDetails.ImmediatelyPoints.ToString()),
                (() => HasModule(ExpansionModule.DesignerPalaceDesigners),Resource.Id.headerImmediatelyPointsPalaceDesigners, null),
                (() => HasModule(ExpansionModule.DesignerGatesWithoutEnd),Resource.Id.headerImmediatelyPointsGatesWithoutEnd, null),
                (() => GranadaOption != GranadaOption.Alone, Resource.Id.headerWalls, (scoreDetails, summary) => scoreDetails.WallLength.ToString()),
                (() => GranadaOption != GranadaOption.Alone,Resource.Id.headerPavilion, (scoreDetails, summary) => scoreDetails.Pavilion.ToString()),
                (() => GranadaOption != GranadaOption.Alone,Resource.Id.headerSeraglio, (scoreDetails, summary) => scoreDetails.Seraglio.ToString()),
                (() => GranadaOption != GranadaOption.Alone,Resource.Id.headerArcades, (scoreDetails, summary) => scoreDetails.Arcades.ToString()),
                (() => GranadaOption != GranadaOption.Alone,Resource.Id.headerChambers, (scoreDetails, summary) => scoreDetails.Chambers.ToString()),
                (() => GranadaOption != GranadaOption.Alone,Resource.Id.headerGarden, (scoreDetails, summary) => scoreDetails.Garden.ToString()),
                (() => GranadaOption != GranadaOption.Alone,Resource.Id.headerTower, (scoreDetails, summary) => scoreDetails.Tower.ToString()),
                (() => HasModule(ExpansionModule.ExpansionBonusCards) || HasModule(ExpansionModule.ExpansionSquares) || HasModule(ExpansionModule.ExpansionCharacters) || HasModule(ExpansionModule.DesignerExtensions) || HasModule(ExpansionModule.DesignerGatesWithoutEnd),Resource.Id.headerBonuses, (scoreDetails, summary) => $"(+{scoreDetails.BuildingsBonuses})"),
                (() => HasModule(ExpansionModule.ExpansionBonusCards),Resource.Id.headerBonusesBonusCards, null),
                (() => HasModule(ExpansionModule.ExpansionSquares),Resource.Id.headerBonusesSquares, null),
                (() => HasModule(ExpansionModule.DesignerExtensions),Resource.Id.headerBonusesExtensions, null),
                (() => HasModule(ExpansionModule.DesignerGatesWithoutEnd),Resource.Id.headerBonusesGatesWithoutEnd, null),
                (() => HasModule(ExpansionModule.ExpansionCharacters),Resource.Id.headerBonusesTheWiseMan, null),
                (() => HasModule(ExpansionModule.ExpansionCharacters),Resource.Id.headerTheCityWatch, (scoreDetails, summary) => scoreDetails.TheCityWatch.ToString()),
                (() => HasModule(ExpansionModule.ExpansionCamps),Resource.Id.headerCamps, (scoreDetails, summary) => scoreDetails.Camps.ToString()),
                (() => HasModule(ExpansionModule.ExpansionStreetTrader),Resource.Id.headerStreetTraders, (scoreDetails, summary) => scoreDetails.StreetTraders.ToString()),
                (() => HasModule(ExpansionModule.ExpansionTreasureChamber),Resource.Id.headerTreasureChamber, (scoreDetails, summary) => scoreDetails.TreasureChamber.ToString()),
                (() => HasModule(ExpansionModule.ExpansionInvaders),Resource.Id.headerInvaders, (scoreDetails, summary) => $"-{scoreDetails.Invaders}"),
                (() => HasModule(ExpansionModule.ExpansionBazaars) && ScoreRound == ScoringRound.Finish,Resource.Id.headerBazaars, (scoreDetails, summary) => scoreDetails.Bazaars.ToString()),
                (() => HasModule(ExpansionModule.ExpansionArtOfTheMoors),Resource.Id.headerArtOfTheMoors, (scoreDetails, summary) => scoreDetails.ArtOfTheMoors.ToString()),
                (() => HasModule(ExpansionModule.ExpansionFalconers),Resource.Id.headerFalconers, (scoreDetails, summary) => scoreDetails.Falconers.ToString()),
                (() => HasModule(ExpansionModule.ExpansionWatchtowers),Resource.Id.headerWatchtowers, (scoreDetails, summary) => scoreDetails.Watchtowers.ToString()),
                (() => HasModule(ExpansionModule.QueenieMedina),Resource.Id.headerMedina, (scoreDetails, summary) => $"-{scoreDetails.Medina}"),
                (() => HasModule(ExpansionModule.DesignerPalaceStaff),Resource.Id.headerBuildingsWithoutServantTile, (scoreDetails, summary) => $"-{scoreDetails.BuildingsWithoutServantTile}"),
                (() => HasModule(ExpansionModule.DesignerOrchards) && ScoreRound == ScoringRound.Finish,Resource.Id.headerOrchards, (scoreDetails, summary) => scoreDetails.Orchards.ToString()),
                (() => HasModule(ExpansionModule.DesignerBathhouses),Resource.Id.headerBathhouses, (scoreDetails, summary) => scoreDetails.Bathhouses.ToString()),
                (() => HasModule(ExpansionModule.DesignerWishingWell),Resource.Id.headerWishingWells, (scoreDetails, summary) => scoreDetails.WishingWells.ToString()),
                (() => HasModule(ExpansionModule.DesignerFreshColors),Resource.Id.headerCompletedProjects, (scoreDetails, summary) => scoreDetails.CompletedProjects.ToString()),
                (() => HasModule(ExpansionModule.DesignerAlhambraZoo),Resource.Id.headerAnimals, (scoreDetails, summary) => scoreDetails.Animals.ToString()),
                (() => HasModule(ExpansionModule.DesignerBuildingsOfPower),Resource.Id.headerBlackDices, (scoreDetails, summary) => scoreDetails.BlackDices.ToString()),
                (() => HasModule(ExpansionModule.DesignerHandymen),Resource.Id.headerHandymen, (scoreDetails, summary) => scoreDetails.Handymen.ToString()),
                (() => HasModule(ExpansionModule.FanTreasures) && ScoreRound == ScoringRound.Finish,Resource.Id.headerTreasures, (scoreDetails, summary) => scoreDetails.Treasures.ToString()),
                (() => HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission1) && ScoreRound == ScoringRound.Finish,Resource.Id.headerMission1, (scoreDetails, summary) => scoreDetails.Mission1.ToString()),
                (() => HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission2) && ScoreRound == ScoringRound.Finish,Resource.Id.headerMission2, (scoreDetails, summary) => scoreDetails.Mission2.ToString()),
                (() => HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission3) && ScoreRound == ScoringRound.Finish,Resource.Id.headerMission3, (scoreDetails, summary) => scoreDetails.Mission3.ToString()),
                (() => HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission4) && ScoreRound == ScoringRound.Finish,Resource.Id.headerMission4, (scoreDetails, summary) => scoreDetails.Mission4.ToString()),
                (() => HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission5) && ScoreRound == ScoringRound.Finish,Resource.Id.headerMission5, (scoreDetails, summary) => scoreDetails.Mission5.ToString()),
                (() => HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission6) && ScoreRound == ScoringRound.Finish,Resource.Id.headerMission6, (scoreDetails, summary) => scoreDetails.Mission6.ToString()),
                (() => HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission7) && ScoreRound == ScoringRound.Finish,Resource.Id.headerMission7, (scoreDetails, summary) => scoreDetails.Mission7.ToString()),
                (() => HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission8) && ScoreRound == ScoringRound.Finish,Resource.Id.headerMission8, (scoreDetails, summary) => scoreDetails.Mission8.ToString()),
                (() => HasModule(ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission9) && ScoreRound == ScoringRound.Finish,Resource.Id.headerMission9, (scoreDetails, summary) => scoreDetails.Mission9.ToString()),
                (() => HasModule(ExpansionModule.Granada),Resource.Id.headerMoatLength, (scoreDetails, summary) => scoreDetails.MoatLength.ToString()),
                (() => HasModule(ExpansionModule.Granada),Resource.Id.headerArena, (scoreDetails, summary) => scoreDetails.Arena.ToString()),
                (() => HasModule(ExpansionModule.Granada),Resource.Id.headerBathHouse, (scoreDetails, summary) => scoreDetails.BathHouse.ToString()),
                (() => HasModule(ExpansionModule.Granada),Resource.Id.headerLibrary, (scoreDetails, summary) => scoreDetails.Library.ToString()),
                (() => HasModule(ExpansionModule.Granada),Resource.Id.headerHostel, (scoreDetails, summary) => scoreDetails.Hostel.ToString()),
                (() => HasModule(ExpansionModule.Granada),Resource.Id.headerHospital, (scoreDetails, summary) => scoreDetails.Hospital.ToString()),
                (() => HasModule(ExpansionModule.Granada),Resource.Id.headerMarket, (scoreDetails, summary) => scoreDetails.Market.ToString()),
                (() => HasModule(ExpansionModule.Granada),Resource.Id.headerPark, (scoreDetails, summary) => scoreDetails.Park.ToString()),
                (() => HasModule(ExpansionModule.Granada),Resource.Id.headerSchool, (scoreDetails, summary) => scoreDetails.School.ToString()),
                (() => HasModule(ExpansionModule.Granada),Resource.Id.headerResidentialArea, (scoreDetails, summary) => scoreDetails.ResidentialArea.ToString()),
                (() => GranadaOption == GranadaOption.With,Resource.Id.headerWallMoat, (scoreDetails, summary) => scoreDetails.WallMoatCombination.ToString()),
            };
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
                || Game.GranadaCompatibleModules.Contains(module));
        }

        public bool HasCaliphsGuideline(CaliphsGuidelinesMission module)
        {
            return Result.CaliphsGuidelines.Contains(module);
        }

        private TableRow CreateHeaderPlayerTableRow(int index, string text)
        {
            TableLayout headerTable = FindViewById<TableLayout>(Resource.Id.headerTable);
            
            TableRow tableRow = new TableRow(this);
            tableRow.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            headerTable.AddView(tableRow, index);

            TextView textView = new TextView(this);
            textView.Text = text;
            LinearLayout.LayoutParams layoutParameters = new TableRow.LayoutParams((int)Resources.GetDimension(Resource.Dimension.game_details_players_header_width), (int)Resources.GetDimension(Resource.Dimension.game_details_cell_height));
            layoutParameters.MarginStart = (int)Resources.GetDimension(Resource.Dimension.game_details_header_rows_gap);
            tableRow.AddView(textView, layoutParameters);

            return tableRow;
        }

        private TableRow CreateDetailsRow(TableLayout parent)
        {
            TableRow tableRow = new TableRow(this);
            tableRow.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            parent.AddView(tableRow);

            //TextView textView = new TextView(this);
            //textView.Text = text;
            //LinearLayout.LayoutParams layoutParameters = new TableRow.LayoutParams((int)Resources.GetDimension(Resource.Dimension.game_details_players_header_width), (int)Resources.GetDimension(Resource.Dimension.game_details_cell_height));
            //layoutParameters.MarginStart = (int)Resources.GetDimension(Resource.Dimension.game_details_header_rows_gap);
            //tableRow.AddView(textView, layoutParameters);

            return tableRow;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            bool showSecondRound = ScoreRound == ScoringRound.ThirdBeforeLeftover || ScoreRound == ScoringRound.Third || ScoreRound == ScoringRound.Finish;
            bool showThirdRound = ScoreRound == ScoringRound.Finish;

            TextView titleDate = FindViewById<TextView>(Resource.Id.titleDate);
            titleDate.Text = $"{Result.StartDateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("es-ES"))} - {(Result.EndDateTime != null ? ((DateTime)Result.EndDateTime).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("es-ES")) : String.Empty)}";

            Button previousResult = FindViewById<Button>(Resource.Id.previousResult);
            Button nextResult = FindViewById<Button>(Resource.Id.nextResult);
            previousResult.SetVisibility(Application.ArchiveResult);
            nextResult.SetVisibility(Application.ArchiveResult);
            previousResult.Enabled = Application.Results.Any(r => r.StartDateTime < Result.StartDateTime);
            nextResult.Enabled = Application.Results.Any(r => r.StartDateTime > Result.StartDateTime);
            previousResult.Click += new EventHandler((object sender, EventArgs e) =>
            {
                Finish();
                Application.ShowResult(Application.Results.Select(r => r.StartDateTime).Where(s => s < Result.StartDateTime).OrderByDescending(s => s).First());
            });
            nextResult.Click += new EventHandler((object sender, EventArgs e) =>
            {
                Finish();
                Application.ShowResult(Application.Results.Select(r => r.StartDateTime).Where(s => s > Result.StartDateTime).OrderBy(s => s).First());
            });

            contentTable = FindViewById<TableLayout>(Resource.Id.contentTable);

            List<List<TableRow>> tableRows = new List<List<TableRow>>();

            int shift = (showSecondRound ? 0 : 1) + (showThirdRound ? 0 : 1);
            for (int i = 0; i < PlayersCount; i++)
            {
                string playerName = GetPlayer(i + 1).Name;

                List<TableRow> rows = new List<TableRow>();
                rows.Add(CreateHeaderPlayerTableRow((i + 2) * 1, playerName));
                if (showSecondRound)
                    rows.Add(CreateHeaderPlayerTableRow((i + 2) * 2, playerName));
                if (showThirdRound)
                    rows.Add(CreateHeaderPlayerTableRow((i + 2) * 3, playerName));
                rows.Add(CreateHeaderPlayerTableRow((i + 2) * (4 - shift) + shift, playerName));

                tableRows.Add(rows);
            }

            //tooltipText tylko od API 25
            TableRow headerRow1 = FindViewById<TableRow>(Resource.Id.headerRow1);
            TableRow headerRow2 = FindViewById<TableRow>(Resource.Id.headerRow2);
            foreach ((TableRow headerRow, UpDown center) in new (TableRow, UpDown)[] { (headerRow1, UpDown.Down), (headerRow2, UpDown.Up) })
            {
                for (int i = 0; i < headerRow.ChildCount; i++)
                    Center(headerRow.GetChildAt(i), center);

                foreach ((Func<bool> condition, int headerRowResourceId, Func<ScoreDetails, bool, string> resultText) in resultConditions)
                    headerRow.FindViewById(headerRowResourceId).SetVisibility(condition.Invoke());
            }

            AddPlayerDetailsRoundBlock(ScoringRound.First);
            if (showSecondRound)
                AddPlayerDetailsRoundBlock(ScoringRound.Second);
            else
                FindViewById<TableRow>(Resource.Id.headerRound2).SetVisibility(false);
            if (showThirdRound)
                AddPlayerDetailsRoundBlock(ScoringRound.Third);
            else
                FindViewById<TableRow>(Resource.Id.headerRound3).SetVisibility(false);
            AddPlayerDetailsRoundBlock(ScoringRound.Finish);
        }

        private void AddPlayerDetailsRoundBlock(ScoringRound round)
        {
            TableRow emptyrow = new TableRow(this);
            emptyrow.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            TextView textView = new TextView(this);
            LinearLayout.LayoutParams layoutParameters = new TableRow.LayoutParams((int)Resources.GetDimension(Resource.Dimension.game_details_cell_width), (int)Resources.GetDimension(Resource.Dimension.game_details_cell_height));
            emptyrow.AddView(textView, layoutParameters);

            contentTable.AddView(emptyrow, contentTable.ChildCount - 1);
            contentTable.RequestLayout();

            for (int i = 0; i < PlayersCount; i++)
                AddPlayerDetailsRow(GetPlayer(i + 1).GetScoreDetails(round), round == ScoringRound.Finish);
        }

        private void AddPlayerDetailsRow(ScoreDetails scoreDetails, bool summary)
        {
            TableRow row = new TableRow(this);
            row.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);

            TableRow headerRow = FindViewById<TableRow>(Resource.Id.headerRow1);
            for (int i = 0; i < headerRow.ChildCount; i++)
            {
                View headerElement = headerRow.GetChildAt(i);
                (Func<bool> condition, int headerRowResourceId, Func<ScoreDetails, bool, string> resultText) resultCondition = resultConditions.Single(r => r.headerRowResourceId == headerElement.Id);

                if (resultCondition.condition.Invoke())
                {
                    TextView textView = new TextView(this);
                    textView.Text = resultCondition.resultText.Invoke(scoreDetails, summary);
                    if (summary && headerElement.Id == Resource.Id.headerSum)
                        textView.Typeface = Android.Graphics.Typeface.DefaultBold;
                    LinearLayout.LayoutParams layoutParameters = new TableRow.LayoutParams((int)Resources.GetDimension(Resource.Dimension.game_details_cell_width), (int)Resources.GetDimension(Resource.Dimension.game_details_cell_height));
                    layoutParameters.MarginStart = (int)Resources.GetDimension(Resource.Dimension.game_details_cell_gap);
                    row.AddView(textView, layoutParameters);
                }
            }

            contentTable.AddView(row, contentTable.ChildCount - 1);
            contentTable.RequestLayout();
        }

        protected void Center(View view, UpDown upDown)
        {
            if (view is TextView textView)
                textView.Gravity = upDown == UpDown.Up ? GravityFlags.Top : GravityFlags.Bottom;
            else if (view is LinearLayout linearLayout)
                linearLayout.SetGravity(upDown == UpDown.Up ? GravityFlags.Top : GravityFlags.Bottom);
            else if (view is ImageView imageView)
                imageView.SetScaleType(upDown == UpDown.Up ? ImageView.ScaleType.FitStart : ImageView.ScaleType.FitEnd);
        }
    }
}