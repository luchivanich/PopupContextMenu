using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace PopupContextMenu
{
    public class ContextMenuButton : Button
    {
        private ContextMenuPage _contextMenuPage;

        #region ItemsContainerWidth

        public static readonly BindableProperty ItemsContainerWidthProperty = BindableProperty.Create(
            nameof(ItemsContainerWidth),
            typeof(double),
            typeof(ContextMenuButton),
            propertyChanged: OnItemsContainerWidthChanged);

        private static void OnItemsContainerWidthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ContextMenuButton contextMenuButton && newValue is double newValueDouble &&
                contextMenuButton._contextMenuPage != null)
            {
                contextMenuButton._contextMenuPage.ItemsContainerWidth= newValueDouble;
            }
        }

        public double ItemsContainerWidth
        {
            get { return (double)GetValue(ItemsContainerWidthProperty); }
            set { SetValue(ItemsContainerWidthProperty, value); }
        }

        #endregion

        #region ItemsContainerHeight

        public static readonly BindableProperty ItemsContainerHeightProperty = BindableProperty.Create(
            nameof(ItemsContainerHeight),
            typeof(double),
            typeof(ContextMenuButton),
            propertyChanged: OnItemsContainerHeightChanged);

        private static void OnItemsContainerHeightChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ContextMenuButton contextMenuButton && newValue is double newValueDouble &&
                contextMenuButton._contextMenuPage != null)
            {
                contextMenuButton._contextMenuPage.ItemsContainerHeight = newValueDouble;
            }
        }

        public double ItemsContainerHeight
        {
            get { return (double)GetValue(ItemsContainerHeightProperty); }
            set { SetValue(ItemsContainerHeightProperty, value); }
        }

        #endregion

        #region Items

        private IEnumerable<MenuItem> _items;
        public IEnumerable<MenuItem> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public ContextMenuButton()
            : base()
        {
            Clicked += ContextMenuButton_Clicked;
        }

        private void ContextMenuButton_Clicked(object sender, EventArgs e)
        {
            _contextMenuPage = new ContextMenuPage(Items);
            _contextMenuPage.ItemsContainerHeight = ItemsContainerHeight;
            _contextMenuPage.ItemsContainerWidth = ItemsContainerWidth;
            SetContextMenuPosition();
            Navigation.PushPopupAsync(_contextMenuPage);
        }

        private void SetContextMenuPosition()
        {
            var coordinates = GetCoordinates.Invoke();
            var leftOffset = (int)(Width / 2);
            var topOffset = (int)(Height / 2);
            var leftCenter = coordinates.x + leftOffset;
            var topCenter = coordinates.y + topOffset;
            _contextMenuPage.SetPosition(leftCenter, leftOffset, topCenter, topOffset);
        }

        public Func<(int x, int y)> GetCoordinates = null;

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            foreach(var item in Items)
            {
                item.BindingContext = this.BindingContext;
            }
        }
    }
}
