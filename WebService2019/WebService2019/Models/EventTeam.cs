using Common.JSON;

// remember to increment the dbVersion in App.xaml.cs when changing this model

namespace BertScout2019.Models
{
    public class EventTeam
    {
        public int? Id { get; set; }
        public string EventKey { get; set; }
        public int TeamNumber { get; set; }

        public JObject ToJson()
        {
            JObject result = new JObject()
            {
                { "Id", Id },
                { "EventKey", EventKey },
                { "TeamNumber", TeamNumber },
            };
            return result;
        }

        public override string ToString()
        {
            return ToJson().ToString();
        }
    }
}
