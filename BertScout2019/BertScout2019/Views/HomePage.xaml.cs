using BertScout2019.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public HomePage()
        {
            InitializeComponent();
        }

        async private void Home_Options_Clicked(object sender, EventArgs e)
        {
            //todo add options page
        }

        async private void Button_Select_FRC_Event_Clicked(object sender, EventArgs e)
        {
            await RootPage.NavigateFromMenu((int)MenuItemType.BrowseFRCEvents);
        }

        async private void Button_Select_Teams_Clicked(object sender, EventArgs e)
        {
            //await RootPage.NavigateFromMenu((int)MenuItemType.BrowseFRCEvents);
        }

        async private void Select_Items_Clicked(object sender, EventArgs e)
        {
            await RootPage.NavigateFromMenu((int)MenuItemType.BrowseItems);
        }
    }
}