using AlhambraScoringAndroid.GamePlay;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidBase.UI;
using System;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.UI
{
    public abstract class PlaceholderPlayerScoreFragmentBase : AndroidX.Fragment.App.Fragment
    {
        private readonly PlayersScoreSectionsPagerAdapter Adapter;

        public int PlayerNumber { get; private set; }
        protected Game Game { get; private set; }

        protected bool IsDirk { get; private set; }
        protected bool IsFinalRound { get; private set; }
        protected PlayerScoreData CorrectingRoundScoring { get; private set; }
        protected PlayerScoreData PreviousRoundScoring { get; private set; }

        protected View Root { get; private set; }

        public List<IControlViewBase> Controls { get; private set; }

        protected abstract int GetContentLayout();
        protected abstract void CreateControls();
        protected abstract void AddControls();
        protected abstract void SetControlsProperties();
        protected abstract void ApplyCorrectingRoundScoring();
        protected abstract void ApplyPreviousRoundScoring();

        public PlaceholderPlayerScoreFragmentBase(int index, Game game, List<PlayerScoreData> correctingRoundScoring, PlayersScoreSectionsPagerAdapter adapter)
        {
            Adapter = adapter;

            PlayerNumber = index;
            Game = game;
            Controls = new List<IControlViewBase>();

            IsDirk = Game.GetPlayer(PlayerNumber).Dirk;
            IsFinalRound = Game.ScoreRound == ScoringRound.Third;
            CorrectingRoundScoring = correctingRoundScoring?[PlayerNumber - 1];
            PreviousRoundScoring = Game.PreviousRoundScoring?[PlayerNumber - 1];
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (Root == null)
            {
                Create(inflater, container);
            }

            return Root;
        }

        public void Create(LayoutInflater inflater, ViewGroup container)
        {
            //TODO długie ładowanie https://stackoverflow.com/questions/20223628/how-to-resolve-layout-has-more-than-80-views-bad-for-performance
            Root = inflater.Inflate(GetContentLayout(), container, false);

            CreateControls();
            AddControls();
            InitializeControls();
            SetControlsProperties();
            if (CorrectingRoundScoring != null)
            {
                ApplyCorrectingRoundScoring();
            }
            else if (PreviousRoundScoring != null)
            {
                ApplyPreviousRoundScoring();
            }

            Button submitButton = Root.FindViewById<Button>(Resource.Id.submitButton);
            submitButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                Adapter.Submit();
            });
        }

        private void InitializeControls()
        {
            foreach (IControlViewBase control in Controls)
                control.Initialize();
        }

        public void RestoreValues()
        {
            foreach (IControlViewBase control in Controls)
                control.RestoreValue();
        }

        protected void AddConditionToVisible(LinearLayout layout, bool condition)
        {
            layout.Visibility = (layout.Visibility == ViewStates.Visible && condition) ? ViewStates.Visible : ViewStates.Gone;
        }
    }

}