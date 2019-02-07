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
    }
}
