using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;

namespace Chat_App.Droid
{
    [Activity(Label = "Chat_App", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
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
            
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}