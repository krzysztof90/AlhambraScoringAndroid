using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.UI;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using AndroidX.ViewPager.Widget;
using Google.Android.Material.Tabs;
using System;
using static Google.Android.Material.Tabs.TabLayout;

namespace AlhambraScoringAndroid.Activities
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

            sectionsPagerAdapter = new PlayersScoreSectionsPagerAdapter(this, SupportFragmentManager);
            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.view_pager);
            viewPager.Adapter = sectionsPagerAdapter;
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

        public void Submit()
        {
            if (Game.ScoreRound != ScoringRound.ThirdBeforeLeftover)
                Application.SubmitScore(this, sectionsPagerAdapter.AllPlayerScoreFragments);
            else
                Application.SubmitScoreBeforeAssignLeftoverBuildings(this, sectionsPagerAdapter.AllPlayerScoreBeforeAssignLeftoverFragments);
        }
    }
}