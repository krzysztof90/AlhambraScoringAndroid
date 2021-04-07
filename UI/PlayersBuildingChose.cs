using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.UI
{
    public class PlayersBuildingChose : LinearLayout
    {
        public readonly List<ScoreLineNumberView> PlayersPanels;

        public PlayersBuildingChose(Context context) : base(context)
        {
            LayoutInflater layoutInflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            layoutInflater.Inflate(Resource.Layout.view_players_buildings_chose, this);

            PlayersPanels = new List<ScoreLineNumberView>()
            {
                FindViewById<ScoreLineNumberView>(Resource.Id.player1HighestPurchasePriceNumericUpDown),
                FindViewById<ScoreLineNumberView>(Resource.Id.player2HighestPurchasePriceNumericUpDown),
                FindViewById<ScoreLineNumberView>(Resource.Id.player3HighestPurchasePriceNumericUpDown),
                FindViewById<ScoreLineNumberView>(Resource.Id.player4HighestPurchasePriceNumericUpDown),
                FindViewById<ScoreLineNumberView>(Resource.Id.player5HighestPurchasePriceNumericUpDown),
                FindViewById<ScoreLineNumberView>(Resource.Id.player6HighestPurchasePriceNumericUpDown),
            };
        }

        public PlayersBuildingChose(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public PlayersBuildingChose(Context context, string title, int min, int max, Dictionary<int, string> playersToShow) : this(context)
        {
            TextView titleView = FindViewById<TextView>(Resource.Id.title);
            titleView.Text = title;

            for (int i = 0; i < 6; i++)
            {
                if (playersToShow.ContainsKey(i + 1))
                {
                    PlayersPanels[i].SetLabel(playersToShow[i + 1]);
                    PlayersPanels[i].SetNumberRange(min, max);
                }
                else
                    PlayersPanels[i].Visibility = ViewStates.Gone;
            }
        }
    }
}