using BertScout2019Data.Models;
using BertWebApi2019.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EventTeamMatchestore.Controllers
{
    public class EventTeamMatchesController : ApiController
    {
        static readonly IRepository<EventTeamMatch> repository = new EventTeamMatchRepository();

        [AcceptVerbs("GET")]
        [HttpGet]
        public IEnumerable<EventTeamMatch> GetAllEventTeamMatches()
        {
            return repository.GetAll();
        }

        [AcceptVerbs("GET")]
        [HttpGet]
        public EventTeamMatch GetEventTeamMatch(string uuid)
        {
            EventTeamMatch item = repository.GetByUuid(uuid);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        [AcceptVerbs("GET")]
        [HttpGet]
        public EventTeamMatch GetEventTeamMatchByEventTeamMatchNumber(int key)
        {
            return repository.GetByKey(key.ToString());
        }

        [AcceptVerbs("GET")]
        [HttpGet]
        public IEnumerable<EventTeamMatch> GetNextBatchEventTeamMatches(string batchInfo)
        {
            return repository.GetNextBatchByKey(batchInfo);
        }

        [AcceptVerbs("POST")]
        [HttpPost]
        public HttpResponseMessage PostEventTeamMatch(EventTeamMatch item)
        {
            item.Id = null; // clear for autoincrement
            item = repository.Add(item);
            var response = Request.CreateResponse(HttpStatusCode.Created, item);
            string uri = Url.Link("DefaultApi", new { uuid = item.Uuid });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [AcceptVerbs("PUT")]
        [HttpPut]
        public void PutEventTeamMatch(string uuid, EventTeamMatch item)
        {
            EventTeamMatch oldItem = repository.GetByUuid(uuid);
            // only update if new .Changed is greater
            if (oldItem != null && oldItem.Changed < item.Changed)
            {
                item.Uuid = uuid;
                if (!repository.Update(item))
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }
        }

        [AcceptVerbs("DELETE")]
        [HttpDelete]
        public void DeleteEventTeamMatch(string uuid)
        {
            repository.RemoveByUuid(uuid);
        }
    }
}
