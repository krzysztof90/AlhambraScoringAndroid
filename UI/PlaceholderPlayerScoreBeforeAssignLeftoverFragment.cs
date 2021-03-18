using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlhambraScoringAndroid.UI
{
    public class PlaceholderPlayerScoreBeforeAssignLeftoverFragment : PlaceholderPlayerScoreFragmentBase
    {
        ScoreLineNumberView buildingsWithoutServantTileNumericUpDown;

    protected override int getContentLayout()
    {
        return Resource.Layout.fragment_game_score_beforeassignleftover;
    }

    protected override void createControls()
    {
        buildingsWithoutServantTileNumericUpDown = root.FindViewById< ScoreLineNumberView>(Resource.Id.buildingsWithoutServantTileNumericUpDown);
    }

    protected override void addControls()
    {
        controls.Add(buildingsWithoutServantTileNumericUpDown);
    }

    protected override void setControlsProperties()
    {
        AddConditionToVisible(buildingsWithoutServantTileNumericUpDown, Game.hasModule(ExpansionModule.DesignerPalaceStaff));

        AddConditionToVisible(buildingsWithoutServantTileNumericUpDown, !isDirk);
    }

    public PlaceholderPlayerScoreBeforeAssignLeftoverFragment(int _index, Game game): base(_index, game)
        {
    }

    public int BuildingsWithoutServantTile=>getNumberValue(buildingsWithoutServantTileNumericUpDown); 
}

}