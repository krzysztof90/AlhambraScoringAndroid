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
        private ExpandableListView newScoreCardsExpandableListView;
        //private ExpandableListView newScoreCards2ExpandableListView;
        //private ExpandableListView newScoreCards3ExpandableListView;
        private ExpandableListView caliphsGuidelinesExpandableListView;

        protected override int ContentView => Resource.Layout.activity_modules_details;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Dictionary<string, List<NewScoreCard>> newScoreCards = new Dictionary<string, List<NewScoreCard>>()
            {
                ["1st soring score cards"] = new List<NewScoreCard>()
                {
                    NewScoreCard.Card1, NewScoreCard.Card2,NewScoreCard.Card3,NewScoreCard.Card4,NewScoreCard.Card5,NewScoreCard.Card6,NewScoreCard.Card7,NewScoreCard.Card8,NewScoreCard.Card9,NewScoreCard.Card10,NewScoreCard.Card11,NewScoreCard.Card12,NewScoreCard.Card13,NewScoreCard.Card14,NewScoreCard.Card15,NewScoreCard.Card16,NewScoreCard.Card17,NewScoreCard.Card18
                },
                ["2nd soring score cards"] = new List<NewScoreCard>()
                {
                    NewScoreCard.Card1, NewScoreCard.Card2,NewScoreCard.Card3,NewScoreCard.Card4,NewScoreCard.Card5,NewScoreCard.Card6,NewScoreCard.Card7,NewScoreCard.Card8,NewScoreCard.Card9,NewScoreCard.Card10,NewScoreCard.Card11,NewScoreCard.Card12,NewScoreCard.Card13,NewScoreCard.Card14,NewScoreCard.Card15,NewScoreCard.Card16,NewScoreCard.Card17,NewScoreCard.Card18
                },
                ["3rd soring score cards"] = new List<NewScoreCard>()
                {
                    NewScoreCard.Card1, NewScoreCard.Card2,NewScoreCard.Card3,NewScoreCard.Card4,NewScoreCard.Card5,NewScoreCard.Card6,NewScoreCard.Card7,NewScoreCard.Card8,NewScoreCard.Card9,NewScoreCard.Card10,NewScoreCard.Card11,NewScoreCard.Card12,NewScoreCard.Card13,NewScoreCard.Card14,NewScoreCard.Card15,NewScoreCard.Card16,NewScoreCard.Card17,NewScoreCard.Card18
                },
            };

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

            ExpandListCheckBoxAdapter<NewScoreCard> adapterNewScoreCards = null;
            ExpandListCheckBoxAdapter<CaliphsGuidelinesMission> adapterCaliphsGuidelines = null;
            if (Game.HasModule(ExpansionModule.ExpansionNewScoreCards))
            {
                newScoreCardsExpandableListView = FindViewById<ExpandableListView>(Resource.Id.listView1);
                adapterNewScoreCards = new ExpandListCheckBoxAdapter<NewScoreCard>(this, newScoreCards, false, false);
                newScoreCardsExpandableListView.SetAdapter(adapterNewScoreCards);
                newScoreCardsExpandableListView.HoldSize();
                //newScoreCards2ExpandableListView = FindViewById<ExpandableListView>(Resource.Id.listView2);
                //ExpandListCheckBoxAdapter<NewScoreCard> adapterNewScoreCards2 = new ExpandListCheckBoxAdapter<NewScoreCard>(this, newScoreCards, false, false);
                //newScoreCards2ExpandableListView.SetAdapter(adapterNewScoreCards2);
                //newScoreCards2ExpandableListView.HoldSize();
                //newScoreCards3ExpandableListView = FindViewById<ExpandableListView>(Resource.Id.listView3);
                //ExpandListCheckBoxAdapter<NewScoreCard> adapterNewScoreCards3 = new ExpandListCheckBoxAdapter<NewScoreCard>(this, newScoreCards, false, false);
                //newScoreCards3ExpandableListView.SetAdapter(adapterNewScoreCards3);
                //newScoreCards3ExpandableListView.HoldSize();
            }

            if (Game.HasModule(ExpansionModule.FanCaliphsGuidelines))
            {
                caliphsGuidelinesExpandableListView = FindViewById<ExpandableListView>(Resource.Id.listView2);
                adapterCaliphsGuidelines = new ExpandListCheckBoxAdapter<CaliphsGuidelinesMission>(this, missions, true, false);
                caliphsGuidelinesExpandableListView.SetAdapter(adapterCaliphsGuidelines);
                caliphsGuidelinesExpandableListView.HoldSize();
            }

            Button startButton = FindViewById<Button>(Resource.Id.startButton);
            startButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                Application.GameApplyModulesDetails(adapterCaliphsGuidelines?.SelectedListMultiple, adapterNewScoreCards?.SelectedListSingle.Select(d => d.Value).ToList());
            });
        }
    }
}
