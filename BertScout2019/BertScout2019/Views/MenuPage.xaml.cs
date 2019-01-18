﻿using BertScout2019.Models;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Home, Title="Home" },
                new HomeMenuItem {Id = MenuItemType.BrowseFRCEvents, Title="Browse FRC Events" },
                new HomeMenuItem {Id = MenuItemType.BrowseItems, Title="Browse Items" },
                new HomeMenuItem {Id = MenuItemType.About, Title="About" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = null; // menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}