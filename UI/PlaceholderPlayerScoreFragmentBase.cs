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
    public abstract class PlaceholderPlayerScoreFragmentBase : AndroidX.Fragment.App.Fragment
    {

    public int index { get; private set; }
        protected Game Game;

    protected bool isDirk;
    protected bool isFinalRound;

    protected View root;

    protected List<LinearLayout> controls;

    protected abstract  int getContentLayout();
    protected abstract void createControls();
    protected abstract void addControls();
    protected abstract void setControlsProperties();

    public PlaceholderPlayerScoreFragmentBase(int _index, Game game)
    {
        index = _index;
        Game = game;
        controls = new List<LinearLayout>();

        isDirk = Game.getPlayer(index).Dirk;
        isFinalRound = Game.ScoreRound == ScoringRound.Third;
    }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
        if (root == null)
        {
            root = inflater.Inflate(getContentLayout(), container, false);

            createControls();
            addControls();
            setControlsProperties();
        }

        return root;
    }

    public void restoreValues()
    {
        foreach (LinearLayout control in controls)
        {
            if (control is ScoreLineNumberView)
            {
            ((ScoreLineNumberView)control).restoreNumber();
        }
            else if (control is ScoreLineCheckBoxView)
            {
            ((ScoreLineCheckBoxView)control).restoreChecked();
        }
    }
}

protected void AddConditionToVisible(LinearLayout layout, bool condition)
{
    layout.Visibility=(layout.Visibility == ViewStates.Visible && condition) ? ViewStates.Visible : ViewStates.Gone;
}

protected int getNumberValue(ScoreLineNumberView control)
{
    if (control == null)
        return 0;
    return control.getNumber();
}
protected bool getCheckBoxValue(ScoreLineCheckBoxView control)
{
    if (control == null)
        return false;
    return control.getChecked();
}
}

}