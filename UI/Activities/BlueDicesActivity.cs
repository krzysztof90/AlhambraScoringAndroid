using AlhambraScoringAndroid.Options;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidBase.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "@string/blue_dices_combinations", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class BlueDicesActivity : BaseActivity
    {
        private ControlNumberView dice1Control;
        private ControlNumberView dice2Control;
        private ControlNumberView dice3Control;
        private AutoGridLayout grid;
        private Dictionary<int, ImageView> pricesImages;
        private ImageView priceFountain;

        protected override int ContentView => Resource.Layout.activity_blue_dices;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            pricesImages = new Dictionary<int, ImageView>()
            {
                [2] = FindViewById<ImageView>(Resource.Id.price2),
                [3] = FindViewById<ImageView>(Resource.Id.price3),
                [4] = FindViewById<ImageView>(Resource.Id.price4),
                [5] = FindViewById<ImageView>(Resource.Id.price5),
                [6] = FindViewById<ImageView>(Resource.Id.price6),
                [7] = FindViewById<ImageView>(Resource.Id.price7),
                [8] = FindViewById<ImageView>(Resource.Id.price8),
                [9] = FindViewById<ImageView>(Resource.Id.price9),
                [10] = FindViewById<ImageView>(Resource.Id.price10),
                [11] = FindViewById<ImageView>(Resource.Id.price11),
                [12] = FindViewById<ImageView>(Resource.Id.price12),
                [13] = FindViewById<ImageView>(Resource.Id.price13),
                [14] = FindViewById<ImageView>(Resource.Id.price14),
            };
            priceFountain = FindViewById<ImageView>(Resource.Id.priceFountain);

            grid = FindViewById<AutoGridLayout>(Resource.Id.grid);

            dice1Control = FindViewById<ControlNumberView>(Resource.Id.dice1);
            dice2Control = FindViewById<ControlNumberView>(Resource.Id.dice2);
            dice3Control = FindViewById<ControlNumberView>(Resource.Id.dice3);

            dice1Control.SetNumberRange<SettingsType>(0, 6, null);
            dice2Control.SetNumberRange<SettingsType>(0, 6, null);
            dice3Control.SetNumberRange<SettingsType>(0, 6, null);

            dice1Control.OnValueChange = () => { ShowAvailablePrices(); };
            dice2Control.OnValueChange = () => { ShowAvailablePrices(); };
            dice3Control.OnValueChange = () => { ShowAvailablePrices(); };

            ShowAvailablePrices();
        }

        private void ShowAvailablePrices()
        {
            int dice1 = dice1Control.Value;
            int dice2 = dice2Control.Value;
            int dice3 = dice3Control.Value;

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