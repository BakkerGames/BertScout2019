using BertScout2019.Models;
using BertScout2019.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace BertScout2019.ViewModels
{
    public class SelectEventTeamMatchesViewModel : BaseViewModel
    {
        public IDataStore<EventTeamMatch> DataStoreMatch => new SqlDataStoreEventTeamMatches(App.currFRCEventKey, App.currTeamNumber);

        public ObservableCollection<EventTeamMatch> Matches { get; set; }
        public Command LoadEventTeamMatchesCommand { get; set; }

        public SelectEventTeamMatchesViewModel()
        {
            Title = $"Team {App.currTeamNumber} - {App.currTeamName}";
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
