using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BertScout2019Data.Models;
using FRCEventStore.Models;

namespace FRCEventStore.Controllers
{
    public class FRCEventsController : ApiController
    {
        static readonly IFRCEventRepository repository = new FRCEventRepository();

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

        public IEnumerable<FRCEvent> GetFRCEventsByEventKey(string eventKey)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.EventKey, eventKey, StringComparison.OrdinalIgnoreCase));
        }

        public HttpResponseMessage PostFRCEvent(FRCEvent item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<FRCEvent>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutFRCEvent(int id, FRCEvent FRCEvent)
        {
            FRCEvent.Id = id;
            if (!repository.Update(FRCEvent))
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
