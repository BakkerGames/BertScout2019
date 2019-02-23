using BertScout2019.Services;
using BertScout2019.ViewModels;
using BertScout2019Data.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewTeamPage : ContentPage
    {
        SelectTeamsByEventViewModel viewModel;
        public IDataStore<Team> DataStoreTeams;
        public IDataStore<EventTeam> DataStoreEventTeams;

        public AddNewTeamPage(SelectTeamsByEventViewModel parentViewModel)
        {
            InitializeComponent();
            viewModel = parentViewModel;
            DataStoreTeams = new SqlDataStoreTeams();
            DataStoreEventTeams = new SqlDataStoreEventTeams();
        }

        private void Add_New_Team_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private bool _addNewTeamBusy = false;
        private void Add_NewTeam_Clicked(object sender, EventArgs e)
        {
            // prevent multiple clicks at once
            if (_addNewTeamBusy)
            {
                return;
            }
            _addNewTeamBusy = true;
            doAddNewTeam();
            Add_New_Team.Text = "";
            _addNewTeamBusy = false;
        }

        private void doAddNewTeam()
        {
            int newTeamNumber = 0;
            if (string.IsNullOrEmpty(Add_New_Team.Text))
            {
                return;
            }
            if (!int.TryParse(Add_New_Team.Text, out newTeamNumber))
            {
                this.Title = "Numbers Only Please!";
                return;
            }
            if (newTeamNumber > 9999 || newTeamNumber < 1)
            {
                this.Title = "Number out of range";
                return;
            }

            // add new team
            Team newTeam;
            try
            {
                newTeam = DataStoreTeams.GetItemByKeyAsync(newTeamNumber.ToString())?.Result;
                if (newTeam == null)
                {
                    throw new SystemException();
                }
            }
            catch (Exception)
            {
                this.Title = $"Team {newTeamNumber} does not exist";
                return;
            }

            foreach (Team existing in viewModel.Teams)
            {
                if (existing.TeamNumber == newTeamNumber)
                {
                    this.Title = $"Team {newTeamNumber} is already in this event";
                    return;
                }
            }

            EventTeam newEventTeam = new EventTeam()
            {
                EventKey = App.currFRCEventKey,
                TeamNumber = newTeamNumber,
            };
            newEventTeam.Changed = 1;
            DataStoreEventTeams.AddItemAsync(newEventTeam);

            bool found = false;
            for (int i = 0; i < viewModel.Teams.Count; i++)
            {
                if (viewModel.Teams[i].TeamNumber > newTeamNumber)
                {
                    viewModel.Teams.Insert(i, newTeam);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                viewModel.Teams.Add(newTeam);
            }

            this.Title = $"Added new team {newTeamNumber}";
            return;
        }
    }
}