using BertScout2019Data.Models;
using BertScout2019XmlData;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Stream stream = EmbeddedData.XmlDataStoreEventTeams();
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

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((EventTeam arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<EventTeam> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public Task<EventTeam> GetItemByTagAsync(string tag)
        {
            throw new System.NotImplementedException();
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
