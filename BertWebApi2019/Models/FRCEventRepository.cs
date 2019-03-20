using BertScout2019Data.Data;
using BertScout2019Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                    AppDomain.CurrentDomain.BaseDirectory
                    , "App_Data"
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
            FRCEvent oldItem = items.Find(p => p.Uuid == item.Uuid);
            if (oldItem == null)
            {
                // this must finish resolving to get item.Id
                int result = _database.SaveFRCEventAsync(item).Result;
                items.Add(item);
            }
            return item;
        }

        public FRCEvent Get(int id)
        {
            return items.Find(p => p.Id == id);
        }

        public FRCEvent GetByUuid(string uuid)
        {
            return items.Find(p => p.Uuid == uuid);
        }

        public IEnumerable<FRCEvent> GetAll()
        {
            return items;
        }

        public IEnumerable<FRCEvent> GetAllByKey(string key)
        {
            // key = partial EventKey
            return items.FindAll(p => p.EventKey.Contains(key));
        }

        public FRCEvent GetByKey(string key)
        {
            // key = "EventKey"
            return items.Find(p => p.EventKey == key);
        }

        public void Remove(int id)
        {
            _database.DeleteFRCEventAsync(id);
            items.RemoveAll(p => p.Id == id);
        }

        public void RemoveByUuid(string uuid)
        {
            Remove(GetByUuid(uuid).Id.Value);
        }

        public bool Update(FRCEvent item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (item.Uuid == null)
            {
                throw new ArgumentNullException("item.Uuid");
            }
            int index = items.FindIndex(p => p.Uuid == item.Uuid);
            if (index == -1)
            {
                return false;
            }
            // update the id to match the local database
            item.Id = items.ElementAt(index).Id;
            items.RemoveAt(index);
            int result = _database.SaveFRCEventAsync(item).Result;
            items.Add(item);
            return true;
        }

        public IEnumerable<FRCEvent> GetNextBatchByKey(string key)
        {
            // key = "EventKey|LastId|Count"
            throw new NotImplementedException();
        }
    }
}
