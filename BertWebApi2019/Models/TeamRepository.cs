using BertScout2019Data.Data;
using BertScout2019Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BertWebApi2019.Models
{
    public class TeamRepository : IRepository<Team>
    {
        public static BertScout2019Database _database;

        private List<Team> items = new List<Team>();

        public TeamRepository()
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
            items = _database.GetTeamsAsync().Result;
        }

        public Team Add(Team item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            Team oldItem = items.Find(p => p.Uuid == item.Uuid);
            if (oldItem == null)
            {
                // this must finish resolving to get item.Id
                int result = _database.SaveTeamAsync(item).Result;
                items.Add(item);
            }
            return item;
        }

        public Team Get(int id)
        {
            return items.Find(p => p.Id == id);
        }

        public Team GetByUuid(string uuid)
        {
            return items.Find(p => p.Uuid == uuid);
        }

        public IEnumerable<Team> GetAll()
        {
            return items;
        }

        public IEnumerable<Team> GetAllByKey(string key)
        {
            // key = partial team name
            return items.FindAll(p => p.Name.Contains(key));
        }

        public Team GetByKey(string key)
        {
            // key = TeamNumber
            return items.Find(p => p.TeamNumber == int.Parse(key));
        }

        public void Remove(int id)
        {
            _database.DeleteTeamAsync(id);
            items.RemoveAll(p => p.Id == id);
        }

        public void RemoveByUuid(string uuid)
        {
            Remove(GetByUuid(uuid).Id.Value);
        }

        public bool Update(Team item)
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
            int result = _database.SaveTeamAsync(item).Result;
            items.Add(item);
            return true;
        }
    }
}
