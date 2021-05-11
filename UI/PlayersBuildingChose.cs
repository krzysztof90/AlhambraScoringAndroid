using AlhambraScoringAndroid.Options;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidBase.UI;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.UI
{
    public class PlayersBuildingChose : LinearLayout
    {
        public readonly List<ControlNumberView> PlayersPanels;

        public PlayersBuildingChose(Context context) : base(context)
        {
            LayoutInflater layoutInflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            layoutInflater.Inflate(Resource.Layout.view_players_buildings_chose, this);

            PlayersPanels = new List<ControlNumberView>()
            {
                FindViewById<ControlNumberView>(Resource.Id.player1HighestPurchasePriceNumericUpDown),
                FindViewById<ControlNumberView>(Resource.Id.player2HighestPurchasePriceNumericUpDown),
                FindViewById<ControlNumberView>(Resource.Id.player3HighestPurchasePriceNumericUpDown),
                FindViewById<ControlNumberView>(Resource.Id.player4HighestPurchasePriceNumericUpDown),
                FindViewById<ControlNumberView>(Resource.Id.player5HighestPurchasePriceNumericUpDown),
                FindViewById<ControlNumberView>(Resource.Id.player6HighestPurchasePriceNumericUpDown),
            };
        }

        public PlayersBuildingChose(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public PlayersBuildingChose(Context context, string title, int min, int max, List<int> exceptNumbers, Dictionary<int, string> playersToShow, SettingsType? validationSettingsType) : this(context)
        {
            TextView titleView = FindViewById<TextView>(Resource.Id.title);
            titleView.Text = title;

            for (int i = 0; i < 6; i++)
            {
                if (playersToShow.ContainsKey(i + 1))
                {
                    PlayersPanels[i].SetLabel(playersToShow[i + 1]);
                    PlayersPanels[i].SetNumberRange(min, max, validationSettingsType);
                    PlayersPanels[i].SetNumberExceptions(exceptNumbers, validationSettingsType);
                }
                else
                    PlayersPanels[i].Visibility = ViewStates.Gone;
            }
        }
    }
}