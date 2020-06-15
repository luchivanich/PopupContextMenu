using System;
using PopupContextMenu;
using PopupContextMenu.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContextMenuButton), typeof(ContextMenuButtonRenderer))]
namespace PopupContextMenu.iOS.Renderers
{
    public class ContextMenuButtonRenderer : ButtonRenderer
    {
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
            return ((int)Frame.Left, (int)Frame.Top);
        }
    }
}
