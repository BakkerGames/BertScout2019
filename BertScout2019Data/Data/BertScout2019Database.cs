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
        private static SQLiteAsyncConnection _database;

        public BertScout2019Database(string dbPath)
        {
            try
            {
                _database = new SQLiteAsyncConnection(dbPath);
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
            _database.DropTableAsync<FRCEvent>().Wait();
            _database.DropTableAsync<Team>().Wait();
            _database.DropTableAsync<EventTeam>().Wait();
            _database.DropTableAsync<EventTeamMatch>().Wait();
        }

        public void CreateTables()
        {
            _database.CreateTableAsync<FRCEvent>().Wait();
            _database.CreateTableAsync<Team>().Wait();
            _database.CreateTableAsync<EventTeam>().Wait();
            _database.CreateTableAsync<EventTeamMatch>().Wait();
        }

        // FRCEvent

        public Task<List<FRCEvent>> GetEventsAsync()
        {
            return _database.Table<FRCEvent>().ToListAsync();
        }

        public Task<FRCEvent> GetEventAsync(string eventKey)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT [FRCEvent].* FROM [FRCEvent]");
            query.Append(" WHERE [FRCEvent].[EventKey] = '");
            query.Append(FixSqlValue(eventKey));
            query.Append("'");
            return _database.GetAsync<FRCEvent>(query.ToString());
        }

        public Task<int> SaveFRCEventAsync(FRCEvent item)
        {
            // note: the caller must let this resolve before item.Id is first
            // available, using either "await" or "int x = ...().Result;"
            if (item.Uuid == null)
            {
                item.Uuid = Guid.NewGuid().ToString();
            }
            return _database.InsertOrReplaceAsync(item);
        }

        public Task<int> DeleteFRCEventAsync(int id)
        {
            return _database.DeleteAsync<FRCEvent>(id);
        }

        // Team

        public Task<List<Team>> GetTeamsAsync()
        {
            return _database.Table<Team>().ToListAsync();
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
            return _database.QueryAsync<Team>(query.ToString());
        }

        public Task<Team> GetTeamAsync(int teamNumber)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT [Team].* FROM [Team]");
            query.Append(" WHERE [Team].[TeamNumber] = ");
            query.Append(teamNumber);
            return _database.GetAsync<Team>(query.ToString());
        }

        public Task<int> SaveTeamAsync(Team item)
        {
            // note: the caller must let this resolve before item.Id is first
            // available, using either "await" or "int x = ...().Result;"
            if (item.Uuid == null)
            {
                item.Uuid = Guid.NewGuid().ToString();
            }
            return _database.InsertOrReplaceAsync(item);
        }

        public Task<int> DeleteTeamAsync(int id)
        {
            return _database.DeleteAsync<Team>(id);
        }

        // EventTeam

        public Task<List<EventTeam>> GetEventTeamsAsync()
        {
            return _database.Table<EventTeam>().ToListAsync();
        }

        public Task<List<EventTeam>> GetEventTeamsAsync(string eventKey)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT [EventTeam].* FROM [EventTeam]");
            query.Append(" WHERE [EventTeam].[EventKey] = '");
            query.Append(FixSqlValue(eventKey));
            query.Append("'");
            return _database.QueryAsync<EventTeam>(query.ToString());
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
            return _database.GetAsync<EventTeam>(query.ToString());
        }

        public Task<int> SaveEventTeamAsync(EventTeam item)
        {
            // note: the caller must let this resolve before item.Id is first
            // available, using either "await" or "int x = ...().Result;"
            if (item.Uuid == null)
            {
                item.Uuid = Guid.NewGuid().ToString();
            }
            return _database.InsertOrReplaceAsync(item);
        }

        public Task<int> DeleteEventTeamAsync(int id)
        {
            return _database.DeleteAsync<EventTeam>(id);
        }

        // EventTeamMatch

        public Task<List<EventTeamMatch>> GetEventTeamMatchesAsync()
        {
            return _database.Table<EventTeamMatch>().ToListAsync();
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
            return _database.QueryAsync<EventTeamMatch>(query.ToString());
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
            return _database.GetAsync<EventTeamMatch>(query.ToString());
        }

        public Task<int> SaveEventTeamMatchAsync(EventTeamMatch item)
        {
            // note: the caller must let this resolve before item.Id is first
            // available, using either "await" or "int x = ...().Result;"
            if (item.Uuid == null)
            {
                item.Uuid = Guid.NewGuid().ToString();
            }
            return _database.InsertOrReplaceAsync(item);
        }

        public Task<int> DeleteEventTeamMatchAsync(int id)
        {
            return _database.DeleteAsync<EventTeamMatch>(id);
        }

        // internal functions

        private string FixSqlValue(string value)
        {
            return value?.Replace("'", "''") ?? "";
        }
    }
}
