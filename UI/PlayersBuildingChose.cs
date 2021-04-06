using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI
{
    public class PlayersBuildingChose : LinearLayout
    {
        public List<ScoreLineNumberView> playersPanels { get; private set; }

        public PlayersBuildingChose(Context context) : base(context)
        {
            LayoutInflater layoutInflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            View view = layoutInflater.Inflate(Resource.Layout.view_players_buildings_chose, this);

            playersPanels = new List<ScoreLineNumberView>()
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
            TextView titleView= FindViewById<TextView>(Resource.Id.title);
            titleView.Text = title;

            for (int i = 0; i < 6; i++)
            {
                if (playersToShow.ContainsKey(i + 1))
                {
                    playersPanels[i].SetLabel(playersToShow[i + 1]);
                    playersPanels[i].SetNumberRange(min, max);
                }
                else
                    playersPanels[i].Visibility = ViewStates.Gone;
            }
        }
    }
}