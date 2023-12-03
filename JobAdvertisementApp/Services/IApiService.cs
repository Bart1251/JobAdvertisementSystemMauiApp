using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertisementApp.Services
{
    public interface IApiService<T>
    {
        Task<T> GetAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> AddAsync(T item);
        Task<bool> UpdateAsync(string id, T item);
        Task<bool> DeleteAsync(string id);
    }
}
