using System.Data.SqlClient;

namespace BertScout2019.Models
{
    public partial class Team
    {
        private static int _ordId = 0;
        private static int _ordTeamNumber = 1;
        private static int _ordName = 2;
        private static int _ordLocation = 3;

        public static Team FromSqlDataReader(SqlDataReader dr)
        {
            Team result = new Team()
            {
                Id = dr.GetInt32(_ordId),
                TeamNumber = dr.GetInt32(_ordTeamNumber),
                Name = dr.IsDBNull(_ordName) ? null : dr.GetString(_ordName),
                Location = dr.IsDBNull(_ordLocation) ? null : dr.GetString(_ordLocation),
            };
            return result;
        }
    }
}
