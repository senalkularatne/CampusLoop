using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Graphics.Drawables;
using Plugin.Permissions;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using Xamarin.Forms;

/* MainActivity for droid handles some unique android only procedures
 */

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(UEApp.Droid.FlatButtonRenderer))]
namespace UEApp.Droid
{
    [Activity(Theme = "@style/MyTheme", Label = "campusLoop", Icon = "@drawable/ic_launcher",
        MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            // set the layout resources first
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;

            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            // Sets the dumb default icon to transparent
            //ActionBar.SetIcon(Android.Resource.Color.Transparent);

            // Colors the action bar the universal red
            //ColorDrawable colorDrawable = new ColorDrawable(Color.ParseColor("#e10118"));
            //ActionBar.SetStackedBackgroundDrawable(colorDrawable);
        }

        // Used for the media plugin
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class FlatButtonRenderer : ButtonRenderer
    {
        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            base.OnDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
        }
    }
}

