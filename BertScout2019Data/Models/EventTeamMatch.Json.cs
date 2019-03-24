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

        public static EventTeamMatch Parse(string value)
        {
            JObject item = JObject.Parse(value);
            EventTeamMatch result = new EventTeamMatch()
            {
                Id = (int?)item.GetValueOrNull("Id"),
                Uuid = (string)item.GetValue("Uuid"),
                Changed = (int)item.GetValue("Changed"),
                EventKey = (string)item.GetValue("EventKey"),
                TeamNumber = (int)item.GetValue("TeamNumber"),
                MatchNumber = (int)item.GetValue("MatchNumber"),
                SandstormMoveType = (int)item.GetValue("SandstormMoveType"),
                SandstormOffPlatform = (int)item.GetValue("SandstormOffPlatform"),
                SandstormHatches = (int)item.GetValue("SandstormHatches"),
                SandstormCargo = (int)item.GetValue("SandstormCargo"),
                CargoShipHatches = (int)item.GetValue("CargoShipHatches"),
                CargoShipCargo = (int)item.GetValue("CargoShipCargo"),
                RocketHatches = (int)item.GetValue("RocketHatches"),
                RocketCargo = (int)item.GetValue("RocketCargo"),
                RocketHighestHatch = (int)item.GetValue("RocketHighestHatch"),
                RocketHighestCargo = (int)item.GetValue("RocketHighestCargo"),
                EndgamePlatform = (int)item.GetValue("EndgamePlatform"),
                EndgameBuddyClimb = (int)item.GetValue("EndgameBuddyClimb"),
                Defense = (int)item.GetValue("Defense"),
                Cooperation = (int)item.GetValue("Cooperation"),
                Fouls = (int)item.GetValue("Fouls"),
                TechFouls = (int)item.GetValue("TechFouls"),
                Broken = (int)item.GetValue("Broken"),
                AllianceResult = (int)item.GetValue("AllianceResult"),
                RocketRankingPoint = (int)item.GetValue("RocketRankingPoint"),
                HabRankingPoint = (int)item.GetValue("HabRankingPoint"),
                ScouterName = (string)item.GetValue("ScouterName"),
                Comments = (string)item.GetValue("Comments"),
            };
            return result;
        }
    }
}