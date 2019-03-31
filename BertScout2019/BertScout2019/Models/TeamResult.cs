namespace BertScout2019.Models
{
    public class TeamResult
    {
        public int TeamNumber { get; set; }
        public string Name { get; set; }
        public int TotalRP { get; set; }
        public int AverageScore { get; set; }
        public int TotalHatches { get; set; }
        public int TotalCargo { get; set; }
        public int AverageHatches { get; internal set; }
        public int AverageCargo { get; internal set; }
    }
}
