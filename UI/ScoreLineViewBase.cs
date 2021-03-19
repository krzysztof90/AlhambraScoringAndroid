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

    public abstract class ScoreLineViewBase<T> : LinearLayout, IScoreLineView
    {
        public T DefaultValue { get; set; }
        protected T StoredValue { get; private set; }
        public Action OnValueChange;

        protected abstract int ResourceLayout { get; }
        protected abstract void CreateControls();
        protected abstract void SetControlsProperties();
        protected abstract void SetLabel(string label);
        protected abstract void SetValue(T value);
        protected abstract T GetValue();

        public ScoreLineViewBase(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Inflate(context, ResourceLayout, this);

            TypedArray typedArray = context.ObtainStyledAttributes(attrs, new int[] { Resource.Attribute.labelValue });
            string label = typedArray.GetText(0);
            typedArray.Recycle();

            DefaultValue = default(T);

            CreateControls();
            SetControlsProperties();
            SetLabel(label);
        }

        public void Initialize()
        {
            StoredValue = DefaultValue;
        }

        protected void AssignStoredValue()
        {
            StoredValue = GetValue();
        }

        public void RestoreValue()
        {
            Value = StoredValue;
        }

        public T Value
        {
            get
            {
                return StoredValue;
            }
            set
            {
                SetValue(value);
            }
        }
    }
}