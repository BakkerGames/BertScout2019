using System.Collections.Generic;
using System.Threading.Tasks;

namespace BertScout2019.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<bool> DeleteItemAsync(string uuid);
        Task<T> GetItemAsync(int id);
        Task<T> GetItemAsync(string uuid);
        Task<T> GetItemByKeyAsync(string key);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<bool> UpdateItemAsync(T item);
    }
}
