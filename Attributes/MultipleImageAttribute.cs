using AlhambraScoringAndroid.Tools.Enums;
using Android.Content.Res;
using Android.Graphics;
using static Android.Graphics.Bitmap;

namespace AlhambraScoringAndroid.Attributes
{
    public class MultipleImageAttribute : ImageAttribute
    {
        public HorizontalVertical Orientation { get; set; }
        public int[] Resources { get; set; }

        public MultipleImageAttribute(HorizontalVertical orientation, params int[] resources) : base(0)
        {
            Orientation = orientation;
            Resources = resources;
        }

        public override Android.Graphics.Bitmap Image(Resources resources)
        {
            Bitmap result = null;

            for (int i = 0; i < Resources.Length; i++)
            {
                Bitmap bitmap = CreateImage(resources, Resources[i]);
                if (result == null)
                    result = bitmap;
                else
                {
                    int width;
                    int height;

                    if (Orientation == HorizontalVertical.Horizontal)
                    {
                        width = result.Width + bitmap.Width;
                        height = result.Height;
                    }
                    else
                    {
                        width = result.Width;
                        height = result.Height + bitmap.Height;
                    }

                    Bitmap newResult = Bitmap.CreateBitmap(width, height, Config.Argb8888);

                    Canvas canvas = new Canvas(newResult);
                    canvas.DrawBitmap(result, 0, 0, null);
                    canvas.DrawBitmap(bitmap, Orientation == HorizontalVertical.Horizontal ? result.Width : 0, Orientation == HorizontalVertical.Vertical ? result.Height : 0, null);

                    if (i > 1)
                        result.Recycle();
                    result = newResult;
                }
            }

            return result;
        }
    }
}