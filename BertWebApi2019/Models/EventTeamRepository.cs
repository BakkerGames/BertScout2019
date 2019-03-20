using BertScout2019Data.Data;
using BertScout2019Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BertWebApi2019.Models
{
    public class EventTeamRepository : IRepository<EventTeam>
    {
        public static BertScout2019Database _database;

        private List<EventTeam> items = new List<EventTeam>();

        public EventTeamRepository()
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
            items = _database.GetEventTeamsAsync().Result;
        }

        public EventTeam Add(EventTeam item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            EventTeam oldItem = items.Find(p => p.Uuid == item.Uuid);
            if (oldItem == null)
            {
                // this must finish resolving to get item.Id
                int result = _database.SaveEventTeamAsync(item).Result;
                items.Add(item);
            }
            return item;
        }

        public EventTeam Get(int id)
        {
            return items.Find(p => p.Id == id);
        }

        public EventTeam GetByUuid(string uuid)
        {
            return items.Find(p => p.Uuid == uuid);
        }

        public IEnumerable<EventTeam> GetAll()
        {
            return items;
        }

        public IEnumerable<EventTeam> GetAllByKey(string key)
        {
            // key = "EventKey"
            return items.FindAll(p => p.EventKey == key);
        }

        public EventTeam GetByKey(string key)
        {
            // key = "EventKey|TeamNumber"
            string[] keys = key.Split('|');
            return items.Find(p => p.EventKey == keys[0]
                              && p.TeamNumber == int.Parse(keys[1]));
        }

        public void Remove(int id)
        {
            _database.DeleteEventTeamAsync(id);
            items.RemoveAll(p => p.Id == id);
        }

        public void RemoveByUuid(string uuid)
        {
            Remove(GetByUuid(uuid).Id.Value);
        }

        public bool Update(EventTeam item)
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
            int result = _database.SaveEventTeamAsync(item).Result;
            items.Add(item);
            return true;
        }

        public IEnumerable<EventTeam> GetNextBatchByKey(string key)
        {
            // key = "EventKey|LastId|Count"
            throw new NotImplementedException();
        }
    }
}
