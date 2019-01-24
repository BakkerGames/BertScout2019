using BertScout2019.Data;
using BertScout2019.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BertScout2019
{
    public partial class App : Application
    {
        static public string BertColor = "#22BE1C";
        static public string HighlightColor = "#23DAFF";
        static public Color SelectedButtonColor = Color.LightGray;

        static public string currFRCEventKey { get; set; }
        static public string currFRCEventName { get; set; }

        static public int currTeamNumber { get; set; }
        static public string currTeamName { get; set; }

        static public BertScout2019Database database;

        private const string dbFilename = "bertscout2019.db3";
        private const string propNameFRCEventKey = "currentFRCEventKey";
        private const string propNameFRCEventName = "currentFRCEventName";

        public App()
        {
            if (Properties.ContainsKey(propNameFRCEventKey))
            {
                currFRCEventKey = (string)Properties[propNameFRCEventKey];
            }
            if (Properties.ContainsKey(propNameFRCEventName))
            {
                currFRCEventName = (string)Properties[propNameFRCEventName];
            }
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex(BertColor)
            };
        }

        public static BertScout2019Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new BertScout2019Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbFilename));
                }
                return database;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            Properties[propNameFRCEventKey] = currFRCEventKey;
            Properties[propNameFRCEventName] = currFRCEventName;
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
