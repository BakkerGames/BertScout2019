using BertScout2019Data.Models;
using BertWebApi2019.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FRCEventStore.Controllers
{
    public class FRCEventsController : ApiController
    {
        static readonly IRepository<FRCEvent> repository = new FRCEventRepository();

        public IEnumerable<FRCEvent> GetAllFRCEvents()
        {
            return repository.GetAll();
        }

        public FRCEvent GetFRCEvent(string uuid)
        {
            FRCEvent item = repository.GetByUuid(uuid);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public FRCEvent GetFRCEventByEventKey(string key)
        {
            return repository.GetByKey(key);
        }

        public HttpResponseMessage PostFRCEvent(FRCEvent item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse(HttpStatusCode.Created, item);
            string uri = Url.Link("DefaultApi", new { uuid = item.Uuid });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutFRCEvent(string uuid, FRCEvent item)
        {
            item.Uuid = uuid;
            if (!repository.Update(item))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteFRCEvent(string uuid)
        {
            repository.RemoveByUuid(uuid);
        }
    }
}
