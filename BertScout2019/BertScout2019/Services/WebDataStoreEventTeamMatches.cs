using BertScout2019Data.Models;
using Common.JSON;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BertScout2019.Services
{
    public class WebDataStoreEventTeamMatches : IDataStore<EventTeamMatch>
    {
        public async Task<bool> AddItemAsync(EventTeamMatch item)
        {
            StringContent content = new StringContent(item.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = App.client.PostAsync("api/EventTeamMatches", content).Result;
            response.EnsureSuccessStatusCode();
            return await Task.FromResult(response.IsSuccessStatusCode);
        }

        public Task<bool> DeleteItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<EventTeamMatch> GetItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EventTeamMatch> GetItemAsync(string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<EventTeamMatch> GetItemByKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EventTeamMatch>> GetItemsAsync(bool forceRefresh = false)
        {
            List<EventTeamMatch> items = new List<EventTeamMatch>();
            HttpResponseMessage response = await App.client.GetAsync("api/EventTeamMatches");
            if (response.IsSuccessStatusCode)
            {
                string tempResult = await response.Content.ReadAsStringAsync();
                JArray tempJArray = JArray.Parse(tempResult);
                foreach (JObject item in tempJArray)
                {
                    items.Add(EventTeamMatch.Parse(item.ToString()));
                }
            }
            return items;
        }

        public async Task<bool> UpdateItemAsync(EventTeamMatch item)
        {
            StringContent content = new StringContent(item.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = App.client.PutAsync($"api/EventTeamMatches?uuid={item.Uuid}", content).Result;
            response.EnsureSuccessStatusCode();
            return await Task.FromResult(response.IsSuccessStatusCode);
        }
    }
}
