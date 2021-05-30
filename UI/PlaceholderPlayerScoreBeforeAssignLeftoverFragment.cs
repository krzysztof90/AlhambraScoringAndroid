using AlhambraScoringAndroid.GamePlay;
using AndroidBase.UI;
using System.Collections.Generic;

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

        protected override void ApplyCorrectingRoundScoring()
        {
            buildingsWithoutServantTileNumericUpDown.Value = CorrectingRoundScoring.BuildingsWithoutServantTile;
        }

        protected override void ApplyPreviousRoundScoring()
        {
        }

        public PlaceholderPlayerScoreBeforeAssignLeftoverFragment(int _index, Game game, List<PlayerScoreData> correctingRoundScoring, PlayersScoreSectionsPagerAdapter adapter) : base(_index, game, correctingRoundScoring, adapter)
        {
        }

        public int BuildingsWithoutServantTile => buildingsWithoutServantTileNumericUpDown.Value;
    }
}