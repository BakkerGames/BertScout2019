using BertScout2019.Data;
using BertScout2019.Models;
using BertScout2019.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsPage : ContentPage
    {
        public IDataStore<FRCEvent> DataStoreFRCEvents => new XmlDataStoreFRCEvent();
        public IDataStore<Team> DataStoreTeams => new XmlDataStoreTeams();
        public IDataStore<EventTeam> DataStoreEventTeams => new XmlDataStoreEventTeams();
        //public BertScout2019Database database;

        public OptionsPage()
        {
            InitializeComponent();
        }

        public void Fill_Database_Button_Clicked(object sender, EventArgs e)
        {
            string saveText = Fill_Database_Button.Text;
            Fill_Database_Button.Text = "Filling...";
            // put frc events from xml into database
            var frcEvents = DataStoreFRCEvents.GetItemsAsync(true).Result;
            foreach (var frcEvent in frcEvents)
            {
                int result = App.Database.SaveFRCEventAsync(frcEvent).Result;
            }
            // todo put teams from xml into database
            var teams = DataStoreTeams.GetItemsAsync(true).Result;
            foreach (var team in teams)
            {
                int result = App.Database.SaveTeamAsync(team).Result;
            }
            // todo put eventteams from xml into database
            var eventTeams = DataStoreEventTeams.GetItemsAsync(true).Result;
            foreach (var eventTeam in eventTeams)
            {
                int result = App.Database.SaveEventTeamAsync(eventTeam).Result;
            }
            Fill_Database_Button.Text = saveText;
        }
    }
}