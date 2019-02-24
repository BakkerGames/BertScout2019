using BertScout2019.Services;
using BertScout2019Data.Models;
using Common.JSON;
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
        public IDataStore<EventTeamMatch> SqlDataEventTeamMatches;
        public IDataStore<EventTeamMatch> WebDataEventTeamMatches;

        private static bool _initialSetup = false;
        private static bool _isBusy = false;

        public SyncDatabasePage()
        {
            InitializeComponent();
            Title = "Sync local data with website";
            Entry_IpAddress.Text = App.syncIpAddress;
            SqlDataEventTeamMatches = new SqlDataStoreEventTeamMatches(App.currFRCEventKey);
            WebDataEventTeamMatches = new WebDataStoreEventTeamMatches();
        }

        private void PrepareSync()
        {
            if (!_initialSetup)
            {
                _initialSetup = true;
                // save ip address
                App.syncIpAddress = Entry_IpAddress.Text;
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
        }

        private void Button_Upload_Clicked(object sender, EventArgs e)
        {
            if (_isBusy)
            {
                return;
            }
            _isBusy = true;
            PrepareSync();

            Label_Results.Text = "Uploading data...";
            int addedCount = 0;
            int updatedCount = 0;

            List<EventTeamMatch> matches = (List<EventTeamMatch>)SqlDataEventTeamMatches.GetItemsAsync().Result;

            foreach (EventTeamMatch item in matches)
            {
                if (item.Changed % 2 == 1)
                {
                    item.Changed++; // change odd to even = no upload next time
                    if (item.Changed <= 2) // first time sending
                    {
                        WebDataEventTeamMatches.AddItemAsync(item);
                        addedCount++;
                    }
                    else
                    {
                        WebDataEventTeamMatches.UpdateItemAsync(item);
                        updatedCount++;
                    }
                    // save it so .Changed is updated
                    App.database.SaveEventTeamMatchAsync(item);
                }
            }

            Label_Results.Text += $"\n\nAdded: {addedCount} - Updated: {updatedCount}";

            _isBusy = false;
        }

        private void Button_Download_Clicked(object sender, EventArgs e)
        {
            if (_isBusy)
            {
                return;
            }
            _isBusy = true;
            PrepareSync();

            Label_Results.Text = "Downloading data...";
            int addedCount = 0;
            int updatedCount = 0;
            int notChangedCount = 0;

            List<EventTeamMatch> matches;
            //matches = GetEventTeamMatchesAsync().Result;
            matches = (List<EventTeamMatch>)WebDataEventTeamMatches.GetItemsAsync().Result;

            foreach (EventTeamMatch item in matches)
            {
                EventTeamMatch matchItem = App.database.GetEventTeamMatchAsyncUuid(item.Uuid).Result;

                if (matchItem == null)
                {
                    App.database.SaveEventTeamMatchAsync(item);
                    addedCount++;
                }
                else if (matchItem.Changed < item.Changed)
                {
                    App.database.SaveEventTeamMatchAsync(item);
                    updatedCount++;
                }
            }

            Label_Results.Text += $"\n\nAdded: {addedCount} - Updated: {updatedCount} - Not Changed: {notChangedCount}";

            _isBusy = false;
        }

        private async Task<List<EventTeamMatch>> GetEventTeamMatchesAsync()
        {
            List<EventTeamMatch> items = null;
            HttpResponseMessage response = await App.client.GetAsync("api/EventTeamMatches");
            if (response.IsSuccessStatusCode)
            {
                string tempResult = await response.Content.ReadAsStringAsync();
                JArray tempJArray = JArray.Parse(tempResult);
                foreach (JObject item in tempJArray)
                {
                    EventTeamMatch matchItem = EventTeamMatch.Parse(item.ToString());
                    items.Add(matchItem);
                }
            }
            return items;
        }
    }
}
