using BertScout2019.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.IO;
using System;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BertScout2019
{
    public partial class App : Application
    {
        public const string BertColor = "#22BE1C";
        public const string HighlightColor = "#23DAFF";
        public Color SelectedButtonColor = Color.LightGray;

        private const string frcEventPropertyID = "currentFRCEventId";
        private const string frcEventProperty = "currentFRCEvent";

        public string CurrentFRCEventId { get; set; }
        public string CurrentFRCEvent { get; set; }

        public const string dbFilename = "bertscout2019.db3";
        public string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbFilename);

        public App()
        {
            if (Properties.ContainsKey(frcEventPropertyID))
            {
                CurrentFRCEventId = (string)Properties[frcEventPropertyID];
            }
            if (Properties.ContainsKey(frcEventProperty))
            {
                CurrentFRCEvent = (string)Properties[frcEventProperty];
            }
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex(BertColor)
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            Properties[frcEventPropertyID] = CurrentFRCEventId;
            Properties[frcEventProperty] = CurrentFRCEvent;
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
