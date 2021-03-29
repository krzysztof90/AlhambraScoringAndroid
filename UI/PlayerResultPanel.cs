using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.UI.Activities;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;

namespace AlhambraScoringAndroid.UI
{
    public class PlayerResultPanel : LinearLayout
    {
        private int PlayerNumber;

        private TextView textViewName;
        private TextView textViewScore;
        private Button addPoint1Button;
        private Button addPoint2Button;

        public PlayerResultPanel(Context context) : base(context)
        {
        }

        public PlayerResultPanel(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public void Initialize( int playerNumber)
        {
            GameInProgressActivity gameActivity = (GameInProgressActivity)Context;

            PlayerNumber = playerNumber;

            LayoutInflater layoutInflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            View view = layoutInflater.Inflate(Resource.Layout.view_player_result, this);

            textViewName = FindViewById<TextView>(Resource.Id.textViewName);
            textViewScore = FindViewById<TextView>(Resource.Id.textViewScore);

            textViewName.Text = gameActivity.Game.GetPlayer(playerNumber).Name;

            addPoint1Button = FindViewById<Button>(Resource.Id.point1Button);
            addPoint1Button.Click += new EventHandler((object sender, EventArgs e) =>
            {
                gameActivity.AddPoints(PlayerNumber, 1);
            });
            addPoint2Button = FindViewById<Button>(Resource.Id.point2Button);
            addPoint2Button.Click += new EventHandler((object sender, EventArgs e) =>
            {
                gameActivity.AddPoints(PlayerNumber, 2);
            });

            if (gameActivity.Game.GetPlayer(playerNumber).Dirk
            || !(gameActivity.Game.HasModule(ExpansionModule.DesignerPalaceDesigners) || gameActivity.Game.HasModule(ExpansionModule.DesignerGatesWithoutEnd)))
            {
                addPoint1Button.Visibility = ViewStates.Gone;
                addPoint2Button.Visibility = ViewStates.Gone;
            }
        }

        public void SetScore(int score)
        {
            textViewScore.Text = score.ToString();
        }

        public void ShowPointButtons(bool show)
        {
            addPoint1Button.Visibility = show? ViewStates.Visible : ViewStates.Gone;
            addPoint2Button.Visibility = show? ViewStates.Visible : ViewStates.Gone;
        }
    }
}
