using BertScout2019Data.Data;
using BertScout2019Data.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace BertWebApi2019v2.Models
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
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
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
            // this must finish resolving to get item.Id
            int result = _database.SaveTeamAsync(item).Result;
            items.Add(item);
            return item;
        }

        public Team Get(int id)
        {
            return items.Find(p => p.Id == id);
        }

        public IEnumerable<Team> GetAll()
        {
            return items;
        }

        public IEnumerable<Team> GetAllByKey(object key)
        {
            return items.FindAll(p => p.TeamNumber == (int)key);
        }

        public Team GetByKey(object key)
        {
            return items.Find(p => p.TeamNumber == (int)key);
        }

        public void Remove(int id)
        {
            _database.DeleteTeamAsync(id);
            items.RemoveAll(p => p.Id == id);
        }

        public bool Update(Team item)
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
            int result = _database.SaveTeamAsync(item).Result;
            items.Add(item);
            return true;
        }
    }
}
