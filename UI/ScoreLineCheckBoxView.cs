using Android.Content;
using Android.Content.Res;
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

        protected override void SetLabelAndColor(string label, ColorStateList color)
        {
            scoreLineCheckBox.Text = label;
            if (color != null)
            {
                scoreLineCheckBox.SetTextColor(color);
                if (color.DefaultColor == -658699)
                    scoreLineCheckBox.SetShadowLayer(1, 1, 1, Android.Graphics.Color.Black);
            }
        }

        protected override void SetValue(bool? value)
        {
            scoreLineCheckBox.Checked = (value!=false);
        }

        protected override bool? GetValue()
        {
            if (this.Visibility == ViewStates.Gone)
                //return DefaultValue;
                return null;
            return scoreLineCheckBox.Checked;
        }
    }
}