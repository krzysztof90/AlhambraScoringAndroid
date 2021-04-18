using AlhambraScoringAndroid.Options;
using Android.Content;
using System;

namespace AlhambraScoringAndroid.InputFilters
{
    public class MinMaxFilter : TextFilter
    {
        private readonly int Min;
        private readonly int Max;

        public MinMaxFilter(Context context, int minValue, int maxValue, SettingsType? validationSettingsType) : base(context, validationSettingsType)
        {
            Min = minValue;
            Max = maxValue;
        }

        protected override string ValidationMessage => System.String.Format(Context.Resources.GetString(Resource.String.allowed_range), Min, Max);

        protected override bool ValidateNumber(string text, bool validateFull, int? defaultValue = null, string fieldName = null)
        {
            if (Int32.TryParse(text, out int input))
            {
                if (input <= Max
                    && (input >= Min || !validateFull))
                    return true;
                else
                    return ValidationError(fieldName);
            }

            return ValidateEmptyValue(text, validateFull, defaultValue, fieldName);
        }
    }

}