using BertScout2019.Models;

namespace BertScout2019.ViewModels
{
    public class FRCEventDetailViewModel : BaseViewModel
    {
        public FRCEvent FRCEvent { get; set; }
        public FRCEventDetailViewModel(FRCEvent item = null)
        {
            Title = item?.Name;
            FRCEvent = item;
        }
    }
}
