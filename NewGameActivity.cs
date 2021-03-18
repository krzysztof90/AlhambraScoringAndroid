using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.UI;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlhambraScoringAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class NewGameActivity : BaseActivity
    {
        //TODO layout

        ExpandableListView expandableListView;

        protected override int getContentView()
        {
            return Resource.Layout.activity_newgame;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Dictionary<string, List<(ExpansionModule, string)>> extensions = new Dictionary<string, List<(ExpansionModule, string)>>()
            {
                ["DESIGNER EXPANSION MODULES"] = new List<(ExpansionModule, string)>() {
                    ((  ExpansionModule.DesignerNewBuildingGrounds, "New Building Grounds")),
            (( ExpansionModule.DesignerMajorConstructionProjects, "Major Construction Projects")),
            (( ExpansionModule.DesignerPalaceStaff, "Palace Staff")),
            (( ExpansionModule.DesignerOrchards, "Orchards")),
            (( ExpansionModule.DesignerTravellingCraftsmen, "Travelling Craftsmen")),
            (( ExpansionModule.DesignerBathhouses, "Bathhouses")),
            (( ExpansionModule.DesignerWishingWell, "Wishing Well")),
            (( ExpansionModule.DesignerFreshColors, "Fresh Colors")),
            (( ExpansionModule.DesignerPalaceDesigners, "Palace Designers")),
            (( ExpansionModule.DesignerAlhambraZoo, "Alhambra Zoo")),
            (( ExpansionModule.DesignerGatesWithoutEnd, "Gates without End")),
            (( ExpansionModule.DesignerBuildingsOfPower, "Buildings of Power")),
            ((ExpansionModule.DesignerExtensions, "Extensions" )),
            (( ExpansionModule.DesignerHandymen, "Handymen")),
            (( ExpansionModule.DesignerPersonalBuildingMarket, "Personal Building Market")),
            (( ExpansionModule.DesignerTreasures, "Treasures")),
            (( ExpansionModule.DesignerCaliphsGuidelines, "Caliph’s Guidelines"))}
            };
            //TODO caliphs chose

            expandableListView = FindViewById<ExpandableListView>(Resource.Id.expandableListView);
            //ExpandListCheckBoxAdapter adapter = new ExpandListCheckBoxAdapter(this, extensions.ToDictionary(d => d.Key, d => d.Value.Select(l => l.Item2).ToList()));
            ExpandListCheckBoxAdapter<ExpansionModule> adapter = new ExpandListCheckBoxAdapter<ExpansionModule>(this, extensions);
            expandableListView.SetAdapter(adapter);

            Button startButton = FindViewById<Button>(Resource.Id.startButton);
            startButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                application().gameApplyModules(adapter.SelectedList);
            });
        }
    }
}