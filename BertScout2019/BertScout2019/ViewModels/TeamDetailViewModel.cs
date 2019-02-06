using BertScout2019.Models;
using BertScout2019.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BertScout2019.ViewModels
{
    public class TeamDetailViewModel : BaseViewModel
    {

        public Team item;

        public IDataStore<EventTeamMatch> DataStoreMatch;

        public ObservableCollection<MatchResult> MatchResults { get; set; }

        public TeamDetailViewModel(Team item)
        {
            this.item = item;
            Title = $"Team {App.currTeamNumber} Details";
            MatchResults = new ObservableCollection<MatchResult>();
            DataStoreMatch = new SqlDataStoreMatchesByEventTeam(App.currFRCEventKey, item.TeamNumber);
            ExecuteLoadEventTeamMatchesCommand();
        }

        public void ExecuteLoadEventTeamMatchesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                MatchResults.Clear();
                var matches = DataStoreMatch.GetItemsAsync(true).Result;
                foreach (var match in matches)
                {
                    MatchResult obj = new MatchResult();
                    obj.Text = $"Match {match.MatchNumber}";
                    MatchResults.Add(obj);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
