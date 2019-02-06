using BertScout2019.Models;

namespace BertScout2019.ViewModels
{
    public class TeamDetailViewModel : BaseViewModel
    {
<<<<<<< HEAD
        //do not edit 
        //public IDataStore<Team> DataStoreTeam => new SqlDataStoreEventTeams(App.currFRCEventKey);

        //try to edit 
        //public <Location> Location => new SqlDataStoreTeamLoc(App.currFRCTeamLoc);
        //public IDataStore<Team> DataStoreTeams => new SqlDataStoreTeams();

        public TeamDetailViewModel()
        {
            Title = "Team Details";

            
=======
        public Team item;

        public TeamDetailViewModel(Team item)
        {
            this.item = item;
            Title = $"Team {App.currTeamNumber} Details";
>>>>>>> master
        }
    }
}
