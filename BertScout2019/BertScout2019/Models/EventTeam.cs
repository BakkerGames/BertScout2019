using SQLite;

namespace BertScout2019.Models
{
    public class EventTeam
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string EventId { get; set; }
        public int TeamNumber { get; set; }
    }
}
