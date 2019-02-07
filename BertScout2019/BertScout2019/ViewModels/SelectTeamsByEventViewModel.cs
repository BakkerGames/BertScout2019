using BertScout2019.Services;
using BertScout2019Data.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace BertScout2019.ViewModels
{
    public class SelectTeamsByEventViewModel : BaseViewModel
    {
        public IDataStore<Team> DataStoreTeam => new SqlDataStoreTeamsByEvent(App.currFRCEventKey);

        public ObservableCollection<Team> Teams { get; set; }
        public Command LoadEventTeamsCommand { get; set; }

        public SelectTeamsByEventViewModel()
        {
            Title = App.currFRCEventName;
            Teams = new ObservableCollection<Team>();
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
                    Teams.Add(team);
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
