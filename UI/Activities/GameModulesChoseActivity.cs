using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using AndroidBase.UI;
using System;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "@string/modules_chose", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class GameModulesChoseActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_game_modules_chose;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            string granadaName = Resources.GetString(Resource.String.granada);
            string alcazabaName = Resources.GetString(Resource.String.alcazaba);

            base.OnCreate(savedInstanceState);

            Dictionary<string, List<ExpansionModule>> extensions = new Dictionary<string, List<ExpansionModule>>()
            {
                [Resources.GetString(Resource.String.expansion_modules)] = new List<ExpansionModule>()
                {
                    ExpansionModule.ExpansionViziersFavour,
                    ExpansionModule.ExpansionCurrencyExchangeCards,
                    ExpansionModule.ExpansionBonusCards,
                    ExpansionModule.ExpansionSquares,
                    ExpansionModule.ExpansionCityGates,
                    ExpansionModule.ExpansionDiamonds,
                    ExpansionModule.ExpansionCharacters,
                    ExpansionModule.ExpansionCamps,
                    ExpansionModule.ExpansionCityWalls,
                    ExpansionModule.ExpansionThieves,
                    ExpansionModule.ExpansionChange,
                    ExpansionModule.ExpansionStreetTrader,
                    ExpansionModule.ExpansionTreasureChamber,
                    ExpansionModule.ExpansionMasterBuilders,
                    ExpansionModule.ExpansionInvaders,
                    ExpansionModule.ExpansionBazaars,
                    ExpansionModule.ExpansionNewScoreCards,
                    ExpansionModule.ExpansionPowerOfSultan,
                    ExpansionModule.ExpansionCaravanserai,
                    ExpansionModule.ExpansionArtOfTheMoors,
                    ExpansionModule.ExpansionFalconers,
                    ExpansionModule.ExpansionWatchtowers,
                    ExpansionModule.ExpansionBuildingSites,
                    ExpansionModule.ExpansionExchangeCertificates,
                },
                [Resources.GetString(Resource.String.queenie_expansion_modules)] = new List<ExpansionModule>()
                {
                    ExpansionModule.QueenieMagicalBuildings,
                    ExpansionModule.QueenieMedina,
                },
                [Resources.GetString(Resource.String.designer_expansion_modules)] = new List<ExpansionModule>() {
                    ExpansionModule.DesignerNewBuildingGrounds,
                    ExpansionModule.DesignerMajorConstructionProjects,
                    ExpansionModule.DesignerPalaceStaff,
                    ExpansionModule.DesignerOrchards,
                    ExpansionModule.DesignerTravellingCraftsmen,
                    ExpansionModule.DesignerBathhouses,
                    ExpansionModule.DesignerWishingWell,
                    ExpansionModule.DesignerFreshColors,
                    ExpansionModule.DesignerPalaceDesigners,
                    ExpansionModule.DesignerAlhambraZoo,
                    ExpansionModule.DesignerGatesWithoutEnd,
                    ExpansionModule.DesignerBuildingsOfPower,
                    ExpansionModule.DesignerExtensions,
                    ExpansionModule.DesignerHandymen,
                    ExpansionModule.FanPersonalBuildingMarket,
                    ExpansionModule.FanTreasures,
                    ExpansionModule.FanCaliphsGuidelines,
                },
                [Resources.GetString(Resource.String.new_market_modules)] = new List<ExpansionModule>()
                {
                    ExpansionModule.NewMarket,
                },
                [Resources.GetString(Resource.String.red_palace_modules)] = new List<ExpansionModule>()
                {
                    ExpansionModule.RedPalaceLandTiles,
                    ExpansionModule.ExpansionNewScoreCards,
                    ExpansionModule.QueenieMedina,
                    ExpansionModule.ExpansionBazaars,
                    ExpansionModule.ExpansionCamps,
                },
            };
            Dictionary<string, List<GranadaOption>> granadaOptions = new Dictionary<string, List<GranadaOption>>()
            {
                [granadaName] = new List<GranadaOption>()
                {
                    GranadaOption.Without,
                    GranadaOption.With,
                    GranadaOption.Alone,
                },
            };
            Dictionary<string, List<AlcazabaOption>> alcazabaOptions = new Dictionary<string, List<AlcazabaOption>>()
            {
                [alcazabaName] = new List<AlcazabaOption>()
                {
                    AlcazabaOption.WithoutTile,
                    AlcazabaOption.WithTile,
                },
            };

            ExpandableListViewExtension expandableListView = FindViewById<ExpandableListViewExtension>(Resource.Id.expandableListView);
            ExpandListCheckBoxAdapterMultiple<ExpansionModule> adapter = new ExpandListCheckBoxAdapterMultiple<ExpansionModule>(this, extensions);
            expandableListView.SetAdapter(adapter);
            expandableListView.HoldSize = true;

            ExpandableListViewExtension expandableListView2 = FindViewById<ExpandableListViewExtension>(Resource.Id.expandableListView2);
            ExpandListCheckBoxAdapterSingle<GranadaOption> adapter2 = new ExpandListCheckBoxAdapterSingle<GranadaOption>(this, granadaOptions);
            expandableListView2.SetAdapter(adapter2);
            expandableListView2.HoldSize = true;

            ExpandableListViewExtension expandableListView3 = FindViewById<ExpandableListViewExtension>(Resource.Id.expandableListView3);
            ExpandListCheckBoxAdapterSingle<AlcazabaOption> adapter3 = new ExpandListCheckBoxAdapterSingle<AlcazabaOption>(this, alcazabaOptions);
            expandableListView3.SetAdapter(adapter3);
            expandableListView3.HoldSize = true;

            Button startButton = FindViewById<Button>(Resource.Id.startButton);
            startButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                Application.GameApplyModules(adapter.SelectedList, adapter2.SelectedList[granadaName], adapter3.SelectedList[alcazabaName]);
            });
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            Game.Reset(true);
        }
    }
}