// For dropdown menu-use picker, info at https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/picker/

using BertScout2019.Models;
using BertScout2019.ViewModels;
using BertScout2019Data.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage

    {
        SortedTeamsByEventViewModel viewModel;

        public ResultPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new SortedTeamsByEventViewModel();
            TeamNumber.BackgroundColor = App.SelectedButtonColor;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //EventTeamsListView.SelectedItem = null;
        }

        private async void EventTeamsListView_ItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            TeamResult item = (TeamResult)args.SelectedItem;
            if (item == null)
            {
                return;
            }
            App.currTeamNumber = item.TeamNumber;
            App.currTeamName = item.Name;
            Team itemTeam = viewModel.DataStoreTeam.GetItemByKeyAsync(item.TeamNumber.ToString()).Result;
            await Navigation.PushAsync(new TeamDetailsPage(App.currFRCEventKey, itemTeam));
        }

        private void TeamNumber_Clicked(object sender, EventArgs e)
        {
            ClearAllSortButtons();
            TeamNumber.BackgroundColor = App.SelectedButtonColor;
            viewModel.SortByTeamNumber();
        }

        private void RP_Clicked(object sender, EventArgs e)
        {
            ClearAllSortButtons();
            RP.BackgroundColor = App.SelectedButtonColor;
            viewModel.SortByRankingPoints();
        }

        private void AvgScore_Clicked(object sender, EventArgs e)
        {
            ClearAllSortButtons();
            AvgScore.BackgroundColor = App.SelectedButtonColor;
            viewModel.SortByAverageScore();
        }

        private void HatchCount_Clicked(object sender, EventArgs e)
        {
            ClearAllSortButtons();
            HatchCount.BackgroundColor = App.SelectedButtonColor;
            viewModel.SortByHatchCount();
        }

        private void CargoCount_Clicked(object sender, EventArgs e)
        {
            ClearAllSortButtons();
            CargoCount.BackgroundColor = App.SelectedButtonColor;
            viewModel.SortByCargoCount();
        }

        private void AverageHatches_Clicked(object sender, EventArgs e)
        {
            ClearAllSortButtons();
            AverageHatches.BackgroundColor = App.SelectedButtonColor;
            viewModel.SortByAverageHatches();
        }

        private void AverageCargo_Clicked(object sender, EventArgs e)
        {
            ClearAllSortButtons();
            AverageCargo.BackgroundColor = App.SelectedButtonColor;
            viewModel.SortByAverageCargo();
        }

        private void ClearAllSortButtons()
        {
            TeamNumber.BackgroundColor = App.UnselectedButtonColor;
            RP.BackgroundColor = App.UnselectedButtonColor;
            AvgScore.BackgroundColor = App.UnselectedButtonColor;
            HatchCount.BackgroundColor = App.UnselectedButtonColor;
            CargoCount.BackgroundColor = App.UnselectedButtonColor;
            AverageHatches.BackgroundColor = App.UnselectedButtonColor;
            AverageCargo.BackgroundColor = App.UnselectedButtonColor;
        }
    }
}