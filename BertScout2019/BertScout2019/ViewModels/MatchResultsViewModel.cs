using BertScout2019.Models;
using System.Collections.ObjectModel;

namespace BertScout2019.ViewModels
{
    public class MatchResultsViewModel : BaseViewModel
    {
        public ObservableCollection<MatchResult> MatchResults { get; set; }
    }
}
