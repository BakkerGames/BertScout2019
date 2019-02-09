using BertScout2019Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BertScout2019.Services
{
    public class SqlDataStoreEventTeams : IDataStore<EventTeam>
    {
        List<EventTeam> items;

        public SqlDataStoreEventTeams()
        {
            // must complete, so don't async/await
            items = App.Database.GetEventTeamsAsync().Result;
        }

        public async Task<bool> AddItemAsync(EventTeam item)
        {
            if (item.Uuid == null)
            {
                item.Uuid = Guid.NewGuid().ToString();
            }
            await App.database.SaveEventTeamAsync(item);
            items = App.Database.GetEventTeamsAsync().Result;
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((EventTeam arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public Task<EventTeam> GetItemAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<EventTeam> GetItemByTagAsync(string tag)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<EventTeam>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(EventTeam item)
        {
            throw new System.NotImplementedException();
        }
    }
}
