using SQLite;
using System;

// remember to increment the dbVersion in App.xaml.cs when changing this model

namespace BertScout2019Data.Models
{
    public partial class EventTeamMatch : IEquatable<EventTeamMatch>, IComparable<EventTeamMatch>
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        public string Uuid { get; set; }
        public int Changed { get; set; }
        [Indexed]
        public string EventKey { get; set; }
        [Indexed]
        public int TeamNumber { get; set; }
        [Indexed]
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
        public int TechFouls { get; set; }
        public int Broken { get; set; }
        public int AllianceResult { get; set; }
        public int RocketRankingPoint { get; set; }
        public int HabRankingPoint { get; set; }
        public string ScouterName { get; set; }
        public string Comments { get; set; }

        public int CompareTo(EventTeamMatch other)
        {
            if (other == null) return 1; // this is greater
            if (ReferenceEquals(this, other)) return 0; // same object
            if (Id == null)
            {
                if (other.Id == null) return 0; // equal
                return -1; // other is greater
            }
            if (other.Id == null) return 1; // this is greater
            return Id.Value.CompareTo(other.Id.Value);
        }

        public bool Equals(EventTeamMatch other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true; // same object
            if (!Id.HasValue || !other.Id.HasValue) return false; // one or both is null
            return Id.Value.Equals(other.Id.Value);
        }
    }
}
