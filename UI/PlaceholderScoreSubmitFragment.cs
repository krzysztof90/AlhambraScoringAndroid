using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlhambraScoringAndroid.UI
{
    public class PlaceholderScoreSubmitFragment : AndroidX.Fragment.App.Fragment
    {
        PlayersScoreSectionsPagerAdapter Adapter;

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
                Adapter.submit();
            });

            return root;
        }
    }

}