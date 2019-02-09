using BertScout2019.Services;
using BertScout2019.ViewModels;
using BertScout2019Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewTeamPage : ContentPage
    {
        SelectTeamsByEventViewModel viewModel;
        public IDataStore<Team> DataStoreTeams;
        public AddNewTeamPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new SelectTeamsByEventViewModel();
        }

        private void Add_New_Team_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Add_NewTeam_Clicked(object sender, EventArgs e)
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
            }
            //todo add new team
            Team newTeam;
            try
            {
                newTeam = DataStoreTeams.GetItemAsync(newTeamNumber).Result;
            }
            catch (Exception ex)
            {
                this.Title = ex.Message; // $"Team {newTeamNumber} does not exist";
                return;
            }
            
            viewModel.Teams.Add(newTeam);

            this.Title = $"Added new team {newTeamNumber}";
            //Navigation.PushAsync(new SelectEventTeamPage());
            return;
        }
    }
}