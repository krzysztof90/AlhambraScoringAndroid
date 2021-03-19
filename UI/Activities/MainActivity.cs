using Android.App;

namespace AlhambraScoringAndroid.Activities
{
    [Activity(Label = "Alhambra scoring", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_main;
    }
}
