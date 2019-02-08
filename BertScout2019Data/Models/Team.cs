using SQLite;

// remember to increment the dbVersion in App.xaml.cs when changing this model

namespace BertScout2019Data.Models
{
    public partial class Team
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        public string Uuid { get; set; }
        public int Changed { get; set; }
        [Indexed]
        public int TeamNumber { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
