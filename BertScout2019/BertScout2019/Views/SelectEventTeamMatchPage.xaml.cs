using BertScout2019.Models;
using BertScout2019.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectEventTeamMatchPage : ContentPage
    {
        SelectEventTeamMatchesViewModel viewModel;

        public SelectEventTeamMatchPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SelectEventTeamMatchesViewModel();
        }

        private async void EventTeamsListView_ItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //todo
            var item = args.SelectedItem as EventTeamMatch;
            if (item == null)
                return;

            //App.currTeamNumber = item.TeamNumber;
            //await Navigation.PushAsync(new SelectEventTeamMatchPage());
        }
    }
}