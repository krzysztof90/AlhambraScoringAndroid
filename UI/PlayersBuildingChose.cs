using AlhambraScoringAndroid.Options;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidBase.Tools;
using AndroidBase.UI;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI
{
    public class PlayersBuildingChose : LinearLayout
    {
        public readonly List<ControlNumberView> PlayersPanels;

        public PlayersBuildingChose(Context context) : base(context)
        {
            LayoutInflater layoutInflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            layoutInflater.Inflate(Resource.Layout.view_players_buildings_chose, this);

            PlayersPanels = new List<int>()
            {
                Resource.Id.player1HighestPurchasePriceNumericUpDown,
                Resource.Id.player2HighestPurchasePriceNumericUpDown,
                Resource.Id.player3HighestPurchasePriceNumericUpDown,
                Resource.Id.player4HighestPurchasePriceNumericUpDown,
                Resource.Id.player5HighestPurchasePriceNumericUpDown,
                Resource.Id.player6HighestPurchasePriceNumericUpDown
            }.Select(r => FindViewById<ControlNumberView>(r)).ToList();
        }

        public PlayersBuildingChose(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public PlayersBuildingChose(Context context, string title, int min, int max, List<int> exceptNumbers, Dictionary<int, (string playerName, int? value)> playersToShow, SettingsType? validationSettingsType) : this(context)
        {
            TextView titleView = FindViewById<TextView>(Resource.Id.title);
            titleView.Text = title;

            for (int i = 0; i < 6; i++)
            {
                if (playersToShow.ContainsKey(i + 1))
                {
                    PlayersPanels[i].SetLabel(playersToShow[i + 1].playerName);
                    PlayersPanels[i].SetNumberRange(min, max, validationSettingsType);
                    PlayersPanels[i].SetNumberExceptions(exceptNumbers, validationSettingsType);
                    if (playersToShow[i + 1].value != null)
                        PlayersPanels[i].Value = (int)playersToShow[i + 1].value;
                }
                else
                    PlayersPanels[i].SetVisibility(false);
            }
        }
    }
}