using BertScout2019Data.Models;
using BertWebApi2019v2.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BertWebApi2019v2.Controllers
{
    public class FRCEventsController : ApiController
    {
        static readonly IRepository<FRCEvent> repository = new FRCEventRepository();

        public IEnumerable<FRCEvent> GetAllFRCEvents()
        {
            return repository.GetAll();
        }

        public FRCEvent GetFRCEvent(int id)
        {
            FRCEvent item = repository.Get(id);
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
            var response = Request.CreateResponse<FRCEvent>(HttpStatusCode.Created, item);
            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutFRCEvent(int id, FRCEvent item)
        {
            item.Id = id;
            if (!repository.Update(item))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteFRCEvent(int id)
        {
            repository.Remove(id);
        }
    }
}
