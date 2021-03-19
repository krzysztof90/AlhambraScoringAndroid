using Android.Content;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System;

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

        protected override void SetLabel(string label)
        {
            textView.Text = label;
        }

        protected override void SetValue(int value)
        {
            editText.Text = value.ToString();
        }

        protected override int GetValue()
        {
            string text = editText.Text;
            if (text.Length == 0 || this.Visibility == ViewStates.Gone)
                return DefaultValue;
            return Int32.Parse(text);
        }

        public void SetNumberRange(int min, int max)
        {
            editText.SetFilters(new IInputFilter[] { new MinMaxFilter(Context, min, max) });
        }
    }
}