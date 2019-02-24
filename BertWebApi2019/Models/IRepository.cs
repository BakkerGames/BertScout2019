using System.Collections.Generic;

namespace BertWebApi2019.Models
{
    interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T GetByKey(string key);
        T GetByUuid(string uuid);
        IEnumerable<T> GetAllByKey(string key);
        T Add(T item);
        void Remove(int id);
        void RemoveByUuid(string uuid);
        bool Update(T item);
    }
}
