using BertScout2019Data.Data;
using BertScout2019Data.Models;
using BertScout2019XmlData;
using Common.JSON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Services;
using System.Xml.Serialization;

namespace WebService2019
{
    /// <summary>
    /// Summary description for Bert133Data
    /// </summary>
    [WebService(Namespace = "http://bert133.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class Bert133Data : WebService
    {

        // app database
        private const string dbFilename = "bertscout2019.db3";
        public static BertScout2019Database _database;

        public static BertScout2019Database Database
        {
            get
            {
                if (_database == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbFilename);
                    _database = new BertScout2019Database(dbPath);
                }
                return _database;
            }
        }

        [WebMethod]
        public string ResetDatabase()
        {
            try
            {
                Database.DropTables();
                Database.CreateTables();
                FillStartingData_FRCEvents();
                FillStartingData_Teams();
                FillStartingData_EventTeams();
                return "Reset database done";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void FillStartingData_FRCEvents()
        {
            List<FRCEvent> items = new List<FRCEvent>();
            Stream stream = EmbeddedData.XmlDataStoreFRCEvents();
            using (var reader = new StreamReader(stream))
            {
                var serializer = new XmlSerializer(typeof(List<FRCEvent>));
                items = (List<FRCEvent>)serializer.Deserialize(reader);
            }
            foreach (FRCEvent item in items)
            {
                item.Id = null;
                item.Uuid = item.EventKey;
                int result = Database.SaveFRCEventAsync(item).Result;
            }
        }

        private void FillStartingData_Teams()
        {
            List<Team> items = new List<Team>();
            Stream stream = EmbeddedData.XmlDataStoreTeams();
            using (var reader = new StreamReader(stream))
            {
                var serializer = new XmlSerializer(typeof(List<Team>));
                items = (List<Team>)serializer.Deserialize(reader);
            }
            foreach (Team item in items)
            {
                item.Id = null;
                item.Uuid = item.TeamNumber.ToString();
                int result = Database.SaveTeamAsync(item).Result;
            }
        }

        private void FillStartingData_EventTeams()
        {
            List<EventTeam> items = new List<EventTeam>();
            Stream stream = EmbeddedData.XmlDataStoreEventTeams();
            using (var reader = new StreamReader(stream))
            {
                var serializer = new XmlSerializer(typeof(List<EventTeam>));
                items = (List<EventTeam>)serializer.Deserialize(reader);
            }
            foreach (EventTeam item in items)
            {
                item.Id = null;
                item.Uuid = $"{item.EventKey}:{item.TeamNumber}";
                int result = Database.SaveEventTeamAsync(item).Result;
            }
        }

        [WebMethod]
        public string GetFRCEvents()
        {
            List<FRCEvent> events = Database.GetEventsAsync().Result;
            JArray result = new JArray();
            foreach (FRCEvent item in events)
            {
                result.Add(item.ToJson());
            }
            return result.ToString();
        }

        [WebMethod]
        public string GetTeams()
        {
            List<Team> events = Database.GetTeamsAsync().Result;
            JArray result = new JArray();
            foreach (Team item in events)
            {
                result.Add(item.ToJson());
            }
            return result.ToString();
        }

        [WebMethod]
        public string GetEventTeams()
        {
            List<EventTeam> events = Database.GetEventTeamsAsync().Result;
            JArray result = new JArray();
            foreach (EventTeam item in events)
            {
                result.Add(item.ToJson());
            }
            return result.ToString();
        }

        [WebMethod]
        public string GetEventTeamMatches()
        {
            List<EventTeamMatch> events = Database.GetEventTeamMatchesAsync().Result;
            JArray result = new JArray();
            foreach (EventTeamMatch item in events)
            {
                result.Add(item.ToJson());
            }
            return result.ToString();
        }
    }
}
