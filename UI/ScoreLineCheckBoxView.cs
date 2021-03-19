using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;

namespace AlhambraScoringAndroid.UI
{
    public class ScoreLineCheckBoxView : ScoreLineViewBase<bool>
    {
        private CheckBox scoreLineCheckBox;

        public ScoreLineCheckBoxView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        protected override int ResourceLayout => Resource.Layout.view_game_score_line_checkbox;

        protected override void CreateControls()
        {
            scoreLineCheckBox = FindViewById<CheckBox>(Resource.Id.scoreLineCheckBox);
        }

        protected override void SetControlsProperties()
        {
            scoreLineCheckBox.Click += new EventHandler((object sender, EventArgs e) =>
            {
                AssignStoredValue();
                OnValueChange?.Invoke();
            });
        }

        protected override void SetLabel(string label)
        {
            scoreLineCheckBox.Text = label;
        }

        protected override void SetValue(bool value)
        {
            scoreLineCheckBox.Checked = value;
        }

        protected override bool GetValue()
        {
            if (this.Visibility == ViewStates.Gone)
                return DefaultValue;
            return scoreLineCheckBox.Checked;
        }
    }
}