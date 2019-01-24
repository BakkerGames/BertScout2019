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
        public Command LoadEventTeamsCommand { get; set; }

        public SelectEventTeamMatchesViewModel(Team item)
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
            App app = Application.Current as App;

            try
            {
                Matches.Clear();
                var teams = DataStoreMatch.GetItemsAsync(true).Result;
                // 94ms sql
                foreach (var team in teams)
                {
                    Matches.Add(team);
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
