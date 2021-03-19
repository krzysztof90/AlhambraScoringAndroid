using AlhambraScoringAndroid.GamePlay;

namespace AlhambraScoringAndroid.UI
{
    public class PlaceholderPlayerScoreBeforeAssignLeftoverFragment : PlaceholderPlayerScoreFragmentBase
    {
        ScoreLineNumberView buildingsWithoutServantTileNumericUpDown;

        protected override int GetContentLayout()
        {
            return Resource.Layout.fragment_game_score_beforeassignleftover;
        }

        protected override void CreateControls()
        {
            buildingsWithoutServantTileNumericUpDown = Root.FindViewById<ScoreLineNumberView>(Resource.Id.buildingsWithoutServantTileNumericUpDown);
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

        public PlaceholderPlayerScoreBeforeAssignLeftoverFragment(int _index, Game game) : base(_index, game)
        {
        }

        public int BuildingsWithoutServantTile => buildingsWithoutServantTileNumericUpDown.Value;
    }
}