using BertScout2019.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BertScout2019.Services
{
    public class XmlDataStoreEventTeams : IDataStore<EventTeam>
    {
        List<EventTeam> items;

        public XmlDataStoreEventTeams()
        {
            items = new List<EventTeam>();
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("BertScout2019.EmbeddedResources.EventTeams.xml");
            using (var reader = new StreamReader(stream))
            {
                var serializer = new XmlSerializer(typeof(List<EventTeam>));
                items = (List<EventTeam>)serializer.Deserialize(reader);
            }
        }

        public async Task<bool> AddItemAsync(EventTeam item)
        {
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((EventTeam arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<EventTeam> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<EventTeam>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(EventTeam item)
        {
            var oldItem = items.Where((EventTeam arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);
            return await Task.FromResult(true);
        }
    }
}
