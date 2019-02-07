using BertScout2019.Models;
using Common.JSON;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    public class Bert133Data : System.Web.Services.WebService
    {

        private string _server = "localhost"; // "(localdb)\\mssqllocaldb";
        private string _database = "BertScout2019";
        private int _timeout = 30;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string GetFRCEvents()
        {
            //string connectionString = GetConnectionString(_server, _database);
            string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString; 
            using (SqlConnection dc = new SqlConnection(connectionString))
            {
                dc.Open();
                string query = "SELECT * FROM [dbo].[FRCEvent];";
                SqlCommand cmd = new SqlCommand(query, dc)
                {
                    CommandType = CommandType.Text,
                    CommandTimeout = _timeout,
                };
                JArray result = new JArray();
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult))
                {
                    while (dr.Read())
                    {
                        FRCEvent obj = FRCEvent.FromSqlDataReader(dr);
                        result.Add(obj.ToJson());
                    }
                }
                return result.ToString();
            }
        }

        public static string GetConnectionString(string server, string database)
        {
            SqlConnectionStringBuilder result = new SqlConnectionStringBuilder()
            {
                DataSource = server,
                InitialCatalog = database,
                IntegratedSecurity = true,
                PersistSecurityInfo = false,
                MultipleActiveResultSets = true,
                Encrypt = false,
                ConnectTimeout = 30,
                Pooling = true
            };
            return result.ToString();
        }

    }
}
