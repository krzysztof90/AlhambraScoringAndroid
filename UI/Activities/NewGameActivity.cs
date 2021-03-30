using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.UI;
using Android.App;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "Nowa gra", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class NewGameActivity : BaseActivity
    {
        private ExpandableListView expandableListView;

        protected override int ContentView => Resource.Layout.activity_new_game;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Dictionary<string, List<ExpansionModule>> extensions = new Dictionary<string, List<ExpansionModule>>()
            {
                ["EXPANSION MODULES"] = new List<ExpansionModule>()
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
                ["QUEENIE EXPANSION MODULES"] = new List<ExpansionModule>()
                {
                    ExpansionModule.QueenieMagicalBuildings,
                    ExpansionModule.QueenieMedina, 
                },
                ["DESIGNER EXPANSION MODULES"] = new List<ExpansionModule>() {
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
                    ExpansionModule.FanCaliphsGuidelines},
            };
            //TODO GRANADA radio buttony: No Granada (domyślnie zaznaczone), Alhambra + Granada (dostępność dodatków), Only Granada (dostępność dodatków;  bez designers i fan (? - wielkość kaeflków w family box))
            //TODO caliphs chose

            expandableListView = FindViewById<ExpandableListView>(Resource.Id.expandableListView);
            ExpandListCheckBoxAdapter<ExpansionModule> adapter = new ExpandListCheckBoxAdapter<ExpansionModule>(this, extensions);
            expandableListView.SetAdapter(adapter);

            Button startButton = FindViewById<Button>(Resource.Id.startButton);
            startButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                Application.GameApplyModules(adapter.SelectedList);
            });
        }
    }
}