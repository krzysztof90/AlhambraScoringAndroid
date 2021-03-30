using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "Szczegóły dodatków", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class GameModulesDetailsChoseActivity : BaseActivity
    {
        private ExpandableListView listView;

        protected override int ContentView => Resource.Layout.activity_modules_details;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Dictionary<string, List<CaliphsGuidelinesMission>> missions = new Dictionary<string, List<CaliphsGuidelinesMission>>()
            {
                ["Caliph’s Guidelines missions"] = new List<CaliphsGuidelinesMission>() {
                    CaliphsGuidelinesMission.Mission1,
                    CaliphsGuidelinesMission.Mission2,
                    CaliphsGuidelinesMission.Mission3,
                    CaliphsGuidelinesMission.Mission4,
                    CaliphsGuidelinesMission.Mission5,
                    CaliphsGuidelinesMission.Mission6,
                    CaliphsGuidelinesMission.Mission7,
                    CaliphsGuidelinesMission.Mission8,
                    CaliphsGuidelinesMission.Mission9,
                },
            };

            listView = FindViewById<ExpandableListView>(Resource.Id.listView);

            ExpandListCheckBoxAdapter<CaliphsGuidelinesMission> adapter = new ExpandListCheckBoxAdapter<CaliphsGuidelinesMission>(this, missions, false);
            listView.SetAdapter(adapter);

            Button startButton = FindViewById<Button>(Resource.Id.startButton);
            startButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                Application.GameApplyModulesDetails(adapter.SelectedList);
            });
        }
    }
}