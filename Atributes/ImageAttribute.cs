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
    }
}