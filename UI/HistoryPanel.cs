using AlhambraScoringAndroid.UI.Activities;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Globalization;

namespace AlhambraScoringAndroid.UI
{
    public class HistoryPanel : LinearLayout
    {
        public readonly DateTime StartDateTime;
        public readonly DateTime EndDateTime;

        private TextView textViewDateTime;

        public HistoryPanel(Context context) : base(context)
        {
            ResultsHistoryActivity activity = (ResultsHistoryActivity)Context;

            LayoutInflater layoutInflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            View view = layoutInflater.Inflate(Resource.Layout.view_game_result, this);

            textViewDateTime = FindViewById<TextView>(Resource.Id.textViewDateTime);

            Button showResultButton = FindViewById<Button>(Resource.Id.showResultButton);
            showResultButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                activity.ShowResult(this);
            });
            Button removeResultButton = FindViewById<Button>(Resource.Id.removeResultButton);
            removeResultButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                activity.RemoveResult(this);
            });
        }

        public HistoryPanel(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public HistoryPanel(Context context, DateTime start, DateTime end) : this(context)
        {
            StartDateTime = start;
            EndDateTime = end;
            textViewDateTime.Text = $"{start.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("es-ES"))} - {end.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("es-ES"))}";
        }
    }
}