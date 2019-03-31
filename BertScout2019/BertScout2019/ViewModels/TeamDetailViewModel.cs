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
        public string savedEventKey;
        public Team savedTeam;

        public int TotalRP = 0;
        public int TotalScore = 0;
        public int MatchCount = 0;
        public int AverageScore = 0;
        public int TotalHatches = 0;
        public int TotalCargo = 0;
        public int AverageHatches = 0;
        public int AverageCargo = 0;

        public IDataStore<EventTeamMatch> DataStoreMatch;

        public ObservableCollection<MatchResult> MatchResults { get; set; }

        public TeamDetailViewModel(string eventKey, Team item)
        {
            savedEventKey = eventKey;
            savedTeam = item;
            Title = $"Team {App.currTeamNumber} Details";
            MatchResults = new ObservableCollection<MatchResult>();
            DataStoreMatch = new SqlDataStoreEventTeamMatches(eventKey, item.TeamNumber);
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
                    obj.EventKey = match.EventKey;
                    obj.TeamNumber = match.TeamNumber;
                    obj.MatchNumber = match.MatchNumber;
                    int matchRP = CalculateMatchRP(match);
                    int matchScore = CalculateMatchResult(match);
                    int hatchCount = CalculateHatchCount(match);
                    int cargoCount = CalculateCargoCount(match);
                    // show match results
                    obj.Text1 = $"Match {match.MatchNumber} -" +
                        $" Score: {matchScore} RP: {matchRP}" +
                        $" Hatch: {hatchCount} Cargo: {cargoCount}";
                    string broken = "";
                    if (match.Broken == 1)
                    {
                        broken= "Broken: Some ";
                    }
                    else if (match.Broken == 2)
                    {
                        broken= "Broken: Lots ";
                    }
                    obj.Text2 = broken + match.Comments;
                    if (matchRP > 0 || matchScore > 0 || match.Broken > 0 || hatchCount > 0 || cargoCount > 0)
                    {
                        TotalRP += matchRP;
                        TotalScore += matchScore;
                        TotalHatches += hatchCount;
                        TotalCargo += cargoCount;
                        MatchCount++;
                        MatchResults.Add(obj);
                    }
                }
                AverageScore = TotalScore / MatchCount;
                AverageHatches = TotalHatches / MatchCount;
                AverageCargo = TotalCargo / MatchCount;
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

        private int CalculateCargoCount(EventTeamMatch match)
        {
            int result = 0;
            result += match.SandstormCargo;
            result += match.CargoShipCargo;
            result += match.RocketCargo;
            return result;
        }

        private int CalculateHatchCount(EventTeamMatch match)
        {
            int result = 0;
            result += match.SandstormHatches;
            result += match.CargoShipHatches;
            result += match.RocketHatches;
            return result;
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
