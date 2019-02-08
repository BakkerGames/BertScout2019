using System.Data.SqlClient;

namespace BertScout2019.Models
{
    public partial class EventTeam
    {
        private static int _ordId = 0;
        private static int _ordEventKey = 1;
        private static int _ordTeamNumber = 2;

        public static EventTeam FromSqlDataReader(SqlDataReader dr)
        {
            EventTeam result = new EventTeam()
            {
                Id = dr.GetInt32(_ordId),
                EventKey = dr.IsDBNull(_ordEventKey) ? null : dr.GetString(_ordEventKey),
                TeamNumber = dr.GetInt32(_ordTeamNumber),
            };
            return result;
        }
    }
}
