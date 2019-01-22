using BertScout2019.Models;
using SQLite;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BertScout2019.Data
{
    public class BertScout2019Database
    {
        readonly SQLiteAsyncConnection database;

        public BertScout2019Database(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<FRCEvent>().Wait();
            database.CreateTableAsync<Team>().Wait();
            database.CreateTableAsync<EventTeam>().Wait();
        }

        public Task<List<FRCEvent>> GetEventsAsync()
        {
            return database.Table<FRCEvent>().ToListAsync();
        }

        public Task<List<Team>> GetEventTeamsAsync(string EventId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * FROM [Team]");
            query.Append(" LEFT JOIN [EventTeam]");
            query.Append(" ON [EventTeam].[EventId] = '");
            query.Append(EventId);
            query.Append("'");
            query.Append(" AND [EventTeam].[TeamNumber] = [Team].[TeamNumber]");
            return database.QueryAsync<Team>(query.ToString());
        }

        public Task<List<EventTeamMatch>> GetEventTeamMatchesAsync(string EventId, int TeamNumber)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * FROM [EventTeamMatch]");
            query.Append(" WHERE [EventTeamMatch].[EventId] = '");
            query.Append(EventId);
            query.Append("'");
            query.Append(" AND [EventTeamMatch].[TeamNumber] = ");
            query.Append(TeamNumber);
            return database.QueryAsync<EventTeamMatch>(query.ToString());
        }
        
        //public Task<int> SaveFRCEventAsync(FRCEvent item)
        //{
        //}
    }
}
