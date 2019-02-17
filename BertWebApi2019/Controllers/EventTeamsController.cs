using BertScout2019Data.Models;
using BertWebApi2019.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EventTeamStore.Controllers
{
    public class EventTeamsController : ApiController
    {
        static readonly IRepository<EventTeam> repository = new EventTeamRepository();

        public IEnumerable<EventTeam> GetAllEventTeams()
        {
            return repository.GetAll();
        }

        public EventTeam GetEventTeam(int id)
        {
            EventTeam item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public EventTeam GetEventTeamByEventTeamNumber(int key)
        {
            return repository.GetByKey(key);
        }

        public HttpResponseMessage PostEventTeam(EventTeam item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<EventTeam>(HttpStatusCode.Created, item);
            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutEventTeam(int id, EventTeam item)
        {
            item.Id = id;
            if (!repository.Update(item))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteEventTeam(int id)
        {
            repository.Remove(id);
        }
    }
}
