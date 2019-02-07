using BertScout2019.Services;
using BertScout2019Data.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BertScout2019.ViewModels
{
    public class SelectEventsViewModel : BaseViewModel
    {
        public IDataStore<FRCEvent> DataStoreFRCEvent => new SqlDataStoreFRCEvents();

        public ObservableCollection<FRCEvent> FRCEvents { get; set; }
        public Command LoadFRCEventsCommand { get; set; }

        public SelectEventsViewModel()
        {
            Title = "Select FRC Event";
            FRCEvents = new ObservableCollection<FRCEvent>();
            LoadFRCEventsCommand = new Command(async () => await ExecuteLoadFRCEventsCommand());
        }

        async Task ExecuteLoadFRCEventsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                FRCEvents.Clear();
                var items = await DataStoreFRCEvent.GetItemsAsync(true);
                foreach (var item in items)
                {
                    FRCEvents.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
