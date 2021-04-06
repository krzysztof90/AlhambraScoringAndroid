using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;

namespace AlhambraScoringAndroid.UI
{
    public class ExpandableListViewExtension : ExpandableListView
    {
        private static EventHandler<ExpandableListView.GroupClickEventArgs> HoldSizeAction = new EventHandler<ExpandableListView.GroupClickEventArgs>((object sender, ExpandableListView.GroupClickEventArgs e) => SetExpandableListViewHeight((ExpandableListView)sender, e));

        private bool holdSize;

        public bool HoldSize
        {
            get
            {
                return holdSize;
            }
            set
            {
                holdSize = value;

                if (holdSize)
                {
                    GroupClick += HoldSizeAction;
                    SetExpandableListViewHeight(this, null);
                }
                else
                    GroupClick -= HoldSizeAction;
            }
        }

        public void Expand()
        {
            IExpandableListAdapter listAdapter = (IExpandableListAdapter)ExpandableListAdapter;

            for (int i = 0; i < listAdapter.GroupCount; i++)
                this.ExpandGroup(i);

            if (HoldSize)
                SetExpandableListViewHeight(this, null);
        }

        public ExpandableListViewExtension(Context context) : base(context)
        {
        }

        public ExpandableListViewExtension(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        //shit android. Problem with multiple ExpandableListView
        public static void SetExpandableListViewHeight(ExpandableListView listView, ExpandableListView.GroupClickEventArgs e)
        {
            int group = e != null ? e.GroupPosition : -1;
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

                        //totalHeight += listItem.MeasuredHeight;
                        //TODO bez tego obejścia za duży rozmiar w GameModulesDetailsChoseActivity
                        totalHeight += 150;
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

            if (e != null)
                e.Handled = false;
        }
    }
}