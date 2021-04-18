using AlhambraScoringAndroid.GamePlay;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;

namespace AlhambraScoringAndroid.UI.Activities
{
    public abstract class BaseActivity : AppCompatActivity
    {
        protected new MyApplication Application => (MyApplication)base.Application;

        public Game Game => Application.Game;

        protected abstract int ContentView { get; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(ContentView);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.action_newGame)
            {
                Application.NewGamePrompt(this);
                return true;
            }
            if (id == Resource.Id.action_showHistory)
            {
                Application.ShowHistory(this);
                return true;
            }
            if (id == Resource.Id.action_settings)
            {
                Application.ShowSettings();
                return true;
            }
            if (id == Resource.Id.action_about)
            {
                Application.ShowAbout();
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}