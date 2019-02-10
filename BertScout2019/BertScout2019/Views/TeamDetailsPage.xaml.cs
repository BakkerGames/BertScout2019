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
            TeamDetails_Number.Text = viewModel.item.TeamNumber.ToString(); //Show Team Number ex: 133
            TeamDetails_Name.Text = viewModel.item.Name; // Show Team Name ex: Bonny Eagle Robotics Team
            TeamDetails_Location.Text = viewModel.item.Location; //Show Location ex: Standish, ME, USA
            TeamDetails_RP.Text = viewModel.TotalRP.ToString(); //Show total RP
            TeamDetails_AVGS.Text = viewModel.AverageScore.ToString(); //Show Average Score
        }

        private void TeamMatchesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // todo make something happen when select result
        }
    }
}