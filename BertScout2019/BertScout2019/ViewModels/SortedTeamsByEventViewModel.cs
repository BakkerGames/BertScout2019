using BertScout2019.Models;
using BertScout2019.Services;
using BertScout2019Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace BertScout2019.ViewModels
{
    public class SortedTeamsByEventViewModel : BaseViewModel
    {
        public IDataStore<Team> DataStoreTeam => new SqlDataStoreTeams(App.currFRCEventKey);

        public ObservableCollection<TeamResult> TeamResults { get; set; }
        public Command LoadEventTeamsCommand { get; set; }

        public SortedTeamsByEventViewModel()
        {
            Title = App.currFRCEventName;
            TeamResults = new ObservableCollection<TeamResult>();
            ExecuteLoadEventTeamsCommand();
        }

        public void ExecuteLoadEventTeamsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                TeamResults.Clear();
                var teams = DataStoreTeam.GetItemsAsync(true).Result;
                foreach (var team in teams)
                {
                    TeamDetailViewModel matchViewModel = new TeamDetailViewModel(App.currFRCEventKey, team);
                    TeamResult teamResult = new TeamResult();
                    teamResult.TeamNumber = team.TeamNumber;
                    teamResult.Name = team.Name;
                    teamResult.TotalRP = matchViewModel.TotalRP;
                    teamResult.AverageScore = matchViewModel.AverageScore;
                    TeamResults.Add(teamResult);
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

        public void SortByAverageScore()
        {
            List<TeamResult> ordered = TeamResults.OrderByDescending(o => o.AverageScore).ToList();
            TeamResults.Clear();
            foreach (TeamResult item in ordered)
            {
                TeamResults.Add(item);
            }
        }

        public void SortByRankingPoints()
        {
            List<TeamResult> ordered = TeamResults.OrderByDescending(o => o.TotalRP).ToList();
            TeamResults.Clear();
            foreach (TeamResult item in ordered)
            {
                TeamResults.Add(item);
            }
        }
    }
}
