using Common.JSON;

// remember to increment the dbVersion in App.xaml.cs when changing this model

namespace BertScout2019.Models
{
    public class EventTeamMatch
    {
        public int? Id { get; set; }
        public string EventKey { get; set; }
        public int TeamNumber { get; set; }
        public int MatchNumber { get; set; }

        public int SandstormMoveType { get; set; }
        public int SandstormOffPlatform { get; set; }
        public int SandstormHatches { get; set; }
        public int SandstormCargo { get; set; }

        public int CargoShipHatches { get; set; }
        public int CargoShipCargo { get; set; }
        public int RocketHatches { get; set; }
        public int RocketCargo { get; set; }
        public int RocketHighestHatch { get; set; }
        public int RocketHighestCargo { get; set; }

        public int EndgamePlatform { get; set; }
        public int EndgameBuddyClimb { get; set; }

        public int Defense { get; set; }
        public int Cooperation { get; set; }
        public int Fouls { get; set; }
        public int Broken { get; set; }

        public int AllianceResult { get; set; }
        public int RocketRankingPoint { get; set; }
        public int HabRankingPoint { get; set; }

        public string ScouterName { get; set; }
        public string Comments { get; set; }

        public JObject ToJson()
        {
            JObject result = new JObject()
            {
                { "Id", Id },
                { "EventKey", EventKey },
                { "TeamNumber", TeamNumber },
                { "MatchNumber", MatchNumber },
                { "SandstormMoveType", SandstormMoveType },
                { "SandstormOffPlatform", SandstormOffPlatform },
                { "SandstormHatches", SandstormHatches },
                { "SandstormCargo", SandstormCargo },
                { "CargoShipHatches", CargoShipHatches },
                { "CargoShipCargo", CargoShipCargo },
                { "RocketHatches", RocketHatches },
                { "RocketCargo", RocketCargo },
                { "RocketHighestHatch", RocketHighestHatch },
                { "RocketHighestCargo", RocketHighestCargo },
                { "EndgamePlatform", EndgamePlatform },
                { "EndgameBuddyClimb", EndgameBuddyClimb },
                { "Defense", Defense },
                { "Cooperation", Cooperation },
                { "Fouls", Fouls },
                { "Broken", Broken },
                { "AllianceResult", AllianceResult },
                { "RocketRankingPoint", RocketRankingPoint },
                { "HabRankingPoint", HabRankingPoint },
                { "ScouterName", ScouterName },
                { "Comments", Comments },
            };
            return result;
        }

        public override string ToString()
        {
            return ToJson().ToString();
        }
    }
}
