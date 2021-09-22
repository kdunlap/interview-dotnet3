using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryStoreAPI.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GroceryStoreAPI.Data.EFCore
{
    public abstract class EfCoreRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        private readonly TContext _context;

        protected EfCoreRepository(TContext context)
        {
            this._context = context;
        }
        
        public async Task<TEntity> GetAsync(long id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return _context.Set<TEntity>().AsQueryable();
        }
        
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        
        public async Task<TEntity> DeleteAsync(long id)
        {
            TEntity entity = await _context.Set<TEntity>().FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
            {
                return null;
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}