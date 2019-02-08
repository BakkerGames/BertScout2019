using Common.JSON;

namespace BertScout2019Data.Models
{
    public partial class EventTeamMatch
    {
        // ToJson and ToString routines

        public JObject ToJson()
        {
            JObject result = new JObject()
            {
                { "Id", Id },
                { "Uuid", Uuid },
                { "Changed", Changed },
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
                { "TechFouls", TechFouls },
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