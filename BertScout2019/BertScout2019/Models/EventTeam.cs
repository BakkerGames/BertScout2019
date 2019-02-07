using SQLite;

// remember to increment the dbVersion in App.xaml.cs when changing this model

namespace BertScout2019.Models
{
    public partial class EventTeam
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        public string Uuid { get; set; }
        public int Changed { get; set; }
        [Indexed]
        public string EventKey { get; set; }
        [Indexed]
        public int TeamNumber { get; set; }
    }
}
