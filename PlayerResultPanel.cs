using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlhambraScoringAndroid
{
    public class PlayerResultPanel : LinearLayout
    {
        private int PlayerNumber;

        private TextView textViewName;
        private TextView textViewScore;

        public PlayerResultPanel(Context context) : base(context)
        {
        }

        public PlayerResultPanel(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public void initialize(GameInProgressActivity gameActivity, int playerNumber)
        {
            GameInProgressActivity GameActivity = gameActivity;
            PlayerNumber = playerNumber;

            LayoutInflater layoutInflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            View view = layoutInflater.Inflate(Resource.Layout.view_playerresult, this);

            textViewName = FindViewById<TextView>(Resource.Id.textViewName);
            textViewScore = FindViewById<TextView>(Resource.Id.textViewScore);

            textViewName.Text = GameActivity.getGame().getPlayer(playerNumber).Name;

            Button addPoint1Button = FindViewById<Button>(Resource.Id.point1Button);
            addPoint1Button.Click += new EventHandler((object sender, EventArgs e) =>
            {
                GameActivity.addPoints(PlayerNumber, 1);
            });
            Button addPoint2Button = FindViewById<Button>(Resource.Id.point2Button);
            addPoint2Button.Click += new EventHandler((object sender, EventArgs e) =>
            {
                GameActivity.addPoints(PlayerNumber, 2);
            });

            if (GameActivity.getGame().getPlayer(playerNumber).Dirk
            || !(GameActivity.getGame().hasModule(ExpansionModule.DesignerPalaceDesigners) || GameActivity.getGame().hasModule(ExpansionModule.DesignerGatesWithoutEnd)))
            {
                addPoint1Button.Visibility = ViewStates.Gone;
                addPoint2Button.Visibility = ViewStates.Gone;
            }
        }

        public void setScore(int score)
        {
            textViewScore.Text = score.ToString();
        }
    }
}
