using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using static System.Math;

namespace PopupContextMenu
{
    public partial class ContextMenuPage : PopupPage
    {
        private double _width;
        private double _height;


        #region ItemsContainerWidth

        public static readonly BindableProperty ItemsContainerWidthProperty = BindableProperty.Create(
            nameof(ItemsContainerWidth),
            typeof(double),
            typeof(ContextMenuPage),
            propertyChanged: OnItemsContainerWidthChanged);

        private static void OnItemsContainerWidthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ContextMenuPage contextMenuPage && newValue is double newValueDouble)
            {
                contextMenuPage.MainFrame.WidthRequest = newValueDouble;
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
            typeof(ContextMenuPage),
            propertyChanged: OnItemsContainerHeightChanged);

        private static void OnItemsContainerHeightChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ContextMenuPage contextMenuPage && newValue is double newValueDouble)
            {
                contextMenuPage.MainFrame.HeightRequest = newValueDouble;
            }
        }

        public double ItemsContainerHeight
        {
            get { return (double)GetValue(ItemsContainerHeightProperty); }
            set { SetValue(ItemsContainerHeightProperty, value); }
        }

        #endregion


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

        public ContextMenuPage(IEnumerable<MenuItem> items)
        {
            Items = items;
            InitializeComponent();
        }

        public void SetPosition(int leftCenter, int leftOffset, int topCenter, int topOffset)
        {
            var marginTop = GetStartCoordinate((int)Application.Current.MainPage.Height, (int)MainFrame.HeightRequest, topCenter, topOffset);
            var marginLeft = GetStartCoordinate((int)Application.Current.MainPage.Width, (int)MainFrame.WidthRequest, leftCenter, leftOffset);
            MainFrame.Margin = new Thickness(marginLeft, marginTop, 0, 0);
        }

        private int GetStartCoordinate(int screeSize, int menuSize, int baseCoordinate, int shift)
        {
            if (baseCoordinate + shift + menuSize > screeSize)
            {
                return Max(0, baseCoordinate - shift - menuSize);
            }
            return baseCoordinate + shift;
        }

        private async void ItemTapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            if (sender is StackLayout senderControl && senderControl.BindingContext is MenuItem MenuItem)
            {
                await Navigation.PopPopupAsync();
                MenuItem.Command?.Execute(MenuItem.CommandParameter);
            }
        }

        private async void PageTapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (_width != width && _height != height && _width > 0 && _height > 0)
            {
                Navigation.PopPopupAsync();
                return;
            }
            _width = width;
            _height = height;
        }
    }
}