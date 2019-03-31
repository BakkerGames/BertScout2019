using BertScout2019.Services;
using BertScout2019.ViewModels;
using BertScout2019Data.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectEventTeamMatchPage : ContentPage
    {
        public IDataStore<EventTeamMatch> SqlDataEventTeamMatches;
        SelectMatchesByEventTeamViewModel viewModel;
        Team currTeam;

        public SelectEventTeamMatchPage(string eventKey, Team team)
        {
            InitializeComponent();
            currTeam = team;
            BindingContext = viewModel = new SelectMatchesByEventTeamViewModel(eventKey, team);
            SqlDataEventTeamMatches = new SqlDataStoreEventTeamMatches(App.currFRCEventKey);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (App.highestMatchNumber < 0)
            {
                App.highestMatchNumber = 0;
            }
            MatchNumberLabelValue.Text = (App.highestMatchNumber + 1).ToString();
        }

        private async void EventTeamsListMatchView_ItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            EventTeamMatch item = (EventTeamMatch)args.SelectedItem;
            if (item == null)
            {
                return;
            }
            App.currMatchNumber = item.MatchNumber;
            await Navigation.PushAsync(new EditEventTeamMatchPage(item));
        }

        private void AddMatch_Minus_Clicked(object sender, System.EventArgs e)
        {
            App.highestMatchNumber--;
            if (App.highestMatchNumber < 0)
            {
                App.highestMatchNumber = 0;
            }
            MatchNumberLabelValue.Text = (App.highestMatchNumber + 1).ToString();
        }

        private void AddMatch_Plus_Clicked(object sender, System.EventArgs e)
        {
            App.highestMatchNumber++;
            if (App.highestMatchNumber > 998)
            {
                App.highestMatchNumber = 998;
            }
            MatchNumberLabelValue.Text = (App.highestMatchNumber + 1).ToString();
        }

        private bool _addNewMatchBusy = false;
        private void AddNewMatch_Clicked(object sender, System.EventArgs e)
        {
            if (_addNewMatchBusy)
            {
                return;
            }
            _addNewMatchBusy = true;
            doAddNewMatch(App.highestMatchNumber + 1);
            App.highestMatchNumber++;
            _addNewMatchBusy = false;
        }

        private async void doAddNewMatch(int value)
        {
            foreach (EventTeamMatch oldMatch in viewModel.Matches)
            {
                if (oldMatch.MatchNumber == value)
                {
                    return;
                }
            }
            EventTeamMatch newMatch = new EventTeamMatch();
            newMatch.EventKey = App.currFRCEventKey;
            newMatch.TeamNumber = App.currTeamNumber;
            newMatch.MatchNumber = value;
            newMatch.Changed = 1; // odd = must upload
            await App.database.SaveEventTeamMatchAsync(newMatch);
            // add new match into list in proper order
            bool found = false;
            for (int i = 0; i < viewModel.Matches.Count; i++)
            {
                if (viewModel.Matches[i].MatchNumber > value)
                {
                    found = true;
                    viewModel.Matches.Insert(i, newMatch);
                    break;
                }
            }
            if (!found)
            {
                viewModel.Matches.Add(newMatch);
            }
            App.currMatchNumber = newMatch.MatchNumber;
            await Navigation.PushAsync(new EditEventTeamMatchPage(newMatch));
        }

        private async void TeamDetails_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new TeamDetailsPage(App.currFRCEventKey, currTeam));
        }
    }
}