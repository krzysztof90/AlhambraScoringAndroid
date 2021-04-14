using Android.Content;
using System;
using System.Collections.Generic;

namespace AlhambraScoringAndroid
{
    public class NumberFilter : TextFilter
    {
        private readonly List<int> Numbers;

        public NumberFilter(Context context, List<int> numbers) : base(context)
        {
            Numbers = numbers;
        }

        protected override string ValidationMessage => Context.Resources.GetString(Resource.String.except_range);

        public override bool ValidateNumberRange(string text, bool validateFull, int? defaultValue = null, string fieldName = null)
        {
            if (!validateFull)
                return true;

            if (Int32.TryParse(text, out int input))
            {
                if (!Numbers.Contains(input))
                    return true;
                else
                    return ValidationError(fieldName);
            }

            return ValidateEmptyValue(((int)defaultValue).ToString(), validateFull, defaultValue, fieldName);
        }
    }
}