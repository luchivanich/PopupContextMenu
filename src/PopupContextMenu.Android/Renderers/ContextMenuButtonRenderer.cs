using PopupContextMenu;
using PopupContextMenu.Droid.Renderers;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ContextMenuButton), typeof(ContextMenuButtonRenderer))]
namespace PopupContextMenu.Droid.Renderers
{
    public class ContextMenuButtonRenderer : ButtonRenderer
    {
        public ContextMenuButtonRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Element is ContextMenuButton contextButton)
            {
                contextButton.GetCoordinates = GetCoordinatesNative;
            }
        }

        private (int x, int y) GetCoordinatesNative()
        {
            var displayMetrics = Resources.DisplayMetrics;
            var density = displayMetrics.Density;

            var screenHeight = displayMetrics.HeightPixels;
            var appHeight = Application.Current.MainPage.Height;
            var heightOffset = screenHeight/density - appHeight;

            var coords = new int[2];
            GetLocationOnScreen(coords);
            return ((int)(coords[0]/density), (int)(coords[1]/density) - (int)heightOffset);
        }
    }
}