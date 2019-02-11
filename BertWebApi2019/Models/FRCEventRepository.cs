using BertScout2019Data.Models;
using System;
using System.Collections.Generic;

namespace FRCEventStore.Models
{
    public class FRCEventRepository : IFRCEventRepository
    {
        private List<FRCEvent> items = new List<FRCEvent>();
        private int _nextId = 1;

        public FRCEventRepository()
        {
            Add(new FRCEvent { Name = "PineTree", EventKey = "NEDIST_PINE_TREE" });
        }

        public IEnumerable<FRCEvent> GetAll()
        {
            return items;
        }

        public FRCEvent Get(int id)
        {
            return items.Find(p => p.Id == id);
        }

        public FRCEvent Add(FRCEvent item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            item.Id = _nextId++;
            items.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            items.RemoveAll(p => p.Id == id);
        }

        public bool Update(FRCEvent item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = items.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            items.RemoveAt(index);
            items.Add(item);
            return true;
        }
    }
}