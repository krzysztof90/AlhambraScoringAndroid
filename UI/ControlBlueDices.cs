using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidBase.UI;
using System;

namespace AlhambraScoringAndroid.UI
{
    class ControlBlueDices : ControlViewBase<int>
    {
        private TextView textView;
        private BlueDiceBox diceBox;

        public ControlBlueDices(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        protected override int ResourceLayout => Resource.Layout.blue_dice_chose;

        protected override TextView LabelControl => textView;
        protected override View InputControl => diceBox;

        protected override void CreateControls()
        {
            textView = FindViewById<TextView>(Resource.Id.controlLabel);
            diceBox = FindViewById<BlueDiceBox>(Resource.Id.controlNumber);
        }

        protected override void SetControlsProperties()
        {
            diceBox.OnValueChange += new Action(() =>
            {
                AssignStoredValue();
                OnValueChange?.Invoke();
            });
        }

        protected override void SetValue(int? value)
        {
            diceBox.Value = value;
        }

        protected override int? GetValue()
        {
            return diceBox.Value;
        }

        public override void SetFocus()
        {
            diceBox.RequestFocus();
        }
    }
}