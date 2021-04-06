using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "Alhambra scoring", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_main;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Application.LoadResults();

            //TODO buttons for new game and history

            //TODO usunąć
            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            Application.NewGame();
            List<ExpansionModule> modules = new List<ExpansionModule>()
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
                    ExpansionModule.QueenieMagicalBuildings,
                    ExpansionModule.QueenieMedina,
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
            };
            List<CaliphsGuidelinesMission> modulesDetails = new List<CaliphsGuidelinesMission>()
            {
                    CaliphsGuidelinesMission.Mission1,
                    CaliphsGuidelinesMission.Mission2,
                    CaliphsGuidelinesMission.Mission3,
                    CaliphsGuidelinesMission.Mission4,
                    CaliphsGuidelinesMission.Mission5,
                    CaliphsGuidelinesMission.Mission6,
                    CaliphsGuidelinesMission.Mission7,
                    CaliphsGuidelinesMission.Mission8,
                    CaliphsGuidelinesMission.Mission9,
            };
            //Application.GameApplyModules(modules, GranadaOption.Without);
            //Application.GameApplyModules(modules, GranadaOption.Alone);
            Application.GameApplyModules(modules, GranadaOption.With);
            //Application.GameApplyModulesDetails(modulesDetails, new List<NewScoreCard>() { NewScoreCard.Card1, NewScoreCard.Card2, NewScoreCard.Card3 });
            Application.GameApplyModulesDetails(modulesDetails, null);
            List<string> players = new List<string>();
            players.Add("E");
            players.Add("K");
            players.Add("3");
            players.Add("4");
            Application.GameStart(players);
            while (Game.ScoreRound != ScoringRound.Second)
                Game.SetNextRound();
        }
    }
}
