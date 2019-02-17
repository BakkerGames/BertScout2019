using BertScout2019Data.Data;
using BertScout2019Data.Models;
using System;
using System.Collections.Generic;
using System.IO;

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
            // this must finish resolving to get item.Id
            int result = _database.SaveEventTeamAsync(item).Result;
            items.Add(item);
            return item;
        }

        public EventTeam Get(int id)
        {
            return items.Find(p => p.Id == id);
        }

        public IEnumerable<EventTeam> GetAll()
        {
            return items;
        }

        public IEnumerable<EventTeam> GetAllByKey(object key)
        {
            return items.FindAll(p => p.EventKey == (string)key);
        }

        public EventTeam GetByKey(object key)
        {
            string[] keys = (string[])key;
            return items.Find(p => p.EventKey == keys[0] && p.TeamNumber == int.Parse(keys[1]));
        }

        public void Remove(int id)
        {
            _database.DeleteEventTeamAsync(id);
            items.RemoveAll(p => p.Id == id);
        }

        public bool Update(EventTeam item)
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
            int result = _database.SaveEventTeamAsync(item).Result;
            items.Add(item);
            return true;
        }
    }
}
