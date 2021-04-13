using Android.Content;
using Android.Content.Res;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI
{
    public class ScoreLineNumberView : ScoreLineViewBase<int>
    {
        private TextView textView;
        private EditText editText;

        public ScoreLineNumberView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        protected override int ResourceLayout => Resource.Layout.view_game_score_line_number;

        protected override void CreateControls()
        {
            textView = FindViewById<TextView>(Resource.Id.scoreLineLabel);
            editText = FindViewById<EditText>(Resource.Id.scoreLineNumber);
        }

        protected override void SetControlsProperties()
        {
            editText.TextChanged += new EventHandler<Android.Text.TextChangedEventArgs>((object sender, Android.Text.TextChangedEventArgs e) =>
            {
                bool restore = false;
                bool handleTextChanged = false;
                foreach (StackTraceElement element in Thread.CurrentThread().GetStackTrace())
                {
                    if (element.ClassName == "android.widget.TextView" && element.MethodName == "onRestoreInstanceState")
                    {
                        restore = true;
                        break;
                    }
                    if (element.ClassName == "android.widget.TextView" && element.MethodName == "handleTextChanged")
                    {
                        handleTextChanged = true;
                    }
                }

                if (!restore)
                {
                    AssignStoredValue();
                    if (!handleTextChanged)
                        editText.SetSelection(editText.Text.Length);
                }

                OnValueChange?.Invoke();
            });
        }

        public override void SetLabel(string label)
        {
            textView.Text = label;
        }

        public override void SetColor(ColorStateList color)
        {
            if (color != null)
            {
                textView.SetTextColor(color);
                if (color.DefaultColor == -658699 || color.DefaultColor == -1)
                    textView.SetShadowLayer(1, 1, 1, Android.Graphics.Color.Black);
            }
        }

        protected override void SetValue(int? value)
        {
            if (value == null)
                editText.Text = System.String.Empty;
            else
                editText.Text = value.ToString();
        }

        protected override int? GetValue()
        {
            string text = editText.Text;
            if (text.Length == 0 || this.Visibility == ViewStates.Gone)
                return null;
            return Int32.Parse(text);
        }

        public void SetNumberRange(int min, int max)
        {
            List<IInputFilter> filters = editText.GetFilters().ToList();
            filters.Add(new MinMaxFilter(Context, min, max));
            editText.SetFilters(filters.ToArray());
        }

        public void SetNumberExceptions(List<int> numbers)
        {
            List<IInputFilter> filters = editText.GetFilters().ToList();
            filters.Add(new NumberFilter(Context, numbers));
            editText.SetFilters(filters.ToArray());
        }

        public bool ValidateNumberRange()
        {
            if (editText != null)
                foreach (IInputFilter inputFilter in editText.GetFilters())
                    //TODO do textView.Text dołączone nowe property ValidationMessage z przypisaną nazwą gracza
                    if (inputFilter is MinMaxFilter minMaxFilter && !minMaxFilter.ValidateNumberRange(editText.Text, true, DefaultValue, textView.Text))
                        return false;
                    else if (inputFilter is NumberFilter numberFilter && !numberFilter.ValidateNumberRange(editText.Text, true, DefaultValue, textView.Text))
                        return false;
            return true;
        }
    }
}