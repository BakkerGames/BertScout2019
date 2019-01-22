using SQLite;

namespace BertScout2019.Models
{
    public class Team
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int TeamNumber { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
