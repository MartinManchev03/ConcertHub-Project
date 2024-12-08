using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Data.Repository
{
    public class Repository<T, Tid> : IRepository<T, Tid> where T : class
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> dbSet;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public IQueryable<T> GetAllAttached()
        {
            return this.dbSet.AsQueryable();
        }
        public async Task<T> GetByIdAsync(Tid id)
        {
            return await dbSet.FindAsync(id);
        }
        public async Task<bool> AddAsync(T entity)
        {
            if(entity != null)
            {
                await this.dbSet.AddAsync(entity);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task UpdateAsync(T entity)
        {
             context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Tid id)      
        {
            var model = await GetByIdAsync(id);
            if(model != null)
            {
                dbSet.Remove(model);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
