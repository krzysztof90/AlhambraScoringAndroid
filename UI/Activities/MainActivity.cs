using Android.App;
using Android.OS;
using Android.Widget;
using System;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "@string/main_title", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_main;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((object sender, UnhandledExceptionEventArgs args) =>
            {
                Exception exception = (Exception)args.ExceptionObject;
                //TODO obsłużyć
            });

            Application.LoadResults();

            base.OnCreate(savedInstanceState);

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
