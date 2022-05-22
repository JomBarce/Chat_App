using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using Firebase;
using AndroidX.AppCompat.App;

namespace Chat_App.Droid
{
    [Activity(Label = "Chat_App", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
            var density = Resources.DisplayMetrics.Density;
            App.screenWidth = Resources.DisplayMetrics.WidthPixels / density;
            App.screenHeight = Resources.DisplayMetrics.HeightPixels / density;

            if (Device.Idiom == TargetIdiom.Phone)
            {
                App.screenHeight = (16 * App.screenWidth) / 9;
            }

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                App.screenWidth = (9 * App.screenHeight) / 16;
            }

            FirebaseApp.InitializeApp(this);

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}