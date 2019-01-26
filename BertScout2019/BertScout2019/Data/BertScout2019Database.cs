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
                CreateTables();
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

        public Task<List<Team>> GetTeamsAsync()
        {
            return database.Table<Team>().ToListAsync();
        }

        public Task<List<Team>> GetEventTeamsAsync(string EventKey)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT [Team].* FROM [Team]");
            query.Append(" LEFT JOIN [EventTeam]");
            query.Append(" ON [EventTeam].[TeamNumber] = [Team].[TeamNumber]");
            query.Append(" WHERE [EventTeam].[EventKey] = '");
            query.Append(EventKey);
            query.Append("'");
            return database.QueryAsync<Team>(query.ToString());
        }

        public Task<List<EventTeamMatch>> GetEventTeamMatchesAsync(string EventKey, int TeamNumber)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT [EventTeamMatch].* FROM [EventTeamMatch]");
            query.Append(" WHERE [EventTeamMatch].[EventKey] = '");
            query.Append(EventKey);
            query.Append("'");
            query.Append(" AND [EventTeamMatch].[TeamNumber] = ");
            query.Append(TeamNumber);
            query.Append(" ORDER BY [EventTeamMatch].[MatchNumber]");
            return database.QueryAsync<EventTeamMatch>(query.ToString());
        }

        public Task<EventTeamMatch> GetEventTeamMatchAsync(string EventKey, int TeamNumber, int MatchNumber)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT [EventTeamMatch].* FROM [EventTeamMatch]");
            query.Append(" WHERE [EventTeamMatch].[EventKey] = '");
            query.Append(EventKey);
            query.Append("'");
            query.Append(" AND [EventTeamMatch].[TeamNumber] = ");
            query.Append(TeamNumber);
            query.Append(" AND [EventTeamMatch].[MatchNumber] = ");
            query.Append(MatchNumber);
            return database.GetAsync<EventTeamMatch>(query.ToString());
        }

        public void DropTables()
        {
            database.DropTableAsync<FRCEvent>().Wait();
            database.DropTableAsync<Team>().Wait();
            database.DropTableAsync<EventTeam>().Wait();
            database.DropTableAsync<EventTeamMatch>().Wait();
        }

        public void CreateTables()
        {
            database.CreateTableAsync<FRCEvent>().Wait();
            database.CreateTableAsync<Team>().Wait();
            database.CreateTableAsync<EventTeam>().Wait();
            database.CreateTableAsync<EventTeamMatch>().Wait();
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
