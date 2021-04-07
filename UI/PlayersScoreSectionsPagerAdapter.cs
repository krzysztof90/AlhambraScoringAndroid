using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.Tools;
using AlhambraScoringAndroid.UI.Activities;
using Android.Content;
using Android.Views;
using AndroidX.Fragment.App;
using AndroidX.ViewPager.Widget;
using Java.Lang;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI
{
    public class PlayersScoreSectionsPagerAdapter : FragmentPagerAdapter
    {
        public readonly GameScoreActivity Activity;
        public ViewPager ViewPager { get; private set; }
        private readonly PlaceholderPlayerScoreFragment[] PlayerScoreFragments;
        private readonly PlaceholderPlayerScoreBeforeAssignLeftoverFragment[] PlayerScoreBeforeAssignLeftoverFragments;

        public List<PlaceholderPlayerScoreFragment> AllPlayerScoreFragments
        {
            get
            {
                for (int i = 0; i < Activity.Game.PlayersCount; i++)
                    if (PlayerScoreFragments[i] == null)
                    {
                        PlayerScoreFragments[i] = new PlaceholderPlayerScoreFragment(i + 1, Activity.Game);
                        PlayerScoreFragments[i].Create((LayoutInflater)Activity.GetSystemService(Context.LayoutInflaterService), ViewPager);
                    }
                return PlayerScoreFragments.ToList();
            }
        }
        public List<PlaceholderPlayerScoreBeforeAssignLeftoverFragment> AllPlayerScoreBeforeAssignLeftoverFragments
        {
            get
            {
                for (int i = 0; i < Activity.Game.PlayersCount; i++)
                    if (PlayerScoreBeforeAssignLeftoverFragments[i] == null)
                    {
                        PlayerScoreBeforeAssignLeftoverFragments[i] = new PlaceholderPlayerScoreBeforeAssignLeftoverFragment(i + 1, Activity.Game);
                        PlayerScoreBeforeAssignLeftoverFragments[i].Create((LayoutInflater)Activity.GetSystemService(Context.LayoutInflaterService), ViewPager);
                    }
                return PlayerScoreBeforeAssignLeftoverFragments.ToList();
            }
        }

        public PlayersScoreSectionsPagerAdapter(GameScoreActivity context, AndroidX.Fragment.App.FragmentManager fm, ViewPager viewPager) : base(fm)
        {
            Activity = context;
            PlayerScoreFragments = new PlaceholderPlayerScoreFragment[Activity.Game.PlayersCount];
            PlayerScoreBeforeAssignLeftoverFragments = new PlaceholderPlayerScoreBeforeAssignLeftoverFragment[Activity.Game.PlayersCount];

            ViewPager = viewPager;
            ViewPager.Adapter = this;
        }

        public override AndroidX.Fragment.App.Fragment GetItem(int position)
        {
            if (position < Activity.Game.PlayersCount)
            {
                if (Activity.Game.ScoreRound != ScoringRound.ThirdBeforeLeftover)
                {
                    PlaceholderPlayerScoreFragment playerScoreFragment = new PlaceholderPlayerScoreFragment(position + 1, Activity.Game);
                    PlayerScoreFragments[position] = playerScoreFragment;
                    return playerScoreFragment;
                }
                else
                {
                    PlaceholderPlayerScoreBeforeAssignLeftoverFragment playerScoreFragment = new PlaceholderPlayerScoreBeforeAssignLeftoverFragment(position + 1, Activity.Game);
                    PlayerScoreBeforeAssignLeftoverFragments[position] = playerScoreFragment;
                    return playerScoreFragment;
                }
            }
            else
                return new PlaceholderScoreSubmitFragment(this);
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            if (position < Activity.Game.PlayersCount)
                return new Java.Lang.String(Activity.Game.GetPlayer(position + 1).Name);
            else
                return new Java.Lang.String("Submit");
        }

        public override int Count => Activity.Game.PlayersCount + 1;

        /// shit android
        public void RestoreValues(int position)
        {
            if (Activity.Game.ScoreRound != ScoringRound.ThirdBeforeLeftover)
                PlayerScoreFragments[position].RestoreValues();
            else
                PlayerScoreBeforeAssignLeftoverFragments[position].RestoreValues();
        }

        public bool ValidateAllPlayerScoreFragments()
        {
            foreach (PlaceholderPlayerScoreFragment playerScoreFragment in AllPlayerScoreFragments)
            {
                IEnumerable<ScoreLineNumberView> playerPanels = playerScoreFragment.Controls.Where(c => c is ScoreLineNumberView).Cast<ScoreLineNumberView>();
                if (!playerPanels.ValidatePlayerPanels())
                    return false;
            }

            return true;
        }

        public bool ValidateAllPlayerScoreBeforeAssignLeftoverFragments()
        {
            foreach (PlaceholderPlayerScoreBeforeAssignLeftoverFragment playerScoreFragment in AllPlayerScoreBeforeAssignLeftoverFragments)
            {
                IEnumerable<ScoreLineNumberView> playerPanels = playerScoreFragment.Controls.Where(c => c is ScoreLineNumberView).Cast<ScoreLineNumberView>();
                if (!playerPanels.ValidatePlayerPanels())
                    return false;
            }
            return true;
        }

        public void Submit()
        {
            Activity.Submit();
        }
    }
}