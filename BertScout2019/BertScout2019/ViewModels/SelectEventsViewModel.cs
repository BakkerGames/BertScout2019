using BertScout2019.Models;
using BertScout2019.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BertScout2019.ViewModels
{
    public class SelectEventsViewModel : BaseViewModel
    {
        //public IDataStore<FRCEvent> DataStoreFRCEvent => DependencyService.Get<IDataStore<FRCEvent>>() ?? new XmlDataStoreFRCEvent();
        //public IDataStore<FRCEvent> DataStoreFRCEvent => new XmlDataStoreFRCEvent();
        public IDataStore<FRCEvent> DataStoreFRCEvent => new SqlDataStoreFRCEvent();

        public ObservableCollection<FRCEvent> FRCEvents { get; set; }
        public Command LoadFRCEventsCommand { get; set; }

        public SelectEventsViewModel()
        {
            Title = "Select FRC Event";
            FRCEvents = new ObservableCollection<FRCEvent>();
            LoadFRCEventsCommand = new Command(async () => await ExecuteLoadFRCEventsCommand());

            //MessagingCenter.Subscribe<NewFRCEventPage, FRCEvent>(this, "AddFRCEvent", async (obj, item) =>
            //{
            //    var newItem = item as FRCEvent;
            //    FRCEvents.Add(newItem);
            //    await DataStoreFRCEvent.AddItemAsync(newItem);
            //});
        }

        async Task ExecuteLoadFRCEventsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                FRCEvents.Clear();
                // 384,390,407 ms with xml, 93,94 ms with sql
                // second call 2ms with xml, 1ms with sql
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
