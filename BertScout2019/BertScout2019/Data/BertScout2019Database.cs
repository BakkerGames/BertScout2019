using BertScout2019.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BertScout2019.Data
{
    public class BertScout2019Database
    {
        static SQLiteAsyncConnection database;

        public BertScout2019Database(string dbPath)
        {
            try
            {
                database = new SQLiteAsyncConnection(dbPath);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                database.CreateTableAsync<FRCEvent>().Wait();
                database.CreateTableAsync<Team>().Wait();
                database.CreateTableAsync<EventTeam>().Wait();
                database.CreateTableAsync<EventTeamMatch>().Wait();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public Task<List<FRCEvent>> GetEventsAsync()
        {
            return database.Table<FRCEvent>().ToListAsync();
        }

        public Task<List<Team>> GetEventTeamsAsync(string EventKey)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * FROM [Team]");
            query.Append(" LEFT JOIN [EventTeam]");
            query.Append(" ON [EventTeam].[EventKey] = '");
            query.Append(EventKey);
            query.Append("'");
            query.Append(" AND [EventTeam].[TeamNumber] = [Team].[TeamNumber]");
            return database.QueryAsync<Team>(query.ToString());
        }

        public Task<List<EventTeamMatch>> GetEventTeamMatchesAsync(string EventKey, int TeamNumber)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * FROM [EventTeamMatch]");
            query.Append(" WHERE [EventTeamMatch].[EventKey] = '");
            query.Append(EventKey);
            query.Append("'");
            query.Append(" AND [EventTeamMatch].[TeamNumber] = ");
            query.Append(TeamNumber);
            return database.QueryAsync<EventTeamMatch>(query.ToString());
        }

        public Task<int> SaveFRCEventAsync(FRCEvent item)
        {
            return database.InsertOrReplaceAsync(item);
        }

        public Task<int> SaveTeamAsync(Team item)
        {
            return database.InsertOrReplaceAsync(item);
        }

        public Task<int> SaveEventTeamAsync(EventTeam item)
        {
            return database.InsertOrReplaceAsync(item);
        }

        public Task<int> SaveEventTeamMatchAsync(EventTeamMatch item)
        {
            return database.InsertOrReplaceAsync(item);
        }

        //public Task<int> SaveItemAsync(TodoItem item)
        //{
        //    if (item.ID != 0)
        //    {
        //        return database.UpdateAsync(item);
        //    }
        //    else
        //    {
        //        return database.InsertAsync(item);
        //    }
        //}

        //public Task<int> DeleteItemAsync(TodoItem item)
        //{
        //    return database.DeleteAsync(item);
        //}
    }
}
