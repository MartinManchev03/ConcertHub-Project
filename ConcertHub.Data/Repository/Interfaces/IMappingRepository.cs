using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Data.Repository
{
    public interface IMappingRepository<T, Tid1, Tid2> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        IQueryable<T> GetAllAttached();
        Task<T> GetByIdAsync(Tid1 id1, Tid2 id2);
        Task<bool> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(Tid1 id1, Tid2 id2);
    }
}
