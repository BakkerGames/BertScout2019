using Common.JSON;

namespace BertScout2019Data.Models
{
    public partial class FRCEvent
    {
        public JObject ToJson()
        {
            JObject result = new JObject()
            {
                { "Id", Id },
                { "Uuid", Uuid },
                { "Changed", Changed },
                { "EventKey", EventKey },
                { "Name", Name },
                { "Location", Location },
                { "StartDate", StartDate },
                { "EndDate", EndDate },
            };
            return result;
        }

        public override string ToString()
        {
            return ToJson().ToString();
        }

        public static FRCEvent Parse(string value)
        {
            JObject item = JObject.Parse(value);
            FRCEvent result = new FRCEvent()
            {
                Id = (int)item.GetValue("Id"),
                Uuid = (string)item.GetValue("Uuid"),
                Changed = (int)item.GetValue("Changed"),
                EventKey = (string)item.GetValue("EventKey"),
                Name = (string)item.GetValue("Name"),
                Location = (string)item.GetValue("Location"),
                StartDate = (string)item.GetValue("StartDate"),
                EndDate = (string)item.GetValue("EndDate"),
            };
            return result;
        }
    }
}
