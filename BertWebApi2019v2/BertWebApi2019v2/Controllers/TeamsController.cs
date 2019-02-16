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

        public IEnumerable<Team> GetAllTeams()
        {
            return repository.GetAll();
        }

        public Team GetTeam(int id)
        {
            Team item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public Team GetTeamByTeamNumber(int key)
        {
            return repository.GetByKey(key);
        }

        public HttpResponseMessage PostTeam(Team item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Team>(HttpStatusCode.Created, item);
            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutTeam(int id, Team item)
        {
            item.Id = id;
            if (!repository.Update(item))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteTeam(int id)
        {
            repository.Remove(id);
        }
    }
}
