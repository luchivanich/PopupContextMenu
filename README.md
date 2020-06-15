# Xamarin Forms Popup Context Menu
Popup Context Menu for Xamarin Forms

## Implementation Notes

This is an example of a popup menu implemented in Xamarin Forms based on Rg.Plugins.Popup (https://github.com/rotorgames/Rg.Plugins.Popup)

There is a control called ContextMenuButton which inherits Button control and is used to initiate Context Menu.

Features of this control:
  1. Contains `List<MenuItem>`. Each item represents an item in context menu
  2. Implements method GetCoordinates that returns control center coordinates on the application screen (take a look at the renderer class ContextMenuButtonRenderer)
  3. When user clicks on it Popup Context Menu appears in a proper place relatively the Context Menu Button. By default ContextMenuPopup tries to allocate itself on the BottomRight of the ContextMenuButton
  
* Basically instead of Button any visual element could be used. I chose button only because of its simplicity.

The PopupContextMenu itself is transparent Rg.Popup page with a Frame that contains context menu commands.

## Usage

```
<controls:ContextMenuButton
    WidthRequest="50"
    HeightRequest="50"
    ItemsContainerHeight="120"
    ItemsContainerWidth="110"
    <controls:ContextMenuButton.Items>
        <x:Array Type="{x:Type MenuItem}">
            <MenuItem Text="Command 1" Command="..." CommandParameter="..."/>
            <MenuItem Text="Command 2" Command="..." CommandParameter="..."/>
            <MenuItem Text="Command 3" Command="..." CommandParameter="..."/>
        </x:Array>
    </controls:ContextMenuButton.Items>
</controls:ContextMenuButton>
```

## Screenshots

![Bottom Right Positioning](https://github.com/luchivanich/PopupContextMenu/blob/master/screenshots/01.bottomright.png)

![Bottom Left Positioning](https://github.com/luchivanich/PopupContextMenu/blob/master/screenshots/02.bottomleft.png)

![Top Right Positioning](https://github.com/luchivanich/PopupContextMenu/blob/master/screenshots/03.topright.png)

![Top Left Positioning](https://github.com/luchivanich/PopupContextMenu/blob/master/screenshots/04.topleft.png)

## Known Issues

There is a problem with context menu position when Safe Area Layout feature is involved (https://docs.microsoft.com/en-us/xamarin/xamarin-forms/platform/ios/page-safe-area-layout). Context Menu page doesn't know about Safe Area Insets of the  underlying page.

