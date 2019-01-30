﻿using BertScout2019.Models;
using BertScout2019.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsPage : ContentPage
    {
        static bool Reset_Database_Button_Clicked_IsBusy = false;

        public IDataStore<FRCEvent> DataStoreFRCEvents => new XmlDataStoreFRCEvent();
        public IDataStore<Team> DataStoreTeams => new XmlDataStoreTeams();
        public IDataStore<EventTeam> DataStoreEventTeams => new XmlDataStoreEventTeams();

        public OptionsPage()
        {
            InitializeComponent();
        }

        private bool _nateCanBreak = false;
        public void Reset_Database_Button_Clicked(object sender, EventArgs e)
        {
            if (_nateCanBreak)
            {
                return;
            }
            _nateCanBreak = true;
            OptionsMessageLabel.Text = "";
            if (Entry_OptionPassword_Value.Text != "bertdata")
            {
                OptionsMessageLabel.Text = "Incorrect password!";
                Reset_Database_Button_Clicked_IsBusy = false;
                return;
            }
            try
            {
                App.Database.DropTables();
                App.Database.CreateTables();
                // put frc events from xml into database
                var frcEvents = DataStoreFRCEvents.GetItemsAsync(true).Result;
                int frcEventId = 0;
                foreach (var frcEvent in frcEvents)
                {
                    frcEvent.Id = ++frcEventId;
                    int result = App.Database.SaveFRCEventAsync(frcEvent).Result;
                }
                // put teams from xml into database
                var teams = DataStoreTeams.GetItemsAsync(true).Result;
                int teamId = 0;
                foreach (var team in teams)
                {
                    team.Id = ++teamId;
                    int result = App.Database.SaveTeamAsync(team).Result;
                }
                // put eventteams from xml into database
                var eventTeams = DataStoreEventTeams.GetItemsAsync(true).Result;
                int eventTeamId = 0;
                foreach (var eventTeam in eventTeams)
                {
                    eventTeam.Id = ++eventTeamId;
                    int result = App.Database.SaveEventTeamAsync(eventTeam).Result;
                }
                OptionsMessageLabel.Text = "Database reset done!";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}