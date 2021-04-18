using Android.App;
using Android.OS;
using Android.Text.Method;
using Android.Widget;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "@string/action_about", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class AboutActivity : BaseActivity
    {
        //TODO copyrights

        protected override int ContentView => Resource.Layout.activity_about;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            FindViewById<TextView>(Resource.Id.textViewMail).MovementMethod = LinkMovementMethod.Instance;
            FindViewById<TextView>(Resource.Id.textViewPaypalLink).MovementMethod = LinkMovementMethod.Instance;
        }
    }
}