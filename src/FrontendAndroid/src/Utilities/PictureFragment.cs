using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace Comparison_shopping_engine_frontend_android
{
    public class PictureFragment : Fragment
    {
        static ImageView image;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.PictureFragment, container, false);
            image = (ImageView) view.FindViewById(Resource.Id.homeImageView);
            // Make homeImageView invisible while there's no photo
            image.Visibility = ViewStates.Invisible;

            return view;
        }

        public static void SetImage(Bitmap bitmap)
        {
            image.SetImageBitmap(bitmap);
        }

        public static void SetImageVisibility(Android.Views.ViewStates state)
        {
            image.Visibility = state;
        }

        public static int GetImageWidth()
        {
            return image.Width;
        }

        public static int GetImageHeight()
        {
            return image.Height;
        }
    }
}