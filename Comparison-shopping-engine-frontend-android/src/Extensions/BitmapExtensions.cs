using Android.Graphics;
using Android.Media;

namespace Comparison_shopping_engine_frontend_android
{
    public static class BitmapExtensions
    {
        /// <summary>
        /// Loads and resizes bitmap according to homeImageView width and height
        /// </summary>
        /// <param name="fileName">Uri of Bitmap</param>
        /// <param name="width">ImageView width</param>
        /// <param name="height">ImageView height</param>
        /// <returns></returns>
        public static Bitmap LoadAndResizeBitmap(this string fileName, int width, int height)
        {
            // First we get the the dimensions of the file on disk
            BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
            BitmapFactory.DecodeFile(fileName, options);

            // Next we calculate the ratio that we need to resize the image by
            // in order to fit the requested dimensions.
            int outHeight = options.OutHeight;
            int outWidth = options.OutWidth;
            int inSampleSize = 1;
            if (outHeight > height || outWidth > width)
            {
                inSampleSize = outWidth > outHeight
                                   ? outHeight / height
                                   : outWidth / width;
            }

            // Now we will load the image and have BitmapFactory resize it for us.
            options.InSampleSize = inSampleSize;
            options.InJustDecodeBounds = false;
            Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);

            resizedBitmap = CheckAndRotateBitmap(fileName, resizedBitmap);

            return resizedBitmap;
        }

        // TODO: for now only used by LoadAndResizeBitmap, change, so it could be called on it's own without providing already loaded Bitmap
        /// <summary>
        /// Checks provided bitmaps orientation and rotates if necessary
        /// </summary>
        /// <param name="fileName">Bitmap Uri, used to determine original orientation</param>
        /// <param name="rotatedBitmap">Bitmap itself</param>
        /// <returns></returns>
        public static Bitmap CheckAndRotateBitmap(this string fileName, Bitmap rotatedBitmap)
        {
            // Check photo orientation and rotate if necessary
            Matrix mtx = new Matrix();
            ExifInterface exif = new ExifInterface(fileName);
            string orientation = exif.GetAttribute(ExifInterface.TagOrientation);

            switch (orientation)
            {
                case "6": // portrait
                    mtx.PreRotate(90);
                    rotatedBitmap = Bitmap.CreateBitmap(rotatedBitmap, 0, 0, rotatedBitmap.Width, rotatedBitmap.Height, mtx, false);
                    mtx.Dispose();
                    mtx = null;
                    break;
                case "1": // landscape
                    break;
                default:
                    /*mtx.PreRotate(90);
                    rotatedBitmap = Bitmap.CreateBitmap(rotatedBitmap, 0, 0, rotatedBitmap.Width, rotatedBitmap.Height, mtx, false);
                    mtx.Dispose();
                    mtx = null;*/
                    break;
            }

            return rotatedBitmap;
        }
    }
}