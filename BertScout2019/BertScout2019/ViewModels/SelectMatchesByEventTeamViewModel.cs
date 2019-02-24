using BertScout2019.Services;
using BertScout2019Data.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BertScout2019.ViewModels
{
    public class SelectMatchesByEventTeamViewModel : BaseViewModel
    {
        public IDataStore<EventTeamMatch> DataStoreMatch;

        public ObservableCollection<EventTeamMatch> Matches { get; set; }

        public SelectMatchesByEventTeamViewModel(string eventKey, Team team)
        {
            DataStoreMatch = new SqlDataStoreEventTeamMatches(eventKey, team.TeamNumber);
            Title = $"Team {team.TeamNumber} - {team.Name}";
            Matches = new ObservableCollection<EventTeamMatch>();
            ExecuteLoadEventTeamMatchesCommand();
        }

        public void ExecuteLoadEventTeamMatchesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Matches.Clear();
                var matches = DataStoreMatch.GetItemsAsync(true).Result;
                foreach (var match in matches)
                {
                    Matches.Add(match);
                    if (App.highestMatchNumber < match.MatchNumber)
                    {
                        App.highestMatchNumber = match.MatchNumber;
                    }
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
