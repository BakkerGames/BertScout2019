using SQLite;

namespace BertScout2019.Models
{
    public class FRCEvent
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string EventId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
