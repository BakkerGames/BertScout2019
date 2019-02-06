using BertScout2019.Models;
using System.Collections.ObjectModel;

namespace BertScout2019.ViewModels
{
    public class TeamDetailViewModel : BaseViewModel
    {

        public Team item;

        public ObservableCollection<MatchResult> MatchResults { get; set; }

        public TeamDetailViewModel(Team item)
        {
            this.item = item;
            Title = $"Team {App.currTeamNumber} Details";
            MatchResults = new ObservableCollection<MatchResult>();
        }
    }
}
