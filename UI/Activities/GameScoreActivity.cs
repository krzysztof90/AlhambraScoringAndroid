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
    [Activity(Label = "@string/scoring", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
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
        }

        public override void OnBackPressed()
        {
            new AlertDialog.Builder(this)
                .SetTitle(Resources.GetString(Resource.String.activity_ending))
                .SetMessage(Resources.GetString(Resource.String.activity_ending_continue_question))
                .SetPositiveButton(Resources.GetString(Resource.String.yes), new DialogInterfaceOnClickListener((IDialogInterface dialog, int which) => base.OnBackPressed()))
                .SetNegativeButton(Resources.GetString(Resource.String.no), new DialogInterfaceOnClickListener(null))
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