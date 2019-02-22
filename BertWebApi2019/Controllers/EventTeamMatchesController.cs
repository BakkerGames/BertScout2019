﻿using BertScout2019Data.Models;
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

        public EventTeamMatch GetEventTeamMatch(string uuid)
        {
            EventTeamMatch item = repository.GetByUuid(uuid);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public EventTeamMatch GetEventTeamMatchByEventTeamMatchNumber(int key)
        {
            return repository.GetByKey(key.ToString());
        }

        public HttpResponseMessage PostEventTeamMatch(EventTeamMatch item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse(HttpStatusCode.Created, item);
            string uri = Url.Link("DefaultApi", new { uuid = item.Uuid });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutEventTeamMatch(string uuid, EventTeamMatch item)
        {
            item.Uuid = uuid;
            if (!repository.Update(item))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteEventTeamMatch(string uuid)
        {
            repository.RemoveByUuid(uuid);
        }
    }
}
