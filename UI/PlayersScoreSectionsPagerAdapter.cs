using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlhambraScoringAndroid.UI
{
    public class PlayersScoreSectionsPagerAdapter : FragmentPagerAdapter
    {
        public GameScoreActivity activity;
        private PlaceholderPlayerScoreFragment[] PlayerScoreFragments;
        private PlaceholderPlayerScoreBeforeAssignLeftoverFragment[] PlayerScoreBeforeAssignLeftoverFragments;

        public List<PlaceholderPlayerScoreFragment> AllPlayerScoreFragments
        {
            get
            {
                for (int i = 0; i < activity.getGame().getPlayersCount; i++)
                    if (PlayerScoreFragments[i] == null)
                        PlayerScoreFragments[i] = new PlaceholderPlayerScoreFragment(i + 1, activity.getGame());
                return PlayerScoreFragments.ToList();
            }
        }
        public List<PlaceholderPlayerScoreBeforeAssignLeftoverFragment> AllPlayerScoreBeforeAssignLeftoverFragments
        {
            get
            {
                for (int i = 0; i < activity.getGame().getPlayersCount; i++)
                    if (PlayerScoreBeforeAssignLeftoverFragments[i] == null)
                        PlayerScoreBeforeAssignLeftoverFragments[i] = new PlaceholderPlayerScoreBeforeAssignLeftoverFragment(i + 1, activity.getGame());
                return PlayerScoreBeforeAssignLeftoverFragments.ToList();
            }
        }

        public PlayersScoreSectionsPagerAdapter(GameScoreActivity context, AndroidX.Fragment.App.FragmentManager fm) : base(fm)
        {
            activity = context;
            PlayerScoreFragments = new PlaceholderPlayerScoreFragment[activity.getGame().getPlayersCount];
            PlayerScoreBeforeAssignLeftoverFragments = new PlaceholderPlayerScoreBeforeAssignLeftoverFragment[activity.getGame().getPlayersCount];
        }

        public override AndroidX.Fragment.App.Fragment GetItem(int position)
        {
            if (position < activity.getGame().getPlayersCount)
            {
                if (activity.getGame().ScoreRound != ScoringRound.ThirdBeforeLeftover)
                {
                    PlaceholderPlayerScoreFragment playerScoreFragment = new PlaceholderPlayerScoreFragment(position + 1, activity.getGame());
                    PlayerScoreFragments[position] = playerScoreFragment;
                    return playerScoreFragment;
                }
                else
                {
                    PlaceholderPlayerScoreBeforeAssignLeftoverFragment playerScoreFragment = new PlaceholderPlayerScoreBeforeAssignLeftoverFragment(position + 1, activity.getGame());
                    PlayerScoreBeforeAssignLeftoverFragments[position] = playerScoreFragment;
                    return playerScoreFragment;
                }
            }
            else
                return new PlaceholderScoreSubmitFragment(this);
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            if (position < activity.getGame().getPlayersCount)
                return new Java.Lang.String(activity.getGame().getPlayer(position + 1).Name);
            else
                return new Java.Lang.String("Submit");
        }

        public override int Count => activity.getGame().getPlayersCount + 1;

        //TODO czy potrzebne?
        public void restoreValues(int position)
        {
            if (activity.getGame().ScoreRound != ScoringRound.ThirdBeforeLeftover)
                PlayerScoreFragments[position].restoreValues();
            else
                PlayerScoreBeforeAssignLeftoverFragments[position].restoreValues();
        }

        public void submit()
        {
            activity.submit();
        }
    }
}