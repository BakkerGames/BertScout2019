using BertScout2019.ViewModels;
using BertScout2019Data.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectEventTeamPage : ContentPage
    {
        SelectTeamsByEventViewModel viewModel;

        public SelectEventTeamPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SelectTeamsByEventViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            EventTeamsListView.SelectedItem = null;
        }

        private async void EventTeamsListView_ItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Team item = (Team)args.SelectedItem;
            if (item == null)
            {
                return;
            }
            App.currTeamNumber = item.TeamNumber;
            App.currTeamName = item.Name;
            await Navigation.PushAsync(new SelectEventTeamMatchPage(App.currFRCEventKey, item));
        }

        async private void AddTeam_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AddNewTeamPage(viewModel));
        }
    }
}