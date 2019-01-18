using BertScout2019.Models;
using BertScout2019.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FRCEventsPage : ContentPage
    {
        FRCEventsViewModel viewModel;

        public FRCEventsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new FRCEventsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as FRCEvent;
            if (item == null)
                return;

            await Navigation.PushAsync(new FRCEventDetailPage(new FRCEventDetailViewModel(item)));

            // Manually deselect item.
            FRCEventsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.FRCEvents.Count == 0)
                viewModel.LoadFRCEventsCommand.Execute(null);
        }
    }
}