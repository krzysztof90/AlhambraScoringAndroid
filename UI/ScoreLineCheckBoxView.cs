using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlhambraScoringAndroid.UI
{
        //TODO część wspólna z ScoreLineNumberView
    public class ScoreLineCheckBoxView : LinearLayout
    {
        CheckBox scoreLineCheckBox;

    public bool defaultValue;
    private bool storedValue;
    public Action changeMethod;

    public void setDefaultValue(bool value)
    {
        defaultValue = value;
    }

    public ScoreLineCheckBoxView(Context context, IAttributeSet attrs): base(context, attrs)
        {
        Inflate(context, Resource.Layout.view_gamescorelinecheckbox, this);

        TypedArray typedArray = context.ObtainStyledAttributes(attrs, new int[] { Resource.Attribute.labelValue });
        string label = typedArray.GetText(0);
        typedArray.Recycle();

        scoreLineCheckBox = FindViewById< CheckBox>(Resource.Id.scoreLineCheckBox);

        storedValue = false;

            scoreLineCheckBox.Click += new EventHandler((object sender, EventArgs e) =>
            {
                storedValue = getValue();
                changeMethod?.Invoke();
            });

        scoreLineCheckBox.Text=label;
    }

public void restoreChecked()
{
    setChecked(storedValue);
}

public bool getValue()
{
    if (this.Visibility == ViewStates.Gone)
        return defaultValue;
    return scoreLineCheckBox.Checked;
}
public void setChecked(bool value)
{
    scoreLineCheckBox.Checked=value;
}
public bool getChecked()
{
    return storedValue;
}

}

}