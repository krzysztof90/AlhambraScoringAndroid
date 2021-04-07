using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using AndroidX.ViewPager.Widget;
using Google.Android.Material.Tabs;
using System;
using static Google.Android.Material.Tabs.TabLayout;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "Punktacja", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class GameScoreActivity : BaseActivity
    {
        private PlayersScoreSectionsPagerAdapter sectionsPagerAdapter;

        protected override int ContentView => Resource.Layout.activity_game_score;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //TODO błąd aplikacji przy przekręcaniu ekranu

            base.OnCreate(savedInstanceState);

            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.view_pager);
            sectionsPagerAdapter = new PlayersScoreSectionsPagerAdapter(this, SupportFragmentManager, viewPager);
            //viewPager.Adapter = sectionsPagerAdapter;
            TabLayout tabs = FindViewById<TabLayout>(Resource.Id.tabs);
            tabs.SetupWithViewPager(viewPager);

            LinearLayout view = (LinearLayout)tabs.GetChildAt(0);
            for (int i = 0; i < view.ChildCount; i++)
            {
                TabView tabView = (TabView)view.GetChildAt(i);
                for (int j = 0; j < tabView.ChildCount; j++)
                    if (tabView.GetChildAt(j) is TextView textView)
                        textView.SetTextColor(Color.White);
            }

            viewPager.PageSelected += new EventHandler<ViewPager.PageSelectedEventArgs>((object sender, ViewPager.PageSelectedEventArgs e) =>
            {
                if (e.Position < Game.PlayersCount)
                {
                    sectionsPagerAdapter.RestoreValues(e.Position);
                }
            });
        }

        public override void OnBackPressed()
        {
            new AlertDialog.Builder(this)
                .SetTitle("Closing Activity")
                .SetMessage("Are you sure you want to close this activity?")
                .SetPositiveButton("Yes", new DialogInterfaceOnClickListener((IDialogInterface dialog, int which) => base.OnBackPressed()))
                .SetNegativeButton("No", new DialogInterfaceOnClickListener(null))
                .Show();
        }

        public void Submit()
        {
            if (Game.ScoreRound != ScoringRound.ThirdBeforeLeftover && sectionsPagerAdapter.ValidateAllPlayerScoreFragments())
                Application.SubmitScore(this, sectionsPagerAdapter.AllPlayerScoreFragments);
            else if (Game.ScoreRound == ScoringRound.ThirdBeforeLeftover && sectionsPagerAdapter.ValidateAllPlayerScoreBeforeAssignLeftoverFragments())
                Application.SubmitScoreBeforeAssignLeftoverBuildings(this, sectionsPagerAdapter.AllPlayerScoreBeforeAssignLeftoverFragments);
        }
    }
}