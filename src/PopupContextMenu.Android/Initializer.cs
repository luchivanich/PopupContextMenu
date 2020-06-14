using Android.Content;
using Android.OS;

namespace PopupContextMenu.Droid
{
    public static class Initializer
    {
        public static void Init(Context context, Bundle savedInstanceState)
        {
            Rg.Plugins.Popup.Popup.Init(context, savedInstanceState);
        }
    }
}