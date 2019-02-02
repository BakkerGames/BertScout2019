using System;
using System.Collections.Generic;
using System.Text;

namespace BertScout2019.ViewModels
{
    public class TeamDetailViewModel : BaseViewModel
    {
        //do not edit 
        //public IDataStore<Team> DataStoreTeam => new SqlDataStoreEventTeams(App.currFRCEventKey);

        //try to edit 
        //public <Location> Location => new SqlDataStoreTeamLoc(App.currFRCTeamLoc);
        //public IDataStore<Team> DataStoreTeams => new SqlDataStoreTeams();

        public TeamDetailViewModel()
        {
            Title = "Team Details";

            
        }
    }
}
