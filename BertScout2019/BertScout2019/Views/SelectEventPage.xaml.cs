using BertScout2019.Models;
using BertScout2019.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectEventPage : ContentPage
    {
        SelectEventsViewModel viewModel;

        public SelectEventPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SelectEventsViewModel(); 
        }

        async void AddFRCEvent_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new NavigationPage(new NewFRCEventPage()));
        }

        async void OnFRCEventSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as FRCEvent;
            if (item == null)
                return;

            App app = Application.Current as App;
            app.CurrentFRCEvent = item.Name;

            //await Navigation.PushAsync(new FRCEventDetailPage(new FRCEventDetailViewModel(item)));

            // Manually deselect item.
            FRCEventsListView.SelectedItem = null;
        }
    }
}