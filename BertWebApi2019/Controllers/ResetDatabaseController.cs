using BertScout2019Data.Data;
using System;
using System.IO;
using System.Web.Http;

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
                return $"Reset database complete - {_database.ToString()}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
