using BertScout2019Data.Models;
using Common.JSON;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BertScout2019.Services
{
    public class WebDataStoreTeams : IDataStore<Team>
    {
        public async Task<bool> AddItemAsync(Team item)
        {
            StringContent content = new StringContent(item.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = App.client.PostAsync("api/Teams", content).Result;
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

        public Task<Team> GetItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Team> GetItemAsync(string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<Team> GetItemByKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Team>> GetItemsAsync(bool forceRefresh = false)
        {
            List<Team> items = new List<Team>();
            HttpResponseMessage response = await App.client.GetAsync("api/Teams");
            if (response.IsSuccessStatusCode)
            {
                string tempResult = await response.Content.ReadAsStringAsync();
                JArray tempJArray = JArray.Parse(tempResult);
                foreach (JObject item in tempJArray)
                {
                    items.Add(Team.Parse(item.ToString()));
                }
            }
            return items;
        }

        public async Task<bool> UpdateItemAsync(Team item)
        {
            StringContent content = new StringContent(item.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = App.client.PutAsync($"api/Teams?uuid={item.Uuid}", content).Result;
            response.EnsureSuccessStatusCode();
            return await Task.FromResult(response.IsSuccessStatusCode);
        }
    }
}
