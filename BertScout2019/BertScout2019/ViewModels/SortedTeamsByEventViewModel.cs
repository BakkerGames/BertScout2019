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
                    teamResult.TotalHatches = matchViewModel.TotalHatches;
                    teamResult.TotalCargo = matchViewModel.TotalCargo;
                    teamResult.AverageHatches = matchViewModel.AverageHatches;
                    teamResult.AverageCargo = matchViewModel.AverageCargo;
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

        internal void SortByTeamNumber()
        {
            List<TeamResult> ordered = TeamResults.OrderBy(o => o.TeamNumber).ToList();
            TeamResults.Clear();
            foreach (TeamResult item in ordered)
            {
                TeamResults.Add(item);
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

        public void SortByHatchCount()
        {
            List<TeamResult> ordered = TeamResults.OrderByDescending(o => o.TotalHatches).ToList();
            TeamResults.Clear();
            foreach (TeamResult item in ordered)
            {
                TeamResults.Add(item);
            }
        }

        internal void SortByAverageHatches()
        {
            List<TeamResult> ordered = TeamResults.OrderByDescending(o => o.AverageHatches).ToList();
            TeamResults.Clear();
            foreach (TeamResult item in ordered)
            {
                TeamResults.Add(item);
            }
        }

        internal void SortByAverageCargo()
        {
            List<TeamResult> ordered = TeamResults.OrderByDescending(o => o.AverageCargo).ToList();
            TeamResults.Clear();
            foreach (TeamResult item in ordered)
            {
                TeamResults.Add(item);
            }
        }

        public void SortByCargoCount()
        {
            List<TeamResult> ordered = TeamResults.OrderByDescending(o => o.TotalCargo).ToList();
            TeamResults.Clear();
            foreach (TeamResult item in ordered)
            {
                TeamResults.Add(item);
            }
        }
    }
}
