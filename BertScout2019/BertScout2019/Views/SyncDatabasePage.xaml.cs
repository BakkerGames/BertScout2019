using BertScout2019Data.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SyncDatabasePage : ContentPage
    {
        private static bool syncFlag = false;

        public SyncDatabasePage()
        {
            InitializeComponent();
            Title = "Sync local data with website";
            Entry_IpAddress.Text = App.syncIpAddress;
        }

        private void Entry_IpAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            // nothing to do here
        }

        private void Button_Sync_Clicked(object sender, EventArgs e)
        {
            // save ip address
            App.syncIpAddress = Entry_IpAddress.Text;
            // sync to database
            RunAsync();
        }

        static async Task<List<FRCEvent>> GetFRCEventsAsync()
        {
            List<FRCEvent> items = null;
            HttpResponseMessage response = await App.client.GetAsync("api/FRCEvents");
            if (response.IsSuccessStatusCode)
            {
                string tempResult = await response.Content.ReadAsStringAsync();
            }
            return items;
        }

        static Uri CreateFRCEventAsync(FRCEvent item)
        {
            StringContent content = new StringContent(item.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = App.client.PostAsync("api/FRCEvents", content).Result;
            response.EnsureSuccessStatusCode();
            // return URI of the created resource.
            return response.Headers.Location;
        }

        static Uri UpdateFRCEventAsync(FRCEvent item)
        {
            StringContent content = new StringContent(item.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = App.client.PutAsync($"api/FRCEvents?uuid={item.Uuid}", content).Result;
            response.EnsureSuccessStatusCode();
            // return URI of the created resource.
            return response.Headers.Location;
        }

        private void RunAsync()
        {
            //List<EventTeamMatch> items;
            //List<FRCEvent> frcEvents;
            Label_Results.Text = "";

            try
            {
                if (!syncFlag)
                {
                    syncFlag = true;
                    Entry_IpAddress.IsEnabled = false;
                    // Update port # in the following line.
                    string uri = App.syncIpAddress;
                    if (!uri.EndsWith("/"))
                    {
                        uri += "/";
                    }
                    if (!uri.Contains(":"))
                    {
                        uri += "bertscout2019/";
                    }
                    if (!uri.StartsWith("http"))
                    {
                        uri = $"http://{uri}";
                    }
                    if (!uri.EndsWith("/"))
                    {
                        uri += "/";
                    }
                    Label_Results.Text += uri;
                    App.client.BaseAddress = new Uri(uri);
                    App.client.DefaultRequestHeaders.Accept.Clear();
                    App.client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                }

                // ### get frcevent works ###
                string result = "";
                HttpResponseMessage response = App.client.GetAsync("api/EventTeamMatches?uuid=21d82936-b562-41ff-9020-068bdf9222a6").Result;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    EventTeamMatch newETM = EventTeamMatch.Parse(result);
                    Label_Results.Text = newETM.ToString();
                }

                // Create a new FRCEvent
                FRCEvent item = new FRCEvent
                {
                    Uuid = Guid.NewGuid().ToString(),
                    Name = "Gizmo Event4202",
                    Location = "Anytown, ME",
                    EventKey = "GIZMOS4202",
                    Changed = 1,
                };
                Uri url = CreateFRCEventAsync(item);
                Label_Results.Text += $"\nCreated at {url.PathAndQuery}";

                item.Name += "-A";
                item.Changed++;
                UpdateFRCEventAsync(item);

                // ------------------------------------------------------------------------

                //// ### get frcevent works ###
                //string result = "";
                //HttpResponseMessage response = App.client.GetAsync("api/EventTeamMatches?uuid=21d82936-b562-41ff-9020-068bdf9222a6").Result;
                //if (response.IsSuccessStatusCode)
                //{
                //    result = response.Content.ReadAsStringAsync().Result;
                //    EventTeamMatch newETM = EventTeamMatch.Parse(result);
                //    Label_Results.Text = newETM.ToString();
                //}

                // ------------------------------------------------------------------------

                //frcEvents = GetFRCEventsAsync().Result;

                //// show all frcevents
                //Console.WriteLine("All FRC Events:");
                //items = await GetFRCEventsAsync();
                //foreach (FRCEvent showItem in items)
                //{
                //    ShowFRCEvent(showItem);
                //}
                //Console.WriteLine();

                //// Create a new FRCEvent
                //FRCEvent item = new FRCEvent
                //{
                //    Name = "Gizmo Event7",
                //    Location = "Anytown, ME",
                //    EventKey = "GIZMOS7",
                //    Changed = 1,
                //};

                //var url = await CreateFRCEventAsync(item);
                //Console.WriteLine($"Created at {url}");

                //// Get the FRCEvent
                //item = await GetFRCEventAsync(url.PathAndQuery);
                //ShowFRCEvent(item);

                //// show all frcevents
                //Console.WriteLine("All FRC Events:");
                //items = await GetFRCEventsAsync();
                //foreach (FRCEvent showItem in items)
                //{
                //    ShowFRCEvent(showItem);
                //}
                //Console.WriteLine();

                ////EventTeamMatch eventTeamMatch = new EventTeamMatch();
                ////eventTeamMatch.EventKey = "WEEKZERO";
                ////eventTeamMatch.TeamNumber = 133;
                ////eventTeamMatch.MatchNumber = 17;
                ////eventTeamMatch.Changed = 1;
                ////StringContent content = new StringContent(eventTeamMatch.ToString());

                ////string result;
                ////HttpResponseMessage response = App.client.PostAsync("api/EventTeamMatches", content).Result;
                ////if (response.IsSuccessStatusCode)
                ////{
                ////    result = response.Content.ReadAsStringAsync().Result;
                ////}
                ////else
                ////{
                ////    result = "error!";
                ////}
                ////Label_Results.Text = result;

                //var urlEtm = await CreateEventTeamMatchAsync(eventTeamMatch);
                //Console.WriteLine($"Created at {urlEtm}");

                //// Update the FRCEvent
                //Console.WriteLine("Updating Location...");
                //item.Location = "Anothertown, ME";
                //await UpdateFRCEventAsync(item);

                //// Get the updated FRCEvent
                //item = await GetFRCEventAsync(url.PathAndQuery);
                //ShowFRCEvent(item);

                //// Delete the FRCEvent
                //var statusCode = await DeleteFRCEventAsync(item.Id);
                //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception ex)
            {
                // todo any error here?
                Label_Results.Text = ex.Message;
            }
        }
    }
}