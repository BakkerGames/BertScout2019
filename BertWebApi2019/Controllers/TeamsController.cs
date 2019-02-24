using BertScout2019Data.Models;
using BertWebApi2019.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TeamStore.Controllers
{
    public class TeamsController : ApiController
    {
        static readonly IRepository<Team> repository = new TeamRepository();

        [AcceptVerbs("GET")]
        [HttpGet]
        public IEnumerable<Team> GetAllTeams()
        {
            return repository.GetAll();
        }

        [AcceptVerbs("GET")]
        [HttpGet]
        public Team GetTeam(string uuid)
        {
            Team item = repository.GetByUuid(uuid);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public Team GetTeamByTeamNumber(int key)
        {
            return repository.GetByKey(key.ToString());
        }

        [AcceptVerbs("POST")]
        [HttpPost]
        public HttpResponseMessage PostTeam(Team item)
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
        public void PutTeam(string uuid, Team item)
        {
            item.Uuid = uuid;
            if (!repository.Update(item))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [AcceptVerbs("DELETE")]
        [HttpDelete]
        public void DeleteTeam(string uuid)
        {
            repository.RemoveByUuid(uuid);
        }
    }
}
