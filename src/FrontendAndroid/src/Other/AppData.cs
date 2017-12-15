using Java.IO;
using Android.Graphics;
using System;
using Comparison_shopping_engine_core_entities;

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
        public static Receipt receipt;
        //If orientation is changed, when in gallery or camera app, homeImageView has a height and width of 0, so I'm storing these separately
        public static int imageViewHeight;
        public static int imageViewWidth;

        public static int theme;

        private static HistoryDatabase database;
        public static HistoryDatabase Database
        {
            get
            {
                if (database == null)
                {
                    System.Console.WriteLine("hey");
                    database = new HistoryDatabase(System.IO.Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                        Configuration.dbPath));
                }

                return database;
            }
        }
    }
}