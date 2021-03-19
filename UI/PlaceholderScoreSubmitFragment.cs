using Android.OS;
using Android.Views;
using Android.Widget;
using System;

namespace AlhambraScoringAndroid.UI
{
    public class PlaceholderScoreSubmitFragment : AndroidX.Fragment.App.Fragment
    {
        private readonly PlayersScoreSectionsPagerAdapter Adapter;

        public PlaceholderScoreSubmitFragment(PlayersScoreSectionsPagerAdapter adapter)
        {
            Adapter = adapter;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.fragment_game_score_submit, container, false);

            Button submitButton = root.FindViewById<Button>(Resource.Id.submitButton);

            submitButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                Adapter.Submit();
            });

            return root;
        }
    }
}