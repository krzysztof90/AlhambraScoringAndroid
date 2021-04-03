using AlhambraScoringAndroid.UI;
using Android.Content;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.Tools
{
    public static class ValidateUtils
    {
        public static bool CheckFailed(Context context, string text)
        {
            Toast.MakeText(context, text, ToastLength.Long).Show();
            return false;
        }

        public static bool ValidatePlayerPanels(this IEnumerable<ScoreLineNumberView> playerPanels)
        {
            foreach (ScoreLineNumberView playerPanel in playerPanels)
                if (playerPanel.Visibility == ViewStates.Visible)
                    if (!playerPanel.ValidateNumberRange())
                        return false;
            return true;
        }
    }

    public class NameValidationException : Exception
    {
        public NameValidationException(string message) : base(message)
        {
        }
    }
}