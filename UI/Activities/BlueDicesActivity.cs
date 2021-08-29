using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "@string/blue_dices_combinations", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class BlueDicesActivity : BaseActivity
    {
        private ControlBlueDices dice1Control;
        private ControlBlueDices dice2Control;
        private ControlBlueDices dice3Control;
        private Dictionary<int, ImageView> pricesImages;
        private ImageView priceFountain;

        protected override int ContentView => Resource.Layout.activity_blue_dices;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            pricesImages = new Dictionary<int, int>()
            {
                [2] = Resource.Id.price2,
                [3] = Resource.Id.price3,
                [4] = Resource.Id.price4,
                [5] = Resource.Id.price5,
                [6] = Resource.Id.price6,
                [7] = Resource.Id.price7,
                [8] = Resource.Id.price8,
                [9] = Resource.Id.price9,
                [10] = Resource.Id.price10,
                [11] = Resource.Id.price11,
                [12] = Resource.Id.price12,
                [13] = Resource.Id.price13,
                [14] = Resource.Id.price14,
            }.ToDictionary(d => d.Key, d => FindViewById<ImageView>(d.Value));

            priceFountain = FindViewById<ImageView>(Resource.Id.priceFountain);

            dice1Control = FindViewById<ControlBlueDices>(Resource.Id.dice1);
            dice2Control = FindViewById<ControlBlueDices>(Resource.Id.dice2);
            dice3Control = FindViewById<ControlBlueDices>(Resource.Id.dice3);

            dice1Control.OnValueChange = () => { ShowAvailablePrices(); };
            dice2Control.OnValueChange = () => { ShowAvailablePrices(); };
            dice3Control.OnValueChange = () => { ShowAvailablePrices(); };

            Button resetButton = FindViewById<Button>(Resource.Id.resetButton);
            resetButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                dice1Control.ResetValue();
                dice2Control.ResetValue();
                dice3Control.ResetValue();
                dice1Control.SetFocus();
            });

            ShowAvailablePrices();
        }

        private void ShowAvailablePrices()
        {
            int dice1 = dice1Control.Value;
            int dice2 = dice2Control.Value;
            int dice3 = dice3Control.Value;

            if (dice1 == 0)
            {
                if (dice2 != 0)
                {
                    if (dice3 != 0)
                    {
                        dice1 = dice3;
                        dice3 = 0;
                    }
                    else
                    {
                        dice1 = dice2;
                        dice2 = 0;
                    }
                }
                else if (dice3 != 0)
                {
                    dice1 = dice3;
                    dice3 = 0;
                }
            }
            else if (dice2 == 0)
            {
                if (dice3 != 0)
                {
                    dice2 = dice3;
                    dice3 = 0;
                }
            }

            List<int> availablePrices = new List<int>();
            if (dice1 != 0)
            {
                List<int> available1 = new List<int>() { dice1 };
                availablePrices = available1.Distinct().ToList();
                if (dice2 != 0)
                {
                    List<int> available2 = new List<int>();
                    foreach (int available in available1)
                    {
                        available2.Add(available + dice2);
                        available2.Add(Math.Abs(available - dice2));
                    }
                    availablePrices = available2.Distinct().ToList();
                    if (dice3 != 0)
                    {
                        List<int> available3 = new List<int>();
                        foreach (int available in available2)
                        {
                            available3.Add(available + dice3);
                            available3.Add(Math.Abs(available - dice3));
                        }
                        availablePrices = available3.Distinct().ToList();
                    }
                }
            }

            bool wild = dice1 != 0 && dice1 == dice2 && dice1 == dice3;

            foreach (KeyValuePair<int, ImageView> priceImage in pricesImages)
            {
                priceImage.Value.Visibility = availablePrices.Contains(priceImage.Key) || wild ? ViewStates.Visible : ViewStates.Invisible;
            }
            priceFountain.Visibility = wild ? ViewStates.Visible : ViewStates.Invisible;
        }
    }
}