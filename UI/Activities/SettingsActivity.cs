using AlhambraScoringAndroid.Options;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidBase.Options;
using AndroidBase.Tools;
using AndroidBase.UI;
using System;
using System.Linq;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "@string/action_settings", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SettingsActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_settings;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            LinearLayout container = FindViewById<LinearLayout>(Resource.Id.container);

            foreach (SettingsType settingsType in Enum.GetValues(typeof(SettingsType)).Cast<SettingsType>().ToList())
            {
                ControlCheckBoxView checkBox = new ControlCheckBoxView(this);
                checkBox.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                checkBox.SetLabel(settingsType.GetEnumDescription(Resources));
                checkBox.Value = SettingsManager.Get(settingsType);
                checkBox.OnValueChange = () => { SettingsManager.Set(settingsType, checkBox.Value); };

                container.AddView(checkBox);
            }
        }
    }
}