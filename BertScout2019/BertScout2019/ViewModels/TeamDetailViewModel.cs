using BertScout2019.Models;
using BertScout2019.Services;
using BertScout2019Data.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BertScout2019.ViewModels
{
    public class TeamDetailViewModel : BaseViewModel
    {

        public Team item;

        public IDataStore<EventTeamMatch> DataStoreMatch;

        public ObservableCollection<MatchResult> MatchResults { get; set; }

        public TeamDetailViewModel(Team item)
        {
            this.item = item;
            Title = $"Team {App.currTeamNumber} Details";
            MatchResults = new ObservableCollection<MatchResult>();
            DataStoreMatch = new SqlDataStoreMatchesByEventTeam(App.currFRCEventKey, item.TeamNumber);
            ExecuteLoadEventTeamMatchesCommand();
        }

        public void ExecuteLoadEventTeamMatchesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                MatchResults.Clear();
                var matches = DataStoreMatch.GetItemsAsync(true).Result;
                foreach (var match in matches)
                {
                    MatchResult obj = new MatchResult();
                    // todo fill in text with useful match results
                    obj.Text1 = $"Match {match.MatchNumber}";
                    obj.Text2 = CalculateMatchResult(match);
                    MatchResults.Add(obj);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private string CalculateMatchResult(EventTeamMatch match)
        {
            string result;
            int score = 0;
            score += match.SandstormMoveType;
            score += match.SandstormOffPlatform;
            score += match.SandstormHatches;
            score += match.SandstormCargo;

            score += match.CargoShipHatches;
            score += match.CargoShipCargo;
            score += match.RocketHatches;
            score += match.RocketCargo;
            score += match.RocketHighestHatch;
            score += match.RocketHighestCargo;

            score += match.EndgamePlatform;
            score += match.EndgameBuddyClimb;

            score += match.Defense;
            score += match.Cooperation;
            score -= match.Fouls * 10;
            score -= match.Broken * 20;

            int rp = 0;

            rp += match.AllianceResult;
            rp += match.RocketRankingPoint;
            rp += match.HabRankingPoint;
            result = $"Score: {score} RP: {rp}";
            // todo add score and ranking points

            return result;
        }
    }
}
