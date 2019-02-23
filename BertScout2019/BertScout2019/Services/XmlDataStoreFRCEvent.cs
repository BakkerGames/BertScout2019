using BertScout2019Data.Models;
using BertScout2019XmlData;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BertScout2019.Services
{
    public class XmlDataStoreFRCEvent : IDataStore<FRCEvent>
    {
        List<FRCEvent> items;

        public XmlDataStoreFRCEvent()
        {
        }

        private void FillList()
        {
            if (items == null)
            {
                items = new List<FRCEvent>();
                Stream stream = EmbeddedData.XmlDataStoreFRCEvents();
                using (var reader = new StreamReader(stream))
                {
                    var serializer = new XmlSerializer(typeof(List<FRCEvent>));
                    items = (List<FRCEvent>)serializer.Deserialize(reader);
                }
            }
        }

        public async Task<bool> AddItemAsync(FRCEvent item)
        {
            FillList();
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            FillList();
            var oldItem = items.Where((FRCEvent arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string uuid)
        {
            FillList();
            var oldItem = items.Where((FRCEvent arg) => arg.Uuid == uuid).FirstOrDefault();
            items.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<FRCEvent> GetItemAsync(int id)
        {
            FillList();
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<FRCEvent> GetItemAsync(string uuid)
        {
            FillList();
            return await Task.FromResult(items.FirstOrDefault(s => s.Uuid == uuid));
        }

        public Task<FRCEvent> GetItemByKeyAsync(string key)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<FRCEvent>> GetItemsAsync(bool forceRefresh = false)
        {
            FillList();
            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(FRCEvent item)
        {
            FillList();
            var oldItem = items.Where((FRCEvent arg) => arg.Uuid == item.Uuid).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);
            return await Task.FromResult(true);
        }
    }
}
