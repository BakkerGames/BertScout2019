﻿using BertScout2019.Data;
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
        static public Color SelectedButtonColor = Color.FromHex(BertColor);
        static public Color UnselectedButtonColor = Color.LightGray;
        static public double dbVersion = 1.1;

        // app properties for easy page communication
        static public int currVersionNumber { get; set; }
        static public string currFRCEventKey { get; set; }
        static public string currFRCEventName { get; set; }
        static public int currTeamNumber { get; set; }
        static public string currTeamName { get; set; }
        static public int currMatchNumber { get; set; }
        static public int highestMatchNumber { get; set; }

        // app database
        private const string dbFilename = "bertscout2019.db3";
        static public BertScout2019Database database;

        // app properties saved by OnSleep()
        private const string propNameVersionNumber = "currentVersionNumber";
        private const string propNameFRCEventKey = "currentFRCEventKey";
        private const string propNameFRCEventName = "currentFRCEventName";

        public App()
        {
            try
            {
                if (Properties.ContainsKey(propNameVersionNumber)
                    && (double)Properties[propNameVersionNumber] == dbVersion)
                {
                    if (Properties.ContainsKey(propNameFRCEventKey))
                    {
                        currFRCEventKey = (string)Properties[propNameFRCEventKey];
                    }
                    if (Properties.ContainsKey(propNameFRCEventName))
                    {
                        currFRCEventName = (string)Properties[propNameFRCEventName];
                    }
                }
            }
            catch (Exception)
            {
                Properties[propNameVersionNumber] = dbVersion;
                Properties[propNameFRCEventKey] = "";
                Properties[propNameFRCEventName] = "";
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
            Properties[propNameVersionNumber] = dbVersion;
            Properties[propNameFRCEventKey] = currFRCEventKey;
            Properties[propNameFRCEventName] = currFRCEventName;
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
