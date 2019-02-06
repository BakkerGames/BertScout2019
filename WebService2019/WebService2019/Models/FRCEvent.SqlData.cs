using System.Data.SqlClient;

namespace BertScout2019.Models
{
    public partial class FRCEvent
    {
        public static int _ordId = 0;
        public static int _ordEventKey = 1;
        public static int _ordName = 2;
        public static int _ordLocation = 3;
        public static int _ordStartDate = 4;
        public static int _ordEndDate = 5;

        public static FRCEvent FromSqlDataReader(SqlDataReader dr)
        {
            FRCEvent result = new FRCEvent()
            {
                Id = dr.GetInt32(_ordId),
                EventKey = dr.IsDBNull(_ordEventKey) ? null : dr.GetString(_ordEventKey),
                Name = dr.IsDBNull(_ordName) ? null : dr.GetString(_ordName),
                Location = dr.IsDBNull(_ordLocation) ? null : dr.GetString(_ordLocation),
                StartDate = dr.GetDateTime(_ordStartDate).ToString(),
                EndDate = dr.GetDateTime(_ordEndDate).ToString(),
            };
            return result;
        }
    }
}
