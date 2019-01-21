using BertScout2019.Models;
using BertScout2019.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace BertScout2019.ViewModels
{
    public class SelectEventsViewModel : BaseViewModel
    {
        //public IDataStore<FRCEvent> DataStoreFRCEvent => DependencyService.Get<IDataStore<FRCEvent>>() ?? new DataStoreFRCEvent();
        public IDataStore<FRCEvent> DataStoreFRCEvent => new XmlDataStoreFRCEvent();

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


                //var assembly = typeof(App).GetTypeInfo().Assembly;
                //Stream stream = assembly.GetManifestResourceStream("BertScout2019.EmbeddedResources.FRCEvents.xml");
                //using (var reader = new StreamReader(stream))
                //{
                //    var serializer = new XmlSerializer(typeof(ObservableCollection<FRCEvent>));
                //    FRCEvents = (ObservableCollection<FRCEvent>)serializer.Deserialize(reader);
                //}
                //foreach (FRCEvent obj in FRCEvents)
                //{
                //    Console.WriteLine(obj.Name);
                //}
                //Console.ReadLine();
                //var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                //Stream stream = assembly.GetManifestResourceStream("WorkingWithFiles.PCLXmlResource.xml");
                //List<FRCEvent> FRCEvents;
                //using (var reader = new System.IO.StreamReader(stream))
                //{
                //    var serializer = new XmlSerializer(typeof(List<FRCEvent>));
                //    FRCEvents = (List<FRCEvent>)serializer.Deserialize(reader);
                //}
                //var listView = new ListView();
                //listView.ItemsSource = FRCEvents;



                var items = await DataStoreFRCEvent.GetItemsAsync(true);
                foreach (var item in items)
                {
                    FRCEvents.Add(item);
                }
                //XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<FRCEvent>));
                //StringWriter sw = new StringWriter();
                //serializer.Serialize(sw, FRCEvents);
                //Console.WriteLine(sw.ToString());
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
