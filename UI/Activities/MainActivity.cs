using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "Alhambra scoring", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_main;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler((object sender, UnhandledExceptionEventArgs args) =>
            {
                Exception exception = (Exception)args.ExceptionObject;
                //TODO
            });

            base.OnCreate(savedInstanceState);

            Application.LoadResults();

            Button newGameButton = FindViewById<Button>(Resource.Id.newGameButton);
            newGameButton.Click += new System.EventHandler((object sender, System.EventArgs e) =>
            {
                Application.NewGamePrompt(this);
            });
            Button showHistoryButton = FindViewById<Button>(Resource.Id.showHistoryButton);
            showHistoryButton.Click += new System.EventHandler((object sender, System.EventArgs e) =>
            {
                Application.ShowHistory(this);
            });
        }
    }
}
