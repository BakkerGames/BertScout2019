using BertScout2019.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BertScout2019.Services
{
    public class SqlDataStoreEventTeamMatch : IDataStore<EventTeamMatch>
    {
        public EventTeamMatch item;

        public SqlDataStoreEventTeamMatch(string frcEventKey, int teamNumber, int matchNumber)
        {
            // must complete, so don't async/await
            item = App.Database.GetEventTeamMatchAsync(frcEventKey, teamNumber, matchNumber).Result;
        }

        public async Task<bool> UpdateItemAsync(EventTeamMatch item)
        {
            return ((await App.database.SaveEventTeamMatchAsync(item)) > 0);
        }

        // none of the following are implemented in this class

        public Task<bool> AddItemAsync(EventTeamMatch item)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<EventTeamMatch> GetItemAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<EventTeamMatch>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new System.NotImplementedException();
        }
    }
}
