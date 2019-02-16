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

        public IEnumerable<EventTeamMatch> GetAllEventTeamMatches()
        {
            return repository.GetAll();
        }

        public EventTeamMatch GetEventTeamMatch(int id)
        {
            EventTeamMatch item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public EventTeamMatch GetEventTeamMatchByEventTeamMatchNumber(int key)
        {
            return repository.GetByKey(key);
        }

        public HttpResponseMessage PostEventTeamMatch(EventTeamMatch item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<EventTeamMatch>(HttpStatusCode.Created, item);
            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutEventTeamMatch(int id, EventTeamMatch item)
        {
            item.Id = id;
            if (!repository.Update(item))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteEventTeamMatch(int id)
        {
            repository.Remove(id);
        }
    }
}
