using BertScout2019Data.Data;
using BertScout2019Data.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace FRCEventStore.Models
{
    public class FRCEventRepository : IFRCEventRepository
    {
        private const string dbFilename = "bertscout2019.db3";
        public static BertScout2019Database _database;

        private List<FRCEvent> items = new List<FRCEvent>();
        private int _nextId = 1;

        public FRCEventRepository()
        {
            // connect to database
            if (_database == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbFilename);
                _database = new BertScout2019Database(dbPath);
            }
            // fill local list
            items = _database.GetEventsAsync().Result;
            //Add(new FRCEvent { Name = "PineTree", EventKey = "NEDIST_PINE_TREE" });
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