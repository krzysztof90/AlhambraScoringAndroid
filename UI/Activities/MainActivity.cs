using Android.App;
using Android.OS;
using Android.Widget;
using System;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "@string/main_title", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : BaseActivity
    {
        protected override bool ShowBackButton => false;
        protected override int ContentView => Resource.Layout.activity_main;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Application.LoadResults();

            base.OnCreate(savedInstanceState);

            Button newGameButton = FindViewById<Button>(Resource.Id.newGameButton);
            newGameButton.Click += new EventHandler((object sender, EventArgs e) =>
            { 
                Application.NewGamePrompt(this);
            });
            Button showHistoryButton = FindViewById<Button>(Resource.Id.showHistoryButton);
            showHistoryButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                Application.ShowHistory(this);
            });
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }
    }
}
