using Android.Content;
using System;

namespace AlhambraScoringAndroid
{
    public class DialogInterfaceOnClickListener: Java.Lang.Object, IDialogInterfaceOnClickListener
    {
        public delegate void OnClickEventHandler(IDialogInterface dialog, int which);

        public OnClickEventHandler OnClickAction;

        public DialogInterfaceOnClickListener(OnClickEventHandler onClickAction)
        {
            OnClickAction = onClickAction;
        }

        public void OnClick(IDialogInterface dialog, int which)
        {
            OnClickAction?.Invoke(dialog, which);
        }
    }
}