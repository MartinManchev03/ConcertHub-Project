using ConcertHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Data.Repository
{
    public interface IRepository<T, Tid> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        IQueryable<T> GetAllAttached();
        Task<T> GetByIdAsync(Tid id);
        Task<bool> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(Tid id);
    }
}
