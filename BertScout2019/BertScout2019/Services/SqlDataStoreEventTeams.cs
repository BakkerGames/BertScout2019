using BertScout2019Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BertScout2019.Services
{
    public class SqlDataStoreEventTeams : IDataStore<EventTeam>
    {
        private bool _paramsFlag = false;
        private string _eventKey = "";

        private List<EventTeam> items;

        public SqlDataStoreEventTeams()
        {
            _paramsFlag = false;
        }

        public SqlDataStoreEventTeams(string eventKey)
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
                    items = App.Database.GetEventTeamsAsync(_eventKey).Result;
                }
                else
                {
                    items = App.Database.GetEventTeamsAsync().Result;
                }
            }
        }

        public async Task<bool> AddItemAsync(EventTeam item)
        {
            FillList();
            if (item.Uuid == null)
            {
                item.Uuid = Guid.NewGuid().ToString();
            }
            await App.database.SaveEventTeamAsync(item);
            items = null;
            FillList();
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            FillList();
            var oldItem = items.Where((EventTeam arg) => arg.Id == id).FirstOrDefault();
            await App.database.DeleteEventTeamAsync(oldItem.Id.Value);
            items.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string uuid)
        {
            FillList();
            var oldItem = items.Where((EventTeam arg) => arg.Uuid == uuid).FirstOrDefault();
            await App.database.DeleteEventTeamAsync(oldItem.Id.Value);
            items.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<EventTeam> GetItemAsync(int id)
        {
            FillList();
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<EventTeam> GetItemAsync(string uuid)
        {
            FillList();
            return await Task.FromResult(items.FirstOrDefault(s => s.Uuid == uuid));
        }

        public async Task<EventTeam> GetItemByKeyAsync(string key)
        {
            // key = EventKey|TeamNumber
            string[] keys = key.Split('|');
            FillList();
            return await Task.FromResult(items.FirstOrDefault(s => s.EventKey == keys[0]
                                                              && s.TeamNumber == int.Parse(keys[1])));
        }

        public async Task<IEnumerable<EventTeam>> GetItemsAsync(bool forceRefresh = false)
        {
            FillList();
            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(EventTeam item)
        {
            FillList();
            var oldItem = items.Where((EventTeam arg) => arg.Uuid == item.Uuid).FirstOrDefault();
            item.Id = oldItem.Id;
            items.Remove(oldItem);
            await App.database.SaveEventTeamAsync(item);
            items = null;
            FillList();
            return await Task.FromResult(true);
        }
    }
}
