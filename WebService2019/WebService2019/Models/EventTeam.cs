// remember to increment the dbVersion in App.xaml.cs when changing this model

namespace BertScout2019.Models
{
    public partial class EventTeam
    {
        public int? Id { get; set; }
        public string EventKey { get; set; }
        public int TeamNumber { get; set; }
    }
}
