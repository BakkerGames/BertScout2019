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

        public int TotalRP = 0;
        public int TotalScore = 0;
        public int MatchCount = 0;
        public int AverageScore = 0;

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
                    obj.Text2 = $"Score = {CalculateMatchResult(match)} RP = {CalculateMatchRP(match)}";
                    int rp = CalculateMatchRP(match);
                    int matchScore = CalculateMatchResult(match);
                    if (rp > 0 || matchScore > 0 || match.Broken > 0)
                    {
                        TotalRP += rp;
                        TotalScore += matchScore;
                        MatchCount++;
                        MatchResults.Add(obj);
                    }
                }
                AverageScore = TotalScore / MatchCount;
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

        private int CalculateMatchRP(EventTeamMatch match)
        {
            int rp = 0;
            rp += match.AllianceResult;
            rp += match.RocketRankingPoint;
            rp += match.HabRankingPoint;

            return rp;
        }

        private int CalculateMatchResult(EventTeamMatch match)
        {
            int score = 0;
            //not scoring movement type
            //score += match.SandstormMoveType;
            score += match.SandstormOffPlatform * 3;
            score += match.SandstormHatches * 2;
            score += match.SandstormCargo * 3;

            score += match.CargoShipHatches * 2;
            score += match.CargoShipCargo * 3;
            score += match.RocketHatches * 2;
            score += match.RocketCargo * 3;
            //not scoring highest platform
            //score += match.RocketHighestHatch;
            //score += match.RocketHighestCargo;

            //score += match.EndgamePlatform;
            switch (match.EndgamePlatform)
            {
                case 1:
                    score += 3;
                    break;
                case 2:
                    score += 6;
                    break;
                case 3:
                    score += 12;
                    break;
            }
            //not scoring buddy climb
            //score += match.EndgameBuddyClimb;

            //score += match.Defense;
            //score += match.Cooperation;
            score -= match.Fouls * 3;
            score -= match.TechFouls * 10;
            //score -= match.Broken*20;

            return score;
        }
    }
}
