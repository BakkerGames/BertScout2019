using BertScout2019.Models;
using BertScout2019.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectEventTeamPage : ContentPage
    {
        SelectEventTeamsViewModel viewModel;

        public SelectEventTeamPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SelectEventTeamsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            EventTeamsListView.SelectedItem = null;
        }

        private async void EventTeamsListView_ItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //todo
            var item = args.SelectedItem as Team;
            if (item == null)
                return;

            App.currTeamNumber = item.TeamNumber;
            App.currTeamName = item.Name;
            await Navigation.PushAsync(new SelectEventTeamMatchPage());
        }
    }
}