using System.Collections.Generic;

namespace BertWebApi2019.Models
{
    interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T GetByKey(object key);
        IEnumerable<T> GetAllByKey(object key);
        T Add(T item);
        void Remove(int id);
        bool Update(T item);
    }
}
