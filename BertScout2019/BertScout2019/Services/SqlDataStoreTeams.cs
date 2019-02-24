using BertScout2019Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BertScout2019.Services
{
    public class SqlDataStoreTeams : IDataStore<Team>
    {
        private bool _paramsFlag = false;
        private string _eventKey = "";

        private List<Team> items;

        public SqlDataStoreTeams()
        {
            _paramsFlag = false;
        }

        public SqlDataStoreTeams(string eventKey)
        {
            _paramsFlag = true;
            _eventKey = eventKey;
        }

        private void FillList()
        {
            if (items == null)
            {
                // must complete, so don't async/await
                if (_paramsFlag)
                {
                    items = App.Database.GetTeamsByEventAsync(_eventKey).Result;
                }
                else
                {
                    items = App.Database.GetTeamsAsync().Result;
                }
            }
        }

        public async Task<bool> AddItemAsync(Team item)
        {
            FillList();
            if (item.Uuid == null)
            {
                item.Uuid = Guid.NewGuid().ToString();
            }
            await App.database.SaveTeamAsync(item);
            items = null;
            FillList();
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            FillList();
            var oldItem = items.Where((Team arg) => arg.Id == id).FirstOrDefault();
            await App.database.DeleteTeamAsync(oldItem.Id.Value);
            items.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string uuid)
        {
            FillList();
            var oldItem = items.Where((Team arg) => arg.Uuid == uuid).FirstOrDefault();
            await App.database.DeleteTeamAsync(oldItem.Id.Value);
            items.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<Team> GetItemAsync(int id)
        {
            FillList();
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<Team> GetItemAsync(string uuid)
        {
            FillList();
            return await Task.FromResult(items.FirstOrDefault(s => s.Uuid == uuid));
        }

        public async Task<Team> GetItemByKeyAsync(string key)
        {
            // key = TeamNumber
            FillList();
            return await Task.FromResult(items.FirstOrDefault(s => s.TeamNumber.ToString() == key));
        }

        public async Task<IEnumerable<Team>> GetItemsAsync(bool forceRefresh = false)
        {
            FillList();
            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(Team item)
        {
            FillList();
            var oldItem = items.Where((Team arg) => arg.Uuid == item.Uuid).FirstOrDefault();
            item.Id = oldItem.Id;
            items.Remove(oldItem);
            await App.database.SaveTeamAsync(item);
            items = null;
            FillList();
            return await Task.FromResult(true);
        }
    }
}
