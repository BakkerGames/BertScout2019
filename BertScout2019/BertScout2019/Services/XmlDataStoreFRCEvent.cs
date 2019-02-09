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
            items = new List<FRCEvent>();
            Stream stream = EmbeddedData.XmlDataStoreFRCEvents();
            using (var reader = new StreamReader(stream))
            {
                var serializer = new XmlSerializer(typeof(List<FRCEvent>));
                items = (List<FRCEvent>)serializer.Deserialize(reader);
            }
        }

        public async Task<bool> AddItemAsync(FRCEvent item)
        {
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((FRCEvent arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<FRCEvent> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public Task<FRCEvent> GetItemByTagAsync(string tag)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<FRCEvent>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(FRCEvent item)
        {
            var oldItem = items.Where((FRCEvent arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);
            return await Task.FromResult(true);
        }
    }
}
