// remember to increment the dbVersion in App.xaml.cs when changing this model

namespace BertScout2019.Models
{
    public partial class Team
    {
        public int? Id { get; set; }
        public int TeamNumber { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
