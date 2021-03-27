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

            //TODO usunąć
            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            Application.NewGame();
            List<ExpansionModule> modules = new List<ExpansionModule>()
                                      {
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
            Application.GameApplyModules(modules);
            List<string> players = new List<string>();
            players.Add("E");
            players.Add("K");
            players.Add("3");
            players.Add("4");
            Application.GameStart(players);
            while (Application.Game.ScoreRound != ScoringRound.Second)
                Application.Game.SetNextRound();
        }
    }
}
