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
using Android.Media;
using Comparison_shopping_engine_core_entities;

namespace Comparison_shopping_engine_frontend_android
{
    [Activity(Label = "CoShE Home", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private OcrWrapper ocr;
        Button homeCameraButton;
        Button homeGalleryButton;
        Button homeResultScreenButton;
        ImageView imageView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "Home" layout resource
            SetContentView(Resource.Layout.Home);

            // Reset App class for safety reasons
            App.file = null;
            App.dir = null;
            App.bitmap = null;

            // Set up Buttons
            homeCameraButton = FindViewById<Button>(Resource.Id.homeCameraButton);
            homeGalleryButton = FindViewById<Button>(Resource.Id.homeGalleryButton);
            homeResultScreenButton = FindViewById<Button>(Resource.Id.homeResultScreenButton);
            imageView = FindViewById<ImageView>(Resource.Id.homeImageView);

            // Check if camera is available
            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();

                homeCameraButton.Click += OnHomeCameraButtonClick;
            }

            // Disable camera button if no camera is available
            else
            {
                homeCameraButton.Enabled = false;
                homeCameraButton.Clickable = false;
            }

            homeGalleryButton.Click += OnHomeGalleryButtonClick;
            homeResultScreenButton.Click += OnHomeResultsScreenButtonClick;

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
            //If orientation is changed, when in gallery or camera app, imageView has a height and width of 0, so I'm storing these separately
            public static int imageViewHeight;
            public static int imageViewWidth;
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
        /// Call to camera app, to get a photo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHomeCameraButtonClick(object sender, EventArgs e)
        {
            App.imageViewHeight = imageView.Height;
            App.imageViewWidth = imageView.Width;
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            App.file = new File(App.dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(App.file));
            StartActivityForResult(intent, 0);
        }

        /// <summary>
        /// Call to photo gallery app, to get a photo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHomeGalleryButtonClick(object sender, EventArgs e)
        {
            App.imageViewHeight = imageView.Height;
            App.imageViewWidth = imageView.Width;
            Intent imageIntent = new Intent();
            imageIntent.SetType("image/*");
            imageIntent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), 1);
        }

        /// <summary>
        /// Go to Results screen, if we have a photo, generate receipt and display as a list of items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnHomeResultsScreenButtonClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ResultsActivity));

            // Generate receipt out of image, if we have one
            if (App.bitmap != null)
            {
                string receiptText = null;
                bool initialized = await ocr.Initialize();

                // TODO: exception or a pop-up to inform user that OCR failed
                if (initialized)
                {
                    receiptText = await ocr.ConvertToText(App.bitmap);
                }

                // Just in case OCR managed to fuck up
                if (receiptText != null && receiptText != "")
                {
                    intent.PutExtra("ReceiptText", receiptText);
                }
            }

            StartActivity(intent);
        }

        /// <summary>
        /// Handles data returned from Android apps
        /// </summary>
        /// <param name="requestCode">Intent code, used to distinguish between different activities</param>
        /// <param name="resultCode">Code that stores info about the result itself</param>
        /// <param name="data">Data returned by Intent activity</param>
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            switch (requestCode)
            {
                // 0 - call Camera
                case 0:
                    if (resultCode == Result.Ok)
                    {
                        Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                        Android.Net.Uri contentUri = Android.Net.Uri.FromFile(App.file);
                        mediaScanIntent.SetData(contentUri);
                        SendBroadcast(mediaScanIntent);

                        // Display in ImageView. We will resize the bitmap to fit the display.
                        // Loading the full sized image will consume to much memory
                        // and cause the application to crash.
                        App.bitmap = App.file.Path.LoadAndResizeBitmap(App.imageViewWidth, App.imageViewHeight);
                        if (App.bitmap != null)
                        {
                            imageView.SetImageBitmap(App.bitmap);
                        }

                        // Dispose of the Java side bitmap.
                        GC.Collect();

                        homeResultScreenButton.Text = "Submit Photo";
                    }
                    break;
                //1 - call Gallery
                case 1:
                    if (resultCode == Result.Ok)
                    {
                        App.bitmap = GetPathToImage(data.Data).LoadAndResizeBitmap(App.imageViewWidth, App.imageViewHeight);
                        imageView.SetImageBitmap(App.bitmap);

                        homeResultScreenButton.Text = "Submit Photo";
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Self explanatory
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private string GetPathToImage(Android.Net.Uri uri)
        {
            string path = null;
            // The projection contains the columns we want to return in our query.
            var projection = new[] { Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data };
            using (var cursor = ManagedQuery(uri, projection, null, null, null))
            {
                if (cursor == null) return path;
                var columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
                cursor.MoveToFirst();
                path = cursor.GetString(columnIndex);
            }
            return path;
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

            resizedBitmap = CheckAndRotateBitmap(fileName, resizedBitmap);

            return resizedBitmap;
        }

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
                    mtx.PreRotate(90);
                    rotatedBitmap = Bitmap.CreateBitmap(rotatedBitmap, 0, 0, rotatedBitmap.Width, rotatedBitmap.Height, mtx, false);
                    mtx.Dispose();
                    mtx = null;
                    break;
            }

            return rotatedBitmap;
        }
    }
}

