using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StringBuilder = System.Text.StringBuilder;

namespace AlhambraScoringAndroid
{
    public class MinMaxFilter : Java.Lang.Object, IInputFilter
    {
        private int mIntMin, mIntMax;
        Context Context;

        public MinMaxFilter(Context context, int minValue, int maxValue)
        {
            this.mIntMin = minValue;
            this.mIntMax = maxValue;

            Context = context;
        }

         ICharSequence? IInputFilter.FilterFormatted(ICharSequence? source, int start, int end, ISpanned? dest, int dstart, int dend)
        {
            string newString = dest.ToString();
            if (dstart > (dest.Length() - 1))
                newString+=source;
            else
                newString= newString.Substring(0, dstart)+   source.ToString().Substring(start, end)+newString.Substring(dend+1);

                if (Int32.TryParse(newString, out int input))
            {
                if (input >= mIntMin && input <= mIntMax)
                    return null;
                else
                    Toast.MakeText(Context, $"Dozwolony zakres to {mIntMin} - {mIntMax}", ToastLength.Long).Show();
            }
                return new Java.Lang.String(System.String.Empty);
        }
}

}