using System.Data.SqlClient;

namespace BertScout2019.Models
{
    public partial class EventTeamMatch
    {
        public static int _ordId = 0;
        public static int _ordEventKey = 1;
        public static int _ordTeamNumber = 2;
        public static int _ordMatchNumber = 3;
        public static int _ordSandstormMoveType = 4;
        public static int _ordSandstormOffPlatform = 5;
        public static int _ordSandstormHatches = 6;
        public static int _ordSandstormCargo = 7;
        public static int _ordCargoShipHatches = 8;
        public static int _ordCargoShipCargo = 9;
        public static int _ordRocketHatches = 10;
        public static int _ordRocketCargo = 11;
        public static int _ordRocketHighestHatch = 12;
        public static int _ordRocketHighestCargo = 13;
        public static int _ordEndgamePlatform = 14;
        public static int _ordEndgameBuddyClimb = 15;
        public static int _ordDefense = 16;
        public static int _ordCooperation = 17;
        public static int _ordFouls = 18;
        public static int _ordBroken = 19;
        public static int _ordAllianceResult = 20;
        public static int _ordRocketRankingPoint = 21;
        public static int _ordHabRankingPoint = 22;
        public static int _ordScouterName = 23;
        public static int _ordComments = 24;

        public static EventTeamMatch FromSqlDataReader(SqlDataReader dr)
        {
            EventTeamMatch result = new EventTeamMatch()
            {
                Id = dr.GetInt32(_ordId),
                EventKey = dr.IsDBNull(_ordEventKey) ? null : dr.GetString(_ordEventKey),
                TeamNumber = dr.GetInt32(_ordTeamNumber),
                MatchNumber = dr.GetInt32(_ordMatchNumber),
                SandstormMoveType = dr.GetInt32(_ordSandstormMoveType),
                SandstormOffPlatform = dr.GetInt32(_ordSandstormOffPlatform),
                SandstormHatches = dr.GetInt32(_ordSandstormHatches),
                SandstormCargo = dr.GetInt32(_ordSandstormCargo),
                CargoShipHatches = dr.GetInt32(_ordCargoShipHatches),
                CargoShipCargo = dr.GetInt32(_ordCargoShipCargo),
                RocketHatches = dr.GetInt32(_ordRocketHatches),
                RocketCargo = dr.GetInt32(_ordRocketCargo),
                RocketHighestHatch = dr.GetInt32(_ordRocketHighestHatch),
                RocketHighestCargo = dr.GetInt32(_ordRocketHighestCargo),
                EndgamePlatform = dr.GetInt32(_ordEndgamePlatform),
                EndgameBuddyClimb = dr.GetInt32(_ordEndgameBuddyClimb),
                Defense = dr.GetInt32(_ordDefense),
                Cooperation = dr.GetInt32(_ordCooperation),
                Fouls = dr.GetInt32(_ordFouls),
                Broken = dr.GetInt32(_ordBroken),
                AllianceResult = dr.GetInt32(_ordAllianceResult),
                RocketRankingPoint = dr.GetInt32(_ordRocketRankingPoint),
                HabRankingPoint = dr.GetInt32(_ordHabRankingPoint),
                ScouterName = dr.IsDBNull(_ordScouterName) ? null : dr.GetString(_ordScouterName),
                Comments = dr.IsDBNull(_ordComments) ? null : dr.GetString(_ordComments),
            };
            return result;
        }
    }
}