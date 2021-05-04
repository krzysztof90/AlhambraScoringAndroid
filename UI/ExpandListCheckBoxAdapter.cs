using AlhambraScoringAndroid.Attributes;
using AlhambraScoringAndroid.Tools;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI
{
    //TODO usuwa zaznaczenia przy przekręcaniu ekranu
    public class ExpandListCheckBoxAdapter<EnumType> : BaseExpandableListAdapter where EnumType : struct, IConvertible, IComparable, IFormattable
    {
        private readonly bool MultipleChoice;
        private readonly Context Context;
        private readonly Dictionary<string, Dictionary<EnumType, bool>> ListMultipleWithSelections;
        private readonly Dictionary<string, (List<EnumType>, EnumType?)> ListSingleWithSelection;
        private readonly List<View[]> childViews;

        public IEnumerable<EnumType> SelectedListMultiple => ListMultipleWithSelections.SelectMany(d => d.Value).Where(d => d.Value).Select(d => d.Key);
        public Dictionary<string, EnumType> SelectedListSingle => ListSingleWithSelection.ToDictionary(d => d.Key, d => (EnumType)d.Value.Item2);

        public ExpandListCheckBoxAdapter(Context context, Dictionary<string, List<EnumType>> expandableListDetail, bool multipleChoice)
        {
            MultipleChoice = multipleChoice;
            Context = context;

            ListMultipleWithSelections = expandableListDetail.ToDictionary(d => d.Key, d => d.Value.ToDictionary(l => l, l => false));
            ListSingleWithSelection = expandableListDetail.ToDictionary(d => d.Key, d => (d.Value, default(EnumType?)));
            for (int i = 0; i < ListSingleWithSelection.Count; i++)
                ListSingleWithSelection[(string)GetGroup(i)] = (ListSingleWithSelection.ElementAt(0).Value.Item1, ListSingleWithSelection.ElementAt(0).Value.Item1.First());

            if (MultipleChoice && expandableListDetail.SelectMany(d => d.Value).Distinct().Count() != expandableListDetail.SelectMany(d => d.Value).Count())
                throw new ArgumentException();
            if (!MultipleChoice && expandableListDetail.Any(d => d.Value.Distinct().Count() != d.Value.Count()))
                throw new ArgumentException();

            childViews = expandableListDetail.Select(d => new View[d.Value.Count]).ToList();
        }

        public EnumType GetChildObject(int groupPosition, int childPosition)
        {
            if (MultipleChoice)
                return ListMultipleWithSelections.ElementAt(groupPosition).Value.ElementAt(childPosition).Key;
            else
                return ListSingleWithSelection.ElementAt(groupPosition).Value.Item1[childPosition];
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return GetChildObject(groupPosition, childPosition).GetEnumDescription(Context.Resources);
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            if (MultipleChoice)
                return ListMultipleWithSelections.ElementAt(groupPosition).Value.Count;
            else
                return ListSingleWithSelection.ElementAt(groupPosition).Value.Item1.Count;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            if (MultipleChoice)
                return ListMultipleWithSelections.ElementAt(groupPosition).Key;
            else
                return ListSingleWithSelection.ElementAt(groupPosition).Key;
        }

        public override int GroupCount => MultipleChoice ? ListMultipleWithSelections.Count : ListSingleWithSelection.Count;

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
            //TODO szerokość imageView z argumentów

            if (MultipleChoice)
            {
                CheckBox checkBox;
                ImageView imageView;
                if (convertView == null)
                {
                    LayoutInflater inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
                    convertView = inflater.Inflate(Resource.Layout.list_extensionmodule_item, null);
                    checkBox = convertView.FindViewById<CheckBox>(Resource.Id.expandedListItem);
                    imageView = convertView.FindViewById<ImageView>(Resource.Id.expandedListItemImage);

                    checkBox.Click += new EventHandler((object sender, EventArgs e) =>
                    {
                        ExpandListCheckBoxPosition groupAndChild = (ExpandListCheckBoxPosition)checkBox.Tag;

                        EnumType key = GetChildObject(groupAndChild.GroupPosition, groupAndChild.ChildPosition);

                        ListMultipleWithSelections.ElementAt(groupAndChild.GroupPosition).Value[key] = !ListMultipleWithSelections.ElementAt(groupAndChild.GroupPosition).Value[key];
                    });

                    convertView.Tag = Android.Util.Pair.Create(checkBox, imageView);
                }
                else
                {
                    Android.Util.Pair pair = (Android.Util.Pair)convertView.Tag;
                    checkBox = (CheckBox)pair.First;
                    imageView = (ImageView)pair.Second;
                }

                EnumType keyLocal = GetChildObject(groupPosition, childPosition);

                checkBox.Text = (string)GetChild(groupPosition, childPosition);

                checkBox.Tag = new ExpandListCheckBoxPosition(groupPosition, childPosition);
                // shit android
                checkBox.Checked = ListMultipleWithSelections.ElementAt(groupPosition).Value.ElementAt(childPosition).Value;

                ImageAttribute imageAttribute = keyLocal.GetEnumAttribute<EnumType, ImageAttribute>();
                if (imageAttribute != null)
                    imageView.SetImageBitmap(imageAttribute.Image(Context.Resources));
            }
            else
            {
                RadioButton radioButton;
                ImageView imageView;
                if (convertView == null)
                {
                    LayoutInflater inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
                    convertView = inflater.Inflate(Resource.Layout.list_extensionmodule_item_radiobuttons, null);
                    radioButton = convertView.FindViewById<RadioButton>(Resource.Id.expandedListItem);
                    imageView = convertView.FindViewById<ImageView>(Resource.Id.expandedListItemImage);

                    radioButton.Click += new EventHandler((object sender, EventArgs e) =>
                    {
                        ExpandListCheckBoxPosition groupAndChild = (ExpandListCheckBoxPosition)radioButton.Tag;

                        EnumType key = GetChildObject(groupAndChild.GroupPosition, groupAndChild.ChildPosition);

                        ListSingleWithSelection[(string)GetGroup(groupAndChild.GroupPosition)] = (ListSingleWithSelection.ElementAt(groupAndChild.GroupPosition).Value.Item1, key);

                        for (int j = 0; j < this.GetChildrenCount(groupAndChild.GroupPosition); j++)
                        {
                            if (j != groupAndChild.ChildPosition)
                            {
                                View listItem = childViews[groupAndChild.GroupPosition][j];
                                Android.Util.Pair pair2 = (Android.Util.Pair)listItem.Tag;
                                RadioButton radioButton3 = (RadioButton)pair2.First;

                                radioButton3.Checked = false;
                            }
                        }
                    });

                    convertView.Tag = Android.Util.Pair.Create(radioButton, imageView);
                }
                else
                {
                    Android.Util.Pair pair = (Android.Util.Pair)convertView.Tag;
                    radioButton = (RadioButton)pair.First;
                    imageView = (ImageView)pair.Second;
                }

                EnumType keyLocal = GetChildObject(groupPosition, childPosition);

                radioButton.Text = (string)GetChild(groupPosition, childPosition);

                radioButton.Tag = new ExpandListCheckBoxPosition(groupPosition, childPosition);
                radioButton.Checked = keyLocal.CompareTo(ListSingleWithSelection.ElementAt(groupPosition).Value.Item2) == 0;

                ImageAttribute imageAttribute = keyLocal.GetEnumAttribute<EnumType, ImageAttribute>();
                if (imageAttribute != null)
                    imageView.SetImageBitmap(imageAttribute.Image(Context.Resources));
            }
            childViews[groupPosition][childPosition] = convertView;
            return convertView;
        }
    }
}