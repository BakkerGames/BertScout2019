using System;
using System.Collections.Generic;
using System.Text;
using BertScout2019Data.Models;

namespace BertScout2019.Models
{
    public class TeamResult : Team
    {
        public int TotalRP { get; set; }
        public decimal AverageScore { get; set; }
    }
}
