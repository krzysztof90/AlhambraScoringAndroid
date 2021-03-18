using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlhambraScoringAndroid.UI
{
    public class ExpandListCheckBoxAdapter<T> : BaseExpandableListAdapter
    {
        Context mContext;
        private readonly Dictionary<string, Dictionary<T, (string, bool)>> mExpandableListDetail;
        public IEnumerable<T> SelectedList { get
            {
                //return mExpandableListDetail.SelectMany(d => d.Value.Select(d2 => d2.Key));
                return mExpandableListDetail.SelectMany(d => d.Value).Where(d=>d.Value.Item2).Select(d=>d.Key);
            }
        }

        public ExpandListCheckBoxAdapter(Context context, Dictionary<string, List<(T, string)>> expandableListDetail)
        {
            mContext = context;

            mExpandableListDetail = expandableListDetail.ToDictionary(d => d.Key, d => d.Value.ToDictionary(l => l.Item1, l => (l.Item2, false)));
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return mExpandableListDetail.ElementAt(groupPosition).Value.ElementAt(childPosition).Value.Item1;
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return mExpandableListDetail.ElementAt(groupPosition).Value.Count;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return mExpandableListDetail.ElementAt(groupPosition).Key;
        }

        public override int GroupCount => mExpandableListDetail.Count;

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override bool HasStableIds => false;

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            TextView textView;
            if (convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)mContext.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Resource.Layout.list_extensionmodule_group, null);
                textView = convertView.FindViewById<TextView>(Resource.Id.listTitle);
                textView.SetTypeface(null, TypefaceStyle.Bold);
                convertView.Tag = textView;
            }
            else
            {
                textView = (TextView)convertView.Tag;
            }

            textView.Text = (string)GetGroup(groupPosition);

            return convertView;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            CheckBox checkBox;
            if (convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)mContext.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Resource.Layout.list_extensionmodule_item, null);
                checkBox = convertView.FindViewById<CheckBox>(Resource.Id.expandedListItem);
                convertView.Tag = checkBox;
            }
            else
            {
                checkBox = (CheckBox)convertView.Tag;
            }

            checkBox.Text = (string)GetChild(groupPosition, childPosition);

            //if (selectedChildCheckBoxStates.size() <= groupPosition)
            //{
            //    ArrayList<Boolean> childState = new ArrayList<>();
            //    for (int i = 0; i < mGroupList.get(groupPosition).size(); i++)
            //    {
            //        if (childState.size() > childPosition)
            //            childState.add(childPosition, false);
            //        else
            //            childState.add(false);
            //    }
            //    if (selectedChildCheckBoxStates.size() > groupPosition)
            //    {
            //        selectedChildCheckBoxStates.add(groupPosition, childState);
            //    }
            //    else
            //        selectedChildCheckBoxStates.add(childState);
            //}
            //else
            {
        //TODO czy potrzebne?
                checkBox.Checked = mExpandableListDetail.ElementAt(groupPosition).Value.ElementAt(childPosition).Value.Item2;
            }

            checkBox.Click += new EventHandler((object sender, EventArgs e) =>
            {
            T key = mExpandableListDetail.ElementAt(groupPosition).Value.ElementAt(childPosition).Key;

                mExpandableListDetail.ElementAt(groupPosition).Value[key] =(mExpandableListDetail.ElementAt(groupPosition).Value[key].Item1, !mExpandableListDetail.ElementAt(groupPosition).Value[key].Item2);
            });

            return convertView;

        }
    }
}