using Android.Content;
using Android.Content.Res;
using Android.Util;
using Android.Widget;
using System;

namespace AlhambraScoringAndroid.UI
{
    public interface IScoreLineView
    {
        public void Initialize();
        public void RestoreValue();
    }

    public abstract class ScoreLineViewBase<T> : LinearLayout, IScoreLineView where T : struct
    {
        public T DefaultValue { get; set; }
        protected T? StoredValue { get; private set; }
        public Action OnValueChange;

        protected abstract int ResourceLayout { get; }
        protected abstract void CreateControls();
        protected abstract void SetControlsProperties();
        protected abstract void SetLabelAndColor(string label, ColorStateList color);
        protected abstract void SetValue(T? value);
        protected abstract T? GetValue();

        public ScoreLineViewBase(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Inflate(context, ResourceLayout, this);

            //TODO zmiana textColor na labelColor
            TypedArray typedArray = context.ObtainStyledAttributes(attrs, new int[] { Resource.Attribute.labelValue, Resource.Attribute.textColor });
            string label = typedArray.GetText(0);
            ColorStateList color = typedArray.GetColorStateList(1);
            typedArray.Recycle();

            DefaultValue = default(T);

            CreateControls();
            SetControlsProperties();
            SetLabelAndColor(label, color);
        }

        public void Initialize()
        {
            //StoredValue = DefaultValue;
            StoredValue = null;
        }

        protected void AssignStoredValue()
        {
            StoredValue = GetValue();
        }

        public void RestoreValue()
        {
            //Value = StoredValue;
                SetValue(StoredValue);
        }

        public T Value
        {
            get
            {
                return StoredValue??DefaultValue;
            }
            set
            {
                SetValue(value);
            }
        }
    }
}