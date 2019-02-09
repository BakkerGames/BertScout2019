using BertScout2019Data.Models;
using System;
using System.Collections.Generic;

namespace FRCEventStore.Models
{
    public class FRCEventRepository : IFRCEventRepository
    {
        private List<BertScout2019Data.Models.FRCEvent> FRCEvents = new List<FRCEvent>();
        private int _nextId = 1;

        public FRCEventRepository()
        {
            Add(new FRCEvent { Name = "PineTree", EventKey ="NEDIST_PINE_TREE" });
        }

        public IEnumerable<FRCEvent> GetAll()
        {
            return FRCEvents;
        }

        public FRCEvent Get(int id)
        {
            return FRCEvents.Find(p => p.Id == id);
        }

        public FRCEvent Add(FRCEvent item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            item.Id = _nextId++;
            FRCEvents.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            FRCEvents.RemoveAll(p => p.Id == id);
        }

        public bool Update(FRCEvent item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = FRCEvents.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            FRCEvents.RemoveAt(index);
            FRCEvents.Add(item);
            return true;
        }
    }
}