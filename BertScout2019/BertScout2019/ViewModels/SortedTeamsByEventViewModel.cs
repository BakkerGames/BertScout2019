using BertScout2019.Models;
using BertScout2019.Services;
using BertScout2019Data.Models;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace BertScout2019.ViewModels
{
    public class SortedTeamsByEventViewModel : BaseViewModel
    {
        public IDataStore<Team> DataStoreTeam => new SqlDataStoreTeams(App.currFRCEventKey);

        public ObservableCollection<TeamResult> Teams { get; set; }
        public Command LoadEventTeamsCommand { get; set; }

        public SortedTeamsByEventViewModel()
        {
            Title = App.currFRCEventName;
            Teams = new ObservableCollection<TeamResult>();
            ExecuteLoadEventTeamsCommand();
        }

        public void ExecuteLoadEventTeamsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            
            try
            {
                Teams.Clear();
                var teams = DataStoreTeam.GetItemsAsync(true).Result;
                foreach (var team in teams)
                {
                   //DataStoreMatch = new SqlDataStoreMatchesByEventTeam(App.currFRCEventKey, team.TeamNumber);


                    TeamDetailViewModel matchViewModel= new TeamDetailViewModel(team);
                    TeamResult teamResult = new TeamResult();
                    teamResult.TeamNumber = team.TeamNumber;
                    teamResult.Name = team.Name;
                    teamResult.TotalRP = matchViewModel.TotalRP;
                    teamResult.AverageScore = matchViewModel.AverageScore;
                    Teams.Add(teamResult);

                }
            }
            catch (Exception ex)
            {
                Title = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
