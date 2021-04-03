using AlhambraScoringAndroid.Tools;
using Android.Content;
using Android.Text;
using Java.Lang;
using System;

namespace AlhambraScoringAndroid
{
    public class MinMaxFilter : Java.Lang.Object, IInputFilter
    {
        private readonly int min;
        private readonly int max;
        private readonly bool allowEmpty;
        private readonly Context Context;

        public MinMaxFilter(Context context, int minValue, int maxValue, bool allowEmpty)
        {
            this.min = minValue;
            this.max = maxValue;
            //TODO nieużyteczne
            this.allowEmpty = allowEmpty;

            Context = context;
        }

        ICharSequence IInputFilter.FilterFormatted(ICharSequence source, int start, int end, ISpanned dest, int dstart, int dend)
        {
            string newString = dest.ToString().Substring(0, dstart) + source.ToString().Substring(start, end) + dest.ToString().Substring(dend);

            return ValidateNumberRange(newString, false) ? null : new Java.Lang.String(System.String.Empty);

        }

        public bool ValidateNumberRange(string text,bool validateMinimum, int? defaultValue = null, string fieldName = null)
        {
            if (Int32.TryParse(text, out int input))
            {
                //TODO walidacja minimum przy lost focus
                if (input <= max
                    && (input >= min || !validateMinimum))
                    return true;
                else
                {
                    StringBuilder message = new StringBuilder();
                    if (fieldName != null)
                        message.Append($"{fieldName}: ");
                    message.Append($"Dozwolony zakres to {min} - {max}");
                    return ValidateUtils.CheckFailed(Context, message.ToString());
                }
            }
            return System.String.IsNullOrEmpty(text) &&  defaultValue != null && ValidateNumberRange(((int)defaultValue).ToString(), validateMinimum);
        }
    }

}