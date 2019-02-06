using BertScout2019.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BertScout2019.Services
{
    public class SqlDataStoreMatchesByEventTeam : IDataStore<EventTeamMatch>
    {
        List<EventTeamMatch> items;

        public SqlDataStoreMatchesByEventTeam(string frcEventKey, int teamNumber)
        {
            // must complete, so don't async/await
            items = App.Database.GetEventTeamMatchesAsync(frcEventKey, teamNumber).Result;
        }

        public async Task<bool> AddItemAsync(EventTeamMatch item)
        {
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((EventTeamMatch arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<EventTeamMatch> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<EventTeamMatch> GetItemAsync(string eventKey, int teamNumber, int matchNumber)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.EventKey == eventKey
                                                              && s.TeamNumber == teamNumber
                                                              && s.MatchNumber == matchNumber));
        }

        public async Task<IEnumerable<EventTeamMatch>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(EventTeamMatch item)
        {
            var oldItem = items.Where((EventTeamMatch arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);
            return await Task.FromResult(true);
        }
    }
}
