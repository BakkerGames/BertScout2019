using BertScout2019.Models;

namespace BertScout2019.ViewModels
{
    public class TeamDetailViewModel : BaseViewModel
    {
        public Team item;

        public TeamDetailViewModel(Team item)
        {
            this.item = item;
            Title = $"Team {App.currTeamNumber} Details";
        }
    }
}
