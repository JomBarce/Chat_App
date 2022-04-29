using Android.Content;
using Android.Widget;
using Chat_App.CustomRenderers;
using Chat_App.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessSearch), typeof(BorderlessSearchRenderer))]
namespace Chat_App.Droid.Renderers
{
    public class BorderlessSearchRenderer :  SearchBarRenderer
    {
        public BorderlessSearchRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            //Configure native control (TextBox)
            if (Control != null)
            {
                Control.Background = null;
                /*
                var searchView = base.Control as SearchView;
                int searchIconId = Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
                ImageView searchViewIcon = (ImageView)searchView.FindViewById<ImageView>(searchIconId);
                searchViewIcon.SetImageDrawable(null);
                */
            }

            // Configure Entry properties
            if (e.NewElement != null)
            {

            }
        }
    }
}