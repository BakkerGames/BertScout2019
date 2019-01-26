using SQLite;

namespace BertScout2019.Models
{
    public class EventTeam
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        [Indexed]
        public string EventKey { get; set; }
        [Indexed]
        public int TeamNumber { get; set; }
    }
}
