using Common.JSON;

namespace BertScout2019Data.Models
{
    public partial class Team
    {
        // ToJson and ToString routines

        public JObject ToJson()
        {
            JObject result = new JObject()
            {
                { "Id", Id },
                { "Uuid", Uuid },
                { "Changed", Changed },
                { "TeamNumber", TeamNumber },
                { "Name", Name },
                { "Location", Location },
            };
            return result;
        }

        public override string ToString()
        {
            return ToJson().ToString();
        }

        public static Team Parse(string value)
        {
            JObject item = JObject.Parse(value);
            Team result = new Team()
            {
                Id = (int)item.GetValue("Id"),
                Uuid = (string)item.GetValue("Uuid"),
                Changed = (int)item.GetValue("Changed"),
                TeamNumber = (int)item.GetValue("TeamNumber"),
                Name = (string)item.GetValue("Name"),
                Location = (string)item.GetValue("Location"),
            };
            return result;
        }
    }
}
