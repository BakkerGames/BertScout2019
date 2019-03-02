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
            viewModel.SortByRankingPoints();
        }

        private void AvgScore_Clicked(object sender, EventArgs e)
        {
            AvgScore.BackgroundColor = App.SelectedButtonColor;
            RP.BackgroundColor = App.UnselectedButtonColor;
            viewModel.SortByAverageScore();
        }

        private void EventTeamsListView_ItemSelected_1(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}