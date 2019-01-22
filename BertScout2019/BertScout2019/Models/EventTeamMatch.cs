using SQLite;

namespace BertScout2019.Models
{
    public class EventTeamMatch
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string EventId { get; set; }
        public int TeamNumber { get; set; }
        public int Match { get; set; }

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

        public int AllianceScore { get; set; }
        public int LostTiedWon { get; set; }
        public int RocketRankingPoint { get; set; }
        public int HabRankingPoint { get; set; }

        public string Comments { get; set; }
    }
}
