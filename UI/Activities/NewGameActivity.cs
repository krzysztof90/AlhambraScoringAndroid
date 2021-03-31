using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "Nowa gra", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class NewGameActivity : BaseActivity
    {
        private ExpandableListView expandableListView;
        private ExpandableListView expandableListView2;

        protected override int ContentView => Resource.Layout.activity_new_game;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Dictionary<string, List<ExpansionModule>> extensions = new Dictionary<string, List<ExpansionModule>>()
            {
                ["EXPANSION MODULES"] = new List<ExpansionModule>()
                {
                    ExpansionModule.ExpansionViziersFavour,
                    ExpansionModule.ExpansionCurrencyExchangeCards,
                    ExpansionModule.ExpansionBonusCards,
                    ExpansionModule.ExpansionSquares,
                    ExpansionModule.ExpansionCityGates,
                    ExpansionModule.ExpansionDiamonds,
                    ExpansionModule.ExpansionCharacters,
                    ExpansionModule.ExpansionCamps,
                    ExpansionModule.ExpansionCityWalls,
                    ExpansionModule.ExpansionThieves,
                    ExpansionModule.ExpansionChange,
                    ExpansionModule.ExpansionStreetTrader,
                    ExpansionModule.ExpansionTreasureChamber,
                    ExpansionModule.ExpansionMasterBuilders,
                    ExpansionModule.ExpansionInvaders,
                    ExpansionModule.ExpansionBazaars,
                    ExpansionModule.ExpansionNewScoreCards,
                    ExpansionModule.ExpansionPowerOfSultan,
                    ExpansionModule.ExpansionCaravanserai,
                    ExpansionModule.ExpansionArtOfTheMoors,
                    ExpansionModule.ExpansionFalconers,
                    ExpansionModule.ExpansionWatchtowers,
                    ExpansionModule.ExpansionBuildingSites,
                    ExpansionModule.ExpansionExchangeCertificates,
                },
                ["QUEENIE EXPANSION MODULES"] = new List<ExpansionModule>()
                {
                    ExpansionModule.QueenieMagicalBuildings,
                    ExpansionModule.QueenieMedina,
                },
                ["DESIGNER EXPANSION MODULES"] = new List<ExpansionModule>() {
                    ExpansionModule.DesignerNewBuildingGrounds,
                    ExpansionModule.DesignerMajorConstructionProjects,
                    ExpansionModule.DesignerPalaceStaff,
                    ExpansionModule.DesignerOrchards,
                    ExpansionModule.DesignerTravellingCraftsmen,
                    ExpansionModule.DesignerBathhouses,
                    ExpansionModule.DesignerWishingWell,
                    ExpansionModule.DesignerFreshColors,
                    ExpansionModule.DesignerPalaceDesigners,
                    ExpansionModule.DesignerAlhambraZoo,
                    ExpansionModule.DesignerGatesWithoutEnd,
                    ExpansionModule.DesignerBuildingsOfPower,
                    ExpansionModule.DesignerExtensions,
                    ExpansionModule.DesignerHandymen,
                    ExpansionModule.FanPersonalBuildingMarket,
                    ExpansionModule.FanTreasures,
                    ExpansionModule.FanCaliphsGuidelines},
            };
            Dictionary<string, List<GranadaOption>> granadaOptions = new Dictionary<string, List<GranadaOption>>()
            {
                ["GRANADA"] = new List<GranadaOption>()
                {
                    GranadaOption.Without,
                    GranadaOption.With,
                    GranadaOption.Alone,
                },
            };
            //TODO GRANADA

            expandableListView = FindViewById<ExpandableListView>(Resource.Id.expandableListView);
            ExpandListCheckBoxAdapter<ExpansionModule> adapter = new ExpandListCheckBoxAdapter<ExpansionModule>(this, extensions, true, true);
            expandableListView.SetAdapter(adapter);
            expandableListView.GroupClick += ExpandableListView_GroupClick;
            setExpandableListViewHeight(expandableListView, -1);

            expandableListView2 = FindViewById<ExpandableListView>(Resource.Id.expandableListView2);
            ExpandListCheckBoxAdapter<GranadaOption> adapter2 = new ExpandListCheckBoxAdapter<GranadaOption>(this, granadaOptions, false, true);
            expandableListView2.SetAdapter(adapter2);
            expandableListView2.GroupClick += ExpandableListView_GroupClick;
            setExpandableListViewHeight(expandableListView2, -1);

            Button startButton = FindViewById<Button>(Resource.Id.startButton);
            startButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                Application.GameApplyModules(adapter.SelectedListMultiple, adapter2.SelectedListSingle["GRANADA"]);
            });
        }

        private void ExpandableListView_GroupClick(object sender, ExpandableListView.GroupClickEventArgs e)
        {
            setExpandableListViewHeight((ExpandableListView)sender, e.GroupPosition);
            e.Handled = false;
        }

        //shit android. Problem with multiple ExpandableListView
        private void setExpandableListViewHeight(ExpandableListView listView, int group)
        {
            IExpandableListAdapter listAdapter = (IExpandableListAdapter)listView.ExpandableListAdapter;
            int totalHeight = 0;
            int desiredWidth = View.MeasureSpec.MakeMeasureSpec(listView.Width, MeasureSpecMode.Exactly);
            for (int i = 0; i < listAdapter.GroupCount; i++)
            {
                View groupItem = listAdapter.GetGroupView(i, false, null, listView);
                groupItem.Measure(desiredWidth, (int)MeasureSpecMode.Unspecified);

                totalHeight += groupItem.MeasuredHeight;

                if (((listView.IsGroupExpanded(i)) && (i != group))
                        || ((!listView.IsGroupExpanded(i)) && (i == group)))
                {
                    for (int j = 0; j < listAdapter.GetChildrenCount(i); j++)
                    {
                        View listItem = listAdapter.GetChildView(i, j, false, null, listView);
                        listItem.Measure(desiredWidth, (int)MeasureSpecMode.Unspecified);

                        totalHeight += listItem.MeasuredHeight;
                    }
                    totalHeight += (listView.DividerHeight * (listAdapter.GetChildrenCount(i) - 1));
                }
            }

            ViewGroup.LayoutParams params2 = listView.LayoutParameters;
            totalHeight += (listView.DividerHeight * (listAdapter.GroupCount - 1));
            //if (height < 10)
            //    height = 200;
            params2.Height = totalHeight;
            listView.LayoutParameters = params2;
            listView.RequestLayout();
        }

    }
}