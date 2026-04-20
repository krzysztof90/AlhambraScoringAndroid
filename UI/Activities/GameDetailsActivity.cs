using AlhambraBase;
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

        protected override int ContentView => Resource.Layout.activity_game_details;

        private TableLayout contentTable;

        private readonly List<(Func<bool> condition, int headerRowResourceId, Func<ScoreDetails, bool, string> resultText)> resultConditions;

        public GameDetailsActivity()
        {
            resultConditions = new List<(Func<bool> condition, int headerRowResourceId, Func<ScoreDetails, bool, string> resultText)>()
            {
                (() => true, Resource.Id.headerSum, (scoreDetails, summary) => scoreDetails.Sum.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.RedPalaceLandTiles), Resource.Id.headerStartingPoints, (scoreDetails, summary) => !summary ? String.Empty : scoreDetails.StartingPoints.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.DesignerPalaceDesigners) || HasModule(AlhambraBase.ExpansionModule.DesignerGatesWithoutEnd)|| HasModule(AlhambraBase.ExpansionModule.RedPalaceLandTiles), Resource.Id.headerImmediatelyPoints, (scoreDetails, summary) => !summary ? String.Empty : scoreDetails.ImmediatelyPoints.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.DesignerPalaceDesigners), Resource.Id.headerImmediatelyPointsPalaceDesigners, null),
                (() => HasModule(AlhambraBase.ExpansionModule.DesignerGatesWithoutEnd), Resource.Id.headerImmediatelyPointsGatesWithoutEnd, null),
                (() => HasModule(AlhambraBase.ExpansionModule.RedPalaceLandTiles), Resource.Id.headerImmediatelyPointsRedPalace, null),
                (() => GranadaOption != GranadaOption.Alone, Resource.Id.headerWalls, (scoreDetails, summary) => scoreDetails.WallLength.ToString()),
                (() => GranadaOption != GranadaOption.Alone, Resource.Id.headerPavilion, (scoreDetails, summary) => scoreDetails.Pavilion.ToString()),
                (() => GranadaOption != GranadaOption.Alone, Resource.Id.headerSeraglio, (scoreDetails, summary) => scoreDetails.Seraglio.ToString()),
                (() => GranadaOption != GranadaOption.Alone, Resource.Id.headerArcades, (scoreDetails, summary) => scoreDetails.Arcades.ToString()),
                (() => GranadaOption != GranadaOption.Alone, Resource.Id.headerChambers, (scoreDetails, summary) => scoreDetails.Chambers.ToString()),
                (() => GranadaOption != GranadaOption.Alone, Resource.Id.headerGarden, (scoreDetails, summary) => scoreDetails.Garden.ToString()),
                (() => GranadaOption != GranadaOption.Alone, Resource.Id.headerTower, (scoreDetails, summary) => scoreDetails.Tower.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.ExpansionBonusCards) || HasModule(AlhambraBase.ExpansionModule.ExpansionSquares) || HasModule(AlhambraBase.ExpansionModule.ExpansionCharacters) || HasModule(AlhambraBase.ExpansionModule.DesignerExtensions) || HasModule(AlhambraBase.ExpansionModule.DesignerGatesWithoutEnd), Resource.Id.headerBonuses, (scoreDetails, summary) => $"(+{scoreDetails.BuildingsBonuses})"),
                (() => HasModule(AlhambraBase.ExpansionModule.ExpansionBonusCards), Resource.Id.headerBonusesBonusCards, null),
                (() => HasModule(AlhambraBase.ExpansionModule.ExpansionSquares), Resource.Id.headerBonusesSquares, null),
                (() => HasModule(AlhambraBase.ExpansionModule.DesignerExtensions), Resource.Id.headerBonusesExtensions, null),
                (() => HasModule(AlhambraBase.ExpansionModule.DesignerGatesWithoutEnd), Resource.Id.headerBonusesGatesWithoutEnd, null),
                (() => HasModule(AlhambraBase.ExpansionModule.ExpansionCharacters), Resource.Id.headerBonusesTheWiseMan, null),
                (() => HasModule(AlhambraBase.ExpansionModule.ExpansionCharacters), Resource.Id.headerTheCityWatch, (scoreDetails, summary) => scoreDetails.TheCityWatch.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.ExpansionCamps), Resource.Id.headerCamps, (scoreDetails, summary) => scoreDetails.Camps.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.ExpansionStreetTrader), Resource.Id.headerStreetTraders, (scoreDetails, summary) => scoreDetails.StreetTraders.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.ExpansionTreasureChamber), Resource.Id.headerTreasureChamber, (scoreDetails, summary) => scoreDetails.TreasureChamber.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.ExpansionInvaders), Resource.Id.headerInvaders, (scoreDetails, summary) => $"-{scoreDetails.Invaders}"),
                (() => HasModule(AlhambraBase.ExpansionModule.ExpansionBazaars) && ScoreRound == ScoringRound.Finish, Resource.Id.headerBazaars, (scoreDetails, summary) => scoreDetails.Bazaars.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.ExpansionArtOfTheMoors), Resource.Id.headerArtOfTheMoors, (scoreDetails, summary) => scoreDetails.ArtOfTheMoors.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.ExpansionFalconers), Resource.Id.headerFalconers, (scoreDetails, summary) => scoreDetails.Falconers.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.ExpansionWatchtowers), Resource.Id.headerWatchtowers, (scoreDetails, summary) => scoreDetails.Watchtowers.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.QueenieMedina), Resource.Id.headerMedina, (scoreDetails, summary) => $"-{scoreDetails.Medina}"),
                (() => HasModule(AlhambraBase.ExpansionModule.DesignerPalaceStaff), Resource.Id.headerBuildingsWithoutServantTile, (scoreDetails, summary) => $"-{scoreDetails.BuildingsWithoutServantTile}"),
                (() => HasModule(AlhambraBase.ExpansionModule.DesignerOrchards) && ScoreRound == ScoringRound.Finish, Resource.Id.headerOrchards, (scoreDetails, summary) => scoreDetails.Orchards.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.DesignerBathhouses), Resource.Id.headerBathhouses, (scoreDetails, summary) => scoreDetails.Bathhouses.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.DesignerWishingWell), Resource.Id.headerWishingWells, (scoreDetails, summary) => scoreDetails.WishingWells.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.DesignerFreshColors), Resource.Id.headerCompletedProjects, (scoreDetails, summary) => scoreDetails.CompletedProjects.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.DesignerAlhambraZoo), Resource.Id.headerAnimals, (scoreDetails, summary) => scoreDetails.Animals.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.DesignerBuildingsOfPower), Resource.Id.headerBlackDices, (scoreDetails, summary) => scoreDetails.BlackDices.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.DesignerHandymen), Resource.Id.headerHandymen, (scoreDetails, summary) => scoreDetails.Handymen.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.FanTreasures) && ScoreRound == ScoringRound.Finish, Resource.Id.headerTreasures, (scoreDetails, summary) => scoreDetails.Treasures.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission1) && ScoreRound == ScoringRound.Finish, Resource.Id.headerMission1, (scoreDetails, summary) => scoreDetails.Mission1.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission2) && ScoreRound == ScoringRound.Finish, Resource.Id.headerMission2, (scoreDetails, summary) => scoreDetails.Mission2.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission3) && ScoreRound == ScoringRound.Finish, Resource.Id.headerMission3, (scoreDetails, summary) => scoreDetails.Mission3.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission4) && ScoreRound == ScoringRound.Finish, Resource.Id.headerMission4, (scoreDetails, summary) => scoreDetails.Mission4.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission5) && ScoreRound == ScoringRound.Finish, Resource.Id.headerMission5, (scoreDetails, summary) => scoreDetails.Mission5.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission6) && ScoreRound == ScoringRound.Finish, Resource.Id.headerMission6, (scoreDetails, summary) => scoreDetails.Mission6.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission7) && ScoreRound == ScoringRound.Finish, Resource.Id.headerMission7, (scoreDetails, summary) => scoreDetails.Mission7.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission8) && ScoreRound == ScoringRound.Finish, Resource.Id.headerMission8, (scoreDetails, summary) => scoreDetails.Mission8.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.FanCaliphsGuidelines) && HasCaliphsGuideline(CaliphsGuidelinesMission.Mission9) && ScoreRound == ScoringRound.Finish, Resource.Id.headerMission9, (scoreDetails, summary) => scoreDetails.Mission9.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.RedPalaceLandTiles), Resource.Id.headerGuards, (scoreDetails, summary) => scoreDetails.Guards.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.Granada), Resource.Id.headerMoatLength, (scoreDetails, summary) => scoreDetails.MoatLength.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.Granada), Resource.Id.headerArena, (scoreDetails, summary) => scoreDetails.Arena.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.Granada), Resource.Id.headerBathHouse, (scoreDetails, summary) => scoreDetails.BathHouse.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.Granada), Resource.Id.headerLibrary, (scoreDetails, summary) => scoreDetails.Library.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.Granada), Resource.Id.headerHostel, (scoreDetails, summary) => scoreDetails.Hostel.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.Granada), Resource.Id.headerHospital, (scoreDetails, summary) => scoreDetails.Hospital.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.Granada), Resource.Id.headerMarket, (scoreDetails, summary) => scoreDetails.Market.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.Granada), Resource.Id.headerPark, (scoreDetails, summary) => scoreDetails.Park.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.Granada), Resource.Id.headerSchool, (scoreDetails, summary) => scoreDetails.School.ToString()),
                (() => HasModule(AlhambraBase.ExpansionModule.Granada), Resource.Id.headerResidentialArea, (scoreDetails, summary) => scoreDetails.ResidentialArea.ToString()),
                (() => GranadaOption == GranadaOption.With, Resource.Id.headerWallMoat, (scoreDetails, summary) => scoreDetails.WallMoatCombination.ToString()),
            };
        }

        private ResultPlayerHistory GetPlayer(int playerNumber)
        {
            return Result.Players[playerNumber - 1];
        }

        private bool HasModule(AlhambraBase.ExpansionModule module)
        {
            if (module == AlhambraBase.ExpansionModule.Granada)
                return GranadaOption != GranadaOption.Without;
            return Result.Modules.Contains((GamePlay.ExpansionModule)module) && (GranadaOption != GranadaOption.Alone
                || GameConstants.GranadaCompatibleModules.Contains(module));
        }

        private GranadaOption GranadaOption => Result.GranadaOption;

        private bool HasCaliphsGuideline(CaliphsGuidelinesMission module)
        {
            return Result.CaliphsGuidelines.Contains(module);
        }

        private TableRow CreateHeaderPlayerTableRow(int index, string text)
        {
            TableLayout headerTable = FindViewById<TableLayout>(Resource.Id.headerTable);

            TableRow tableRow = new TableRow(this)
            {
                LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent)
            };
            headerTable.AddView(tableRow, index);

            TextView textView = new TextView(this)
            {
                Text = text
            };
            LinearLayout.LayoutParams layoutParameters = new TableRow.LayoutParams((int)Resources.GetDimension(Resource.Dimension.game_details_players_header_width), (int)Resources.GetDimension(Resource.Dimension.game_details_cell_height))
            {
                MarginStart = (int)Resources.GetDimension(Resource.Dimension.game_details_header_rows_gap)
            };
            tableRow.AddView(textView, layoutParameters);

            return tableRow;
        }

        private TableRow CreateDetailsRow(TableLayout parent)
        {
            TableRow tableRow = new TableRow(this)
            {
                LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent)
            };
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
            bool showThirdRound = (ScoreRound == ScoringRound.Third && Game.HasThirdBeforeLeftoverRound) || ScoreRound == ScoringRound.Finish;

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

            int shift = (showSecondRound ? 0 : 1) + (showThirdRound ? 0 : 1);
            for (int i = 0; i < PlayersCount; i++)
            {
                string playerName = GetPlayer(i + 1).Name;

                CreateHeaderPlayerTableRow((i + 2) * 1, playerName);
                if (showSecondRound)
                    CreateHeaderPlayerTableRow((i + 2) * 2, playerName);
                if (showThirdRound)
                    CreateHeaderPlayerTableRow((i + 2) * 3, playerName);
                if (playerName != Player.DirkName)
                    CreateHeaderPlayerTableRow((i + 2) * (4 - shift) + shift, playerName);
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
            TableRow emptyrow = new TableRow(this)
            {
                LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent)
            };
            TextView textView = new TextView(this);
            LinearLayout.LayoutParams layoutParameters = new TableRow.LayoutParams((int)Resources.GetDimension(Resource.Dimension.game_details_cell_width), (int)Resources.GetDimension(Resource.Dimension.game_details_cell_height));
            emptyrow.AddView(textView, layoutParameters);

            contentTable.AddView(emptyrow, contentTable.ChildCount - 1);
            contentTable.RequestLayout();

            for (int i = 0; i < PlayersCount; i++)
                if (!(round == ScoringRound.Finish && GetPlayer(i + 1).Name == Player.DirkName))
                    AddPlayerDetailsRow(GetPlayer(i + 1).GetScoreDetails(round), round == ScoringRound.Finish);
        }

        private void AddPlayerDetailsRow(ScoreDetails scoreDetails, bool summary)
        {
            TableRow row = new TableRow(this)
            {
                LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent)
            };

            TableRow headerRow = FindViewById<TableRow>(Resource.Id.headerRow1);
            for (int i = 0; i < headerRow.ChildCount; i++)
            {
                View headerElement = headerRow.GetChildAt(i);
                (Func<bool> condition, int headerRowResourceId, Func<ScoreDetails, bool, string> resultText) resultCondition = resultConditions.Single(r => r.headerRowResourceId == headerElement.Id);

                if (resultCondition.condition.Invoke())
                {
                    TextView textView = new TextView(this)
                    {
                        Text = resultCondition.resultText.Invoke(scoreDetails, summary)
                    };
                    if (summary && headerElement.Id == Resource.Id.headerSum)
                        textView.Typeface = Android.Graphics.Typeface.DefaultBold;
                    LinearLayout.LayoutParams layoutParameters = new TableRow.LayoutParams((int)Resources.GetDimension(Resource.Dimension.game_details_cell_width), (int)Resources.GetDimension(Resource.Dimension.game_details_cell_height))
                    {
                        MarginStart = (int)Resources.GetDimension(Resource.Dimension.game_details_cell_gap)
                    };
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