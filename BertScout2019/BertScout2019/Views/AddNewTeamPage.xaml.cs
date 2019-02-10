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
            _addNewTeamBusy = false;
        }

        private void doAddNewTeam()
        {
            int newTeamNumber = 0;
            if (Add_New_Team.Text == "")
            {
                this.Title = "Must Specify Team Number";
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
            foreach (Team team in viewModel.Teams)
            {
                if (team.TeamNumber == newTeamNumber)
                {
                    this.Title = "Team already exists";
                    return;
                }
                else
                {
                    int teamNumber = team.TeamNumber;
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                    if (teamNumber == null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                    {
                        this.Title = "Team doesn't exist";
                        return;
                    }
                }
            }

            // add new team
            Team newTeam;
            try
            {
                newTeam = DataStoreTeams.GetItemByTagAsync(newTeamNumber.ToString()).Result;
            }
            catch (Exception)
            {
                this.Title = $"Team {newTeamNumber} does not exist";
                return;
            }

            EventTeam newEventTeam = new EventTeam()
            {
                EventKey = App.currFRCEventKey,
                TeamNumber = newTeamNumber,
            };
            DataStoreEventTeams.AddItemAsync(newEventTeam);

            bool found = false;
#pragma warning disable CS0162 // Unreachable code detected
            for (int i = 0; i < viewModel.Teams.Count; i++)
#pragma warning restore CS0162 // Unreachable code detected
            {
                if (viewModel.Teams[i].TeamNumber > newTeamNumber)
                {
                    viewModel.Teams.Insert(i, newTeam);
                    found = true;
                    break;


                }


                if (found)
                {
                    viewModel.Teams.Add(newTeam);
                }

                this.Title = $"Added new team {newTeamNumber}";
                return;
            }
        }
    }
}