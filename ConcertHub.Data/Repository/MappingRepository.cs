using ConcertHub.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Data.Repository
{
    public class MappingRepository<T, Tid1, Tid2> : IMappingRepository<T, Tid1, Tid2> where T : class
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> dbSet;

        public MappingRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public async Task<bool> AddAsync(T entity)
        {
            if (entity != null)
            {
                await this.dbSet.AddAsync(entity);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteAsync(Tid1 id1, Tid2 id2)
        {
            var model = await GetByIdAsync(id1, id2);
            if (model != null)
            {
                dbSet.Remove(model);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            if(entity != null)
            {
                dbSet.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public IQueryable<T> GetAllAttached()
        {
            return this.dbSet.AsQueryable();
        }

        public async Task<T> GetByIdAsync(Tid1 id1, Tid2 id2)
        {
            return await dbSet.FindAsync(id1, id2);
        }

        public async Task UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

    }
}
