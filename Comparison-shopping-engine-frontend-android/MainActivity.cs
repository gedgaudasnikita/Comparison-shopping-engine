using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using Android.Provider;
using System.Collections.Generic;
using Android.Content.PM;
using Java.IO;
using Android.Graphics;

namespace Comparison_shopping_engine_frontend_android
{
    [Activity(Label = "Comparison_shopping_engine_frontend_android", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private OcrWrapper ocr;
        ImageView imageView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Home);

            // Set up Buttons
            Button homeCameraButton = FindViewById<Button>(Resource.Id.homeCameraButton);
            Button homeResultScreenButton = FindViewById<Button>(Resource.Id.homeNextButton);
            imageView = FindViewById<ImageView>(Resource.Id.homeImageView);

            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();

                homeCameraButton.Click += OnHomeCameraButtonClick;
            }

            // Short-term solution for testing, when no camera is found, camera button is red
            else
            {
                homeCameraButton.SetBackgroundColor(Color.DarkRed);
            }

            ocr = new OcrWrapper(this);
        }

        /// <summary>
        /// static class used for storing camera picture data and save location
        /// </summary>
        public static class App
        {
            public static File file;
            public static File dir;
            public static Bitmap bitmap;
        }

        /// <summary>
        /// Checks if device is able to call camera app
        /// </summary>
        /// <returns>True if device is able to call camera app</returns>
        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        /// <summary>
        /// Self explanatory
        /// </summary>
        private void CreateDirectoryForPictures()
        {
            App.dir = new File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "CoShE Pictures");
            if (!App.dir.Exists())
                App.dir.Mkdirs();
        }

        /// <summary>
        /// Call to camera, to get a photo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHomeCameraButtonClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            App.file = new File(App.dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(App.file));
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            // Make it available in the gallery

            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Android.Net.Uri contentUri = Android.Net.Uri.FromFile(App.file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            // Display in ImageView. We will resize the bitmap to fit the display.
            // Loading the full sized image will consume to much memory
            // and cause the application to crash.

            int height = Resources.DisplayMetrics.HeightPixels;
            int width = imageView.Height;
            App.bitmap = App.file.Path.LoadAndResizeBitmap(width, height);
            if (App.bitmap != null)
            {
                imageView.SetImageBitmap(App.bitmap);
                App.bitmap = null;
            }

            // Dispose of the Java side bitmap.
            GC.Collect();
        }

    }

    public static class BitmapHelpers
    {
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

            return resizedBitmap;
        }
    }
}

