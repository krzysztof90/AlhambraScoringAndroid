﻿using AlhambraScoringAndroid.GamePlay;
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
using static Android.Widget.AdapterView;

namespace AlhambraScoringAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class GamePlayersChoseActivity : BaseActivity
    {
        //TODO equals
        //TODO string.empty

        protected override int getContentView()
        {
            return Resource.Layout.activity_gameplayerschose;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Spinner spinner = FindViewById<Spinner>(Resource.Id.players_count_spinner);
            ArrayAdapter adapter = ArrayAdapter.CreateFromResource(this,
                    Resource.Array.players_count_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            spinner.ItemSelected += Spinner_ItemSelected;

            Button startButton = FindViewById<Button>(Resource.Id.startButton2);
            startButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                //TODO validate names on the run

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

                try
                {
                    application().gameStart(players);
                }
                catch (Exception exception)
                {
                    Toast.MakeText(ApplicationContext, exception.Message, ToastLength.Long).Show();
                }
            });
        }

        private void Spinner_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            EditText editText6 = FindViewById<EditText>(Resource.Id.text_player6);
            editText6.Visibility = (e.Position + 2 < 6 ? ViewStates.Gone : ViewStates.Visible);
            EditText editText5 = FindViewById<EditText>(Resource.Id.text_player5);
            editText5.Visibility = (e.Position + 2 < 5 ? ViewStates.Gone : ViewStates.Visible);
            EditText editText4 = FindViewById<EditText>(Resource.Id.text_player4);
            editText4.Visibility = (e.Position+ 2 < 4 ? ViewStates.Gone : ViewStates.Visible);
            EditText editText3 = FindViewById<EditText>(Resource.Id.text_player3);
            if (e.Position + 2 != 2)
            {
                editText3.Enabled = true;
                if (editText3.Text == Player.DirkName)
                {
                    editText3.Text = String.Empty;
                }
            }
            else
            {
                editText3.Enabled = false;
                editText3.Text = Player.DirkName;
            }
        }
    }

}