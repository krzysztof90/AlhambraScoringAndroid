using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using AndroidBase.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using static Android.Widget.AdapterView;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "@string/players_chose", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class GamePlayersChoseActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_game_players_chose;

        private List<EditText> textBoxes;

        private EditText GetTextBox(int playerNumber)
        {
            return textBoxes[playerNumber - 1];
        }

        private EditText CreateTextBox()
        {
            LinearLayout container = FindViewById<LinearLayout>(Resource.Id.container);

            EditText textBox = new EditText(this);
            LinearLayout.LayoutParams layoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            layoutParameters.TopMargin = (int)Resources.GetDimension(Resource.Dimension.chose_player_panel_gap);
            textBox.LayoutParameters = layoutParameters;
            textBox.InputType = InputTypes.TextFlagCapSentences;

            container.AddView(textBox);

            return textBox;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            textBoxes = new List<EditText>();
            for (int i = 1; i <= 6; i++)
                textBoxes.Add(CreateTextBox());

            Spinner spinner = FindViewById<Spinner>(Resource.Id.players_count_spinner);
            ArrayAdapter adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.players_count_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            spinner.ItemSelected += Spinner_ItemSelected;

            Button startButton = FindViewById<Button>(Resource.Id.startButton);
            startButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                List<string> players = new List<string>();

                int playersCount = spinner.SelectedItemPosition + 2;

                for (int i = 1; i <= playersCount; i++)
                    players.Add(GetTextBox(i).Text);

                Application.GameSetPlayers(players);
            });
        }

        private void Spinner_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            int playersCount = e.Position + 2;

            for (int i = 4; i <= 6; i++)
                GetTextBox(i).SetVisibility(playersCount >= i);

            EditText editTextPlayer3 = GetTextBox(3);
            if (playersCount != 2)
            {
                editTextPlayer3.Enabled = true;
                if (editTextPlayer3.Text == Player.DirkName)
                    editTextPlayer3.Text = String.Empty;
            }
            else
            {
                editTextPlayer3.Enabled = false;
                editTextPlayer3.Text = Player.DirkName;
            }
        }

        public override void OnBackPressed()
        {
            Application.MainScreen(this);
        }
    }
}