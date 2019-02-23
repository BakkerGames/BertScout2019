﻿using BertScout2019Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BertScout2019.Services
{
    public class SqlDataStoreEventTeams : IDataStore<EventTeam>
    {
        private List<EventTeam> items;

        public SqlDataStoreEventTeams()
        {
        }

        private void FillList()
        {
            if (items == null)
            {
                // must complete, so don't async/await
                items = App.Database.GetEventTeamsAsync().Result;
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
            items = App.Database.GetEventTeamsAsync().Result;
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            FillList();
            var oldItem = items.Where((EventTeam arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string uuid)
        {
            FillList();
            var oldItem = items.Where((EventTeam arg) => arg.Uuid == uuid).FirstOrDefault();
            items.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public Task<EventTeam> GetItemAsync(int id)
        {
            //FillList();
            throw new System.NotImplementedException();
        }

        public Task<EventTeam> GetItemAsync(string uuid)
        {
            //FillList();
            throw new NotImplementedException();
        }

        public Task<EventTeam> GetItemByKeyAsync(string key)
        {
            //FillList();
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<EventTeam>> GetItemsAsync(bool forceRefresh = false)
        {
            FillList();
            return await Task.FromResult(items);
        }

        public Task<bool> UpdateItemAsync(EventTeam item)
        {
            //FillList();
            throw new System.NotImplementedException();
        }
    }
}
