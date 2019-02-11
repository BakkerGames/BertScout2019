using BertScout2019Data.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BertScout2019Data.Data
{
    public class BertScout2019Database
    {
        static SQLiteAsyncConnection database;

        public BertScout2019Database(string dbPath)
        {
            try
            {
                database = new SQLiteAsyncConnection(dbPath);
                CreateTables();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
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

        // FRCEvent

        public Task<List<FRCEvent>> GetEventsAsync()
        {
            return database.Table<FRCEvent>().ToListAsync();
        }

        public Task<FRCEvent> GetEventAsync(string eventKey)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT [FRCEvent].* FROM [FRCEvent]");
            query.Append(" WHERE [FRCEvent].[EventKey] = '");
            query.Append(FixSqlValue(eventKey));
            query.Append("'");
            return database.GetAsync<FRCEvent>(query.ToString());
        }

        public Task<int> SaveFRCEventAsync(FRCEvent item)
        {
        	// note: the caller must let this resolve before item.Id is first
        	// available, using either "await" or "int x = ...().Result;"
            if (item.Uuid == null)
            {
                item.Uuid = Guid.NewGuid().ToString();
            }
            return database.InsertOrReplaceAsync(item);
        }

        // Team

        public Task<List<Team>> GetTeamsAsync()
        {
            return database.Table<Team>().ToListAsync();
        }

        public Task<List<Team>> GetTeamsByEventAsync(string eventKey)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT [Team].* FROM [Team]");
            query.Append(" LEFT JOIN [EventTeam]");
            query.Append(" ON [EventTeam].[TeamNumber] = [Team].[TeamNumber]");
            query.Append(" WHERE [EventTeam].[EventKey] = '");
            query.Append(FixSqlValue(eventKey));
            query.Append("'");
            return database.QueryAsync<Team>(query.ToString());
        }

        public Task<Team> GetTeamAsync(int teamNumber)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT [Team].* FROM [Team]");
            query.Append(" WHERE [Team].[TeamNumber] = ");
            query.Append(teamNumber);
            return database.GetAsync<Team>(query.ToString());
        }

        public Task<int> SaveTeamAsync(Team item)
        {
        	// note: the caller must let this resolve before item.Id is first
        	// available, using either "await" or "int x = ...().Result;"
            if (item.Uuid == null)
            {
                item.Uuid = Guid.NewGuid().ToString();
            }
            return database.InsertOrReplaceAsync(item);
        }

        // EventTeam

        public Task<List<EventTeam>> GetEventTeamsAsync()
        {
            return database.Table<EventTeam>().ToListAsync();
        }

        public Task<List<EventTeam>> GetEventTeamsAsync(string eventKey)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT [EventTeam].* FROM [EventTeam]");
            query.Append(" WHERE [EventTeam].[EventKey] = '");
            query.Append(FixSqlValue(eventKey));
            query.Append("'");
            return database.QueryAsync<EventTeam>(query.ToString());
        }

        public Task<EventTeam> GetEventTeamAsync(string eventKey, int teamNumber)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT [EventTeam].* FROM [EventTeam]");
            query.Append(" WHERE [EventTeam].[EventKey] = '");
            query.Append(FixSqlValue(eventKey));
            query.Append("'");
            query.Append(" AND [EventTeamMatch].[TeamNumber] = ");
            query.Append(teamNumber);
            return database.GetAsync<EventTeam>(query.ToString());
        }

        public Task<int> SaveEventTeamAsync(EventTeam item)
        {
        	// note: the caller must let this resolve before item.Id is first
        	// available, using either "await" or "int x = ...().Result;"
            if (item.Uuid == null)
            {
                item.Uuid = Guid.NewGuid().ToString();
            }
            return database.InsertOrReplaceAsync(item);
        }

        // EventTeamMatch

        public Task<List<EventTeamMatch>> GetEventTeamMatchesAsync()
        {
            return database.Table<EventTeamMatch>().ToListAsync();
        }

        public Task<List<EventTeamMatch>> GetEventTeamMatchesAsync(string eventKey, int teamNumber)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT [EventTeamMatch].* FROM [EventTeamMatch]");
            query.Append(" WHERE [EventTeamMatch].[EventKey] = '");
            query.Append(FixSqlValue(eventKey));
            query.Append("'");
            query.Append(" AND [EventTeamMatch].[TeamNumber] = ");
            query.Append(teamNumber);
            query.Append(" ORDER BY [EventTeamMatch].[MatchNumber]");
            return database.QueryAsync<EventTeamMatch>(query.ToString());
        }

        public Task<EventTeamMatch> GetEventTeamMatchAsync(string eventKey, int teamNumber, int matchNumber)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT [EventTeamMatch].* FROM [EventTeamMatch]");
            query.Append(" WHERE [EventTeamMatch].[EventKey] = '");
            query.Append(FixSqlValue(eventKey));
            query.Append("'");
            query.Append(" AND [EventTeamMatch].[TeamNumber] = ");
            query.Append(teamNumber);
            query.Append(" AND [EventTeamMatch].[MatchNumber] = ");
            query.Append(matchNumber);
            return database.GetAsync<EventTeamMatch>(query.ToString());
        }

        public Task<int> SaveEventTeamMatchAsync(EventTeamMatch item)
        {
        	// note: the caller must let this resolve before item.Id is first
        	// available, using either "await" or "int x = ...().Result;"
            if (item.Uuid == null)
            {
                item.Uuid = Guid.NewGuid().ToString();
            }
            return database.InsertOrReplaceAsync(item);
        }

        // internal functions

        private string FixSqlValue(string value)
        {
            return value?.Replace("'", "''") ?? "";
        }
    }
}
