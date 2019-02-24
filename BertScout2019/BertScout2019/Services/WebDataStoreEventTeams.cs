using BertScout2019Data.Models;
using Common.JSON;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BertScout2019.Services
{
    public class WebDataStoreEventTeams : IDataStore<EventTeam>
    {
        private readonly string apiPath = "api/EventTeams";
        private readonly string mediaType = "application/json";

        public async Task<bool> AddItemAsync(EventTeam item)
        {
            StringContent content = new StringContent(item.ToString(), Encoding.UTF8, mediaType);
            HttpResponseMessage response = App.client.PostAsync(apiPath, content).Result;
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

        public Task<EventTeam> GetItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EventTeam> GetItemAsync(string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<EventTeam> GetItemByKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EventTeam>> GetItemsAsync(bool forceRefresh = false)
        {
            List<EventTeam> items = new List<EventTeam>();
            HttpResponseMessage response = await App.client.GetAsync(apiPath);
            if (response.IsSuccessStatusCode)
            {
                string tempResult = await response.Content.ReadAsStringAsync();
                JArray tempJArray = JArray.Parse(tempResult);
                foreach (JObject item in tempJArray)
                {
                    items.Add(EventTeam.Parse(item.ToString()));
                }
            }
            return items;
        }

        public async Task<bool> UpdateItemAsync(EventTeam item)
        {
            StringContent content = new StringContent(item.ToString(), Encoding.UTF8, mediaType);
            HttpResponseMessage response = App.client.PutAsync($"{apiPath}?uuid={item.Uuid}", content).Result;
            response.EnsureSuccessStatusCode();
            return await Task.FromResult(response.IsSuccessStatusCode);
        }
    }
}
