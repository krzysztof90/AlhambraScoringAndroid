using AlhambraScoringAndroid.Options;
using Android.Views;
using AndroidBase.UI;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.Tools
{
    public static class ValidateUtils
    {
        public static bool ValidatePlayerPanels(this IEnumerable<ControlNumberView> playerPanels)
        {
            foreach (ControlNumberView playerPanel in playerPanels)
                if (playerPanel.Visibility == ViewStates.Visible)
                    if (!playerPanel.ValidateNumberRange<SettingsType>())
                        return false;
            return true;
        }
    }
}