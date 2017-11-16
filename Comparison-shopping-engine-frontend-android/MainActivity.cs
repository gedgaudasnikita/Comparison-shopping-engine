using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using Android.Provider;
using System.Collections.Generic;
using Android.Content.PM;
using Java.IO;
using Android.Views;

namespace Comparison_shopping_engine_frontend_android
{
    [Activity(Label = "CoShE Home", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private Lazy<OcrWrapper> ocr;
        Button homeCameraButton;
        Button homeGalleryButton;
        Button homeResultScreenButton;
        Button homeConfigButton;
        ImageView imageView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "Home" layout resource
            SetContentView(Resource.Layout.Home);

            // Reset App class for safety reasons
            AppData.file = null;
            AppData.dir = null;
            AppData.bitmap = null;
            

            // Set up Buttons
            homeCameraButton = FindViewById<Button>(Resource.Id.homeCameraButton);
            homeGalleryButton = FindViewById<Button>(Resource.Id.homeGalleryButton);
            homeResultScreenButton = FindViewById<Button>(Resource.Id.homeResultScreenButton);
            homeConfigButton = FindViewById<Button>(Resource.Id.homeConfigButton);
            imageView = FindViewById<ImageView>(Resource.Id.homeImageView);

            // Make imageView invisible while there's no photo
            imageView.Visibility = Android.Views.ViewStates.Invisible;

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
            }

            homeGalleryButton.Click += OnHomeGalleryButtonClick;
            homeResultScreenButton.Click += OnHomeResultsScreenButtonClick;

            ocr = new Lazy<OcrWrapper>(() => new OcrWrapper(this));

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
        /// Creates a directory on phone for taken pictures
        /// </summary>
        private void CreateDirectoryForPictures()
        {
            AppData.dir = new File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "CoShE Pictures");
            if (!AppData.dir.Exists())
                AppData.dir.Mkdirs();
        }

        /// <summary>
        /// Call to camera app, to get a photo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHomeCameraButtonClick(object sender, EventArgs e)
        {
            AppData.imageViewHeight = imageView.Height;
            AppData.imageViewWidth = imageView.Width;
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            AppData.file = new File(AppData.dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(AppData.file));
            StartActivityForResult(intent, 0);
        }

        /// <summary>
        /// Call to photo gallery app, to get a photo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHomeGalleryButtonClick(object sender, EventArgs e)
        {
            AppData.imageViewHeight = imageView.Height;
            AppData.imageViewWidth = imageView.Width;
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
            if (AppData.bitmap != null)
            {
                string receiptText = null;
                bool initialized = await ocr.Value.Initialize();

                // TODO: exception or a pop-up to inform user that OCR failed
                if (initialized)
                {
                    receiptText = await ocr.Value.ConvertToText(AppData.bitmap);
                }

                // Just in case OCR managed to fuck up
                if (receiptText != null && receiptText != "")
                {
                    intent.PutExtra("ReceiptText", receiptText);
                }
            }

            StartActivity(intent);
        }

        private void OnHomeConfigButtonClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ConfigActivity));

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
                        Android.Net.Uri contentUri = Android.Net.Uri.FromFile(AppData.file);
                        mediaScanIntent.SetData(contentUri);
                        SendBroadcast(mediaScanIntent);

                        // Display in ImageView. We will resize the bitmap to fit the display.
                        // Loading the full sized image will consume to much memory
                        // and cause the application to crash.
                        AppData.bitmap = AppData.file.Path.LoadAndResizeBitmap(AppData.imageViewWidth, AppData.imageViewHeight);
                        if (AppData.bitmap != null)
                        {
                            imageView.SetImageBitmap(AppData.bitmap);
                            imageView.Visibility = Android.Views.ViewStates.Visible;
                        }

                        // Not sure if needed, source had it, better keep it in case.
                        GC.Collect();

                        homeResultScreenButton.Text = "Submit Photo";
                    }
                    break;
                //1 - call Gallery
                case 1:
                    if (resultCode == Result.Ok)
                    {
                        AppData.bitmap = GetPathToImage(data.Data).LoadAndResizeBitmap(AppData.imageViewWidth, AppData.imageViewHeight);
                        imageView.SetImageBitmap(AppData.bitmap);
                        imageView.Visibility = Android.Views.ViewStates.Visible;

                        homeResultScreenButton.Text = "Submit Photo";
                    }
                    break;
                default:
                    break;
            }
        }

        // Probably could be turned into extension method, but I'm not sure how to acomplish that
        /// <summary>
        /// Return string path to image from Uri
        /// </summary>
        /// <param name="uri">Uri of Bitmap, from which path will be provided</param>
        /// <returns></returns>
        private string GetPathToImage(Android.Net.Uri uri)
        {
            string doc_id = "";
            using (var c1 = ContentResolver.Query(uri, null, null, null, null))
            {
                c1.MoveToFirst();
                String document_id = c1.GetString(0);
                doc_id = document_id.Substring(document_id.LastIndexOf(":") + 1);
            }

            string path = null;

            // The projection contains the columns we want to return in our query.
            string selection = Android.Provider.MediaStore.Images.Media.InterfaceConsts.Id + " =? ";
            using (var cursor = ManagedQuery(Android.Provider.MediaStore.Images.Media.ExternalContentUri, null, selection, new string[] { doc_id }, null))
            {
                if (cursor == null) return path;
                var columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
                cursor.MoveToFirst();
                path = cursor.GetString(columnIndex);
            }
            return path;
        }
    }
}

