﻿using AlhambraScoringAndroid.GamePlay;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.ViewPager.Widget;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.UI
{
    public abstract class PlaceholderPlayerScoreFragmentBase : AndroidX.Fragment.App.Fragment
    {
        public int PlayerNumber { get; private set; }
        protected Game Game { get; private set; }

        protected bool IsDirk { get; private set; }
        protected bool IsFinalRound { get; private set; }

        protected View Root { get; private set; }

        public List<IScoreLineView> Controls { get; private set; }

        protected abstract int GetContentLayout();
        protected abstract void CreateControls();
        protected abstract void AddControls();
        protected abstract void SetControlsProperties();

        public PlaceholderPlayerScoreFragmentBase(int index, Game game)
        {
            PlayerNumber = index;
            Game = game;
            Controls = new List<IScoreLineView>();

            IsDirk = Game.GetPlayer(PlayerNumber).Dirk;
            IsFinalRound = Game.ScoreRound == ScoringRound.Third;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (Root == null)
            {
                Create( inflater,  container);
            }

            return Root;
        }

        public void Create(LayoutInflater inflater, ViewGroup container)
        {
            Root = inflater.Inflate(GetContentLayout(), container, false);

            CreateControls();
            AddControls();
            SetControlsProperties();
            InitializeControls();
        }

        private void InitializeControls()
        {
            foreach (IScoreLineView control in Controls)
                control.Initialize();
        }

        public void RestoreValues()
        {
            foreach (IScoreLineView control in Controls)
                control.RestoreValue();
        }

        protected void AddConditionToVisible(LinearLayout layout, bool condition)
        {
            layout.Visibility = (layout.Visibility == ViewStates.Visible && condition) ? ViewStates.Visible : ViewStates.Gone;
        }
    }

}