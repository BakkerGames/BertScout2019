using BertScout2019.Services;
using BertScout2019Data.Models;
using Common.JSON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

        private int totalUploaded = 0;
        //private int toBeUploaded = 0;

        public SyncDatabasePage()
        {
            InitializeComponent();
            Title = "Sync local data with website";
            Entry_IpAddress.Text = App.syncIpAddress;
            Entry_KindleName.Text = App.kindleName;
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
                App.kindleName = Entry_KindleName.Text;
                Entry_KindleName.IsEnabled = false;

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

            List<EventTeamMatch> matches = (List<EventTeamMatch>)SqlDataEventTeamMatches.GetItemsAsync().Result;

            try
            {

                // make and use a copy of the list because it will crash otherwise
                List<EventTeamMatch> copyOfMatches = new List<EventTeamMatch>();
                foreach (EventTeamMatch item in matches)
                {
                    copyOfMatches.Add(item);
                }

                foreach (EventTeamMatch item in copyOfMatches)
                {
                    if (item.EventKey != App.currFRCEventKey)
                    {
                        continue;
                    }

                    if (item.Changed > 0 && item.Changed % 2 == 1)
                    {
                        item.Changed++; // change odd to even = no upload next time
                        if (item.Changed <= 2) // first time sending
                        {
                            WebDataEventTeamMatches.AddItemAsync(item);
                            addedCount++;
                            totalUploaded++;
                        }
                        else
                        {
                            WebDataEventTeamMatches.UpdateItemAsync(item);
                            updatedCount++;
                            totalUploaded++;
                        }
                        // save it so .Changed is updated
                        // this modifies the original list "matches", which is why a copy is needed
                        SqlDataEventTeamMatches.UpdateItemAsync(item);
                    }

                    if (addedCount + updatedCount >= 10)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Label_Results.Text += $"\n\nError during transmission\n\n{ex.Message}\n\n{ex.InnerException}";
                _isBusy = false;
                return;
            }

            Label_Results.Text += $"\n\nAdded: {addedCount} - Updated: {updatedCount} - Total: {totalUploaded} of {matches.Count}";

            if (addedCount + updatedCount > 0)
            {
                Label_Results.Text += $"\n\nUpload again to send next batch";
            }
            else
            {
                Label_Results.Text += $"\n\nUpload complete";
            }

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
            int lastId = 0;

            Label_Results.Text = "Downloading data...";

            List<EventTeamMatch> matches = (List<EventTeamMatch>)SqlDataEventTeamMatches.GetItemsAsync().Result;

            try
            {
                do
                {
                    addedCount = 0;
                    updatedCount = 0;
                    notChangedCount = 0;
                    string batchInfo = $"{App.currFRCEventKey}|{lastId}|10";
                    HttpResponseMessage response = App.client.GetAsync($"api/EventTeamMatches?batchInfo={batchInfo}").Result;
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        JArray results = JArray.Parse(result);
                        foreach (JObject obj in results)
                        {
                            EventTeamMatch item = EventTeamMatch.Parse(obj.ToString());

                            if (lastId < item.Id.Value)
                            {
                                lastId = item.Id.Value;
                            }

                            EventTeamMatch oldItem = matches.FirstOrDefault(p => p.Uuid == item.Uuid);

                            if (oldItem == null)
                            {
                                item.Changed = 0; // downloaded records are excluded from sending
                                SqlDataEventTeamMatches.AddItemAsync(item);
                                addedCount++;
                            }
                            else if (oldItem.Changed > 0 && oldItem.Changed < item.Changed)
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

                    if (addedCount + updatedCount + notChangedCount > 0)
                    {
                        Label_Results.Text += $"\n\nAdded: {addedCount} - Updated: {updatedCount} - Not Changed: {notChangedCount}";
                    }
                }
                while (addedCount + updatedCount + notChangedCount > 0);

                Label_Results.Text += "\n\nDownload complete";

            }
            catch (Exception ex)
            {
                Label_Results.Text += $"\n\nError during transmission\n\n{ex.Message}\n\n{ex.InnerException}";
                _isBusy = false;
                return;
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

        private void Button_Reset_Upload_Clicked(object sender, EventArgs e)
        {
            if (_isBusy)
            {
                return;
            }
            _isBusy = true;

            //List<string> downloadedUuids = new List<string>();
            List<EventTeamMatch> matches = (List<EventTeamMatch>)SqlDataEventTeamMatches.GetItemsAsync().Result;

            // make and use a copy of the list because it will crash otherwise
            List<EventTeamMatch> copyOfMatches = new List<EventTeamMatch>();
            foreach (EventTeamMatch item in matches)
            {
                copyOfMatches.Add(item);
            }

            foreach (EventTeamMatch item in copyOfMatches)
            {
                if (item.Changed > 0 && item.Changed % 2 == 0)
                {
                    item.Changed--; // trigger resend
                    SqlDataEventTeamMatches.UpdateItemAsync(item);
                }
            }

            Label_Results.Text = $"Reset complete";
            Label_Results.Text += "\n\nPlease upload data again";

            _isBusy = false;
        }

        private void Button_Export_Clicked(object sender, EventArgs e)
        {
            if (_isBusy)
            {
                return;
            }
            _isBusy = true;

            Label_Results.Text = "Exporting data...";

            List<EventTeamMatch> matches = (List<EventTeamMatch>)SqlDataEventTeamMatches.GetItemsAsync().Result;

            JArray exportJarray = new JArray();
            StringBuilder exportData = new StringBuilder();
            exportData.AppendLine("[");

            int exportCount = 0;
            foreach (EventTeamMatch item in matches)
            {
                item.Id = null; // don't preserve id
                item.Changed = 0;
                exportData.Append(item.ToString());
                exportData.AppendLine(",");
                exportCount++;
            }
            exportData.AppendLine("]");

            string myDocumentsPath = "";

            myDocumentsPath = "/storage/sdcard0/Documents"; // android
            if (!Directory.Exists(myDocumentsPath))
            {
                myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // windows
            }
            string path = Path.Combine(myDocumentsPath, $"{Entry_KindleName.Text}_{App.currFRCEventKey}.json");

            File.WriteAllText(path, exportData.ToString());

            Label_Results.Text += $"\n\nCount: {exportCount}";
            Label_Results.Text += $"\n\nExport complete";

            _isBusy = false;
        }

        private void Button_Import_Clicked(object sender, EventArgs e)
        {
            if (_isBusy)
            {
                return;
            }
            _isBusy = true;

            Label_Results.Text = "Importing data...";

            List<EventTeamMatch> matches = (List<EventTeamMatch>)SqlDataEventTeamMatches.GetItemsAsync().Result;

            string myDocumentsPath = "";

            myDocumentsPath = "/storage/sdcard0/Documents"; // android
            if (!Directory.Exists(myDocumentsPath))
            {
                myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // windows
            }
            string path = Path.Combine(myDocumentsPath, $"All_{App.currFRCEventKey}.json");

            if (!File.Exists(path))
            {
                Label_Results.Text += $"\n\nFile not found: {path}";
                _isBusy = false;
                return;
            }

            string allMatchData = File.ReadAllText(path);

            JArray matchJsonData = JArray.Parse(allMatchData);

            Label_Results.Text += $"\n\nMatches found: {matchJsonData.Count}";

            int addedCount = 0;
            int updatedCount = 0;
            int notChangedCount = 0;

            foreach (JObject obj in matchJsonData)
            {
                EventTeamMatch item = EventTeamMatch.Parse(obj.ToString());
                EventTeamMatch oldItem = matches.FirstOrDefault(p => p.Uuid == item.Uuid);

                if (oldItem == null)
                {
                    item.Changed = 0; // downloaded records are excluded from sending
                    SqlDataEventTeamMatches.AddItemAsync(item);
                    addedCount++;
                }
                else if (oldItem.Changed > 0 && oldItem.Changed < item.Changed)
                {
                    SqlDataEventTeamMatches.UpdateItemAsync(item);
                    updatedCount++;
                }
                else
                {
                    notChangedCount++;
                }
            }

            Label_Results.Text += $"\n\nAdded: {addedCount} - Updated: {updatedCount} - Not Changed: {notChangedCount}";

            Label_Results.Text += "\n\nImport complete";

            _isBusy = false;
        }

        private void Entry_KindleName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Entry_KindleName.Text))
            {
                App.kindleName = Entry_KindleName.Text;
            }
        }
    }
}
