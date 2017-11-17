using Java.IO;
using Android.Graphics;

namespace Comparison_shopping_engine_frontend_android
{
    /// <summary>
    /// static class used for storing camera picture data and save location
    /// </summary>
    public static class AppData
    {
        public static File file;
        public static File dir;
        public static Bitmap bitmap;
        //If orientation is changed, when in gallery or camera app, imageView has a height and width of 0, so I'm storing these separately
        public static int imageViewHeight;
        public static int imageViewWidth;

        public static int theme;
    }
}