using BertScout2019Data.Models;
using BertScout2019XmlData;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BertScout2019.Services
{
    public class XmlDataStoreTeams : IDataStore<Team>
    {
        List<Team> items;

        public XmlDataStoreTeams()
        {
        }

        private void FillList()
        {
            if (items == null)
            {
                items = new List<Team>();
                Stream stream = EmbeddedData.XmlDataStoreTeams();
                using (var reader = new StreamReader(stream))
                {
                    var serializer = new XmlSerializer(typeof(List<Team>));
                    items = (List<Team>)serializer.Deserialize(reader);
                }
            }
        }

        public async Task<bool> AddItemAsync(Team item)
        {
            FillList();
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            FillList();
            var oldItem = items.Where((Team arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string uuid)
        {
            FillList();
            var oldItem = items.Where((Team arg) => arg.Uuid == uuid).FirstOrDefault();
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

        public Task<Team> GetItemByKeyAsync(string key)
        {
            throw new System.NotImplementedException();
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
            items.Remove(oldItem);
            items.Add(item);
            return await Task.FromResult(true);
        }
    }
}
