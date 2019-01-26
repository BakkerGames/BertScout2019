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

            ShowNewMatchNumber.Text = (App.highestMatchNumber + 1).ToString();

            BindingContext = viewModel = new SelectEventTeamMatchesViewModel();
        }


        private async void EventTeamsListMatchView_ItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //todo
            var item = args.SelectedItem as EventTeamMatch;
            if (item == null)
                return;

            //App.currTeamNumber = item.TeamNumber;
            //await Navigation.PushAsync(new SelectEventTeamMatchPage());
        }

        private void AddMatch_Minus_Clicked(object sender, System.EventArgs e)
        {
            int value = int.Parse(ShowNewMatchNumber.Text);
            if (value > 1)
            {
                ShowNewMatchNumber.Text = (value - 1).ToString();
            }
        }

        private void AddMatch_Plus_Clicked(object sender, System.EventArgs e)
        {
            int value = int.Parse(ShowNewMatchNumber.Text);
            if (value < 999)
            {
                ShowNewMatchNumber.Text = (value + 1).ToString();
            }
        }

        private void AddNewMatch_Clicked(object sender, System.EventArgs e)
        {
            int value = int.Parse(ShowNewMatchNumber.Text);
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
    }
}