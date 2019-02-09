using BertScout2019Data.Models;
using System.Collections.Generic;

namespace FRCEventStore.Models
{
    interface IFRCEventRepository
    {
        IEnumerable<FRCEvent> GetAll();
        FRCEvent Get(int id);
        FRCEvent Add(FRCEvent item);
        void Remove(int id);
        bool Update(FRCEvent item);
    }
}
