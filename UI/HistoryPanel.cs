using AlhambraScoringAndroid.UI.Activities;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AlhambraScoringAndroid.UI
{
    public class HistoryPanel : LinearLayout
    {
        public readonly DateTime StartDateTime;
        public readonly DateTime EndDateTime;

        private readonly TextView textViewDateTime;

        public HistoryPanel(Context context) : base(context)
        {
            ResultsHistoryActivity activity = (ResultsHistoryActivity)Context;

            LayoutInflater layoutInflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            //TODO LinearLayout layout_height na wrap_content
            //TODO TextView gravity top
            View view = layoutInflater.Inflate(Resource.Layout.view_game_result, this);

            textViewDateTime = FindViewById<TextView>(Resource.Id.dateTimeTextView);

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

        public HistoryPanel(Context context, DateTime start, DateTime end, List<string> playerNames) : this(context)
        {
            StartDateTime = start;
            EndDateTime = end;
            textViewDateTime.Text = $"{start.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("es-ES"))} - {end.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("es-ES"))}{Environment.NewLine}{String.Join(", ", playerNames)}";
        }
    }
}