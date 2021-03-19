using Android.Content;
using Android.Text;
using Android.Widget;
using Java.Lang;
using System;

namespace AlhambraScoringAndroid
{
    public class MinMaxFilter : Java.Lang.Object, IInputFilter
    {
        private readonly int min;
        private readonly int max;
        private readonly Context Context;

        public MinMaxFilter(Context context, int minValue, int maxValue)
        {
            this.min = minValue;
            this.max = maxValue;

            Context = context;
        }

        ICharSequence IInputFilter.FilterFormatted(ICharSequence source, int start, int end, ISpanned dest, int dstart, int dend)
        {
            string newString = dest.ToString();
            if (dstart > (dest.Length() - 1))
                newString += source;
            else
                newString = newString.Substring(0, dstart) + source.ToString().Substring(start, end) + newString.Substring(dend);

            if (Int32.TryParse(newString, out int input))
            {
                if (input >= min && input <= max)
                    return null;
                else
                    Toast.MakeText(Context, $"Dozwolony zakres to {min} - {max}", ToastLength.Long).Show();
            }
            return new Java.Lang.String(System.String.Empty);
        }
    }

}