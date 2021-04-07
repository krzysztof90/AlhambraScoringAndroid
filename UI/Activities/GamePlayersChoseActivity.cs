using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using static Android.Widget.AdapterView;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "Wybór graczy", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class GamePlayersChoseActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_game_players_chose;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Spinner spinner = FindViewById<Spinner>(Resource.Id.players_count_spinner);
            ArrayAdapter adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.players_count_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            spinner.ItemSelected += Spinner_ItemSelected;

            Button startButton = FindViewById<Button>(Resource.Id.startButton2);
            startButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                List<string> players = new List<string>();

                int playersCount = spinner.SelectedItemPosition + 2;

                players.Add((FindViewById<EditText>(Resource.Id.text_player1)).Text);
                players.Add((FindViewById<EditText>(Resource.Id.text_player2)).Text);
                if (playersCount > 2)
                    players.Add((FindViewById<EditText>(Resource.Id.text_player3)).Text);
                if (playersCount > 3)
                    players.Add((FindViewById<EditText>(Resource.Id.text_player4)).Text);
                if (playersCount > 4)
                    players.Add((FindViewById<EditText>(Resource.Id.text_player5)).Text);
                if (playersCount > 5)
                    players.Add((FindViewById<EditText>(Resource.Id.text_player6)).Text);

                Application.GameSetPlayers(players);
            });
        }

        private void Spinner_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            EditText editText6 = FindViewById<EditText>(Resource.Id.text_player6);
            editText6.Visibility = (e.Position + 2 < 6 ? ViewStates.Gone : ViewStates.Visible);
            EditText editText5 = FindViewById<EditText>(Resource.Id.text_player5);
            editText5.Visibility = (e.Position + 2 < 5 ? ViewStates.Gone : ViewStates.Visible);
            EditText editText4 = FindViewById<EditText>(Resource.Id.text_player4);
            editText4.Visibility = (e.Position + 2 < 4 ? ViewStates.Gone : ViewStates.Visible);
            EditText editText3 = FindViewById<EditText>(Resource.Id.text_player3);
            if (e.Position + 2 != 2)
            {
                editText3.Enabled = true;
                if (editText3.Text == Player.DirkName)
                    editText3.Text = String.Empty;
            }
            else
            {
                editText3.Enabled = false;
                editText3.Text = Player.DirkName;
            }
        }
    }

}