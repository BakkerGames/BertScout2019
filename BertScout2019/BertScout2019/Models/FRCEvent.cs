using SQLite;

// remember to increment the dbVersion in App.xaml.cs when changing this model

namespace BertScout2019.Models
{
    public partial class FRCEvent
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        [Indexed]
        public string EventKey { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
