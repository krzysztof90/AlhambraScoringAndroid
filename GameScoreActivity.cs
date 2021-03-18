using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.UI;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.ViewPager.Widget;
using Google.Android.Material.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlhambraScoringAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class GameScoreActivity : BaseActivity
    {
        PlayersScoreSectionsPagerAdapter sectionsPagerAdapter;

        protected override int getContentView()
        {
            return Resource.Layout.activity_gamescore;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            sectionsPagerAdapter = new PlayersScoreSectionsPagerAdapter(this, SupportFragmentManager);
            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.view_pager);
            viewPager.Adapter = sectionsPagerAdapter;
            TabLayout tabs = FindViewById<TabLayout>(Resource.Id.tabs);
            tabs.SetupWithViewPager(viewPager);

            viewPager.PageSelected += new EventHandler<ViewPager.PageSelectedEventArgs>((object sender, ViewPager.PageSelectedEventArgs e) =>
            {
                if (e.Position < getGame().getPlayersCount)
                {
                    sectionsPagerAdapter.restoreValues(e.Position);
                }
            });
        }

        public void submit()
        {
            if (getGame().ScoreRound != ScoringRound.ThirdBeforeLeftover)
            {
                application().submitScore(this, sectionsPagerAdapter.AllPlayerScoreFragments);
            }
            else
            {
                application().submitScoreBeforeAssignLeftoverBuildings(this, sectionsPagerAdapter.AllPlayerScoreBeforeAssignLeftoverFragments);
            }
        }
    }

}