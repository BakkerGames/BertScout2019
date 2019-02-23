﻿using BertScout2019Data.Models;
using Common.JSON;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BertScout2019.Services
{
    public class WebDataStoreFRCEvents : IDataStore<FRCEvent>
    {
        public async Task<bool> AddItemAsync(FRCEvent item)
        {
            StringContent content = new StringContent(item.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = App.client.PostAsync("api/FRCEvents", content).Result;
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

        public Task<FRCEvent> GetItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<FRCEvent> GetItemAsync(string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<FRCEvent> GetItemByKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FRCEvent>> GetItemsAsync(bool forceRefresh = false)
        {
            List<FRCEvent> items = new List<FRCEvent>();
            HttpResponseMessage response = await App.client.GetAsync("api/FRCEvents");
            if (response.IsSuccessStatusCode)
            {
                string tempResult = await response.Content.ReadAsStringAsync();
                JArray tempJArray = JArray.Parse(tempResult);
                foreach (JObject item in tempJArray)
                {
                    items.Add(FRCEvent.Parse(item.ToString()));
                }
            }
            return items;
        }

        public async Task<bool> UpdateItemAsync(FRCEvent item)
        {
            StringContent content = new StringContent(item.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = App.client.PutAsync($"api/FRCEvents?uuid={item.Uuid}", content).Result;
            response.EnsureSuccessStatusCode();
            return await Task.FromResult(response.IsSuccessStatusCode);
        }
    }
}
