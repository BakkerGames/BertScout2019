using BertScout2019Data.Data;
using BertScout2019Data.Models;
using Common.JSON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Services;

namespace WebService2019
{
    /// <summary>
    /// Summary description for Bert133Data
    /// </summary>
    [WebService(Namespace = "http://bert133.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
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
                return "Reset database done";
            }
            catch (Exception ex)
            {
                return ex.Message;
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
            //string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            //using (SqlConnection dc = new SqlConnection(connectionString))
            //{
            //    dc.Open();
            //    string query = "SELECT * FROM [dbo].[Team];";
            //    SqlCommand cmd = new SqlCommand(query, dc)
            //    {
            //        CommandType = CommandType.Text,
            //        CommandTimeout = _timeout,
            //    };
            //    JArray result = new JArray();
            //    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult))
            //    {
            //        while (dr.Read())
            //        {
            //            Team obj = Team.FromSqlDataReader(dr);
            //            result.Add(obj.ToJson());
            //        }
            //    }
            //    return result.ToString();
            //}
            return "Not implemented";
        }

        [WebMethod]
        public string GetEventTeams()
        {
            //string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            //using (SqlConnection dc = new SqlConnection(connectionString))
            //{
            //    dc.Open();
            //    string query = "SELECT * FROM [dbo].[EventTeam];";
            //    SqlCommand cmd = new SqlCommand(query, dc)
            //    {
            //        CommandType = CommandType.Text,
            //        CommandTimeout = _timeout,
            //    };
            //    JArray result = new JArray();
            //    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult))
            //    {
            //        while (dr.Read())
            //        {
            //            EventTeam obj = EventTeam.FromSqlDataReader(dr);
            //            result.Add(obj.ToJson());
            //        }
            //    }
            //    return result.ToString();
            //}
            return "Not implemented";
        }

        [WebMethod]
        public string GetEventTeamMatches()
        {
            //string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            //using (SqlConnection dc = new SqlConnection(connectionString))
            //{
            //    dc.Open();
            //    string query = "SELECT * FROM [dbo].[EventTeamMatch];";
            //    SqlCommand cmd = new SqlCommand(query, dc)
            //    {
            //        CommandType = CommandType.Text,
            //        CommandTimeout = _timeout,
            //    };
            //    JArray result = new JArray();
            //    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult))
            //    {
            //        while (dr.Read())
            //        {
            //            EventTeamMatch obj = EventTeamMatch.FromSqlDataReader(dr);
            //            result.Add(obj.ToJson());
            //        }
            //    }
            //    return result.ToString();
            //}
            return "Not implemented";
        }
    }
}
