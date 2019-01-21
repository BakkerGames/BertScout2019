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
                    Name = "Event 1",
                    Location = "TBD",
                    StartDate ="03/01/2019",
                    EndDate = "03/01/2019",
                },
                new FRCEvent { Id = Guid.NewGuid().ToString(),
                    Name = "Event 2",
                    Location = "TBD",
                    StartDate ="03/02/2019",
                    EndDate = "03/02/2019",
                },
                new FRCEvent { Id = Guid.NewGuid().ToString(),
                    Name = "Event 3",
                    Location = "TBD",
                    StartDate ="03/03/2019",
                    EndDate = "03/03/2019",
                },
                new FRCEvent { Id = Guid.NewGuid().ToString(),
                    Name = "Event 4",
                    Location = "TBD",
                    StartDate ="03/04/2019",
                    EndDate = "03/04/2019",
                },
                new FRCEvent { Id = Guid.NewGuid().ToString(),
                    Name = "Event 5",
                    Location = "TBD",
                    StartDate ="03/05/2019",
                    EndDate = "03/05/2019",
                },
                new FRCEvent { Id = Guid.NewGuid().ToString(),
                    Name = "Event 6",
                    Location = "TBD",
                    StartDate ="03/06/2019",
                    EndDate = "03/06/2019",
                },
                new FRCEvent { Id = Guid.NewGuid().ToString(),
                    Name = "Event 7",
                    Location = "TBD",
                    StartDate ="03/07/2019",
                    EndDate = "03/07/2019",
                },
                new FRCEvent { Id = Guid.NewGuid().ToString(),
                    Name = "Event 8",
                    Location = "TBD",
                    StartDate ="03/08/2019",
                    EndDate = "03/08/2019",
                },
                new FRCEvent { Id = Guid.NewGuid().ToString(),
                    Name = "Event 9",
                    Location = "TBD",
                    StartDate ="03/09/2019",
                    EndDate = "03/09/2019",
                },
                new FRCEvent { Id = Guid.NewGuid().ToString(),
                    Name = "Event 10",
                    Location = "TBD",
                    StartDate ="03/10/2019",
                    EndDate = "03/10/2019",
                },
                new FRCEvent { Id = Guid.NewGuid().ToString(),
                    Name = "Event 11",
                    Location = "TBD",
                    StartDate ="03/11/2019",
                    EndDate = "03/11/2019",
                },
                new FRCEvent { Id = Guid.NewGuid().ToString(),
                    Name = "Event 12",
                    Location = "TBD",
                    StartDate ="03/12/2019",
                    EndDate = "03/12/2019",
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