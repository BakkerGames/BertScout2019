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
        public const string BertColor = "#22BE1C";
        public const string HighlightColor = "#23DAFF";
        public Color SelectedButtonColor = Color.LightGray;

        private const string propNameFRCEventKey = "currentFRCEventKey";
        private const string propNameFRCEventName = "currentFRCEventName";

        public string currFRCEventKey { get; set; }
        public string currFRCEventName { get; set; }

        public const string dbFilename = "bertscout2019.db3";
        public string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbFilename);

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
