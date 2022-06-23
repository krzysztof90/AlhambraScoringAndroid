using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.Tools;
using AlhambraScoringAndroid.UI.Activities;
using Android.App;
using Android.Content;
using Android.Views;
using AndroidBase.UI;
using AndroidX.Fragment.App;
using AndroidX.ViewPager.Widget;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI
{
    //TODO jeżeli zmiana zakładki i zanim zmieniona zakładka kliknięcie w pole tekstowe - pole tekstowe zostaje z poprzedniej zakładki
    public class PlayersScoreSectionsPagerAdapter : FragmentPagerAdapter
    {
        public readonly GameScoreActivity Activity;
        public ViewPager ViewPager { get; private set; }
        private readonly PlaceholderPlayerScoreFragment[] PlayerScoreFragments;
        private readonly List<int> selectedPages;

        public List<PlayerScoreData> AllPlayerScoreData
        {
            get
            {
                return GetAllPlayerScoreFragments().Select(p => new PlayerScoreData(p)).ToList();
            }
        }

        public PlayersScoreSectionsPagerAdapter(GameScoreActivity context, AndroidX.Fragment.App.FragmentManager fm, ViewPager viewPager) : base(fm)
        {
            Activity = context;
            PlayerScoreFragments = new PlaceholderPlayerScoreFragment[Count];

            selectedPages = new List<int>();
            selectedPages.Add(0);

            ViewPager = viewPager;
            ViewPager.Adapter = this;

            viewPager.PageSelected += new EventHandler<ViewPager.PageSelectedEventArgs>((object sender, ViewPager.PageSelectedEventArgs e) =>
            {
                RestoreValues(e.Position);
                if (!selectedPages.Contains(e.Position))
                    selectedPages.Add(e.Position);
            });
        }

        public override AndroidX.Fragment.App.Fragment GetItem(int position)
        {
            PlaceholderPlayerScoreFragment playerScoreFragment = new PlaceholderPlayerScoreFragment(position + 1, Activity.Game, Activity.CorrectingScoring(), this);
            PlayerScoreFragments[position] = playerScoreFragment;
            return playerScoreFragment;
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            if (position < Count)
                return new Java.Lang.String(Activity.Game.GetPlayer(position + 1).Name);
            else
                return new Java.Lang.String(Activity.Resources.GetString(Resource.String.submit));
        }

        public override int Count => Activity.Game.PlayersCount;

        /// shit android
        public void RestoreValues(int position)
        {
            PlayerScoreFragments[position].RestoreValues();
        }

        private List<PlaceholderPlayerScoreFragment> GetAllPlayerScoreFragments()
        {
            for (int i = 0; i < Count; i++)
                if (PlayerScoreFragments[i] == null)
                {
                    PlayerScoreFragments[i] = new PlaceholderPlayerScoreFragment(i + 1, Activity.Game, Activity.CorrectingScoring(), this);
                    PlayerScoreFragments[i].Create((LayoutInflater)Activity.GetSystemService(Context.LayoutInflaterService), ViewPager);
                }
            return PlayerScoreFragments.ToList();
        }

        public bool ValidateAllPlayerScoreFragments()
        {
            foreach (PlaceholderPlayerScoreFragment playerScoreFragment in GetAllPlayerScoreFragments())
            {
                IEnumerable<ControlNumberView> playerPanels = playerScoreFragment.Controls.Where(c => c is ControlNumberView).Cast<ControlNumberView>();
                if (!playerPanels.ValidatePlayerPanels())
                    return false;
            }

            return true;
        }

        public void Submit()
        {
            bool visitedAllPages = true;
            for (int i = 0; i < Count; i++)
                if (!selectedPages.Contains(i))
                {
                    visitedAllPages = false;
                    break;
                }

            if (!visitedAllPages && Activity.CorrectingScoring() == null)
            {
                new AlertDialog.Builder(Activity)
                    .SetTitle(Activity.Resources.GetString(Resource.String.submit_all_players))
                    .SetMessage(Activity.Resources.GetString(Resource.String.continue_question))
                    .SetPositiveButton(Activity.Resources.GetString(Resource.String.yes), new DialogInterfaceOnClickListener((IDialogInterface dialog, int which) => Activity.Submit()))
                    .SetNegativeButton(Activity.Resources.GetString(Resource.String.no), new DialogInterfaceOnClickListener(null))
                    .Show();
            }
            else
                Activity.Submit();
        }
    }
}