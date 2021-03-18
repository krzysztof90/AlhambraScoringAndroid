using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlhambraScoringAndroid.UI
{
    public class ScoreLineNumberView : LinearLayout
    {
        TextView textView;
        EditText editText;

        public int defaultValue;
        private int storedValue;
        public Action changeMethod;

        public void setDefaultValue(int value)
        {
            defaultValue = value;
        }

        public ScoreLineNumberView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Inflate(context, Resource.Layout.view_gamescorelinenumber, this);

            TypedArray typedArray = context.ObtainStyledAttributes(attrs, new int[] { Resource.Attribute.labelValue });
            string label = typedArray.GetText(0);
            typedArray.Recycle();

            textView = FindViewById<TextView>(Resource.Id.scoreLineLabel);
            editText = FindViewById<EditText>(Resource.Id.scoreLineNumber);

            defaultValue = 0;
            storedValue = 0;

            editText.TextChanged += new EventHandler<Android.Text.TextChangedEventArgs>((object sender, Android.Text.TextChangedEventArgs e) =>
            {
                bool restore = false;
                bool handleTextChanged = false;
                foreach (StackTraceElement element in Thread.CurrentThread().GetStackTrace())
                {
                    //TODO method str
                    if (element.MethodName=="onRestoreInstanceState")
                    {
                        restore = true;
                        break;
                    }
                    if (element.MethodName == "handleTextChanged")
                    {
                        handleTextChanged = true;
                    }
                }

                if (!restore)
                {
                    storedValue = getValue();
                    if (!handleTextChanged)
                        editText.SetSelection(editText.Text.Length);
                }

                changeMethod?.Invoke();
            });

            textView.Text=label;
        }

        public void restoreNumber()
        {
            setNumber(storedValue);
        }

        public void setNumberRange(int min, int max)
        {
            editText.SetFilters(new IInputFilter[] { new MinMaxFilter(Context, min, max) });
        }

        public void setNumber(int number)
        {
            editText.Text=number.ToString();
        }

        private int getValue()
        {
            string text = editText.Text;
            if (text.Length== 0 || this.Visibility == ViewStates.Gone)
                return defaultValue;
            return Int32.Parse(text);
        }
        public int getNumber()
        {
            return storedValue;
        }

    }

}