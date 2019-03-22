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

        private void RP_Clicked(object sender, EventArgs e)
        {
            RP.BackgroundColor = App.SelectedButtonColor;
            AvgScore.BackgroundColor = App.UnselectedButtonColor;
            HatchCount.BackgroundColor = App.UnselectedButtonColor;
            CargoCount.BackgroundColor = App.UnselectedButtonColor;
            viewModel.SortByRankingPoints();
        }

        private void AvgScore_Clicked(object sender, EventArgs e)
        {
            RP.BackgroundColor = App.UnselectedButtonColor;
            AvgScore.BackgroundColor = App.SelectedButtonColor;
            HatchCount.BackgroundColor = App.UnselectedButtonColor;
            CargoCount.BackgroundColor = App.UnselectedButtonColor;
            viewModel.SortByAverageScore();
        }

        private void HatchCount_Clicked(object sender, EventArgs e)
        {
            RP.BackgroundColor = App.UnselectedButtonColor;
            AvgScore.BackgroundColor = App.UnselectedButtonColor;
            HatchCount.BackgroundColor = App.SelectedButtonColor;
            CargoCount.BackgroundColor = App.UnselectedButtonColor;
            viewModel.SortByHatchCount();
        }

        private void CargoCount_Clicked(object sender, EventArgs e)
        {
            RP.BackgroundColor = App.UnselectedButtonColor;
            AvgScore.BackgroundColor = App.UnselectedButtonColor;
            HatchCount.BackgroundColor = App.UnselectedButtonColor;
            CargoCount.BackgroundColor = App.SelectedButtonColor;
            viewModel.SortByCargoCount();
        }
    }
}