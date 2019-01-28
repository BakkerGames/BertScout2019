using BertScout2019.Models;
using BertScout2019.Services;

namespace BertScout2019.ViewModels
{
    public class EditEventTeamMatchViewModel : BaseViewModel
    {
        public IDataStore<EventTeamMatch> DataStoreMatch => new SqlDataStoreEventTeamMatch(App.currFRCEventKey, App.currTeamNumber, App.currMatchNumber);

        public EventTeamMatch item;

        public EditEventTeamMatchViewModel(EventTeamMatch item)
        {
            this.item = item;
            Title = $"Team {App.currTeamNumber} - Match {App.currMatchNumber}";
        }
    }
}
