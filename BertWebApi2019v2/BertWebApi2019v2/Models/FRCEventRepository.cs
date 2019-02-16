using BertScout2019Data.Data;
using BertScout2019Data.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace BertWebApi2019.Models
{
    public class FRCEventRepository : IRepository<FRCEvent>
    {
        public static BertScout2019Database _database;

        private List<FRCEvent> items = new List<FRCEvent>();

        public FRCEventRepository()
        {
            // connect to database
            if (_database == null)
            {
                string dbPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                    , BertScout2019Database.dbFilename);
                _database = new BertScout2019Database(dbPath);
            }
            // fill local list
            items = _database.GetEventsAsync().Result;
        }

        public FRCEvent Add(FRCEvent item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            // this must finish resolving to get item.Id
            int result = _database.SaveFRCEventAsync(item).Result;
            items.Add(item);
            return item;
        }

        public FRCEvent Get(int id)
        {
            return items.Find(p => p.Id == id);
        }

        public IEnumerable<FRCEvent> GetAll()
        {
            return items;
        }

        public IEnumerable<FRCEvent> GetAllByKey(object key)
        {
            return items.FindAll(p => p.EventKey == (string)key);
        }

        public FRCEvent GetByKey(object key)
        {
            return items.Find(p => p.EventKey == (string)key);
        }

        public void Remove(int id)
        {
            _database.DeleteFRCEventAsync(id);
            items.RemoveAll(p => p.Id == id);
        }

        public bool Update(FRCEvent item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (item.Id == null)
            {
                throw new ArgumentNullException("item.Id");
            }
            int index = items.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            items.RemoveAt(index);
            int result = _database.SaveFRCEventAsync(item).Result;
            items.Add(item);
            return true;
        }
    }
}
