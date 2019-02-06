﻿using BertScout2019.Models;
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
            items.Add(item);
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

        public Task<IEnumerable<EventTeam>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(EventTeam item)
        {
            throw new System.NotImplementedException();
        }

        //public SqlDataStoreEventTeams(string frcEventKey)
        //{
        //    // must complete, so don't async/await
        //    items = App.Database.GetEventTeamsAsync(frcEventKey).Result;
        //}

        //public async Task<bool> AddItemAsync(Team item)
        //{
        //    items.Add(item);
        //    return await Task.FromResult(true);
        //}

        //public async Task<bool> DeleteItemAsync(int id)
        //{
        //    var oldItem = items.Where((Team arg) => arg.Id == id).FirstOrDefault();
        //    items.Remove(oldItem);
        //    return await Task.FromResult(true);
        //}

        //public async Task<Team> GetItemAsync(int id)
        //{
        //    return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        //}

        //public async Task<Team> GetItemAsync(string eventKey, int teamNumber)
        //{
        //    return await Task.FromResult(items.FirstOrDefault(s => s.TeamNumber == teamNumber));
        //}

        //public async Task<IEnumerable<Team>> GetItemsAsync(bool forceRefresh = false)
        //{
        //    return await Task.FromResult(items);
        //}

        //public async Task<bool> UpdateItemAsync(Team item)
        //{
        //    var oldItem = items.Where((Team arg) => arg.Id == item.Id).FirstOrDefault();
        //    items.Remove(oldItem);
        //    items.Add(item);
        //    return await Task.FromResult(true);
        //}
    }
}
