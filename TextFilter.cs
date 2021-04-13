using AlhambraScoringAndroid.Tools;
using Android.Content;
using Android.Text;
using Java.Lang;

namespace AlhambraScoringAndroid
{
    public abstract class TextFilter : Java.Lang.Object, IInputFilter
    {
        protected readonly Context Context;
        
        public abstract bool ValidateNumberRange(string text, bool validateFull, int? defaultValue = null, string fieldName = null);
        protected abstract string ValidationMessage { get; }

        public TextFilter(Context context)
        {
            Context = context;
        }

        //TODO pełna walidacja przy lost focus
        ICharSequence IInputFilter.FilterFormatted(ICharSequence source, int start, int end, ISpanned dest, int dstart, int dend)
        {
            string newString = dest.ToString().Substring(0, dstart) + source.ToString().Substring(start, end) + dest.ToString().Substring(dend);

            return ValidateNumberRange(newString, false) ? null : new Java.Lang.String(System.String.Empty);
        }

        protected  bool ValidateEmptyValue(string text, bool validateFull, int? defaultValue , string fieldName )
        {
            return System.String.IsNullOrEmpty(text) && defaultValue != null && ValidateNumberRange(((int)defaultValue).ToString(), validateFull, null, fieldName);
        }

        protected bool ValidationError(string fieldName)
        {
            StringBuilder message = new StringBuilder();
            if (fieldName != null)
                message.Append($"{fieldName}: ");
            message.Append(ValidationMessage);
            return ValidateUtils.CheckFailed(Context, message.ToString());
        }
    }
}