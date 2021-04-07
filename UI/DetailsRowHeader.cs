using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;

namespace AlhambraScoringAndroid.UI
{
    //TODO usunąć
    public class DetailsRowHeader : TableRow
    {
        protected  int ResourceLayout => Resource.Layout.details_row_header;

        public DetailsRowHeader(Android.Content.Context context, IAttributeSet attrs) : base(context, attrs)
        {
            //Inflate(context, ResourceLayout, this);

            LayoutInflater inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            View view = inflater.Inflate(ResourceLayout, null, true);
            this.AddView(view);

            //Must be alphabetically
            TypedArray typedArray = context.ObtainStyledAttributes(attrs, new int[] { Resource.Attribute.labelColor, Resource.Attribute.labelValue });
            string label = typedArray.GetText(1);
            ColorStateList color = typedArray.GetColorStateList(0);
            typedArray.Recycle();

            //DefaultValue = default(T);

            //CreateControls();
            //SetControlsProperties();
            //SetLabel(label);
            //SetColor(color);
        }

        public DetailsRowHeader(Android.Content.Context context) : base(context)
        {
        }


        protected DetailsRowHeader(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
    }
}