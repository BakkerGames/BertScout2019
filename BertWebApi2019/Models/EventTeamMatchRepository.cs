using BertScout2019Data.Data;
using BertScout2019Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public EventTeamMatch GetByUuid(string uuid)
        {
            return items.Find(p => p.Uuid == uuid);
        }

        public IEnumerable<EventTeamMatch> GetAll()
        {
            return items;
        }

        public IEnumerable<EventTeamMatch> GetAllByKey(string key)
        {
            // key = "EventKey|TeamNumber"
            string[] keys = key.Split('|');
            return items.FindAll(p => p.EventKey == keys[0]
                                      && p.TeamNumber == int.Parse(keys[1]));
        }

        public EventTeamMatch GetByKey(string key)
        {
            // key = "EventKey|TeamNumber|MatchNumber"
            string[] keys = key.Split('|');
            return items.Find(p => p.EventKey == keys[0]
                                   && p.TeamNumber == int.Parse(keys[1])
                                   && p.MatchNumber == int.Parse(keys[2]));
        }

        public void Remove(int id)
        {
            _database.DeleteEventTeamMatchAsync(id);
            items.RemoveAll(p => p.Id == id);
        }

        public void RemoveByUuid(string uuid)
        {
            Remove(GetByUuid(uuid).Id.Value);
        }

        public bool Update(EventTeamMatch item)
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
            int result = _database.SaveEventTeamMatchAsync(item).Result;
            items.Add(item);
            return true;
        }
    }
}
