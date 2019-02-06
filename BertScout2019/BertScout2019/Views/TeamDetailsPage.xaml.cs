using BertScout2019.Models;
using BertScout2019.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            viewModel.MatchResults.Add(new MatchResult() { Text = "Hello" });
            viewModel.MatchResults.Add(new MatchResult() { Text = "Nate!" });
        }

        private void TeamMatchesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}