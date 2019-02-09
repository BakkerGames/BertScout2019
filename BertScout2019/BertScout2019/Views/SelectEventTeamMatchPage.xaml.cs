using BertScout2019.ViewModels;
using BertScout2019Data.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectEventTeamMatchPage : ContentPage
    {
        SelectMatchesByEventTeamViewModel viewModel;
        Team currTeam;

        public SelectEventTeamMatchPage(string eventKey, Team team)
        {
            InitializeComponent();
            currTeam = team;
            BindingContext = viewModel = new SelectMatchesByEventTeamViewModel(eventKey, team);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            EventTeamMatchesListView.SelectedItem = null;
            int tempHighest = App.highestMatchNumber + 1;
            if (tempHighest > 999)
            {
                tempHighest = 999;
            }
            MatchNumberLabelValue.Text = tempHighest.ToString();
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
            int value = 0;
            if (!int.TryParse(MatchNumberLabelValue.Text, out value))
            {
                MatchNumberLabelValue.Text = (1).ToString();
            }
            if (value > 1)
            {
                MatchNumberLabelValue.Text = (value - 1).ToString();
                App.highestMatchNumber = value;
            }
        }

        private void AddMatch_Plus_Clicked(object sender, System.EventArgs e)
        {
            int value = 0;
            if (!int.TryParse(MatchNumberLabelValue.Text, out value))
            {
                MatchNumberLabelValue.Text = (1).ToString();
            }
            if (value < 999)
            {
                MatchNumberLabelValue.Text = (value + 1).ToString();
                App.highestMatchNumber = value;
            }
        }

        private bool _addNewMatchBusy = false;
        private void AddNewMatch_Clicked(object sender, System.EventArgs e)
        {
            // prevent multiple clicks at once
            if (_addNewMatchBusy)
            {
                return;
            }
            _addNewMatchBusy = true;
            doAddNewMatch();
            _addNewMatchBusy = false;
        }

        private void doAddNewMatch()
        { 
            int value = int.Parse(MatchNumberLabelValue.Text);
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
            App.database.SaveEventTeamMatchAsync(newMatch);
            if (App.highestMatchNumber < value)
            {
                App.highestMatchNumber = value;
            }
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
        }

        private async void TeamDetails_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new TeamDetailsPage(currTeam));
        }
    }
}