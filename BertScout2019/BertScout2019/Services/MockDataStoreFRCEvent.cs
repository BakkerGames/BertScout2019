using BertScout2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BertScout2019.Services
{
    public class MockDataStoreFRCEvent : IDataStore<FRCEvent>
    {
        List<FRCEvent> items;

        public MockDataStoreFRCEvent()
        {
            items = new List<FRCEvent>();
            var mockItems = new List<FRCEvent>
            {
                new FRCEvent { Id = Guid.NewGuid().ToString(),
                    Name = "Week Zero",
                    Location = "TBD",
                    StartDate = DateTime.Parse("02/16/2019"),
                    EndDate = DateTime.Parse("02/16/2019"),
                },
                new FRCEvent { Id = Guid.NewGuid().ToString(),
                    Name = "NE District Pine Tree Event",
                    Location = "Lewiston, ME, USA",
                    StartDate = DateTime.Parse("04/04/2019"),
                    EndDate = DateTime.Parse("04/06/2019"),
                },
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(FRCEvent item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(FRCEvent item)
        {
            var oldItem = items.Where((FRCEvent arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((FRCEvent arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<FRCEvent> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<FRCEvent>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}