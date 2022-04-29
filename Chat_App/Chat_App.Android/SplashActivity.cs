using Android.App;
using AndroidX.AppCompat.App;

namespace Chat_App.Droid
{
    [Activity(Label = "Chat_App", Icon = "@mipmap/icon", Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(typeof(MainActivity));
        }
    }
}