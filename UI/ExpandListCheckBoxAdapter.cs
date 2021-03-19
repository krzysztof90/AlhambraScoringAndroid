﻿using AlhambraScoringAndroid.Tools;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI
{
    public class ExpandListCheckBoxAdapter<EnumType> : BaseExpandableListAdapter where EnumType : struct, IConvertible, IComparable, IFormattable
    {
        private readonly Context Context;
        private readonly Dictionary<string, Dictionary<EnumType, bool>> listWithSelections;

        public IEnumerable<EnumType> SelectedList => listWithSelections.SelectMany(d => d.Value).Where(d => d.Value).Select(d => d.Key);

        public ExpandListCheckBoxAdapter(Context context, Dictionary<string, List<EnumType>> expandableListDetail)
        {
            Context = context;

            //TODO unique validation
            listWithSelections = expandableListDetail.ToDictionary(d => d.Key, d => d.Value.ToDictionary(l => l, l => false));
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return listWithSelections.ElementAt(groupPosition).Value.ElementAt(childPosition).Key.GetEnumDescription();
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return listWithSelections.ElementAt(groupPosition).Value.Count;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return listWithSelections.ElementAt(groupPosition).Key;
        }

        public override int GroupCount => listWithSelections.Count;

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
                LayoutInflater inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
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
                LayoutInflater inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Resource.Layout.list_extensionmodule_item, null);
                checkBox = convertView.FindViewById<CheckBox>(Resource.Id.expandedListItem);

                checkBox.Click += new EventHandler((object sender, EventArgs e) =>
                {
                    ExpandListCheckBoxPosition groupAndChild = (ExpandListCheckBoxPosition)checkBox.Tag;

                    EnumType key = listWithSelections.ElementAt(groupAndChild.GroupPosition).Value.ElementAt(groupAndChild.ChildPosition).Key;

                    listWithSelections.ElementAt(groupAndChild.GroupPosition).Value[key] = !listWithSelections.ElementAt(groupAndChild.GroupPosition).Value[key];
                });

                convertView.Tag = checkBox;
            }
            else
            {
                checkBox = (CheckBox)convertView.Tag;
            }

            checkBox.Text = (string)GetChild(groupPosition, childPosition);

            checkBox.Tag = new ExpandListCheckBoxPosition(groupPosition, childPosition);
            /// shit android
            checkBox.Checked = listWithSelections.ElementAt(groupPosition).Value.ElementAt(childPosition).Value;

            return convertView;
        }
    }
}