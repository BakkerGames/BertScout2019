using BertScout2019.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BertScout2019.Services
{
    public class SqlDataStoreFRCEvents : IDataStore<FRCEvent>
    {
        List<FRCEvent> items;

        public SqlDataStoreFRCEvents()
        {
            // must complete, so don't async/await
            items = App.Database.GetEventsAsync().Result;
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
