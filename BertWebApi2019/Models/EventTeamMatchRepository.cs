using BertScout2019Data.Data;
using BertScout2019Data.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace BertWebApi2019.Models
{
    public class EventTeamMatchRepository : IRepository<EventTeamMatch>
    {
        public static BertScout2019Database _database;

        private List<EventTeamMatch> items = new List<EventTeamMatch>();

        public EventTeamMatchRepository()
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
            items = _database.GetEventTeamMatchesAsync().Result;
        }

        public EventTeamMatch Add(EventTeamMatch item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            EventTeamMatch oldItem = items.Find(p => p.Uuid == item.Uuid);
            if (oldItem == null)
            {
                // this must finish resolving to get item.Id
                int result = _database.SaveEventTeamMatchAsync(item).Result;
                items.Add(item);
            }
            return item;
        }

        public EventTeamMatch Get(int id)
        {
            return items.Find(p => p.Id == id);
        }

        public IEnumerable<EventTeamMatch> GetAll()
        {
            return items;
        }

        public IEnumerable<EventTeamMatch> GetAllByKey(object key)
        {
            return items.FindAll(p => p.EventKey == (string)key);
        }

        public EventTeamMatch GetByKey(object key)
        {
            return items.Find(p => p.EventKey == (string)key);
        }

        public void Remove(int id)
        {
            _database.DeleteEventTeamMatchAsync(id);
            items.RemoveAll(p => p.Id == id);
        }

        public bool Update(EventTeamMatch item)
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
            int result = _database.SaveEventTeamMatchAsync(item).Result;
            items.Add(item);
            return true;
        }
    }
}
