using BertScout2019.Services;
using BertScout2019Data.Models;
using Common.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SyncDatabasePage : ContentPage
    {
        public IDataStore<EventTeamMatch> SqlDataEventTeamMatches;
        public IDataStore<EventTeamMatch> WebDataEventTeamMatches;

        private readonly string _mediaType = "application/json";

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

                App.client = new HttpClient(); ;
                App.client.BaseAddress = new Uri(uri);
                App.client.DefaultRequestHeaders.Accept.Clear();
                App.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaType));
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

            int addedCount = 0;
            int updatedCount = 0;

            Label_Results.Text = "Uploading data...";

            try
            {
                List<EventTeamMatch> matches = (List<EventTeamMatch>)SqlDataEventTeamMatches.GetItemsAsync().Result;

                // make and use a copy of the list because it will crash otherwise
                List<EventTeamMatch> copyOfMatches = new List<EventTeamMatch>();
                foreach (EventTeamMatch item in matches)
                {
                    copyOfMatches.Add(item);
                }

                foreach (EventTeamMatch item in copyOfMatches)
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
                        // this modifies the original list "matches", which is why a copy is needed
                        SqlDataEventTeamMatches.UpdateItemAsync(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Label_Results.Text += $"\n\nError during transmission\n\n{ex.Message}\n\n{ex.InnerException}";
                _isBusy = false;
                return;
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

            int addedCount = 0;
            int updatedCount = 0;
            int notChangedCount = 0;
            List<string> downloadedUuids = new List<string>();

            Label_Results.Text = "Downloading data...";

            try
            {
                HttpResponseMessage response = App.client.GetAsync("api/EventTeamMatches").Result;
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    JArray results = JArray.Parse(result);
                    foreach (JObject obj in results)
                    {
                        EventTeamMatch oldItem = null;
                        EventTeamMatch item = EventTeamMatch.Parse(obj.ToString());
                        downloadedUuids.Add(item.Uuid);

                        oldItem = SqlDataEventTeamMatches.GetItemAsync(item.Uuid).Result;

                        if (oldItem == null)
                        {
                            SqlDataEventTeamMatches.AddItemAsync(item);
                            addedCount++;
                        }
                        else if (oldItem.Changed < item.Changed)
                        {
                            SqlDataEventTeamMatches.UpdateItemAsync(item);
                            updatedCount++;
                        }
                        else
                        {
                            notChangedCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Label_Results.Text += $"\n\nError during transmission\n\n{ex.Message}\n\n{ex.InnerException}";
                _isBusy = false;
                return;
            }

            Label_Results.Text += $"\n\nAdded: {addedCount} - Updated: {updatedCount} - Not Changed: {notChangedCount}";

            bool needResend = false;

            List<EventTeamMatch> matches = (List<EventTeamMatch>)SqlDataEventTeamMatches.GetItemsAsync().Result;
            
            // make and use a copy of the list because it will crash otherwise
            List<EventTeamMatch> copyOfMatches = new List<EventTeamMatch>();
            foreach (EventTeamMatch item in matches)
            {
                copyOfMatches.Add(item);
            }

            foreach (EventTeamMatch item in copyOfMatches)
            {
                var oldItem = downloadedUuids.Where((string arg) => arg == item.Uuid).FirstOrDefault();
                if (oldItem == null)
                {
                    item.Changed = 1; // trigger initial send
                    SqlDataEventTeamMatches.UpdateItemAsync(item);
                    needResend = true;
                }
            }

            if (needResend)
            {
                Label_Results.Text += $"\n\nPlease upload data again";
            }

            _isBusy = false;
        }

        private void Entry_IpAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Entry_IpAddress.Text))
            {
                // save ip address
                App.syncIpAddress = Entry_IpAddress.Text;
            }
        }
    }
}
