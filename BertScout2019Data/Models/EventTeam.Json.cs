using Common.JSON;

namespace BertScout2019Data.Models
{
    public partial class EventTeam
    {
        // ToJson and ToString routines

        public JObject ToJson()
        {
            JObject result = new JObject()
            {
                { "Id", Id },
                { "Uuid", Uuid },
                { "Changed", Changed },
                { "EventKey", EventKey },
                { "TeamNumber", TeamNumber },
            };
            return result;
        }

        public override string ToString()
        {
            return ToJson().ToString();
        }

        public static EventTeam Parse(string value)
        {
            JObject item = JObject.Parse(value);
            EventTeam result = new EventTeam()
            {
                Id = (int)item.GetValue("Id"),
                Uuid = (string)item.GetValue("Uuid"),
                Changed = (int)item.GetValue("Changed"),
                EventKey = (string)item.GetValue("EventKey"),
                TeamNumber = (int)item.GetValue("TeamNumber"),
            };
            return result;
        }
    }
}
