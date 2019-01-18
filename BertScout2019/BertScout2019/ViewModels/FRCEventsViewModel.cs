using BertScout2019.Models;
using BertScout2019.Services;
using BertScout2019.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BertScout2019.ViewModels
{
    public class FRCEventsViewModel : BaseViewModel
    {
        public IDataStore<FRCEvent> DataStoreFRCEvent => DependencyService.Get<IDataStore<FRCEvent>>() ?? new MockDataStoreFRCEvent();

        public ObservableCollection<FRCEvent> FRCEvents { get; set; }
        public Command LoadFRCEventsCommand { get; set; }

        public FRCEventsViewModel()
        {
            Title = "FRC Events";
            FRCEvents = new ObservableCollection<FRCEvent>();
            LoadFRCEventsCommand = new Command(async () => await ExecuteLoadFRCEventsCommand());

            MessagingCenter.Subscribe<NewFRCEventPage, FRCEvent>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as FRCEvent;
                FRCEvents.Add(newItem);
                await DataStoreFRCEvent.AddItemAsync(newItem);
            });
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
