using Android.Content.Res;
using Android.Graphics;
using System;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.Attributes
{
    public class ImageAttribute : Attribute
    {
        public const int ImageMaxSize = 50;

        public int Resource { get; set; }

        public static Dictionary<int, Bitmap> CreatedImages;

        static ImageAttribute()
        {
            CreatedImages = new Dictionary<int, Bitmap>();
        }

        public virtual Android.Graphics.Bitmap Image(Resources resources)
        {
            return CreateImage(resources, Resource);
        }

        public ImageAttribute(int resource)
        {
            Resource = resource;
        }

        protected Android.Graphics.Bitmap CreateImage(Resources resources, int resource)
        {
            if (CreatedImages.ContainsKey(resource))
                return CreatedImages[resource];

            Bitmap result = BitmapFactory.DecodeResource(resources, resource/*, decodeSampledBitmapFromResource(resources, resource, 50, 50)*/);
            //Bitmap result = BitmapFactory.DecodeResource(resources, resource);
            double proportion = ((double)Math.Max(result.Height, result.Width)) / ImageMaxSize;
            if (proportion > 1)
            {
                Bitmap tmp = Bitmap.CreateScaledBitmap(result, (int)(result.Width / proportion), (int)(result.Height / proportion), false);
                result.Recycle();
                result = tmp;
            }
            CreatedImages[resource] = result;
            return result;
        }

        //TODO usunąć
        public static BitmapFactory.Options decodeSampledBitmapFromResource(Resources res, int resId, int reqWidth, int reqHeight)
        {
            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InJustDecodeBounds = true;
            BitmapFactory.DecodeResource(res, resId, options);

            // Calculate inSampleSize
            options.InSampleSize = calculateInSampleSize(options, reqWidth, reqHeight);

            // Decode bitmap with inSampleSize set
            options.InJustDecodeBounds = false;

            //options.InScaled = false;

            return options;
        }

        //TODO usunąć
        public static int calculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            // Raw height and width of image
            int height = options.OutHeight;
            int width = options.OutWidth;
            int inSampleSize = 1;

            if (height > reqHeight || width > reqWidth)
            {
                int halfHeight = height / 2;
                int halfWidth = width / 2;

                // Calculate the largest inSampleSize value that is a power of 2 and keeps both
                // height and width larger than the requested height and width.
                while ((halfHeight / inSampleSize) >= reqHeight
                        && (halfWidth / inSampleSize) >= reqWidth)
                {
                    inSampleSize *= 2;
                }
            }

            return inSampleSize;
        }


    }
}