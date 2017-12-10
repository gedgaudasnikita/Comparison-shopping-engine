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
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Android.Gms.Common;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;

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
        ImageView homeImageView;
        TextView homeTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RetrieveConfig();
            SetTheme(AppData.theme);
            base.OnCreate(savedInstanceState);

            
            // Set our view from the "Home" layout resource
            SetContentView(Resource.Layout.Home);

            // Reset App class for safety reasons
            AppData.file = null;
            AppData.dir = null;
            AppData.bitmap = null;
            

            // Set up Elements
            homeCameraButton = FindViewById<Button>(Resource.Id.homeCameraButton);
            homeGalleryButton = FindViewById<Button>(Resource.Id.homeGalleryButton);
            homeResultScreenButton = FindViewById<Button>(Resource.Id.homeResultScreenButton);
            homeConfigButton = FindViewById<Button>(Resource.Id.homeConfigButton);
            homeImageView = FindViewById<ImageView>(Resource.Id.homeImageView);
            homeTextView = FindViewById<TextView>(Resource.Id.homeTextView);

            Localise();

            // Make homeImageView invisible while there's no photo
            homeImageView.Visibility = Android.Views.ViewStates.Invisible;

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
            homeConfigButton.Click += OnHomeConfigButtonClick;

            ocr = new Lazy<OcrWrapper>(() => new OcrWrapper(this));
        }

        protected void Localise()
        {
            homeCameraButton.Text = AppResources.CameraButton;
            homeGalleryButton.Text = AppResources.FromGalleryButton;
            homeResultScreenButton.Text = AppResources.ResultScreenButton;
            homeConfigButton.Text = AppResources.ConfigButton;
            homeTextView.Text = AppResources.HomeScreenText;
        }

        protected override void OnDestroy()
        {
            SaveConfig();
            base.OnDestroy();
        }

        protected override void OnRestart()
        {
            SaveConfig();
            base.OnRestart();
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
            AppData.imageViewHeight = homeImageView.Height;
            AppData.imageViewWidth = homeImageView.Width;
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
            AppData.imageViewHeight = homeImageView.Height;
            AppData.imageViewWidth = homeImageView.Width;
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

                receiptText = await UiHelpers.ExecuteWithProgressDialog<string>
                (
                    async (IEnumerable<Action<int>> progressListeners) => 
                    {
                        bool initialized = await ocr.Value.Initialize();

                        //Partial result because tesseract starts the event processing at the odd 40 something percent
                        progressListeners.ToList().ForEach(listener => listener(30));

                        if (initialized)
                        {
                            return await ocr.Value.ConvertToText(AppData.bitmap, progressListeners);
                        }
                        else
                        {
                            return "";
                        }
                    }, 
                    AppResources.ConvertProgress,
                    this
                );

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

            StartActivityForResult(intent, 2);
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
                            homeImageView.SetImageBitmap(AppData.bitmap);
                            homeImageView.Visibility = Android.Views.ViewStates.Visible;
                        }

                        // Not sure if needed, source had it, better keep it in case.
                        GC.Collect();

                        homeResultScreenButton.Text = AppResources.SubmitPhotoButton;
                    }
                    break;
                //1 - call Gallery
                case 1:
                    if (resultCode == Result.Ok)
                    {
                        AppData.bitmap = GetPathToImage(data.Data).LoadAndResizeBitmap(AppData.imageViewWidth, AppData.imageViewHeight);
                        homeImageView.SetImageBitmap(AppData.bitmap);
                        homeImageView.Visibility = Android.Views.ViewStates.Visible;

                        homeResultScreenButton.Text = AppResources.SubmitPhotoButton;
                    }
                    break;
                //2 - config screen
                case 2:
                    Recreate();
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

        private void SaveConfig()
        {
            //Store app preferences
            var prefs = Application.Context.GetSharedPreferences("CoShE", FileCreationMode.Private);
            var prefEditor = prefs.Edit();
            prefEditor.PutInt("Theme", AppData.theme);
            prefEditor.Commit();
        }

        private void RetrieveConfig()
        {
            //Retrieve app preferences
            var prefs = Application.Context.GetSharedPreferences("CoShE", FileCreationMode.Private);
            AppData.theme = prefs.GetInt("Theme", Android.Resource.Style.ThemeMaterial);
        }
    }
}

