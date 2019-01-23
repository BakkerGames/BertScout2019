using BertScout2019.Models;
using BertScout2019.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace BertScout2019.ViewModels
{
    public class SelectEventTeamsViewModel : BaseViewModel
    {
        public IDataStore<Team> DataStoreTeam => new SqlDataStoreEventTeams(App.currFRCEventKey);

        public ObservableCollection<Team> Teams { get; set; }
        public Command LoadEventTeamsCommand { get; set; }

        public SelectEventTeamsViewModel()
        {
            App app = Application.Current as App;
            Title = App.currFRCEventName;
            Teams = new ObservableCollection<Team>();
            ExecuteLoadEventTeamsCommand();
            //LoadEventTeamsCommand = new Command(async () => await ExecuteLoadEventTeamsCommand());
        }

        public void ExecuteLoadEventTeamsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            App app = Application.Current as App;

            try
            {
                Teams.Clear();
                var teams = DataStoreTeam.GetItemsAsync(true).Result;
                // 94ms sql
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
