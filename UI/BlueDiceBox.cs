using Android.Content;
using Android.Util;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI
{
    public class BlueDiceBox : LinearLayout
    {
        private Dictionary<int, (ImageView imageView, int notSelectedImage, int selectedImage)> pipsImages;

        public int? SelectedValue { get; private set; }
        public int? Value
        {
            get
            {
                return SelectedValue;
            }
            set
            {
                RemoveCurrentSelection();
                SelectedValue = value;
                SetCurrentSelection();
            }
        }
        public Action OnValueChange;

        protected int ResourceLayout => Resource.Layout.blue_dice;

        public BlueDiceBox(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Inflate(context, ResourceLayout, this);

            pipsImages = new Dictionary<int, (int resource, int notSelectedImage, int selectedImage)>()
            {
                [1] = (Resource.Id.pips1, Resource.Drawable.Dice1, Resource.Drawable.DiceNegative1),
                [2] = (Resource.Id.pips2, Resource.Drawable.Dice2, Resource.Drawable.DiceNegative2),
                [3] = (Resource.Id.pips3, Resource.Drawable.Dice3, Resource.Drawable.DiceNegative3),
                [4] = (Resource.Id.pips4, Resource.Drawable.Dice4, Resource.Drawable.DiceNegative4),
                [5] = (Resource.Id.pips5, Resource.Drawable.Dice5, Resource.Drawable.DiceNegative5),
                [6] = (Resource.Id.pips6, Resource.Drawable.Dice6, Resource.Drawable.DiceNegative6),
            }.ToDictionary(d => d.Key, d => (FindViewById<ImageView>(d.Value.resource), d.Value.notSelectedImage, d.Value.selectedImage));

            foreach (KeyValuePair<int, (ImageView imageView, int notSelectedImage, int selectedImage)> imagePair in pipsImages)
            {
                ImageView imageView = imagePair.Value.imageView;

                imageView.Click += new EventHandler((object sender, EventArgs e) =>
                {
                    RemoveCurrentSelection();

                    if (SelectedValue == imagePair.Key)
                        SelectedValue = null;
                    else
                        SelectedValue = imagePair.Key;

                    SetCurrentSelection();
                });
            }
        }

        private void RemoveCurrentSelection()
        {
            if (SelectedValue != null)
                pipsImages[(int)SelectedValue].imageView.SetImageResource(pipsImages[(int)SelectedValue].notSelectedImage);

            //OnValueChange?.Invoke();
        }

        private void SetCurrentSelection()
        {
            if (SelectedValue != null)
                pipsImages[(int)SelectedValue].imageView.SetImageResource(pipsImages[(int)SelectedValue].selectedImage);

            OnValueChange?.Invoke();
        }
    }
}