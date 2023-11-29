using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using AndroidBase.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "@string/modules_details_chose", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class GameModulesDetailsChoseActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_modules_details;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //TODO na każdej zakładce przycisk next przechodzący do następnej zakładki

            Dictionary<string, List<NewScoreCard>> newScoreCards = new Dictionary<string, List<NewScoreCard>>()
            {
                [Resources.GetString(Resource.String.scoring_score_card_1)] = new List<NewScoreCard>()
                {
                    NewScoreCard.Card1, NewScoreCard.Card2, NewScoreCard.Card3, NewScoreCard.Card4, NewScoreCard.Card5, NewScoreCard.Card6, NewScoreCard.Card7, NewScoreCard.Card8, NewScoreCard.Card9, NewScoreCard.Card10, NewScoreCard.Card11, NewScoreCard.Card12, NewScoreCard.Card13, NewScoreCard.Card14, NewScoreCard.Card15, NewScoreCard.Card16, NewScoreCard.Card17, NewScoreCard.Card18
                },
                [Resources.GetString(Resource.String.scoring_score_card_2)] = new List<NewScoreCard>()
                {
                    NewScoreCard.Card1, NewScoreCard.Card2, NewScoreCard.Card3, NewScoreCard.Card4, NewScoreCard.Card5, NewScoreCard.Card6, NewScoreCard.Card7, NewScoreCard.Card8, NewScoreCard.Card9, NewScoreCard.Card10, NewScoreCard.Card11, NewScoreCard.Card12, NewScoreCard.Card13, NewScoreCard.Card14, NewScoreCard.Card15, NewScoreCard.Card16, NewScoreCard.Card17, NewScoreCard.Card18
                },
                [Resources.GetString(Resource.String.scoring_score_card_3)] = new List<NewScoreCard>()
                {
                    NewScoreCard.Card1, NewScoreCard.Card2, NewScoreCard.Card3, NewScoreCard.Card4, NewScoreCard.Card5, NewScoreCard.Card6, NewScoreCard.Card7, NewScoreCard.Card8, NewScoreCard.Card9, NewScoreCard.Card10, NewScoreCard.Card11, NewScoreCard.Card12, NewScoreCard.Card13, NewScoreCard.Card14, NewScoreCard.Card15, NewScoreCard.Card16, NewScoreCard.Card17, NewScoreCard.Card18
                },
            };

            Dictionary<string, List<CaliphsGuidelinesMission>> missions = new Dictionary<string, List<CaliphsGuidelinesMission>>()
            {
                [Resources.GetString(Resource.String.caliphs_guidelines_missions)] = new List<CaliphsGuidelinesMission>() {
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

            ExpandListCheckBoxAdapterSingle<NewScoreCard> adapterNewScoreCards = null;
            ExpandListCheckBoxAdapterMultiple<CaliphsGuidelinesMission> adapterCaliphsGuidelines = null;

            if (Game.HasModule(ExpansionModule.ExpansionNewScoreCards))
            {
                ExpandableListViewExtension newScoreCardsExpandableListView = FindViewById<ExpandableListViewExtension>(Resource.Id.listView1);
                adapterNewScoreCards = new ExpandListCheckBoxAdapterSingle<NewScoreCard>(this, newScoreCards);
                newScoreCardsExpandableListView.SetAdapter(adapterNewScoreCards);
                newScoreCardsExpandableListView.HoldSize = true;
                newScoreCardsExpandableListView.Expand();
            }

            if (Game.HasModule(ExpansionModule.FanCaliphsGuidelines))
            {
                ExpandableListViewExtension caliphsGuidelinesExpandableListView = FindViewById<ExpandableListViewExtension>(Resource.Id.listView2);
                adapterCaliphsGuidelines = new ExpandListCheckBoxAdapterMultiple<CaliphsGuidelinesMission>(this, missions);
                caliphsGuidelinesExpandableListView.SetAdapter(adapterCaliphsGuidelines);
                caliphsGuidelinesExpandableListView.HoldSize = true;
                caliphsGuidelinesExpandableListView.Expand();
            }

            Button startButton = FindViewById<Button>(Resource.Id.startButton);
            startButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                Application.GameApplyModulesDetails(adapterCaliphsGuidelines?.SelectedList, adapterNewScoreCards?.SelectedList.Select(d => d.Value).ToList());
            });
        }
    }
}
