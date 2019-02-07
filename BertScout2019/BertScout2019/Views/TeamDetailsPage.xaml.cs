using BertScout2019.ViewModels;
using BertScout2019Data.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamDetailsPage : ContentPage
    {
        TeamDetailViewModel viewModel;

        public TeamDetailsPage(Team item)
        {
            InitializeComponent();

            BindingContext = viewModel = new TeamDetailViewModel(item);
        }

        protected override void OnAppearing()
        {
            TeamDetails_Number.Text = viewModel.item.TeamNumber.ToString(); //App.currTeamNumber.ToString();
            TeamDetails_Name.Text = viewModel.item.Name; // App.currTeamName;
            TeamDetails_Location.Text = viewModel.item.Location; //"anytownusa";
        }

        private void TeamMatchesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // todo make something happen when select result
        }
    }
}