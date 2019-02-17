using BertScout2019.Services;
using BertScout2019Data.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsPage : ContentPage
    {
        private bool _nateSyncFlag = false;
        private bool _syncFlag;

        public IDataStore<FRCEvent> DataStoreFRCEvents => new XmlDataStoreFRCEvent();
        public IDataStore<Team> DataStoreTeams => new XmlDataStoreTeams();
        public IDataStore<EventTeam> DataStoreEventTeams => new XmlDataStoreEventTeams();

        public OptionsPage()
        {
            InitializeComponent();
        }

        public void Reset_Database_Button_Clicked(object sender, EventArgs e)
        {
            if (_nateSyncFlag)
            {
                return;
            }
            _nateSyncFlag = true;
            OptionsMessageLabel.Text = "";
            try
            {
                App.Database.DropTables();
                App.Database.CreateTables();
                // put frc events from xml into database
                var frcEvents = DataStoreFRCEvents.GetItemsAsync(true).Result;
                foreach (var item in frcEvents)
                {
                    item.Id = null;
                    int result = App.Database.SaveFRCEventAsync(item).Result;
                }
                // put teams from xml into database
                var teams = DataStoreTeams.GetItemsAsync(true).Result;
                foreach (var item in teams)
                {
                    item.Id = null;
                    int result = App.Database.SaveTeamAsync(item).Result;
                }
                // put eventteams from xml into database
                var eventTeams = DataStoreEventTeams.GetItemsAsync(true).Result;
                foreach (var item in eventTeams)
                {
                    item.Id = null;
                    int result = App.Database.SaveEventTeamAsync(item).Result;
                }
                App.currFRCEventKey = "";
                App.currFRCEventName = "";
                App.currTeamName = "";
                App.currTeamNumber = 0;
                App.currMatchNumber = 0;
                OptionsMessageLabel.Text = "Database reset done!";
                Reset_Database_Button.IsEnabled = false;
            }
            catch (Exception ex)
            {
                OptionsMessageLabel.Text = ex.Message;
            }
        }

        private void Entry_OptionPassword_Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_nateSyncFlag)
            {
                return;
            }
            Reset_Database_Button.IsEnabled = (Entry_OptionPassword_Value.Text.ToLower() == App.OptionPassword.ToLower());
        }

        private async void Button_SyncDatabase_Clicked(object sender, EventArgs e)
        {
            if (_syncFlag)
            {
                return;
            }
            _syncFlag = true;
            Button_SyncDatabase.BackgroundColor = App.SelectedButtonColor;
            await Navigation.PushAsync(new SyncDatabasePage());
            Button_SyncDatabase.BackgroundColor = App.UnselectedButtonColor;
            _syncFlag = false;
        }
    }
}
