using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.UI.Activities;
using AndroidX.Fragment.App;
using Java.Lang;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI
{
    public class PlayersScoreSectionsPagerAdapter : FragmentPagerAdapter
    {
        public readonly GameScoreActivity activity;
        private readonly PlaceholderPlayerScoreFragment[] PlayerScoreFragments;
        private readonly PlaceholderPlayerScoreBeforeAssignLeftoverFragment[] PlayerScoreBeforeAssignLeftoverFragments;

        public List<PlaceholderPlayerScoreFragment> AllPlayerScoreFragments
        {
            get
            {
                for (int i = 0; i < activity.Game.PlayersCount; i++)
                    if (PlayerScoreFragments[i] == null)
                        PlayerScoreFragments[i] = new PlaceholderPlayerScoreFragment(i + 1, activity.Game);
                return PlayerScoreFragments.ToList();
            }
        }
        public List<PlaceholderPlayerScoreBeforeAssignLeftoverFragment> AllPlayerScoreBeforeAssignLeftoverFragments
        {
            get
            {
                for (int i = 0; i < activity.Game.PlayersCount; i++)
                    if (PlayerScoreBeforeAssignLeftoverFragments[i] == null)
                        PlayerScoreBeforeAssignLeftoverFragments[i] = new PlaceholderPlayerScoreBeforeAssignLeftoverFragment(i + 1, activity.Game);
                return PlayerScoreBeforeAssignLeftoverFragments.ToList();
            }
        }

        public PlayersScoreSectionsPagerAdapter(GameScoreActivity context, AndroidX.Fragment.App.FragmentManager fm) : base(fm)
        {
            activity = context;
            PlayerScoreFragments = new PlaceholderPlayerScoreFragment[activity.Game.PlayersCount];
            PlayerScoreBeforeAssignLeftoverFragments = new PlaceholderPlayerScoreBeforeAssignLeftoverFragment[activity.Game.PlayersCount];
        }

        public override AndroidX.Fragment.App.Fragment GetItem(int position)
        {
            if (position < activity.Game.PlayersCount)
            {
                if (activity.Game.ScoreRound != ScoringRound.ThirdBeforeLeftover)
                {
                    PlaceholderPlayerScoreFragment playerScoreFragment = new PlaceholderPlayerScoreFragment(position + 1, activity.Game);
                    PlayerScoreFragments[position] = playerScoreFragment;
                    return playerScoreFragment;
                }
                else
                {
                    PlaceholderPlayerScoreBeforeAssignLeftoverFragment playerScoreFragment = new PlaceholderPlayerScoreBeforeAssignLeftoverFragment(position + 1, activity.Game);
                    PlayerScoreBeforeAssignLeftoverFragments[position] = playerScoreFragment;
                    return playerScoreFragment;
                }
            }
            else
                return new PlaceholderScoreSubmitFragment(this);
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            if (position < activity.Game.PlayersCount)
                return new Java.Lang.String(activity.Game.GetPlayer(position + 1).Name);
            else
                return new Java.Lang.String("Submit");
        }

        public override int Count => activity.Game.PlayersCount + 1;

        /// <summary>
        /// shit android
        /// </summary>
        public void RestoreValues(int position)
        {
            if (activity.Game.ScoreRound != ScoringRound.ThirdBeforeLeftover)
                PlayerScoreFragments[position].RestoreValues();
            else
                PlayerScoreBeforeAssignLeftoverFragments[position].RestoreValues();
        }

        public void Submit()
        {
            activity.Submit();
        }
    }
}