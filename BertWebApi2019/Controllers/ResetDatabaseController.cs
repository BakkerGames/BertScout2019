using BertScout2019Data.Data;
using BertScout2019Data.Models;
using BertScout2019XmlData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using System.Xml.Serialization;

namespace BertWebApi2019.Controllers
{
    public class ResetDatabaseController : ApiController
    {
        private static BertScout2019Database _database;

        public ResetDatabaseController()
        {
            // connect to database
            if (_database == null)
            {
                string dbPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory
                    , "App_Data"
                    , BertScout2019Database.dbFilename);
                _database = new BertScout2019Database(dbPath);
            }
        }

        [AcceptVerbs("GET")]
        [HttpGet]
        public string Reset()
        {
            try
            {
                _database.DropTables();
                _database.CreateTables();

                DateTime startTime = DateTime.UtcNow;

                // fill the database with xml data

                int frcEventCount = 0;
                List<FRCEvent> frcEvents = new List<FRCEvent>();
                Stream frcEventStream = EmbeddedData.XmlDataStoreFRCEvents();
                using (var reader = new StreamReader(frcEventStream))
                {
                    var serializer = new XmlSerializer(typeof(List<FRCEvent>));
                    frcEvents = (List<FRCEvent>)serializer.Deserialize(reader);
                }
                foreach (FRCEvent item in frcEvents)
                {
                    _database.SaveFRCEventAsync(item);
                    frcEventCount++;
                }

                int teamCount = 0;
                List<Team> teams = new List<Team>();
                Stream teamStream = EmbeddedData.XmlDataStoreTeams();
                using (var reader = new StreamReader(teamStream))
                {
                    var serializer = new XmlSerializer(typeof(List<Team>));
                    teams = (List<Team>)serializer.Deserialize(reader);
                }
                foreach (Team item in teams)
                {
                    _database.SaveTeamAsync(item);
                    teamCount++;
                }

                int eventTeamCount = 0;
                List<EventTeam> eventTeams = new List<EventTeam>();
                Stream eventTeamStream = EmbeddedData.XmlDataStoreEventTeams();
                using (var reader = new StreamReader(eventTeamStream))
                {
                    var serializer = new XmlSerializer(typeof(List<EventTeam>));
                    eventTeams = (List<EventTeam>)serializer.Deserialize(reader);
                }
                foreach (EventTeam item in eventTeams)
                {
                    _database.SaveEventTeamAsync(item);
                    eventTeamCount++;
                }

                DateTime endTime = DateTime.UtcNow;

                return "Reset database complete - " +
                    $"\nStartTime: {startTime} - EndTime: {endTime}" +
                    $"\nFRCEVents: {frcEventCount} - Teams: {teamCount} - EventTeams: {eventTeamCount}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
