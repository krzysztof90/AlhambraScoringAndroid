using AlhambraScoringAndroid.GamePlay;
using AndroidBase.UI;

namespace AlhambraScoringAndroid.UI
{
    public class PlaceholderPlayerScoreBeforeAssignLeftoverFragment : PlaceholderPlayerScoreFragmentBase
    {
        ControlNumberView buildingsWithoutServantTileNumericUpDown;

        protected override int GetContentLayout()
        {
            return Resource.Layout.fragment_game_score_beforeassignleftover;
        }

        protected override void CreateControls()
        {
            buildingsWithoutServantTileNumericUpDown = Root.FindViewById<ControlNumberView>(Resource.Id.buildingsWithoutServantTileNumericUpDown);
        }

        protected override void AddControls()
        {
            Controls.Add(buildingsWithoutServantTileNumericUpDown);
        }

        protected override void SetControlsProperties()
        {
            AddConditionToVisible(buildingsWithoutServantTileNumericUpDown, Game.HasModule(ExpansionModule.DesignerPalaceStaff));

            AddConditionToVisible(buildingsWithoutServantTileNumericUpDown, !IsDirk);
        }

        public PlaceholderPlayerScoreBeforeAssignLeftoverFragment(int _index, Game game, PlayersScoreSectionsPagerAdapter adapter) : base(_index, game, adapter)
        {
        }

        public int BuildingsWithoutServantTile => buildingsWithoutServantTileNumericUpDown.Value;
    }
}