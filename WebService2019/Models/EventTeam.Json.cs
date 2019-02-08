using Common.JSON;

namespace BertScout2019.Models
{
    public partial class EventTeam
    {
        // ToJson and ToString routines

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
