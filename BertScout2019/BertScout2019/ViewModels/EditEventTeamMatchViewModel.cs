using BertScout2019.Models;

namespace BertScout2019.ViewModels
{
    public class EditEventTeamMatchViewModel : BaseViewModel
    {
        public EventTeamMatch item;

        public EditEventTeamMatchViewModel(EventTeamMatch item)
        {
            this.item = item;
            Title = $"Team {App.currTeamNumber} - Match {App.currMatchNumber}";
        }
    }
}
