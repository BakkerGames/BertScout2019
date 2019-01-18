using BertScout2019.Models;
using BertScout2019.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // async private void Button_Select_FRC_Event_Clicked(object sender, EventArgs e) { }
        private void Button_Select_FRC_Event_Clicked(object sender, EventArgs e)
        {
            if (!((Button)sender).Text.EndsWith("Hello!") 
                && 
                !((Button)sender).Text.StartsWith("Please"))
            {
                ((Button)sender).Text += " - Hello!";
            }
            else
            {
                ((Button)sender).Text = "Please don't click me again!";
            }
        }

        async private void Button_Select_Teams_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new ItemsPage());
            await RootPage.NavigateFromMenu((int)MenuItemType.Browse);
        }

        async private void Home_Options_Clicked(object sender, EventArgs e)
        {
            //todo add in options
        }
    }
}