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
    }
}
