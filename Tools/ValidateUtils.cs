using Android.Content;
using Android.Widget;
using System;

namespace AlhambraScoringAndroid.Tools
{
    public static class ValidateUtils
    {
        public static bool CheckFailed(Context context, string text)
        {
            Toast.MakeText(context, text, ToastLength.Long).Show();
            return false;
        }
    }

    public class NameValidationException : Exception
    {
        public NameValidationException(string message) : base(message)
        {
        }
    }
}