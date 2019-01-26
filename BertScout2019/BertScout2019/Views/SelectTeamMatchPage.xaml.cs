using BertScout2019.Models;
using BertScout2019.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectTeamMatchPage : ContentPage
    {
        SelectEventTeamMatchesViewModel viewModel;

        public SelectTeamMatchPage()
        {
            //todo removed so it will compile
            //InitializeComponent();
            //BindingContext = viewModel = new SelectEventTeamMatchesViewModel();
        }

        private async void EventTeamMatchesListView_ItemSelected(object sender, EventArgs e)
        {
            //todo
            App.currTeamNumber = ((EventTeamMatch)sender).TeamNumber;
            await Navigation.PushAsync(new SelectTeamMatchPage());
        }
        
    }
}